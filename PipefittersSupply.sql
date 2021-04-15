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

