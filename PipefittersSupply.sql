-- CREATE DATABASE PipefittersSupply
-- GO

-- USE PipefittersSupply;

CREATE SCHEMA Sales
GO
CREATE SCHEMA Purchasing
GO
CREATE SCHEMA HumanResources
GO
CREATE SCHEMA Finance
GO

DROP TABLE IF EXISTS
    HumanResources.EmployeeTypes,
    HumanResources.Employees,
    HumanResources.TimeCard,
    HumanResources.ExemptionLkup,
    HumanResources.FedWithHolding,
    Finance.CashAccounts,
    Finance.CashReceipts,
    Finance.CashDisbursementType,
    Finance.CashDisbursement,


    Sales.CompositionTypes,
    Sales.InventoryTypes,
    Sales.DiameterTypes,
    Sales.Inventory,    
    Sales.Customers,
    Sales.SalesOrders ,
    Sales.SalesOrderDetails,
    Sales.Invoices,
    Sales.InvoiceDetails
    
    
GO

CREATE TABLE HumanResources.EmployeeTypes
(
  EmployeeTypeID int IDENTITY NOT NULL,
  EmployeeTypeName nvarchar(25) NOT NULL,
  PRIMARY KEY CLUSTERED (EmployeeTypeID)
)
GO

CREATE UNIQUE INDEX EmployeeTypes_EmployeeTypeName 
  ON HumanResources.EmployeeTypes (EmployeeTypeName)
GO

CREATE TABLE HumanResources.Employees
(
  EmployeeID int NOT NULL,
  EmployeeTypeID int NOT NULL,
  LastName nvarchar(25) NOT NULL,
  FirstName nvarchar(25) NOT NULL,
  MiddleInitial nchar(1) NULL,
  SSN nchar(9) NOT NULL,
  AddressLine1 nvarchar(30) NOT NULL,
  AddressLine2 nvarchar(30) NULL,
  City nvarchar(30) NOT NULL,
  [State] nchar(2) NOT NULL,
  ZipCode nvarchar(10) NOT NULL,
  Telephone nvarchar(14) NOT NULL,
  MaritalStatus nchar(1) DEFAULT 'S' NOT NULL,
  Exemptions int NOT NULL,
  PayRate decimal(18, 2) NOT NULL,
  StartDate datetime2(0) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (EmployeeID),
  CONSTRAINT CHK_MaritalStatus CHECK (MaritalStatus IN ('M', 'S')),
  CONSTRAINT CHK_PayRate CHECK (PayRate >= 7.50 AND PayRate <= 40.00)
);
GO

CREATE INDEX Employees_EmployeeTypeID 
  ON HumanResources.Employees (EmployeeTypeID);

CREATE UNIQUE INDEX Employees_FullName 
  ON HumanResources.Employees (LastName, FirstName, MiddleInitial);

CREATE UNIQUE INDEX Employees_SSN 
  ON HumanResources.Employees (SSN);
GO

CREATE INDEX idx_Employees$Exemptions 
  ON HumanResources.Employees (Exemptions);
GO

ALTER TABLE HumanResources.Employees WITH CHECK ADD CONSTRAINT [FK_Employees$EmployeeTypeID_EmployeeTypes$EmployeeTypeID] FOREIGN KEY(EmployeeTypeID)
REFERENCES HumanResources.EmployeeTypes (EmployeeTypeID)
ON DELETE NO ACTION
GO

CREATE TABLE HumanResources.TimeCard
(
  TimeCardID int NOT NULL,
  EmployeeID int NOT NULL,
  SupervisorID int NOT NULL,
  PayPeriodEnded DATETIME2(0) NOT NULL,
  RegularHours int NOT NULL,
  OverTimeHours int DEFAULT 0 NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (TimeCardID),
  CONSTRAINT CHK_RegularHours CHECK (RegularHours >= 0 AND RegularHours < 185),
  CONSTRAINT CHK_OverTimeHours CHECK (OverTimeHours >= 0 AND OverTimeHours <= 201)
)
GO

CREATE INDEX TimeCard_EmployeeID 
  ON HumanResources.TimeCard (EmployeeID)
GO

CREATE INDEX TimeCard_SupervisorID 
  ON HumanResources.TimeCard (SupervisorID)
GO

CREATE INDEX idx_TimeCard$PayPeriodEnded 
  ON HumanResources.TimeCard (PayPeriodEnded)
GO

ALTER TABLE HumanResources.TimeCard WITH CHECK ADD CONSTRAINT [FK_TimeCard$EmployeeID_Employees$EmployeeID] FOREIGN KEY(EmployeeID)
REFERENCES HumanResources.Employees (EmployeeID)
ON DELETE NO ACTION
GO

ALTER TABLE HumanResources.TimeCard WITH CHECK ADD CONSTRAINT [FK_TimeCard$SupervisorID_Employees$EmployeeID] FOREIGN KEY(SupervisorID)
REFERENCES HumanResources.Employees (EmployeeID)
ON DELETE NO ACTION
GO

CREATE TABLE HumanResources.ExemptionLkup
(
    ExemptionLkupID INT NOT NULL,
    NumberOfExemptions INT NOT NULL,
    ExemptionAmount DECIMAL(18,2) NOT NULL,
    PRIMARY KEY CLUSTERED (ExemptionLkupID)
)
GO

CREATE UNIQUE INDEX idx_ExemptionLkup$NumberOfExemptions
    ON HumanResources.ExemptionLkup (NumberOfExemptions)
GO

CREATE TABLE HumanResources.FedWithHolding
(
    FedWithHoldingID INT NOT NULL,
    MaritalStatus NCHAR(1) NOT NULL,
    FedTaxBracket NVARCHAR(2) NOT NULL,
    LowerLimit DECIMAL(18,2) NOT NULL,
    UpperLimit DECIMAL(18,2) NOT NULL,
    TaxRate DECIMAL(5,2) NOT NULL,
    BracketBaseAmount DECIMAL(18,2) NOT NULL,
    PRIMARY KEY CLUSTERED (FedWithHoldingID)
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

ALTER TABLE HumanResources.Employees WITH CHECK ADD CONSTRAINT [FK_Employees$Exemptions_ExemptionLkup$ExemptionLkupID] FOREIGN KEY(Exemptions)
REFERENCES HumanResources.ExemptionLkup (ExemptionLkupID)
ON DELETE NO ACTION
GO


CREATE TABLE Finance.CashAccounts
(
  CashAccountID int NOT NULL,
  AccountDescription nvarchar(30) NOT NULL,
  BankName nvarchar(30) NOT NULL,
  DateEstablished datetime2(0) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (CashAccountID)
)
GO

CREATE UNIQUE INDEX CashAccounts_AccountDescription 
  ON Finance.CashAccounts (AccountDescription)
GO

CREATE TABLE Finance.CashDisbursementType
(
  CashDisbursementTypeID INT NOT NULL,
  EventTypeName NVARCHAR(25) NOT NULL,
  PayeeTypeName NVARCHAR(25) NOT NULL,
  PRIMARY KEY CLUSTERED (CashDisbursementTypeID),
)
GO

CREATE TABLE Finance.CashDisbursement
(
  CashDisbursementID INT NOT NULL,
  CheckNumber NVARCHAR(15) NOT NULL,
  CashAccountID INT NOT NULL,
  CashDisbursementTypeID INT NOT NULL,
  PayeeID INT NOT NULL,
  EmployeeID INT NOT NULL,
  EventID INT NOT NULL,
  DisbursementAmount DECIMAL(18,2) NOT NULL,
  DisbursementDate DATETIME2(0) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (CashDisbursementID)
)
GO

CREATE UNIQUE INDEX IDX_CashDisbursement$CheckNumber$CashAccountID 
  ON Finance.CashDisbursement (CheckNumber, CashAccountID)
GO

CREATE INDEX IDX_CashDisbursements$CashAccountID
  ON Finance.CashDisbursement(CashAccountID)
GO

CREATE INDEX IDX_CashDisbursements$CashDisbursementTypeID
  ON Finance.CashDisbursement(CashDisbursementTypeID)
GO

CREATE INDEX IDX_CashDisbursements$PayeeID
  ON Finance.CashDisbursement(PayeeID)
GO

CREATE INDEX IDX_CashDisbursements$EmployeeID
  ON Finance.CashDisbursement(EmployeeID)
GO

CREATE INDEX IDX_CashDisbursements$EventID
  ON Finance.CashDisbursement(EventID)
GO

ALTER TABLE Finance.CashDisbursement WITH CHECK ADD CONSTRAINT [FK_CashDisbursements$CashAccountID_CashAccounts$CashAccountID] FOREIGN KEY(CashAccountID)
REFERENCES Finance.CashAccounts(CashAccountID)
ON DELETE NO ACTION
GO

ALTER TABLE Finance.CashDisbursement WITH CHECK ADD CONSTRAINT [FK_CashDisbursements$CashDisbursementTypeID_CashDisbursementTypes$CashDisbursementTypeID] FOREIGN KEY(CashDisbursementTypeID)
REFERENCES Finance.CashDisbursementType(CashDisbursementTypeID)
ON DELETE NO ACTION
GO

ALTER TABLE Finance.CashDisbursement WITH CHECK ADD CONSTRAINT [FK_CashDisbursements$EmployeeID_Employees$EmployeeID] FOREIGN KEY(EmployeeID)
REFERENCES HumanResources.Employees(EmployeeID)
ON DELETE NO ACTION
GO



















CREATE TABLE Sales.CompositionTypes
(
  CompositionTypeID int IDENTITY NOT NULL,
  CompositionTypeName nchar(1) NOT NULL,
  [Description] nvarchar(25) NOT NULL UNIQUE,
  PRIMARY KEY (CompositionTypeID)
);
GO

CREATE UNIQUE INDEX CompositionTypes_CompositionTypeName 
  ON Sales.CompositionTypes (CompositionTypeName);
GO

-- SELECT name, USER_NAME(principal_id) AS principal FROM sys.schemas WHERE name <> USER_NAME(principal_id); --don't list user schemas

CREATE TABLE Sales.InventoryTypes
(
  InventoryTypeID int IDENTITY NOT NULL,
  InventoryTypeName nchar(1) NOT NULL,
  [Description] nvarchar(25) NOT NULL,
  PRIMARY KEY (InventoryTypeID)
);
GO

CREATE UNIQUE INDEX InventoryTypes_InventoryTypeName 
  ON Sales.InventoryTypes (InventoryTypeName);
GO

CREATE TABLE Sales.DiameterTypes
(
  DiameterTypeID int IDENTITY NOT NULL,
  DiameterTypeName nchar(3) NOT NULL,
  [Description] nvarchar(25) NOT NULL,
  PRIMARY KEY (DiameterTypeID)
);
GO

CREATE UNIQUE INDEX DiameterTypes_DiameterTypeName 
  ON Sales.DiameterTypes (DiameterTypeName);
GO

CREATE TABLE Sales.Inventory
(
  InventoryID int NOT NULL,
  CompositionTypeID int NOT NULL,
  InventoryTypeID int NOT NULL,
  DiameterTypeID int NOT NULL,
  ListPrice decimal(18, 2) DEFAULT 0 NOT NULL,
  [Description] varchar(35) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (InventoryID)
)
GO

CREATE INDEX Inventory_CompositionTypeID 
  ON Sales.Inventory (CompositionTypeID)
GO

CREATE INDEX Inventory_InventoryTypeID 
  ON Sales.Inventory (InventoryTypeID)
GO

CREATE INDEX Inventory_DiameterTypeID 
  ON Sales.Inventory (DiameterTypeID)
GO

ALTER TABLE Sales.Inventory  WITH CHECK ADD  CONSTRAINT [FK_CompositionTypes_CompositionTypeID] FOREIGN KEY(CompositionTypeID)
REFERENCES Sales.CompositionTypes (CompositionTypeID)
ON DELETE CASCADE
GO

ALTER TABLE Sales.Inventory  WITH CHECK ADD  CONSTRAINT [FK_InventoryTypes_InventoryTypeID] FOREIGN KEY(InventoryTypeID)
REFERENCES Sales.InventoryTypes (InventoryTypeID)
ON DELETE CASCADE
GO

ALTER TABLE Sales.Inventory  WITH CHECK ADD  CONSTRAINT [FK_DiameterTypes_DiameterTypeID] FOREIGN KEY(DiameterTypeID)
REFERENCES Sales.DiameterTypes (DiameterTypeID)
ON DELETE CASCADE
GO

CREATE TABLE Sales.Customers
(
  CustomerID int NOT NULL,
  CustomerName nvarchar(2) NOT NULL,
  AddressLine1 nvarchar(50) NOT NULL,
  AddressLine2 nvarchar(50) NULL,
  City nvarchar(25) NOT NULL,
  State char(2) NOT NULL,
  ZipCode int NOT NULL,
  Telephone nvarchar(14) NOT NULL,
  CreditLimit decimal(18, 2) DEFAULT 0 NOT NULL CHECK(CreditLimit <= 50000),
  PrimaryContact varchar(25) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (CustomerID)
)
GO

CREATE UNIQUE INDEX Customers_CustomerName 
  ON Sales.Customers (CustomerName)
GO

ALTER TABLE Sales.Customers
    ALTER COLUMN CustomerName nvarchar(25) NOT NULL
GO
ALTER TABLE Sales.Customers
    ALTER COLUMN ZipCode nvarchar(10) NOT NULL
GO

CREATE TABLE Sales.SalesOrders
(
  SalesOrderID int NOT NULL,
  CustomerID int NOT NULL,
  EmployeeID int NOT NULL,
  SalesOrderDate datetime2(0) NOT NULL,
  CustomerPO nvarchar(15) NOT NULL,
  SalesOrderAmount decimal(18, 2) DEFAULT 0 NOT NULL CHECK(SalesOrderAmount >= 0),
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (SalesOrderID)
)
GO

CREATE INDEX SalesOrders_CustomerID 
  ON Sales.SalesOrders (CustomerID)
GO

CREATE INDEX SalesOrders_EmployeeID 
  ON Sales.SalesOrders (EmployeeID)
GO

CREATE INDEX SalesOrders_SalesOrderDate 
  ON Sales.SalesOrders (SalesOrderDate)
GO

CREATE INDEX SalesOrders_CustomerPO 
  ON Sales.SalesOrders (CustomerPO)
GO

ALTER TABLE Sales.SalesOrders WITH CHECK ADD CONSTRAINT [FK_Customers_CustomerID] FOREIGN KEY(CustomerID)
REFERENCES Sales.Customers (CustomerID)
ON UPDATE CASCADE
ON DELETE NO ACTION
GO

ALTER TABLE Sales.SalesOrders WITH CHECK ADD CONSTRAINT [FK_Employees_EmployeeID] FOREIGN KEY(EmployeeID)
REFERENCES Sales.Employees (EmployeeID)
ON UPDATE CASCADE
ON DELETE NO ACTION
GO

CREATE TABLE Sales.SalesOrderDetails
(
  SalesOrderDetailID int NOT NULL,
  SalesOrderID int NOT NULL,
  InventoryID int NOT NULL,
  QuantityOrdered int NOT NULL,
  UnitPrice decimal(18, 2) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (SalesOrderDetailID)
)
GO

CREATE INDEX SalesOrderDetails_SalesOrderID 
  ON Sales.SalesOrderDetails (SalesOrderID)
GO

CREATE INDEX SalesOrderDetails_InventoryID 
  ON Sales.SalesOrderDetails (InventoryID)
GO

ALTER TABLE Sales.SalesOrderDetails WITH CHECK ADD CONSTRAINT [FK_SalesOrders_SalesOrderID] FOREIGN KEY(SalesOrderID)
REFERENCES Sales.SalesOrders (SalesOrderID)
ON UPDATE CASCADE
ON DELETE NO ACTION
GO

ALTER TABLE Sales.SalesOrderDetails WITH CHECK ADD CONSTRAINT [FK_Inventory_InventoryID] FOREIGN KEY(InventoryID)
REFERENCES Sales.Inventory (InventoryID)
ON UPDATE CASCADE
ON DELETE NO ACTION
GO

CREATE TABLE Sales.Invoices
(
  InvoiceID int NOT NULL,
  SalesOrderID int NOT NULL,
  CustomerID int NOT NULL,
  EmployeeID int NOT NULL,
  ShippingDate datetime2(0) NOT NULL,
  SalesAmount decimal(18, 2) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (InvoiceID)
)
GO

CREATE UNIQUE INDEX Invoices_SalesOrderID 
  ON Sales.Invoices (SalesOrderID)
GO

CREATE INDEX Invoices_CustomerID 
  ON Sales.Invoices (CustomerID)
GO

CREATE INDEX Invoices_EmployeeID 
  ON Sales.Invoices (EmployeeID)
GO

CREATE INDEX Invoices_ShippingDate 
  ON Sales.Invoices (ShippingDate)
GO

ALTER TABLE Sales.Invoices WITH CHECK ADD CONSTRAINT [FK_Invoices_SalesOrders_SalesOrderID] FOREIGN KEY(SalesOrderID)
REFERENCES Sales.SalesOrders (SalesOrderID)
ON UPDATE CASCADE
ON DELETE NO ACTION
GO

ALTER TABLE Sales.Invoices WITH CHECK ADD CONSTRAINT [FK_Invoices_Customers_CustomerID] FOREIGN KEY(CustomerID)
REFERENCES Sales.Customers (CustomerID)
ON UPDATE CASCADE
ON DELETE NO ACTION
GO

ALTER TABLE Sales.Invoices WITH CHECK ADD CONSTRAINT [FK_Invoices_Employees_EmployeeID] FOREIGN KEY(EmployeeID)
REFERENCES Sales.Employees (EmployeeID)
ON UPDATE CASCADE
ON DELETE NO ACTION
GO

CREATE TABLE Sales.InvoiceDetails
(
  InvoiceDetailsID int NOT NULL,
  InvoiceID int NOT NULL,
  InventoryID int NOT NULL,
  QuantityShipped int NOT NULL,
  UnitPrice decimal(18, 2) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (InvoiceDetailsID)
)
GO

CREATE INDEX InvoiceDetails_InvoiceID 
  ON Sales.InvoiceDetails (InvoiceID)
GO

CREATE INDEX InvoiceDetails_InventoryID 
  ON Sales.InvoiceDetails (InventoryID)
GO

ALTER TABLE Sales.InvoiceDetails WITH CHECK ADD CONSTRAINT [FK_InvoiceDetails_Invoices_InvoiceID] FOREIGN KEY(InvoiceID)
REFERENCES Sales.Invoices (InvoiceID)
ON UPDATE CASCADE
ON DELETE NO ACTION
GO

ALTER TABLE Sales.InvoiceDetails WITH CHECK ADD CONSTRAINT [FK_InvoiceDetails_Inventory_InventoryID] FOREIGN KEY(InventoryID)
REFERENCES Sales.Inventory (InventoryID)
ON UPDATE CASCADE
ON DELETE NO ACTION
GO

CREATE TABLE Sales.CashReceipts
(
  CashReceiptID int NOT NULL,
  InvoiceID int NOT NULL,
  CashAccountID int NOT NULL,
  CustomerID int NOT NULL,
  EmployeeID int NOT NULL,
  RemittanceAdviceID nvarchar(25) NOT NULL,
  CashReceiptDate datetime2(0) NOT NULL,
  CashReceiptAmount decimal(18, 2) NOT NULL,
  CustomerCheckNumber nvarchar(15) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (CashReceiptID)
)
GO

CREATE INDEX CashReceipts_InvoiceID 
  ON Sales.CashReceipts (InvoiceID);

CREATE INDEX CashReceipts_CashAccountID 
  ON Sales.CashReceipts (CashAccountID);

CREATE INDEX CashReceipts_CustomerID 
  ON Sales.CashReceipts (CustomerID)
GO

CREATE INDEX CashReceipts_EmployeeID 
  ON Sales.CashReceipts (EmployeeID)
GO

CREATE INDEX CashReceipts_CashReceiptDate 
  ON Sales.CashReceipts (CashReceiptDate)
GO

ALTER TABLE Sales.CashReceipts WITH CHECK ADD CONSTRAINT [FK_CashReceipts_Invoices_InvoiceID] FOREIGN KEY(InvoiceID)
REFERENCES Sales.Invoices (InvoiceID)
ON DELETE NO ACTION
GO

ALTER TABLE Sales.CashReceipts WITH CHECK ADD CONSTRAINT [FK_CashReceipts$CashAccountID_CashAccounts$CashAccountID] FOREIGN KEY(CashAccountID)
REFERENCES Finance.CashAccounts (CashAccountID)
ON DELETE NO ACTION
GO

ALTER TABLE Sales.CashReceipts WITH CHECK ADD CONSTRAINT [FK_CashReceipts_Customers_CustomerID] FOREIGN KEY(CustomerID)
REFERENCES Sales.Customers (CustomerID)
GO

ALTER TABLE Sales.CashReceipts WITH CHECK ADD CONSTRAINT [FK_CashReceipts$EmployeeID_Employees$EmployeeID] FOREIGN KEY(EmployeeID)
REFERENCES HumanResources.Employees (EmployeeID)
GO







