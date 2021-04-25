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

