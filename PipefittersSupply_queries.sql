-- CREATE OR ALTER FUNCTION Sales.udf_GetAllCustomers()
-- RETURNS TABLE
-- AS
-- RETURN
--     SELECT 
--         cust.CustomerID, cust.CustomerName, cust.AddressLine1, cust.AddressLine2,
--         cust.City + ', ' + cust.[State] + ' ' + cust.ZipCode AS CityStateZip,
--         cust.Telephone, cust.CreditLimit, cust.PrimaryContact
--     FROM Sales.Customers cust;
-- GO

-- CREATE OR ALTER FUNCTION Sales.udf_GetCustomerByID(
--     @customerID INT
-- )
-- RETURNS TABLE
-- AS
-- RETURN
--     SELECT 
--         cust.CustomerID, cust.CustomerName, cust.AddressLine1, cust.AddressLine2,
--         cust.City + ', ' + cust.[State] + ' ' + cust.ZipCode AS CityStateZip,
--         cust.Telephone, cust.CreditLimit, cust.PrimaryContact
--     FROM Sales.Customers cust
--     WHERE cust.CustomerID = @customerID;
-- GO

-- CREATE OR ALTER FUNCTION Sales.udf_GetSalesOrderDetailsBySalesOrderID(
--     @salesOrderID INT
-- )
-- RETURNS TABLE
-- AS
-- RETURN
--     SELECT 
--         sod.InventoryID, 
--         inv.[Description], 
--         sod.QuantityOrdered, 
--         sod.UnitPrice AS Price,
--         inv.ListPrice AS 'List Price',
--         sod.QuantityOrdered * sod.UnitPrice AS Extension,
--         sod.SalesOrderID
--     FROM Sales.SalesOrderDetails sod
--         INNER JOIN Sales.Inventory inv ON sod.InventoryID = inv.InventoryID
--     WHERE sod.SalesOrderID = @salesOrderID
-- GO

CREATE OR ALTER FUNCTION HumanResources.udf_GetAllEmployeeFullNames()
RETURNS TABLE
AS
RETURN
    SELECT 
        emp.EmployeeID, emp.FirstName + ' ' + ISNULL(emp.MiddleInitial + ' ', '')  + emp.LastName AS 'Employee Name'
    FROM HumanResources.Employees emp;
GO

CREATE OR ALTER FUNCTION HumanResources.udf_CalculateTaxablePayAmount(
    @periodStartDate DATE,
    @periodEndDate DATE
)
RETURNS TABLE
AS
RETURN
    SELECT 
        tcard.EmployeeID, 
        tcard.TimeCardID, 
        FORMAT(tcard.PayPeriodEnded, 'MMM dd yyyy') AS MonthEnded,
        tcard.RegularHours,
        tcard.OverTimeHours,
        tcard.RegularHours * emp.PayRate AS RegularPay,
        tcard.OverTimeHours * (emp.PayRate * 1.5) AS OverTimePay,
        (tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5)) AS GrossPay,
        lkup.ExemptionAmount,
        IIF(((tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5))) < lkup.ExemptionAmount, 
            0, ((tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5))) - lkup.ExemptionAmount) AS TaxableAmount
    FROM HumanResources.TimeCard tcard
        INNER JOIN HumanResources.Employees emp ON tcard.EmployeeID = emp.EmployeeID
        INNER JOIN HumanResources.ExemptionLkup lkup ON emp.Exemptions = lkup.ExemptionLkupID
    WHERE tcard.PayPeriodEnded BETWEEN @periodStartDate AND  @periodEndDate
GO

CREATE OR ALTER FUNCTION HumanResources.udf_CalculateFederalWithHoldingTax(
    @periodStartDate DATE,
    @periodEndDate DATE
)
RETURNS TABLE
AS
RETURN
	SELECT
		iQ.EmployeeID,
		iQ.TimeCardID,
		iQ.MonthEnded,
		emp.MaritalStatus,	
		iQ.GrossPay,
		iQ.ExemptionAmount,
		iQ.TaxableAmount,
		(
			SELECT ROUND(((iQ.TaxableAmount - fwh.LowerLimit) * fwh.TaxRate) + fwh.BracketBaseAmount, 2)
			FROM HumanResources.FedWithHolding fwh 
			WHERE fwh.MaritalStatus = emp.MaritalStatus AND fwh.LowerLimit <= iQ.TaxableAmount AND fwh.UpperLimit >= iQ.TaxableAmount
		) 
		AS FederalWitholdingTax
	FROM 
	(
		SELECT 
			tcard.EmployeeID, 
			tcard.TimeCardID,
			FORMAT(tcard.PayPeriodEnded, 'MMM dd yyyy') AS MonthEnded,
			(tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5)) AS GrossPay,
			lkup.ExemptionAmount,
			IIF(((tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5))) - lkup.ExemptionAmount >= 0, 
				((tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5))) - lkup.ExemptionAmount, 0) AS TaxableAmount
		FROM HumanResources.TimeCard tcard
			INNER JOIN HumanResources.Employees emp ON tcard.EmployeeID = emp.EmployeeID
			INNER JOIN HumanResources.ExemptionLkup lkup ON emp.Exemptions = lkup.ExemptionLkupID
		WHERE tcard.PayPeriodEnded >= @periodStartDate AND  PayPeriodEnded <= @periodEndDate
	) AS iQ
		INNER JOIN HumanResources.Employees emp ON iQ.EmployeeID = emp.EmployeeID;
GO

CREATE OR ALTER FUNCTION HumanResources.udf_CalculateNetPay(
    @periodStartDate DATE,
    @periodEndDate DATE
)
RETURNS TABLE
AS
RETURN
	SELECT 
		getFWT.EmployeeID,
		getFWT.TimeCardID,
		getFWT.MonthEnded,
		getFWT.MaritalStatus,	
		CAST(getFWT.GrossPay AS decimal(18,2)) AS GrossPay,
		getFWT.ExemptionAmount,
		CAST(getFWT.TaxableAmount AS decimal(18,2)) AS TaxableAmount,
		CAST(getFWT.FederalWitholdingTax AS decimal(18,2)) AS FederalWitholdingTax,
		CAST(ROUND(getFWT.GrossPay * 0.062, 2) AS decimal(18,2)) AS FICA,
		CAST(ROUND(getFWT.GrossPay * 0.0145, 2) AS decimal(18,2)) AS Medicare,
		CAST(
			ROUND(IIF(getFWT.TaxableAmount = 0, 
					  getFWT.GrossPay - ((getFWT.GrossPay * 0.062) + (getFWT.GrossPay * 0.0145)), 
					  getFWT.TaxableAmount - (getFWT.FederalWitholdingTax + (getFWT.GrossPay * 0.062) + (getFWT.GrossPay * 0.0145))), 2)
			AS decimal(18,2)
		) AS NetPay
	FROM 
	(
		SELECT
			iQ.EmployeeID,
			iQ.TimeCardID,
			iQ.MonthEnded,
			iQ.MaritalStatus,	
			iQ.GrossPay,
			iQ.ExemptionAmount,
			iQ.TaxableAmount,
			(
				SELECT ROUND(((iQ.TaxableAmount - fwh.LowerLimit) * fwh.TaxRate) + fwh.BracketBaseAmount, 2)
				FROM HumanResources.FedWithHolding fwh 
				WHERE fwh.MaritalStatus = iQ.MaritalStatus AND fwh.LowerLimit <= iQ.TaxableAmount AND fwh.UpperLimit >= iQ.TaxableAmount
			) 
			AS FederalWitholdingTax
		FROM 
		(
			SELECT 
				tcard.EmployeeID, 
				tcard.TimeCardID,
				emp.MaritalStatus,
				FORMAT(tcard.PayPeriodEnded, 'MMM dd yyyy') AS MonthEnded,
				(tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5)) AS GrossPay,
				lkup.ExemptionAmount,
				IIF(((tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5))) - lkup.ExemptionAmount >= 0, 
					((tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5))) - lkup.ExemptionAmount, 0) AS TaxableAmount
			FROM HumanResources.TimeCard tcard
				INNER JOIN HumanResources.Employees emp ON tcard.EmployeeID = emp.EmployeeID
				INNER JOIN HumanResources.ExemptionLkup lkup ON emp.Exemptions = lkup.ExemptionLkupID
			WHERE tcard.PayPeriodEnded >= @periodStartDate AND  PayPeriodEnded <= @periodEndDate
		) AS iQ	
	) AS getFWT
GO

CREATE OR ALTER FUNCTION HumanResources.udf_CalculateWagesAndTaxesPayable(
    @periodEndDate DATE
)
RETURNS TABLE
AS
RETURN
	SELECT 
		getFWT.MonthEnded,
		CAST(SUM(getFWT.FederalWitholdingTax) AS decimal(18,2)) AS FedTaxPayable,
		CAST(ROUND(SUM(getFWT.GrossPay * 0.062), 2) AS decimal(18,2)) AS FicaPayable,
		CAST(ROUND(SUM(getFWT.GrossPay * 0.0145), 2) AS decimal(18,2)) AS MedicarePayable,
		CAST(
			ROUND(SUM(IIF(getFWT.TaxableAmount = 0, 
					  getFWT.GrossPay - ((getFWT.GrossPay * 0.062) + (getFWT.GrossPay * 0.0145)), 
					  getFWT.TaxableAmount - (getFWT.FederalWitholdingTax + (getFWT.GrossPay * 0.062) + (getFWT.GrossPay * 0.0145)))), 2)
			AS decimal(18,2)
		) AS WagesPayable
	FROM 
	(
		SELECT
			iQ.MonthEnded,	
			iQ.GrossPay,
			iQ.TaxableAmount,
			(
				SELECT ROUND(((iQ.TaxableAmount - fwh.LowerLimit) * fwh.TaxRate) + fwh.BracketBaseAmount, 2)
				FROM HumanResources.FedWithHolding fwh 
				WHERE fwh.MaritalStatus = iQ.MaritalStatus AND fwh.LowerLimit <= iQ.TaxableAmount AND fwh.UpperLimit >= iQ.TaxableAmount
			) 
			AS FederalWitholdingTax
		FROM 
		(
			SELECT 
				tcard.EmployeeID, 
				tcard.TimeCardID,
				emp.MaritalStatus,
				FORMAT(tcard.PayPeriodEnded, 'MMM dd yyyy') AS MonthEnded,
				(tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5)) AS GrossPay,
				lkup.ExemptionAmount,
				IIF(((tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5))) - lkup.ExemptionAmount >= 0, 
					((tcard.RegularHours * emp.PayRate) + (tcard.OverTimeHours * (emp.PayRate * 1.5))) - lkup.ExemptionAmount, 0) AS TaxableAmount
			FROM HumanResources.TimeCard tcard
				INNER JOIN HumanResources.Employees emp ON tcard.EmployeeID = emp.EmployeeID
				INNER JOIN HumanResources.ExemptionLkup lkup ON emp.Exemptions = lkup.ExemptionLkupID
			WHERE tcard.PayPeriodEnded <= @periodEndDate
		) AS iQ	
	) AS getFWT
	GROUP BY getFWT.MonthEnded
GO




