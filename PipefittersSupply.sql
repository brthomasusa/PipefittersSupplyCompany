CREATE DATABASE PipefittersSupply
GO

USE PipefittersSupply;

SELECT type_desc, size * 8 / 1024 AS size_MB, physical_name
FROM sys.master_files
WHERE database_id = DB_ID('PipefittersSupply');

