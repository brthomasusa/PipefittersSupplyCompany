INSERT INTO Sales.Customers
    (CustomerID, CustomerName, AddressLine1, AddressLine2, City, [State], ZipCode, Telephone, CreditLimit, PrimaryContact)
VALUES
    (10001, 'Dunn Plumbing', '2763 Cosgrove Road', '2nd Floor', 'West Haven', 'CT', '06516-1960', '203-913-2063', 6000, 'Bill Dunn'),
    (10003, 'Ace Construction', '3755 Spring Valley Rd', 'Ste 111', 'Dallas', 'TX', '75518', '972-328-1111', 9000, 'Roger A. McPherson'),
    (10005, 'Bryant Boiler Repair', '10740 N. Central Exprwy', 'Ste 11', 'Richardson', 'TX', '75665', '214-913-2063', 15000, 'William Bryant'),
    (10007, 'Bucknell Air Conditioner', '33 Airport Blvd', 'Ste 322', 'Irving', 'TX', '75123', '972-913-2063', 6000, 'Michelle Willetson'),
    (10010, 'Cole & Co.', '2700 Centerville Road', 'Dock 13', 'Garland', 'TX', '76987', '972-913-2063', 8000, 'Cassandra Didonato')
GO

INSERT INTO Sales.CompositionTypes
    (CompositionTypeName, [Description])
VALUES
    ('B', 'Brass'),
    ('C', 'Copper')
GO

INSERT INTO Sales.InventoryTypes
    (InventoryTypeName, [Description])
VALUES
    ('4', '4-foot pipe'),
    ('8', '8-foot pipe'),
    ('C', 'Cap fitting'),
    ('L', 'Elbow'),
    ('T', 'T-connector')
GO

INSERT INTO Sales.DiameterTypes
    (DiameterTypeName, [Description])
VALUES
    ('025', '0.25'),
    ('050', '0.50'),
    ('100', '1.00'),
    ('200', '2.00'),
    ('300', '3.00'),
    ('400', '4.00')
GO

INSERT INTO Sales.Inventory
    (InventoryID, CompositionTypeID, InventoryTypeID, DiameterTypeID, ListPrice, [Description])
VALUES
    (1001, 1, 1, 1, 21.95, '0.25-inch brass 4-foot pipe'),
    (1002, 1, 1, 2, 26.49, '0.50-inch brass 4-foot pipe'),
    (1003, 1, 1, 3, 34.79, '1.00-inch brass 4-foot pipe'),
    (1004, 1, 1, 4, 43.69, '2.00-inch brass 4-foot pipe'),
    (1005, 1, 1, 5, 55.29, '3.00-inch brass 4-foot pipe'),
    (1006, 1, 1, 6, 65.19, '4.00-inch brass 4-foot pipe'),
    (1030, 1, 5, 6, 28.49, '4.00-inch brass T-Connector'),
    (1035, 2, 1, 5, 18.25, '3.00-inch copper 4-foot pipe'),
    (1038, 2, 2, 2, 14.95, '0.50-inch copper 8-foot pipe'),
    (1041, 2, 2, 5, 38.59, '3.00-inch copper 8-foot pipe'),
    (1044, 2, 3, 2, 1.95, '0.50-inch copper Cap fitting'),
    (1047, 2, 3, 5, 4.19, '3.00-inch copper Cap fitting'),
    (1053, 2, 4, 5, 5.29, '3.00-inch copper Elbow'),
    (1059, 2, 5, 5, 6.59, '3.00-inch T-Connector')    
GO

INSERT INTO Sales.EmployeeTypes
    (EmployeeTypeName)
VALUES
    ('Salesperson')
GO
INSERT INTO Sales.EmployeeTypes
    (EmployeeTypeName)
VALUES
    ('Accountant')
GO

INSERT INTO Sales.Employees
(EmployeeID, EmployeeTypeID, LastName, FirstName, MiddleInitial, SSN, AddressLine1, City, [State], ZipCode, Telephone, MaritalStatus, Exemptions, PayRate, StartDate)
VALUES
(107, 1, 'Sanchez', 'Ken', 'J', '123789999', '321 Tarrant Pl', 'Fort Worth', 'TX', '78965', '817-987-1234', 'M', 5, 59.55, '1998-12-02'),
(109, 1, 'Duffy', 'Terri', 'L', '999912345', '98 Reiger Ave', 'Dallas', 'TX', '75214', '214-987-1234', 'M', 2, 15.00, '2018-10-22'),
(111, 1, 'Goldberg', 'Jozef', 'P', '036889999', '6667 Melody Lane, Apt 2', 'Dallas', 'TX', '75231', '469-321-1234', 'S', 1, 25.00, '2013-02-28'),
(112, 1, 'Brown', 'Jamie', 'J', '123700009', '98777 Nigeria Town Rd', 'Arlington', 'TX', '78658', '817-555-5555', 'M', 2, 25.00, '2017-12-22'),
(115, 1, 'Trump', 'Donald', 'J', '781287999', '1 Cowgirl Circle', 'Fort Worth', 'TX', '78965', '817-123-9874', 'M', 4, 35.00, '2003-07-02')
GO

INSERT INTO Sales.SalesOrders
    (SalesOrderID, CustomerID, EmployeeID, SalesOrderDate, CustomerPO, SalesOrderAmount)
VALUES
    (100001, 10001, 112, '2021-01-14', '101-PR-753979', 1941.51),
    (100002, 10003, 107, '2021-01-16', '26754', 4419.30),
    (100003, 10007, 109, '2021-01-16', 'BP-8666789', 781.00),
    (100004, 10005, 111, '2021-01-17', '276-555438', 825.76),
    (100005, 10010, 115, '2021-01-17', '985553', 2322.50)
GO

INSERT INTO Sales.SalesOrderDetails
    (SalesOrderDetailID, SalesOrderID, InventoryID, QuantityOrdered, UnitPrice)
VALUES
    ()
GO