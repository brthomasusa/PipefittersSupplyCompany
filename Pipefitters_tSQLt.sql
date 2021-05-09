USE Pipefitters_tSQLt;
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
  EmployeeID int PRIMARY KEY CLUSTERED,
  EmployeeTypeID int NOT NULL REFERENCES HumanResources.EmployeeTypes (EmployeeTypeID),
  LastName nvarchar(25) NOT NULL,
  FirstName nvarchar(25) NOT NULL,
  MiddleInitial nchar(1) NULL,
  SSN nchar(9) NOT NULL UNIQUE,
  AddressLine1 nvarchar(30) NOT NULL,
  AddressLine2 nvarchar(30) NULL,
  City nvarchar(30) NOT NULL,
  [State] nchar(2) NOT NULL,
  ZipCode nvarchar(10) NOT NULL,
  Telephone nvarchar(14) NOT NULL,
  MaritalStatus nchar(1) CHECK (MaritalStatus IN ('M', 'S')) DEFAULT 'S' NOT NULL,
  Exemptions int DEFAULT 0 NOT NULL,
  PayRate decimal(18, 2) CHECK (PayRate >= 7.50 AND PayRate <= 40.00) NOT NULL,
  StartDate datetime2(0) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
);
GO

CREATE INDEX idx_Employees$EmployeeTypeID 
  ON HumanResources.Employees (EmployeeTypeID);
GO

CREATE UNIQUE INDEX idx_Employees$LastName$FirstName$MiddlieInitial 
  ON HumanResources.Employees (LastName, FirstName, MiddleInitial);
GO