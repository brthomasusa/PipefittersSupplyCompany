CREATE OR ALTER FUNCTION Sales.udf_GetAllCustomers()
RETURNS TABLE
AS
RETURN
    SELECT 
        cust.CustomerID, cust.CustomerName, cust.AddressLine1, cust.AddressLine2,
        cust.City + ', ' + cust.[State] + ' ' + cust.ZipCode AS Address,
        cust.Telephone, cust.CreditLimit, cust.PrimaryContact
    FROM Sales.Customers cust;