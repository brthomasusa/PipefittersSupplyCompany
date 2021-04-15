-- CREATE DATABASE PipefittersSupply
-- GO

-- USE PipefittersSupply;

-- SELECT type_desc, size * 8 / 1024 AS size_MB, physical_name
-- FROM sys.master_files
-- WHERE database_id = DB_ID('PipefittersSupply');

-- SELECT SUSER_SNAME(owner_sid) AS databaseOwner, name FROM sys.databases; 

-- CREATE SCHEMA Sales
-- GO

-- CREATE SCHEMA Purchasing
-- GO

-- DROP TABLE IF EXISTS
--     Sales.CompositionTypes,
--     Sales.InventoryTypes,
--     Sales.DiameterTypes,
--     Sales.Inventory,
--     Sales.EmployeeTypes,
--     Sales.Employees

-- GO

-- CREATE TABLE Sales.CompositionTypes
-- (
--     CompositionTypeID int IDENTITY NOT NULL,
--     CompositionTypeName nchar(1) NOT NULL,
--     [Description] nvarchar(25) NOT NULL UNIQUE,
--     PRIMARY KEY (CompositionTypeID)
-- );

-- CREATE UNIQUE INDEX CompositionTypes_CompositionTypeName 
--   ON Sales.CompositionTypes (CompositionTypeName);

-- SELECT name, USER_NAME(principal_id) AS principal FROM sys.schemas WHERE name <> USER_NAME(principal_id); --don't list user schemas

-- CREATE TABLE Sales.InventoryTypes
-- (
--     InventoryTypeID int IDENTITY NOT NULL,
--     InventoryTypeName nchar(1) NOT NULL,
--     [Description] nvarchar(25) NOT NULL,
--     PRIMARY KEY (InventoryTypeID)
-- );

-- CREATE UNIQUE INDEX InventoryTypes_InventoryTypeName 
--   ON Sales.InventoryTypes (InventoryTypeName);

-- CREATE TABLE Sales.DiameterTypes
-- (
--     DiameterTypeID int IDENTITY NOT NULL,
--     DiameterTypeName nchar(3) NOT NULL,
--     [Description] nvarchar(25) NOT NULL,
--     PRIMARY KEY (DiameterTypeID)
-- );

-- CREATE UNIQUE INDEX DiameterTypes_DiameterTypeName 
--   ON Sales.DiameterTypes (DiameterTypeName);

-- CREATE TABLE Sales.Inventory
-- (
--     InventoryID int NOT NULL,
--     CompositionTypeID int NOT NULL,
--     InventoryTypeID int NOT NULL,
--     DiameterTypeID int NOT NULL,
--     ListPrice decimal(18, 2) DEFAULT 0 NOT NULL,
--     CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
--     LastModifiedDate datetime2(7) NULL,
--     PRIMARY KEY CLUSTERED (InventoryID)
-- )
-- GO

-- CREATE INDEX Inventory_CompositionTypeID 
--   ON Sales.Inventory (CompositionTypeID)
-- GO

-- CREATE INDEX Inventory_InventoryTypeID 
--   ON Sales.Inventory (InventoryTypeID)
-- GO

-- CREATE INDEX Inventory_DiameterTypeID 
--   ON Sales.Inventory (DiameterTypeID)
-- GO

-- ALTER TABLE Sales.Inventory  WITH CHECK ADD  CONSTRAINT [FK_CompositionTypes_CompositionTypeID] FOREIGN KEY(CompositionTypeID)
-- REFERENCES Sales.CompositionTypes (CompositionTypeID)
-- ON DELETE CASCADE
-- GO

-- ALTER TABLE Sales.Inventory  WITH CHECK ADD  CONSTRAINT [FK_InventoryTypes_InventoryTypeID] FOREIGN KEY(InventoryTypeID)
-- REFERENCES Sales.InventoryTypes (InventoryTypeID)
-- ON DELETE CASCADE
-- GO

-- ALTER TABLE Sales.Inventory  WITH CHECK ADD  CONSTRAINT [FK_DiameterTypes_DiameterTypeID] FOREIGN KEY(DiameterTypeID)
-- REFERENCES Sales.DiameterTypes (DiameterTypeID)
-- ON DELETE CASCADE
-- GO

-- CREATE TABLE Sales.EmployeeTypes
-- (
--     EmployeeTypeID int IDENTITY NOT NULL,
--     EmployeeTypeName nvarchar(25) NOT NULL,
--     PRIMARY KEY CLUSTERED (EmployeeTypeID)
-- )
-- GO

-- CREATE UNIQUE INDEX EmployeeTypes_EmployeeTypeName 
--   ON Sales.EmployeeTypes (EmployeeTypeName)
-- GO

-- CREATE TABLE Sales.Employees
-- (
--     EmployeeID int NOT NULL,
--     EmployeeTypeID int NOT NULL,
--     LastName nvarchar(25) NOT NULL,
--     FirstName nvarchar(25) NOT NULL,
--     MiddleInitial nchar(1) NULL,
--     SSN nchar(9) NOT NULL,
--     AddressLine1 nvarchar(30) NOT NULL,
--     AddressLine2 nvarchar(30) NULL,
--     City nvarchar(30) NOT NULL,
--     State nchar(2) NOT NULL,
--     ZipCode nvarchar(10) NOT NULL,
--     Telephone nvarchar(14) NOT NULL,
--     MaritalStatus nchar(1) NOT NULL,
--     Exemptions int NOT NULL,
--     PayRate decimal(18, 2) NOT NULL,
--     StartDate datetime2(0) NOT NULL,
--     CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
--     LastModifiedDate datetime2(7) NULL,
--     PRIMARY KEY (EmployeeID)
-- );

-- CREATE INDEX Employees_EmployeeTypeID 
--   ON Sales.Employees (EmployeeTypeID);

-- CREATE UNIQUE INDEX Employees_FullName 
--   ON Sales.Employees (LastName, FirstName, MiddleInitial);

-- CREATE UNIQUE INDEX Employees_SSN 
--   ON Sales.Employees (SSN);

-- GO

-- ALTER TABLE Sales.Employees WITH CHECK ADD CONSTRAINT [FK_EmployeeTypes_EmployeeTypeID] FOREIGN KEY(EmployeeTypeID)
-- REFERENCES Sales.EmployeeTypes (EmployeeTypeID)
-- ON UPDATE CASCADE
-- ON DELETE NO ACTION
-- GO








