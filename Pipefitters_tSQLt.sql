CREATE DATABASE Pipefitters_tSQLt
GO

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
  IsActive BIT DEFAULT 1 NOT NULL,
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

INSERT INTO HumanResources.Employees
    (EmployeeID, EmployeeTypeID, LastName, FirstName, MiddleInitial, SSN, AddressLine1, City, [State], ZipCode, Telephone, MaritalStatus, Exemptions, PayRate, StartDate, IsActive)
VALUES
    (101, 2, 'Sanchez', 'Ken', 'J', '123789999', '321 Tarrant Pl', 'Fort Worth', 'TX', '78965', '817-987-1234', 'M', 5, 40.00, '1998-12-02', 1),
    (108, 2, 'Phide', 'Terri', 'M', '638912345', '3455 South Corinth Circle', 'Dallas', 'TX', '75224', '214-987-1234', 'M', 1, 28.00, '2014-09-22', 1),
    (109, 2, 'Duffy', 'Terri', 'L', '999912345', '98 Reiger Ave', 'Dallas', 'TX', '75214', '214-987-1234', 'M', 2, 30.00, '2018-10-22', 1),
    (111, 2, 'Goldberg', 'Jozef', 'P', '036889999', '6667 Melody Lane, Apt 2', 'Dallas', 'TX', '75231', '469-321-1234', 'S', 1, 29.00, '2013-02-28', 1),
    (112, 2, 'Brown', 'Jamie', 'J', '123700009', '98777 Nigeria Town Rd', 'Arlington', 'TX', '78658', '817-555-5555', 'M', 2, 29.00, '2017-12-22', 1),
    (113, 2, 'Bush', 'George', 'W', '325559874', '777 Ervay Street', 'Dallas', 'TX', '75208', '214-555-5555', 'M', 5, 30.00, '2016-10-19', 1),
    (114, 1, 'Bushnell', 'Loretta', 'J', '370005409', '123 Main St', 'Flower Mound', 'TX', '78630', '817-555-5555', 'M', 2, 25.00, '2017-02-22', 1),
    (115, 1, 'Jacknoff', 'Jorge', 'C', '325509345', '777 Ervay Street', 'Dallas', 'TX', '75208', '214-987-5555', 'M', 3, 25.00, '2014-06-19', 1),
    (116, 3, 'Trump', 'Donald', 'J', '781287999', '1 Cowgirl Circle', 'Fort Worth', 'TX', '78965', '817-123-9874', 'M', 4, 8.00, '2003-07-02', 1),
    (117, 3, 'Trump', 'Ivanka', 'Z', '256281432', '1 Cowgirl Circle', 'Fort Worth', 'TX', '78965', '817-123-9874', 'M', 2, 10.50, '2003-07-02', 1),
    (118, 4, 'Thompson', 'Douglas', 'J', '371005409', '4338 Main Place', 'Rice', 'TX', '78430', '972-555-5555', 'S', 2, 17.00, '2017-02-22', 1),
    (119, 4, 'Hernandez', 'Jesus', 'G', '323509345', '1254 34th St', 'Irving', 'TX', '75268', '972-987-5555', 'M', 3, 15.00, '2019-06-19', 1),
    (120, 5, 'Doe', 'Jonny', 'A', '567882345', '32584 Collett Ln', 'Saginaw', 'TX', '78965', '817-123-9874', 'M', 4, 22.00, '2015-07-22', 1),
    (121, 5, 'Smith', 'Samuel', 'P', '256200432', '34 Wensworth Drive', 'Roanocke', 'TX', '78965', '817-123-9874', 'M', 2, 22.50, '2018-03-14', 1),
    (122, 6, 'Gomez', 'Roberto', 'J', '687005409', '4338 Main Place', 'Desoto', 'TX', '74430', '972-555-5555', 'S', 2, 17.00, '2017-02-22', 1),
    (123, 6, 'Hernandez', 'George', 'G', '478509345', '65 Adelia Lane', 'Ricardson', 'TX', '75268', '972-987-5555', 'M', 3, 15.00, '2019-06-19', 1)
GO

CREATE TABLE HumanResources.ExemptionLookUps
(
    ExemptionLookUpID INT PRIMARY KEY CLUSTERED,
    NumberOfExemptions INT CHECK (NumberOfExemptions >= 0) NOT NULL UNIQUE,
    ExemptionAmount DECIMAL(18,2) CHECK (ExemptionAmount >= 0) NOT NULL
)
GO

INSERT INTO HumanResources.ExemptionLookUps
    (ExemptionLookUpID, NumberOfExemptions, ExemptionAmount)
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

ALTER TABLE HumanResources.Employees WITH CHECK ADD CONSTRAINT [FK_Employees$Exemptions_ExemptionLookUps$ExemptionLookUpID] FOREIGN KEY(Exemptions)
REFERENCES HumanResources.ExemptionLookUps (ExemptionLookUpID)
ON DELETE NO ACTION
GO

CREATE TABLE HumanResources.FedWithHolding
(
    FedWithHoldingID INT PRIMARY KEY CLUSTERED,
    MaritalStatus NCHAR(1) NOT NULL,
    FedTaxBracket NVARCHAR(2) NOT NULL,
    LowerLimit DECIMAL(18,2) NOT NULL,
    UpperLimit DECIMAL(18,2) NOT NULL,
    TaxRate DECIMAL(5,3) NOT NULL,
    BracketBaseAmount DECIMAL(18,2) NOT NULL
)
GO

CREATE INDEX idx_FedWithHolding$MaritalStatus 
  ON HumanResources.FedWithHolding (MaritalStatus)
GO

CREATE INDEX idx_FedWithHolding$UpperLimit 
  ON HumanResources.FedWithHolding (UpperLimit)
GO

CREATE INDEX idx_FedWithHolding$LowerLimit 
  ON HumanResources.FedWithHolding (LowerLimit)
GO

CREATE UNIQUE INDEX idx_FedWithHolding$MaritalStatus$FedTaxBracket   
   ON HumanResources.FedWithHolding (MaritalStatus, FedTaxBracket)   
GO

INSERT INTO HumanResources.FedWithHolding
    (FedWithHoldingID, MaritalStatus, FedTaxBracket, LowerLimit, UpperLimit, TaxRate, BracketBaseAmount)
VALUES
    (1, 'M', '1', 0.00, 1313.00, 0.00, 0.00),
    (2, 'M', '2', 1313.01, 2038.00, 0.10, 0.00),
    (3, 'M', '3', 2038.01, 6304.00, 0.15, 72.50),
    (4, 'M', '4', 6304.01, 9844.00, 0.25, 712.40),
    (5, 'M', '5', 9844.01, 18050.00, 0.28, 1597.40),
    (6, 'M', '6', 18050.01, 31725.00, 0.33, 3895.08),
    (7, 'M', '7', 31725.01, 1000000.00, 0.35, 8407.83),
    (8, 'S', '1', 0.00, 598.00, 0.00, 0.00),
    (9, 'S', '2', 598.01, 867.00, 0.10, 0.00),
    (10, 'S', '3', 867.01, 3017.00, 0.15, 26.90),
    (11, 'S', '4', 3017.01, 5544.00, 0.25, 349.40),
    (12, 'S', '5', 5540.01, 14467.00, 0.28, 981.15),
    (13, 'S', '6', 14467.01, 31250.00, 0.33, 3479.59),
    (14, 'S', '7', 31250.01, 1000000.00, 0.35, 9017.98)    
GO

CREATE TABLE HumanResources.TimeCards
(
  TimeCardID int PRIMARY KEY CLUSTERED,
  EmployeeID int NOT NULL REFERENCES HumanResources.Employees (EmployeeID),
  SupervisorID int NOT NULL REFERENCES HumanResources.Employees (EmployeeID),
  PayPeriodEnded DATETIME2(0) NOT NULL,
  RegularHours int CHECK (RegularHours >= 0 AND RegularHours < 185) NOT NULL,
  OverTimeHours int DEFAULT 0 CHECK (OverTimeHours >= 0 AND OverTimeHours <= 201) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
)
GO

CREATE INDEX idx_TimeCard$EmployeeID 
  ON HumanResources.TimeCards (EmployeeID)
GO

CREATE INDEX idx_TimeCard$SupervisorID 
  ON HumanResources.TimeCards (SupervisorID)
GO

CREATE INDEX idx_TimeCard$PayPeriodEnded 
  ON HumanResources.TimeCards (PayPeriodEnded)
GO

INSERT INTO HumanResources.TimeCards
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
    (16, 123, 113, '2021-01-31', 168, 0),
    (17, 101, 101, '2021-02-28', 160, 0),
    (18, 108, 101, '2021-02-28', 120, 8),
    (19, 109, 101, '2021-02-28', 160, 2),
    (20, 111, 101, '2021-02-28', 160, 0),
    (21, 112, 101, '2021-02-28', 160, 0),
    (22, 113, 101, '2021-02-28', 160, 0),
    (23, 114, 108, '2021-02-28', 160, 0),
    (24, 115, 108, '2021-02-28', 160, 0),
    (25, 116, 109, '2021-02-28', 152, 0),
    (26, 117, 109, '2021-02-28', 160, 0),
    (27, 118, 111, '2021-02-28', 160, 6),
    (28, 119, 111, '2021-02-28', 160, 0),
    (29, 120, 112, '2021-02-28', 160, 0),
    (30, 121, 112, '2021-02-28', 160, 0),
    (31, 122, 113, '2021-02-28', 160, 7),
    (32, 123, 113, '2021-02-28', 160, 0)    
GO

CREATE TABLE Finance.CashAccounts
(
  CashAccountID int PRIMARY KEY CLUSTERED,
  AccountDescription nvarchar(30) NOT NULL UNIQUE,
  BankName nvarchar(30) NOT NULL,
  DateEstablished datetime2(0) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
)
GO

INSERT INTO Finance.CashAccounts
    (CashAccountID, AccountDescription, BankName, DateEstablished)
VALUES
    (101, 'Primary Checking Account', 'First Bank', '2020-09-03'),
    (102, 'Payroll Account', 'First Bank', '2020-09-03'),
    (103, 'Financing Proceeds', 'First Bank', '2020-12-20')
GO

CREATE TABLE Finance.CashDisbursementTypes
(
  CashDisbursementTypeID INT PRIMARY KEY CLUSTERED,
  EventTypeName NVARCHAR(25) NOT NULL  UNIQUE,
  PayeeTypeName NVARCHAR(25) NOT NULL  UNIQUE
)
GO

INSERT INTO Finance.CashDisbursementTypes
    (CashDisbursementTypeID, EventTypeName, PayeeTypeName)
VALUES
    (1, 'Labor Acquisition', 'Employee'),
    (2, 'Purchasing', 'Vendor'),
    (3, 'Loan Payment', 'Creditor'),
    (4, 'Dividend Payments', 'Stockholder')
GO

CREATE TABLE Finance.CashDisbursements
(
  CashDisbursementID INT PRIMARY KEY CLUSTERED,
  CheckNumber NVARCHAR(15) NOT NULL,
  CashAccountID INT NOT NULL REFERENCES Finance.CashAccounts(CashAccountID),
  CashDisbursementTypeID INT NOT NULL REFERENCES Finance.CashDisbursementTypes(CashDisbursementTypeID),
  PayeeID INT NOT NULL,
  EmployeeID INT NOT NULL REFERENCES HumanResources.Employees (EmployeeID),
  EventID INT NOT NULL,
  DisbursementAmount DECIMAL(18,2) NOT NULL,
  DisbursementDate DATETIME2(0) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
)
GO

CREATE INDEX IDX_CashDisbursements$CashAccountID
  ON Finance.CashDisbursements (CashAccountID)
GO

CREATE INDEX IDX_CashDisbursements$CashDisbursementTypeID
  ON Finance.CashDisbursements (CashDisbursementTypeID)
GO

CREATE INDEX IDX_CashDisbursements$EmployeeID
  ON Finance.CashDisbursements (EmployeeID)
GO

CREATE INDEX IDX_CashDisbursements$PayeeID
  ON Finance.CashDisbursements (PayeeID)
GO

CREATE INDEX IDX_CashDisbursements$EventID
  ON Finance.CashDisbursements (EventID)
GO

CREATE UNIQUE INDEX IDX_CashDisbursement$CheckNumber$CashAccountID 
  ON Finance.CashDisbursements (CheckNumber, CashAccountID)
GO

CREATE TABLE Finance.CashReceiptTypes
(
  CashReceiptTypeID INT PRIMARY KEY CLUSTERED,
  EventTypeName NVARCHAR(25) NOT NULL,
  PayeeTypeName NVARCHAR(25) NOT NULL
)
GO

CREATE UNIQUE INDEX idx_CashReceiptType$EventTypeName
    ON Finance.CashReceiptTypes (EventTypeName)
GO

CREATE UNIQUE INDEX idx_CashReceiptType$PayeeTypeName
    ON Finance.CashReceiptTypes (PayeeTypeName)
GO

INSERT INTO Finance.CashReceiptTypes
    (CashReceiptTypeID, EventTypeName, PayeeTypeName)
VALUES
    (1, 'Sale', 'Customer'),
    (2, 'Debt Issue', 'Creditor'),
    (3, 'Stock Issue', 'Stockholder')
GO

CREATE TABLE Finance.CashReceipts
(
  CashReceiptID int PRIMARY KEY CLUSTERED,
  CashReceiptTypeID INT NOT NULL REFERENCES Finance.CashReceiptTypes (CashReceiptTypeID),
  EventID int NOT NULL,
  CashAccountID int NOT NULL REFERENCES Finance.CashAccounts (CashAccountID),
  PayeeID int NOT NULL,
  EmployeeID int NOT NULL REFERENCES HumanResources.Employees (EmployeeID),
  RemittanceAdviceID nvarchar(25) NOT NULL,
  CashReceiptDate datetime2(0) NOT NULL,
  CashReceiptAmount decimal(18, 2) CHECK (CashReceiptAmount >= 0) NOT NULL,
  PayorCheckNumber nvarchar(15) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
)
GO

CREATE INDEX idx_CashReceipts$CashReceiptTypeID 
  ON Finance.CashReceipts (CashReceiptTypeID);
GO

CREATE INDEX idx_CashReceipts$CashAccountID 
  ON Finance.CashReceipts (CashAccountID);
GO

CREATE INDEX idx_CashReceipts$EmployeeID 
  ON Finance.CashReceipts (EmployeeID)
GO

CREATE INDEX idx_CashReceipts$EventID 
  ON Finance.CashReceipts (EventID);
GO

CREATE INDEX idx_CashReceipts$PayeeID 
  ON Finance.CashReceipts (PayeeID)
GO

