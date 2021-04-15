CREATE DATABASE PipefittersSupply
GO

USE PipefittersSupply;

-- SELECT type_desc, size * 8 / 1024 AS size_MB, physical_name
-- FROM sys.master_files
-- WHERE database_id = DB_ID('PipefittersSupply');

-- SELECT SUSER_SNAME(owner_sid) AS databaseOwner, name FROM sys.databases; 

-- CREATE SCHEMA Sales
-- GO

-- CREATE SCHEMA Purchasing
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

CREATE TABLE Sales.Inventory
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






