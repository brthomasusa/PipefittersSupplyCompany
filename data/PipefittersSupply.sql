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
    Finance.CashReceiptType,
    Finance.CashReceipts,
    Finance.CashDisbursementType,
    Finance.CashDisbursement,               
    Finance.StockholderCreditor,
    Finance.LoanAgreement,
    Finance.LoanRepaymentSchedule,
    Finance.StockSubscription,
    Finance.DividendPymtRate,
    Purchasing.Vendors,
    Purchasing.CompositionTypes,
    Purchasing.InventoryTypes,
    Purchasing.DiameterTypes,
    Purchasing.Inventory,     
    Purchasing.PurchaseOrders,
    Purchasing.PurchaseOrderDetails,
    Purchasing.InventoryReceipts,
    Purchasing.InventoryReceiptDetails,
    Purchasing.PurchaseType,
    Purchasing.InventoryReceipts,
    Purchasing.InventoryReceiptDetails,
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

CREATE TABLE Finance.StockholderCreditor
(
  FinancierID int NOT NULL,
  FinancierName nvarchar(25) NOT NULL,
  AddressLine1 nvarchar(50) NOT NULL,
  AddressLine2 nvarchar(50) NULL,
  City nvarchar(25) NOT NULL,
  [State] char(2) NOT NULL,
  ZipCode nvarchar(10) NOT NULL,
  Telephone nvarchar(14) NOT NULL,
  PrimaryContact varchar(25) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (FinancierID)
)
GO

CREATE UNIQUE INDEX idx_StockholderCreditor$FinancierName 
  ON Finance.StockholderCreditor (FinancierName)
GO

CREATE TABLE Finance.LoanAgreement
(
    LoadID INT NOT NULL,
    FinancierID int NOT NULL,
    EmployeeID int NOT NULL,
    LoanAmount DECIMAL(18,2) NOT NULL,
    InterestRate NUMERIC(10,6) NOT NULL,        
    LoanDate DATETIME2(0) NOT NULL,
    MaturityDate DATETIME2(0) NOT NULL,
    PymtsPerYear INT NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL,
    PRIMARY KEY CLUSTERED (LoadID),
    CONSTRAINT CHK_LoanDateMaturityDate CHECK (LoanDate < MaturityDate)
)
GO

CREATE INDEX idx_LoanAgreement$FinancierID 
  ON Finance.LoanAgreement (FinancierID);
GO

CREATE INDEX idx_LoanAgreement$EmployeeID 
  ON Finance.LoanAgreement (EmployeeID);
GO

ALTER TABLE Finance.LoanAgreement WITH CHECK ADD CONSTRAINT [FK_LoanAgreement$FinancierID_StockholderCreditor$FinancierID] FOREIGN KEY(FinancierID)
REFERENCES Finance.StockholderCreditor (FinancierID)
ON DELETE NO ACTION
GO

ALTER TABLE Finance.LoanAgreement WITH CHECK ADD CONSTRAINT [FK_LoanAgreement$EmployeeID_Employees$EmployeeID] FOREIGN KEY(EmployeeID)
REFERENCES HumanResources.Employees (EmployeeID)
ON DELETE NO ACTION
GO

CREATE TABLE Finance.CashReceiptType
(
  CashReceiptTypeID INT NOT NULL,
  EventTypeName NVARCHAR(25) NOT NULL,
  PayeeTypeName NVARCHAR(25) NOT NULL,
  PRIMARY KEY CLUSTERED (CashReceiptTypeID),
)
GO

CREATE UNIQUE INDEX idx_CashReceiptType$EventTypeName
    ON Finance.CashReceiptType(EventTypeName)
GO

CREATE UNIQUE INDEX idx_CashReceiptType$PayeeTypeName
    ON Finance.CashReceiptType(PayeeTypeName)
GO

CREATE TABLE Finance.CashReceipts
(
  CashReceiptID int NOT NULL,
  CashReceiptTypeID INT NOT NULL,
  EventID int NOT NULL,
  CashAccountID int NOT NULL,
  PayeeID int NOT NULL,
  EmployeeID int NOT NULL,
  RemittanceAdviceID nvarchar(25) NOT NULL,
  CashReceiptDate datetime2(0) NOT NULL,
  CashReceiptAmount decimal(18, 2) NOT NULL,
  PayorCheckNumber nvarchar(15) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (CashReceiptID)
)
GO

CREATE INDEX idx_CashReceipts$CashReceiptTypeID 
  ON Finance.CashReceipts (CashReceiptTypeID);
GO

CREATE INDEX idx_CashReceipts$EventID 
  ON Finance.CashReceipts (EventID);
GO

CREATE INDEX idx_CashReceipts$CashAccountID 
  ON Finance.CashReceipts (CashAccountID);
GO

CREATE INDEX idx_CashReceipts$PayeeID 
  ON Finance.CashReceipts (PayeeID)
GO

CREATE INDEX idx_CashReceipts$EmployeeID 
  ON Finance.CashReceipts (EmployeeID)
GO

CREATE INDEX idx_CashReceipts$CashReceiptDate 
  ON Finance.CashReceipts (CashReceiptDate)
GO

ALTER TABLE Finance.CashReceipts WITH CHECK ADD CONSTRAINT [FK_CashReceipts$CashReceiptTypeID_CashReceiptType$CashReceiptTypeID] FOREIGN KEY(CashReceiptTypeID)
REFERENCES Finance.CashReceiptType (CashReceiptTypeID)
ON DELETE NO ACTION
GO

ALTER TABLE Finance.CashReceipts WITH CHECK ADD CONSTRAINT [FK_CashReceipts$CashAccountID_CashAccounts$CashAccountID] FOREIGN KEY(CashAccountID)
REFERENCES Finance.CashAccounts (CashAccountID)
ON DELETE NO ACTION
GO

ALTER TABLE Finance.CashReceipts WITH CHECK ADD CONSTRAINT [FK_CashReceipts$EmployeeID_Employees$EmployeeID] FOREIGN KEY(EmployeeID)
REFERENCES HumanResources.Employees (EmployeeID)
ON DELETE NO ACTION
GO

-- ALTER TABLE Finance.CashReceipts WITH CHECK ADD CONSTRAINT [FK_CashReceipts_Invoices_InvoiceID] FOREIGN KEY(InvoiceID)
-- REFERENCES Sales.Invoices (InvoiceID)
-- ON DELETE NO ACTION
-- GO

-- ALTER TABLEFinance.CashReceipts WITH CHECK ADD CONSTRAINT [FK_CashReceipts_Customers_CustomerID] FOREIGN KEY(CustomerID)
-- REFERENCES Sales.Customers (CustomerID)
-- GO

CREATE TABLE Finance.LoanRepaymentSchedule
(
    ScheduledPaymentID int NOT NULL,
    LoanID INT NOT NULL,
    PaymentDueDate DATETIME2(2) NOT NULL,
    PaymentNumber INT CHECK (PaymentNumber >= 0) NOT NULL,
    PrincipalAmount DECIMAL(18,2) CHECK (PrincipalAmount >= 0) NOT NULL,
    InterestAmount DECIMAL(18,2) CHECK (InterestAmount >= 0) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL,
    PRIMARY KEY CLUSTERED (ScheduledPaymentID)
)
GO

CREATE INDEX idx_LoanPayments$LoanID 
  ON Finance.LoanRepaymentSchedule (LoanID)
GO

ALTER TABLE Finance.LoanRepaymentSchedule WITH CHECK ADD CONSTRAINT [FK_LoanPayments$LoadID_LoanAgreement$LoanID] FOREIGN KEY(LoanID)
REFERENCES Finance.LoanAgreement (LoadID)
ON DELETE NO ACTION
GO

CREATE TABLE Finance.StockSubscription
(
    StockID int NOT NULL,
    FinancierID INT NOT NULL,
    EmployeeID INT NOT NULL,
    SharesIssured INT CHECK (SharesIssured >= 0) NOT NULL,
    PricePerShare DECIMAL(18,2) CHECK (PricePerShare >= 0) NOT NULL,
    StockIssueDate DATETIME2(0) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL,
    PRIMARY KEY CLUSTERED (StockID)
)
GO

CREATE INDEX idx_StockSubscription$FinancierID 
  ON Finance.StockSubscription (FinancierID)
GO

CREATE INDEX idx_StockSubscription$EmployeeID 
  ON Finance.StockSubscription (EmployeeID)
GO

ALTER TABLE Finance.StockSubscription WITH CHECK ADD CONSTRAINT [FK_StockSubscription$EmployeeID_Employees$EmployeeID] FOREIGN KEY(EmployeeID)
REFERENCES HumanResources.Employees (EmployeeID)
ON DELETE NO ACTION
GO

ALTER TABLE Finance.StockSubscription WITH CHECK ADD CONSTRAINT [FK_StockSubscription$FinancierID_StockholderCreditor$FinancierID] FOREIGN KEY(FinancierID)
REFERENCES Finance.StockholderCreditor (FinancierID)
ON DELETE NO ACTION
GO

CREATE TABLE Finance.DividendPymtRate
(
    DividendID INT NOT NULL,
    StockID INT NOT NULL,
    DividendDeclarationDate DATETIME2(0) NOT NULL,
    DividendPerShare DECIMAL(18,2) CHECK (DividendPerShare >= 0) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL,
    PRIMARY KEY CLUSTERED (DividendID)
)
GO

CREATE INDEX idx_DividendPymtRate$StockID 
  ON Finance.DividendPymtRate (StockID)
GO

ALTER TABLE Finance.DividendPymtRate WITH CHECK ADD CONSTRAINT [FK_DividendPymtRate$StockID_StockSubscription$StockID] FOREIGN KEY(StockID)
REFERENCES Finance.StockSubscription (StockID)
ON DELETE NO ACTION
GO

CREATE TABLE Purchasing.Vendors
(
  VendorID int NOT NULL,
  VendorName nvarchar(25) NOT NULL,
  AddressLine1 nvarchar(50) NOT NULL,
  AddressLine2 nvarchar(50) NULL,
  City nvarchar(25) NOT NULL,
  [State] char(2) NOT NULL,
  ZipCode nvarchar(10) NOT NULL,
  Telephone nvarchar(14) NOT NULL,
  PrimaryContact varchar(25) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (VendorID)
)
GO 

CREATE UNIQUE INDEX idx_Vendors$VendorName 
  ON Purchasing.Vendors (VendorName)
GO

CREATE TABLE Purchasing.CompositionTypes
(
  CompositionTypeID int IDENTITY NOT NULL,
  CompositionTypeName nchar(1) NOT NULL,
  [Description] nvarchar(25) NOT NULL UNIQUE,
  PRIMARY KEY (CompositionTypeID)
);
GO

CREATE UNIQUE INDEX idx_CompositionTypes$CompositionTypeName 
  ON Purchasing.CompositionTypes (CompositionTypeName);
GO

CREATE TABLE Purchasing.InventoryTypes
(
  InventoryTypeID int IDENTITY NOT NULL,
  InventoryTypeName nchar(1) NOT NULL,
  [Description] nvarchar(25) NOT NULL,
  PRIMARY KEY (InventoryTypeID)
);
GO

CREATE UNIQUE INDEX idx_InventoryTypes$InventoryTypeName 
  ON Purchasing.InventoryTypes (InventoryTypeName);
GO

CREATE TABLE Purchasing.DiameterTypes
(
  DiameterTypeID int IDENTITY NOT NULL,
  DiameterTypeName nchar(3) NOT NULL,
  [Description] nvarchar(25) NOT NULL,
  PRIMARY KEY (DiameterTypeID)
);
GO

CREATE UNIQUE INDEX idx_DiameterTypes$DiameterTypeName 
  ON Purchasing.DiameterTypes (DiameterTypeName);
GO

CREATE TABLE Purchasing.Inventory
(
  InventoryID int NOT NULL,
  CompositionTypeID int NOT NULL,
  InventoryTypeID int NOT NULL,
  DiameterTypeID int NOT NULL,
  StandardCost DECIMAL(18,2) DEFAULT 0 NOT NULL,
  ListPrice decimal(18, 2) DEFAULT 0 NOT NULL,
  [Description] varchar(35) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (InventoryID)
)
GO

CREATE INDEX idx_Inventory$CompositionTypeID 
  ON Purchasing.Inventory (CompositionTypeID)
GO

CREATE INDEX idx_Inventory$InventoryTypeID 
  ON Purchasing.Inventory (InventoryTypeID)
GO

CREATE INDEX idx_Inventory$DiameterTypeID 
  ON Purchasing.Inventory (DiameterTypeID)
GO

ALTER TABLE Purchasing.Inventory  WITH CHECK ADD  CONSTRAINT [FK_Inventory$CompositionTypeID_CompositionType$CompositionTypeID] FOREIGN KEY(CompositionTypeID)
REFERENCES Purchasing.CompositionTypes (CompositionTypeID)
ON DELETE CASCADE
GO

ALTER TABLE Purchasing.Inventory  WITH CHECK ADD  CONSTRAINT [FK_Inventory$InventoryTypeID_InventoryTypes$InventoryTypeID] FOREIGN KEY(InventoryTypeID)
REFERENCES Purchasing.InventoryTypes (InventoryTypeID)
ON DELETE CASCADE
GO

ALTER TABLE Purchasing.Inventory  WITH CHECK ADD  CONSTRAINT [FK_Inventory$DiameterTypeID_DiameterTypes$DiameterTypeID] FOREIGN KEY(DiameterTypeID)
REFERENCES Purchasing.DiameterTypes (DiameterTypeID)
ON DELETE CASCADE
GO

CREATE TABLE Purchasing.PurchaseOrders
(
    PurchaseOrderID INT PRIMARY KEY CLUSTERED,
    VendorID INT NOT NULL REFERENCES Purchasing.Vendors(VendorID),
    EmployeeID INT NOT NULL REFERENCES HumanResources.Employees(EmployeeID),
    PurchaseOrderDate DATETIME2(0) NOT NULL,
    ExpectedDeliveryDate DATETIME2(2) NOT NULL,
    PurchaseOrderAmount DECIMAL(18,2) CHECK (PurchaseOrderAmount >= 0) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL   
)
GO

CREATE INDEX idx_PurchaseOrders$VendorID 
  ON Purchasing.PurchaseOrders (VendorID)
GO

CREATE INDEX idx_PurchaseOrders$EmployeeID 
  ON Purchasing.PurchaseOrders (EmployeeID)
GO

ALTER TABLE Purchasing.PurchaseOrders
    ADD CONSTRAINT CHK_PurchaseOrderDate$ExpectedDeliveryDate CHECK (ExpectedDeliveryDate >= PurchaseOrderDate)
GO

CREATE TABLE Purchasing.PurchaseOrderDetails
(
    PurchaseOrderDetailID INT PRIMARY KEY CLUSTERED,
    PurchaseOrderID INT NOT NULL REFERENCES Purchasing.PurchaseOrders(PurchaseOrderID),
    InventoryID INT NOT NULL REFERENCES Purchasing.Inventory(InventoryID),
    VendorPartNumber NVARCHAR(25) NOT NULL,
    QuantityOrdered INT CHECK (QuantityOrdered >= 0) NOT NULL,
    UnitCost DECIMAL(18,2) CHECK (UnitCost >= 0) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL     
)
GO

CREATE INDEX idx_PurchaseOrderDetails$PurchaseOrderID 
  ON Purchasing.PurchaseOrderDetails (PurchaseOrderID)
GO

CREATE INDEX idx_PurchaseOrderDetails$InventoryID 
  ON Purchasing.PurchaseOrderDetails (InventoryID)
GO

CREATE TABLE Purchasing.PurchaseType
(
    PurchaseTypeID INT PRIMARY KEY CLUSTERED,
    ResourceTypeName NVARCHAR(25) NOT NULL UNIQUE
)
GO

CREATE TABLE Purchasing.InventoryReceipts
(
    InventoryReceiptsID INT PRIMARY KEY CLUSTERED,
    PurchaseOrderID INT REFERENCES Purchasing.PurchaseOrders (PurchaseOrderID) NOT NULL,
    VendorID INT NOT NULL REFERENCES Purchasing.Vendors(VendorID),
    EmployeeID INT NOT NULL REFERENCES HumanResources.Employees(EmployeeID),
    PurchaseTypeID INT REFERENCES Purchasing.PurchaseType (PurchaseTypeID),
    InventoryReceiptDate DATETIME2(0) NOT NULL,
    InventoryReceiptAmount DECIMAL(18,2) CHECK (InventoryReceiptAmount >= 0) NOT NULL,
    VendorInvoiceNumber NVARCHAR(30) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL     
)
GO
      
CREATE INDEX idx_InventoryReceipts$PurchaseOrderID 
  ON Purchasing.InventoryReceipts (PurchaseOrderID)
GO

CREATE INDEX idx_InventoryReceipts$VendorID 
  ON Purchasing.InventoryReceipts (VendorID)
GO

CREATE INDEX idx_InventoryReceipts$EmployeeID 
  ON Purchasing.InventoryReceipts (EmployeeID)
GO

CREATE INDEX idx_InventoryReceipts$PurchaseTypeID 
  ON Purchasing.InventoryReceipts (PurchaseTypeID)
GO

CREATE TABLE Purchasing.InventoryReceiptDetails
(
    InventoryReceiptDetailID INT PRIMARY KEY CLUSTERED,
    InventoryReceiptsID INT NOT NULL REFERENCES Purchasing.InventoryReceipts (InventoryReceiptsID),
    InventoryID INT NOT NULL REFERENCES Purchasing.Inventory (InventoryID),
    QuantityReceived INT CHECK (QuantityReceived >= 0) NOT NULL,
    ReceivedPrice DECIMAL(18,2) CHECK (ReceivedPrice >= 0) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL    
)
GO

CREATE TABLE Purchasing.NonInventoryItems
(
    ResourceID INT PRIMARY KEY CLUSTERED,
    ResourceName NVARCHAR(30) NOT NULL UNIQUE,
    BalanceSheetName NVARCHAR(50) NULL,
    IncomeStmtName NVARCHAR(50) NULL,
    [Description] NVARCHAR(100) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL      
)
GO

CREATE TABLE Purchasing.NonInventoryItemDetails
(
    NonInventoryItemDetailID INT PRIMARY KEY CLUSTERED,
    InventoryReceiptsID INT REFERENCES Purchasing.InventoryReceipts (InventoryReceiptsID),
    ResourceID INT REFERENCES Purchasing.NonInventoryItems (ResourceID),
    ResourceDetailName NVARCHAR(50) NOT NULL,
    ResourceItemDesc NVARCHAR(256) NOT NULL,
    UsefulLife INT CHECK (UsefulLife >= 0) NOT NULL,
    QuantityPurchased INT CHECK (QuantityPurchased >= 0) NOT NULL,
    UnitCost DECIMAL(18,2) CHECK (UnitCost >= 0) NOT NULL,
    SalvageValue DECIMAL(18,2) CHECK (SalvageValue >= 0) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL     
)
GO

CREATE INDEX idx_NonInventoryItemDetails$ResourceID 
  ON Purchasing.NonInventoryItemDetails (ResourceID)
GO
 
CREATE INDEX idx_NonInventoryItemDetails$InventoryReceiptsID 
  ON Purchasing.NonInventoryItemDetails (InventoryReceiptsID)
GO

CREATE INDEX idx_InventoryReceiptDetails$InventoryID 
  ON Purchasing.InventoryReceiptDetails (InventoryID)
GO

CREATE INDEX idx_InventoryReceiptDetails$InventoryReceiptsID 
  ON Purchasing.InventoryReceiptDetails (InventoryReceiptsID)
GO

CREATE TABLE Sales.Customers
(
    CustomerID int NOT NULL,
    CustomerName nvarchar(25) NOT NULL,
    AddressLine1 nvarchar(50) NOT NULL,
    AddressLine2 nvarchar(50) NULL,
    City nvarchar(25) NOT NULL,
    [State] char(2) NOT NULL,
    ZipCode nvarchar(10) NOT NULL,
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
ON DELETE NO ACTION
GO

ALTER TABLE Sales.SalesOrders WITH CHECK ADD CONSTRAINT [FK_Employees_EmployeeID] FOREIGN KEY(EmployeeID)
REFERENCES HumanResources.Employees (EmployeeID)
ON DELETE NO ACTION
GO
    
CREATE TABLE Sales.SalesOrderDetails
(
  SalesOrderDetailID int NOT NULL,
  SalesOrderID int NOT NULL REFERENCES Sales.SalesOrders (SalesOrderID),
  InventoryID INT NOT NULL REFERENCES Purchasing.Inventory (InventoryID),
  QuantityOrdered int NOT NULL,
  UnitPrice decimal(18, 2) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  PRIMARY KEY CLUSTERED (SalesOrderDetailID)
)
GO

CREATE INDEX idx_SalesOrderDetails$SalesOrderID 
  ON Sales.SalesOrderDetails (SalesOrderID)
GO

CREATE INDEX idx_SalesOrderDetails$InventoryID 
  ON Sales.SalesOrderDetails (InventoryID)
GO

CREATE TABLE Sales.Invoices
(
  InvoiceID int PRIMARY KEY CLUSTERED,
  SalesOrderID int NOT NULL REFERENCES Sales.SalesOrders (SalesOrderID),
  CustomerID int NOT NULL REFERENCES Sales.Customers (CustomerID),
  EmployeeID int NOT NULL REFERENCES HumanResources.Employees (EmployeeID),
  ShippingDate datetime2(0) NOT NULL,
  SalesAmount decimal(18, 2) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
)
GO

CREATE UNIQUE INDEX idx_Invoices$SalesOrderID 
  ON Sales.Invoices (SalesOrderID)
GO

CREATE INDEX idx_Invoices$CustomerID 
  ON Sales.Invoices (CustomerID)
GO

CREATE INDEX idx_Invoices$EmployeeID 
  ON Sales.Invoices (EmployeeID)
GO

CREATE TABLE Sales.InvoiceDetails
(
  InvoiceDetailsID int PRIMARY KEY CLUSTERED,
  InvoiceID int NOT NULL REFERENCES Sales.Invoices (InvoiceID),
  InventoryID int NOT NULL REFERENCES Purchasing.Inventory (InventoryID),
  QuantityShipped int CHECK (QuantityShipped >= 0) NOT NULL,
  UnitPrice decimal(18, 2) CHECK (UnitPrice >= 0) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
)
GO

CREATE INDEX idx_InvoiceDetails$InvoiceID 
  ON Sales.InvoiceDetails (InvoiceID)
GO

CREATE INDEX idx_InvoiceDetails$InventoryID 
  ON Sales.InvoiceDetails (InventoryID)
GO

CREATE TABLE Purchasing.NonInventoryItems
(
    ResourceID INT PRIMARY KEY CLUSTERED,
    ResourceName NVARCHAR(30) NOT NULL UNIQUE,
    BalanceSheetName NVARCHAR(50) NULL,
    IncomeStmtName NVARCHAR(50) NULL,
    [Description] NVARCHAR(100) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL      
)
GO








