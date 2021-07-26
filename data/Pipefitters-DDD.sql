CREATE DATABASE Pipefitters_DDD
GO

USE Pipefitters_DDD
GO

CREATE SCHEMA Sales
GO
CREATE SCHEMA Purchasing
GO
CREATE SCHEMA HumanResources
GO
CREATE SCHEMA Finance
GO

CREATE TABLE HumanResources.EmployeeTypes
(
  EmployeeTypeID int IDENTITY PRIMARY KEY CLUSTERED,
  EmployeeTypeName nvarchar(25) NOT NULL UNIQUE
)
GO

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

CREATE TABLE HumanResources.Employees
(
  EmployeeID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
  EmployeeTypeID int NOT NULL REFERENCES HumanResources.EmployeeTypes (EmployeeTypeID),
  SupervisorID UNIQUEIDENTIFIER NOT NULL REFERENCES HumanResources.Employees (EmployeeID),
  LastName nvarchar(25) NOT NULL,
  FirstName nvarchar(25) NOT NULL,
  MiddleInitial nchar(1) NULL,
  SSN nchar(9) NOT NULL UNIQUE,
  AddressLine1 nvarchar(30) NOT NULL,
  AddressLine2 nvarchar(30) NULL,
  City nvarchar(30) NOT NULL,
  StateCode nchar(2) NOT NULL,
  ZipCode nvarchar(10) NOT NULL,
  Telephone nvarchar(14) NOT NULL,
  MaritalStatus nchar(1) CHECK (MaritalStatus IN ('M', 'S')) DEFAULT 'S' NOT NULL,
  Exemptions int DEFAULT 0 NOT NULL,
  PayRate decimal(18, 2) CHECK (PayRate >= 7.50 AND PayRate <= 40.00) NOT NULL,
  StartDate datetime2(0) NOT NULL,
  IsActive BIT DEFAULT 1 NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
);
GO

INSERT INTO HumanResources.Employees
    (EmployeeID, EmployeeTypeID, SupervisorID, LastName, FirstName, MiddleInitial, SSN, AddressLine1, City, StateCode, ZipCode, Telephone, MaritalStatus, Exemptions, PayRate, StartDate, IsActive)
VALUES
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', 2, '4B900A74-E2D9-4837-B9A4-9E828752716E','Sanchez', 'Ken', 'J', '123789999', '321 Tarrant Pl', 'Fort Worth', 'TX', '78965', '817-987-1234', 'M', 5, 40.00, '1998-12-02', 1)
GO

INSERT INTO HumanResources.Employees
    (EmployeeID, EmployeeTypeID, SupervisorID, LastName, FirstName, MiddleInitial, SSN, AddressLine1, City, StateCode, ZipCode, Telephone, MaritalStatus, Exemptions, PayRate, StartDate, IsActive)
VALUES
    ('660bb318-649e-470d-9d2b-693bfb0b2744', 2, '4B900A74-E2D9-4837-B9A4-9E828752716E','Phide', 'Terri', 'M', '638912345', '3455 South Corinth Circle', 'Dallas', 'TX', '75224', '214-987-1234', 'M', 1, 28.00, '2014-09-22', 1),
    ('9f7b902d-566c-4db6-b07b-716dd4e04340', 2, '4B900A74-E2D9-4837-B9A4-9E828752716E','Duffy', 'Terri', 'L', '999912345', '98 Reiger Ave', 'Dallas', 'TX', '75214', '214-987-1234', 'M', 2, 30.00, '2018-10-22', 1),
    ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', 2, '4B900A74-E2D9-4837-B9A4-9E828752716E','Goldberg', 'Jozef', 'P', '036889999', '6667 Melody Lane, Apt 2', 'Dallas', 'TX', '75231', '469-321-1234', 'S', 1, 29.00, '2013-02-28', 1),
    ('0cf9de54-c2ca-417e-827c-a5b87be2d788', 2, '4B900A74-E2D9-4837-B9A4-9E828752716E','Brown', 'Jamie', 'J', '123700009', '98777 Nigeria Town Rd', 'Arlington', 'TX', '78658', '817-555-5555', 'M', 2, 29.00, '2017-12-22', 1),
    ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', 2, '4B900A74-E2D9-4837-B9A4-9E828752716E','Bush', 'George', 'W', '325559874', '777 Ervay Street', 'Dallas', 'TX', '75208', '214-555-5555', 'M', 5, 30.00, '2016-10-19', 1)
GO
