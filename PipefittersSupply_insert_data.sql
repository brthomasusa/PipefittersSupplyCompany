INSERT INTO HumanResources.EmployeeTypes
    (EmployeeTypeName)
VALUES
    ('Accountant'),
    ('Administrator'),
    ('Maintenance'),
    ('Materials Handler'),
    ('Purchasing Agent'),
    ('Salesperson')
GO

INSERT INTO HumanResources.Employees
    (EmployeeID, EmployeeTypeID, LastName, FirstName, MiddleInitial, SSN, AddressLine1, City, [State], ZipCode, Telephone, MaritalStatus, Exemptions, PayRate, StartDate)
VALUES
    (101, 2, 'Sanchez', 'Ken', 'J', '123789999', '321 Tarrant Pl', 'Fort Worth', 'TX', '78965', '817-987-1234', 'M', 5, 40.00, '1998-12-02'),
    (108, 2, 'Phide', 'Terri', 'M', '638912345', '3455 South Corinth Circle', 'Dallas', 'TX', '75224', '214-987-1234', 'M', 1, 28.00, '2014-09-22'),
    (109, 2, 'Duffy', 'Terri', 'L', '999912345', '98 Reiger Ave', 'Dallas', 'TX', '75214', '214-987-1234', 'M', 2, 30.00, '2018-10-22'),
    (111, 2, 'Goldberg', 'Jozef', 'P', '036889999', '6667 Melody Lane, Apt 2', 'Dallas', 'TX', '75231', '469-321-1234', 'S', 1, 29.00, '2013-02-28'),
    (112, 2, 'Brown', 'Jamie', 'J', '123700009', '98777 Nigeria Town Rd', 'Arlington', 'TX', '78658', '817-555-5555', 'M', 2, 29.00, '2017-12-22'),
    (113, 2, 'Bush', 'George', 'W', '325559874', '777 Ervay Street', 'Dallas', 'TX', '75208', '214-555-5555', 'M', 5, 30.00, '2016-10-19'),
    (114, 1, 'Bushnell', 'Loretta', 'J', '370005409', '123 Main St', 'Flower Mound', 'TX', '78630', '817-555-5555', 'M', 2, 25.00, '2017-02-22'),
    (115, 1, 'Jacknoff', 'Jorge', 'C', '325509345', '777 Ervay Street', 'Dallas', 'TX', '75208', '214-987-5555', 'M', 3, 25.00, '2014-06-19'),
    (116, 3, 'Trump', 'Donald', 'J', '781287999', '1 Cowgirl Circle', 'Fort Worth', 'TX', '78965', '817-123-9874', 'M', 4, 8.00, '2003-07-02'),
    (117, 3, 'Trump', 'Ivanka', 'Z', '256281432', '1 Cowgirl Circle', 'Fort Worth', 'TX', '78965', '817-123-9874', 'M', 2, 10.50, '2003-07-02'),
    (118, 4, 'Thompson', 'Douglas', 'J', '371005409', '4338 Main Place', 'Rice', 'TX', '78430', '972-555-5555', 'S', 2, 17.00, '2017-02-22'),
    (119, 4, 'Hernandez', 'Jesus', 'G', '323509345', '1254 34th St', 'Irving', 'TX', '75268', '972-987-5555', 'M', 3, 15.00, '2019-06-19'),
    (120, 5, 'Doe', 'Jonny', 'A', '567882345', '32584 Collett Ln', 'Saginaw', 'TX', '78965', '817-123-9874', 'M', 4, 22.00, '2015-07-22'),
    (121, 5, 'Smith', 'Samuel', 'P', '256200432', '34 Wensworth Drive', 'Roanocke', 'TX', '78965', '817-123-9874', 'M', 2, 22.50, '2018-03-14'),
    (122, 6, 'Gomez', 'Roberto', 'J', '687005409', '4338 Main Place', 'Desoto', 'TX', '74430', '972-555-5555', 'S', 2, 17.00, '2017-02-22'),
    (123, 6, 'Hernandez', 'George', 'G', '478509345', '65 Adelia Lane', 'Ricardson', 'TX', '75268', '972-987-5555', 'M', 3, 15.00, '2019-06-19')
GO

INSERT INTO HumanResources.TimeCard
    (TimeCardID, EmployeeID, SupervisorID, PayPeriodEnded, RegularHours, OverTimeHours)
VALUES
    (1, 101, 101, '2021-01-31', 168, 0),
    (2, 108, 101, '2021-01-31', 168, 4),
    (3, 109, 101, '2021-01-31', 168, 2),
    (4, 111, 101, '2021-01-31', 168, 0),
    (5, 112, 101, '2021-01-31', 168, 0),
    (6, 113, 101, '2021-01-31', 168, 0),
    (7, 114, 108, '2021-01-31', 168, 0),
    (8, 115, 108, '2021-01-31', 168, 0),
    (9, 116, 109, '2021-01-31', 150, 0),
    (10, 117, 109, '2021-01-31', 168, 0),
    (11, 118, 111, '2021-01-31', 168, 6),
    (12, 119, 111, '2021-01-31', 168, 6),
    (13, 120, 112, '2021-01-31', 168, 0),
    (14, 121, 112, '2021-01-31', 168, 0),
    (15, 122, 113, '2021-01-31', 168, 0),
    (16, 123, 113, '2021-01-31', 168, 0)
GO

INSERT INTO HumanResources.ExemptionLkup
    (ExemptionLkupID, NumberOfExemptions, ExemptionAmount)
VALUES
    (1, 1, 304.17),
    (2, 2, 608.34),
    (3, 3, 912.42),
    (4, 4, 1216.68),
    (5, 5, 1520.85),
    (6, 6, 1825.02),
    (7, 7, 2129.19),
    (8, 8, 2433.36),
    (9, 9, 2737.53),
    (10, 10, 3041.70),
    (11, 11, 3345.87)
GO

INSERT INTO HumanResources.FedWithHolding
    (FedWithHoldingID, MaritalStatus, FedTaxBracket, LowerLimit, UpperLimit, TaxRate, BracketBaseAmount)
VALUES
    (1, 'M', '1', 0.00, 1313.00, 0.00, 0.00),
    (2, 'M', '2', 1313.01, 2038.00, 10.00, 0.00),
    (3, 'M', '3', 2038.01, 6304.00, 15.00, 72.50),
    (4, 'M', '4', 6304.01, 9844.00, 25.00, 712.40),
    (5, 'M', '5', 9844.01, 18050.00, 28.00, 1597.40),
    (6, 'M', '6', 18050.01, 31725.00, 33.00, 3895.08),
    (7, 'M', '7', 31725.01, 1000000.00, 35.00, 8407.83),
    (8, 'S', '1', 0.00, 598.00, 0.00, 0.00),
    (9, 'S', '2', 598.01, 867.00, 10.00, 0.00),
    (10, 'S', '3', 867.01, 3017.00, 15.00, 26.90),
    (11, 'S', '4', 3017.01, 5544.00, 25.00, 349.40),
    (12, 'S', '5', 5540.01, 14467.00, 28.00, 981.15),
    (13, 'S', '6', 14467.01, 31250.00, 33.00, 3479.59),
    (14, 'S', '7', 31250.01, 1000000.00, 35.00, 9017.98)    
GO

INSERT INTO Finance.CashDisbursementType
    (CashDisbursementTypeID, EventTypeName, PayeeTypeName)
VALUES
    (1, 'Labor Acquisition', 'Employee'),
    (2, 'Purchasing', 'Vendor')
GO






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
    (1056, 2, 5, 2, 3.39, '0.50-inch T-Connector'),
    (1059, 2, 5, 5, 6.59, '3.00-inch T-Connector')    
GO

INSERT INTO Sales.SalesOrders
    (SalesOrderID, CustomerID, EmployeeID, SalesOrderDate, CustomerPO, SalesOrderAmount)
VALUES
    (100001, 10001, 112, '2021-01-14', '101-PR-753979', 1941.51),
    (100002, 10003, 107, '2021-01-16', '26754', 3406.01),
    (100003, 10007, 109, '2021-01-16', 'BP-8666789', 21.95),
    (100004, 10005, 111, '2021-01-17', '276-555438', 659.10),
    (100005, 10010, 115, '2021-01-17', '985553', 488.89)
GO

INSERT INTO Sales.SalesOrderDetails
    (SalesOrderDetailID, SalesOrderID, InventoryID, QuantityOrdered, UnitPrice)
VALUES
    (1, 100001, 1006, 10, 65.19),
    (2, 100001, 1030, 14, 28.49),
    (3, 100001, 1038, 50, 14.95),
    (4, 100001, 1044, 30, 1.95),
    (5, 100001, 1056, 25, 3.39),
    (6, 100002, 1005, 42, 55.29),
    (7, 100002, 1041, 27, 38.59),
    (8, 100002, 1047, 10, 4.19),
    (9, 100003, 1001, 100, 21.95),
    (10, 100004, 1035, 19, 18.25),
    (11, 100004, 1044, 25, 1.95),
    (12, 100004, 1059, 40, 6.59),
    (13, 100005, 1002, 5, 26.49),
    (14, 100005, 1005, 2, 55.29),
    (15, 100005, 1044, 21, 1.95),
    (16, 100005, 1047, 7, 4.19),
    (17, 100005, 1053, 5, 5.29),
    (18, 100005, 1056, 9, 3.39),
    (19, 100005, 1059, 18, 6.59)             
GO