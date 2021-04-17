CREATE OR ALTER FUNCTION Sales.udf_GetAllCustomers()
RETURNS TABLE
AS
RETURN
    SELECT 
        cust.CustomerID, cust.CustomerName, cust.AddressLine1, cust.AddressLine2,
        cust.City + ', ' + cust.[State] + ' ' + cust.ZipCode AS CityStateZip,
        cust.Telephone, cust.CreditLimit, cust.PrimaryContact
    FROM Sales.Customers cust;
GO

CREATE OR ALTER FUNCTION Sales.udf_GetCustomerByID(
    @customerID INT
)
RETURNS TABLE
AS
RETURN
    SELECT 
        cust.CustomerID, cust.CustomerName, cust.AddressLine1, cust.AddressLine2,
        cust.City + ', ' + cust.[State] + ' ' + cust.ZipCode AS CityStateZip,
        cust.Telephone, cust.CreditLimit, cust.PrimaryContact
    FROM Sales.Customers cust
    WHERE cust.CustomerID = @customerID;
GO

CREATE OR ALTER FUNCTION Sales.udf_GetSalesOrderDetailsBySalesOrderID(
    @salesOrderID INT
)
RETURNS TABLE
AS
RETURN
    SELECT 
        sod.InventoryID, 
        inv.[Description], 
        sod.QuantityOrdered, 
        sod.UnitPrice AS Price,
        inv.ListPrice AS 'List Price',
        sod.QuantityOrdered * sod.UnitPrice AS Extension,
        sod.SalesOrderID
    FROM Sales.SalesOrderDetails sod
        INNER JOIN Sales.Inventory inv ON sod.InventoryID = inv.InventoryID
    WHERE sod.SalesOrderID = @salesOrderID
GO
