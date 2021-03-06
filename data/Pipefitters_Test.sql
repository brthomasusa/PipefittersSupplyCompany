-- Database creation and seed script for the 'Pipefitters_Test' database.
-- Create the database and run this script from within. The test in the
-- PipefittersSupplyCompany.IntegrationTests folder reseed the database
-- Before each test run. There are two appsettings.json file: one in the
-- root of WebApi project and the other in root of the Integration test
-- project. The connection string info in those files need to be updated.

-- DBCC CHECKIDENT ('Shared.EconomicEventTypes', RESEED, 0);

-- USE master
-- DROP DATABASE Pipefitters_Test
-- DROP DATABASE Pipefitters_v3
-- GO

CREATE DATABASE Pipefitters_Test
GO


USE Pipefitters_Test
GO

CREATE SCHEMA Sales
GO
CREATE SCHEMA Purchasing
GO
CREATE SCHEMA HumanResources
GO
CREATE SCHEMA Finance
GO
CREATE SCHEMA Shared
GO

CREATE TABLE Shared.EconomicEventTypes
(
    EventTypeId int IDENTITY PRIMARY KEY CLUSTERED,
    EventTypeName NVARCHAR(50) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL     
)

CREATE UNIQUE INDEX idx_EconomicEventTypes$EventTypeName   
   ON Shared.EconomicEventTypes (EventTypeName)   
GO

INSERT INTO Shared.EconomicEventTypes
    (EventTypeName)
VALUES
    ('Cash Receipt from Sales'),
    ('Cash Receipt from Loan Agreement'),
    ('Cash Receipt from Stock Subscription'),     
    ('Cash Disbursement for Loan Payment'),
    ('Cash Disbursement for Divident Payment'),
    ('Cash Disbursement for TimeCard Payment'),
    ('Cash Disbursement for Inventory Receipt')   
GO


CREATE TABLE Shared.ExternalAgentTypes
(
    AgentTypeId int IDENTITY PRIMARY KEY CLUSTERED,
    AgentTypeName NVARCHAR(50) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL     
)

CREATE UNIQUE INDEX idx_ExternalAgentTypes$AgentTypeName   
   ON Shared.ExternalAgentTypes (AgentTypeName)   
GO

INSERT INTO Shared.ExternalAgentTypes
    (AgentTypeName)
VALUES
    ('Customer'),
    ('Creditor'),
    ('Stockholder'),
    ('Vendor'),
    ('Employee'),
    ('Financier')
GO

CREATE TABLE Shared.ExternalAgents
(
    AgentId UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    AgentTypeId int NOT NULL REFERENCES Shared.ExternalAgentTypes (AgentTypeId),
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL     
)
GO

CREATE INDEX idx_ExternalAgentTypes$AgentTypeId   
   ON Shared.ExternalAgentTypes (AgentTypeId)   
GO

INSERT INTO Shared.ExternalAgents
    (AgentId, AgentTypeId)
VALUES
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', 5),
    ('5C60F693-BEF5-E011-A485-80EE7300C695', 5),
    ('660bb318-649e-470d-9d2b-693bfb0b2744', 5),
    ('9f7b902d-566c-4db6-b07b-716dd4e04340', 5),
    ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', 5),
    ('0cf9de54-c2ca-417e-827c-a5b87be2d788', 5),
    ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', 5),
    ('604536a1-e734-49c4-96b3-9dfef7417f9a', 5),
    ('e6b86ea3-6479-48a2-b8d4-54bd6cbbdbc5', 5)
GO

CREATE TABLE Shared.EconomicEvents
(
    EventId UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    EventTypeId int NOT NULL REFERENCES Shared.EconomicEventTypes (EventTypeId),
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL     
)
GO
CREATE INDEX idx_EconomicEvents$EventTypeId   
   ON Shared.EconomicEvents (EventTypeId)   
GO

CREATE TABLE HumanResources.Employees
(
  EmployeeId UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
  SupervisorId UNIQUEIDENTIFIER NOT NULL REFERENCES HumanResources.Employees (EmployeeID),
  LastName nvarchar(25) NOT NULL,
  FirstName nvarchar(25) NOT NULL,
  MiddleInitial nchar(1) NULL,
  SSN nchar(9) NOT NULL UNIQUE,
  Telephone nvarchar(14) NOT NULL,
  AddressLine1 nvarchar(30) NOT NULL,
  AddressLine2 nvarchar(30) NULL,
  City nvarchar(30) NOT NULL,
  StateCode nchar(2) NOT NULL,
  Zipcode nvarchar(10) NOT NULL,  
  MaritalStatus nchar(1) CHECK (MaritalStatus IN ('M', 'S')) DEFAULT 'S' NOT NULL,
  Exemptions int DEFAULT 0 NOT NULL,
  PayRate decimal(18, 2) CHECK (PayRate >= 7.50 AND PayRate <= 40.00) NOT NULL,
  StartDate datetime2(0) NOT NULL,
  IsActive BIT DEFAULT 1 NOT NULL,
  IsSupervisor BIT DEFAULT 0 NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
);
GO

CREATE INDEX idx_Employees$SupervisorId 
  ON HumanResources.Employees (SupervisorId)
GO
CREATE UNIQUE INDEX idx_Employees$LastNameFirstNameMi   
   ON HumanResources.Employees (LastName,FirstName,MiddleInitial)   
GO
CREATE UNIQUE INDEX idx_Employees$SSN   
   ON HumanResources.Employees (SSN)   
GO

ALTER TABLE HumanResources.Employees WITH CHECK ADD CONSTRAINT [FK_Employees$EmployeeId_ExternalAgents$AgentId] 
    FOREIGN KEY(EmployeeId)
    REFERENCES Shared.ExternalAgents (AgentId)
    ON DELETE NO ACTION
GO

INSERT INTO HumanResources.Employees
    (EmployeeId, SupervisorID, LastName, FirstName, MiddleInitial, SSN, Telephone, AddressLine1, AddressLine2, City, StateCode, Zipcode, MaritalStatus, Exemptions, PayRate, StartDate, IsActive, IsSupervisor)
VALUES
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', '4B900A74-E2D9-4837-B9A4-9E828752716E','Sanchez', 'Ken', 'J', '123789999', '817-987-1234', '321 Tarrant Pl', null, 'Fort Worth', 'TX', '78965', 'M', 5, 40.00, '1998-12-02', 1, 1),
    ('5C60F693-BEF5-E011-A485-80EE7300C695', 'e716ac28-e354-4d8d-94e4-ec51f08b1af8','Carter', 'Wayne', 'L', '423789999', '972-523-1234', '321 Fort Worth Ave', null, 'Dallas', 'TX', '75211', 'M', 3, 40.00, '1998-12-02', 1, 0),
    ('660bb318-649e-470d-9d2b-693bfb0b2744', '4B900A74-E2D9-4837-B9A4-9E828752716E','Phide', 'Terri', 'M', '638912345', '214-987-1234', '3455 South Corinth Circle', null, 'Dallas', 'TX', '75224', 'M', 1, 28.00, '2014-09-22', 1, 1),
    ('9f7b902d-566c-4db6-b07b-716dd4e04340', '4B900A74-E2D9-4837-B9A4-9E828752716E','Duffy', 'Terri', 'L', '699912345', '214-987-1234', '98 Reiger Ave', null, 'Dallas', 'TX', '75214', 'M', 2, 30.00, '2018-10-22', 1, 0),
    ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', '4B900A74-E2D9-4837-B9A4-9E828752716E','Goldberg', 'Jozef', 'P', '036889999', '469-321-1234', '6667 Melody Lane', 'Apt 2', 'Dallas', 'TX', '75231', 'S', 1, 29.00, '2013-02-28', 1, 0),
    ('0cf9de54-c2ca-417e-827c-a5b87be2d788', '4B900A74-E2D9-4837-B9A4-9E828752716E','Brown', 'Jamie', 'J', '123700009', '817-555-5555', '98777 Nigeria Town Rd', null, 'Arlington', 'TX', '78658', 'M', 2, 29.00, '2017-12-22', 1, 0),
    ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', '4B900A74-E2D9-4837-B9A4-9E828752716E','Bush', 'George', 'W', '325559874', '214-555-5555', '777 Ervay Street', null, 'Dallas', 'TX', '75208', 'M', 5, 30.00, '2016-10-19', 1, 1),
    ('604536a1-e734-49c4-96b3-9dfef7417f9a', '660bb318-649e-470d-9d2b-693bfb0b2744','Rainey', 'Ma', 'A', '775559874', '903-555-5555', '1233 Back Alley Rd', null, 'Corsicana', 'TX', '75110', 'M', 2, 27.25, '2018-01-05', 1, 0),
    ('e6b86ea3-6479-48a2-b8d4-54bd6cbbdbc5', '4B900A74-E2D9-4837-B9A4-9E828752716E','Beck', 'Jeffery', 'W', '825559874', '214-555-5555', '321 Fort Worth Ave', null, 'Dallas', 'TX', '75211', 'M', 5, 30.00, '2016-10-19', 1, 0)
GO

CREATE TABLE Shared.DomainUsers
(
    UserId UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),    
    UserName NVARCHAR(256) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL    
)
GO

CREATE UNIQUE INDEX idx_DomainUsers$UserName   
   ON Shared.DomainUsers (UserName)   
GO
CREATE UNIQUE INDEX idx_DomainUsers$Email   
   ON Shared.DomainUsers (Email)   
GO

ALTER TABLE Shared.DomainUsers WITH CHECK ADD CONSTRAINT [FK_DomainUser$UserId_ExternalAgents$AgentId] 
    FOREIGN KEY(UserId)
    REFERENCES Shared.ExternalAgents (AgentId)
    ON DELETE NO ACTION
GO

INSERT INTO Shared.DomainUsers
    (UserId, UserName, Email)
VALUES
    ('660bb318-649e-470d-9d2b-693bfb0b2744', 'tphide', 'terri.phide@pipefitterssupplycompany.com')
GO

CREATE TABLE Finance.Financiers
(
  FinancierID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
  FinancierName nvarchar(50) NOT NULL,
  Telephone nvarchar(14) NOT NULL,
  AddressLine1 nvarchar(30) NOT NULL,
  AddressLine2 nvarchar(30) NULL,
  City nvarchar(30) NOT NULL,
  StateCode nchar(2) NOT NULL,
  Zipcode nvarchar(10) NOT NULL,   
  ContactLastName nvarchar(25) NOT NULL,
  ContactFirstName nvarchar(25) NOT NULL,
  ContactMiddleInitial nchar(1) NULL,
  ContactTelephone nvarchar(14) NOT NULL,  
  IsActive BIT DEFAULT 0 NOT NULL,
  UserId UNIQUEIDENTIFIER not null REFERENCES Shared.DomainUsers (UserId),
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL,
  CONSTRAINT idx_FinancierName UNIQUE(FinancierName)
)
GO

ALTER TABLE Finance.Financiers WITH CHECK ADD CONSTRAINT [FK_Financiers$FinancierId_ExternalAgents$AgentId] 
    FOREIGN KEY(FinancierId)
    REFERENCES Shared.ExternalAgents (AgentId)
    ON DELETE NO ACTION
GO

CREATE INDEX idx_Financier$UserId   
   ON Finance.Financiers (UserId)   
GO

CREATE UNIQUE INDEX idx_Financier$FinancierName   
   ON Finance.Financiers (FinancierName)   
GO

INSERT INTO Shared.ExternalAgents
    (AgentId, AgentTypeId)
VALUES
    ('12998229-7ede-4834-825a-0c55bde75695', 6),
    ('94b1d516-a1c3-4df8-ae85-be1f34966601', 6),
    ('bf19cf34-f6ba-4fb2-b70e-ab19d3371886', 6),
    ('b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 6),
    ('01da50f9-021b-4d03-853a-3fd2c95e207d', 6),
    ('84164388-28ff-4b47-bd63-dd9326d32236', 6)
    
GO

INSERT INTO Finance.Financiers
    (FinancierID, FinancierName, Telephone, AddressLine1, AddressLine2, City, StateCode, Zipcode, ContactLastName, ContactFirstName, ContactMiddleInitial, ContactTelephone, IsActive, UserId)
VALUES
    ('12998229-7ede-4834-825a-0c55bde75695', 'Arturo Sandoval', '888-719-8128', '5232 Outriggers Way', 'Ste 401', 'Oxnard', 'CA', '93035', 'Sandoval', 'Arturo', 'T', '888-719-8128', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('94b1d516-a1c3-4df8-ae85-be1f34966601', 'Paul Van Horn Enterprises', '415-328-9870', '825 Mandalay Beach Rd', 'Level 2', 'Oxnard', 'CA', '94402', 'Crocker', 'Patrick', 'T', '415-328-9870', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('bf19cf34-f6ba-4fb2-b70e-ab19d3371886', 'New World Tatoo Parlor', '630-321-9875', '1690 S. El Camino Real', 'Room 2C', 'San Mateo', 'CA', '75224', 'Jozef Jr.', 'JoJo', 'D', '630-321-9875', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 'Bertha Mae Jones Innovative Financing', '886-587-0001', '12333 Menard Heights Blvd', 'Ste 1001', 'Palo Alto', 'CA', '94901', 'Sinosky', 'Betty', 'L', '886-587-0001', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('01da50f9-021b-4d03-853a-3fd2c95e207d', 'Pimps-R-US Financial Management, Inc.', '415-912-5570', '96541 Sunset Rise Plaza', 'Ste 2', 'Oxnard', 'CA', '93035', 'Daniels', 'Javier', 'A', '888-719-8100', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('84164388-28ff-4b47-bd63-dd9326d32236', 'I Exist-Only-To-Be-Deleted', '415-912-5570', '985211 Highway 78 East', null, 'Oxnard', 'CA', '93035', 'Gutierrez', 'Monica', 'T', '415-912-5570', 1, '660bb318-649e-470d-9d2b-693bfb0b2744')
GO
   
-- STOPPED HERE
-- STOPPED HERE
-- STOPPED HERE

CREATE TABLE Finance.LoanAgreements
(
    LoanId uniqueidentifier NOT NULL PRIMARY KEY default NEWID(),
    FinancierId  uniqueidentifier NOT NULL REFERENCES Finance.Financiers (FinancierId),    
    LoanAmount DECIMAL(18,2) CHECK(LoanAmount > 0) NOT NULL,
    InterestRate NUMERIC(5,4) CHECK(InterestRate >= 0) NOT NULL,        
    LoanDate DATETIME2(0) NOT NULL,
    MaturityDate DATETIME2(0) NOT NULL,
    PymtsPerYear INT CHECK(PymtsPerYear > 0) NOT NULL,
    UserId  [uniqueidentifier] NOT NULL REFERENCES HumanResources.Users (UserId),
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL,
    CONSTRAINT CHK_LoanDateMaturityDate CHECK (LoanDate <= MaturityDate)
)
GO

CREATE INDEX idx_LoanAgreement$FinancierID 
  ON Finance.LoanAgreements (FinancierId);
GO

CREATE INDEX idx_LoanAgreement$UserID 
  ON Finance.LoanAgreements (UserId);
GO

CREATE UNIQUE INDEX idx_LoanAgreement$LoanId_FinancierId 
  ON Finance.LoanAgreements (LoanId, FinancierId);
GO

ALTER TABLE Finance.LoanAgreements WITH CHECK ADD CONSTRAINT [FK_LoanAgreements$LoanId_EconomicEvents$EventId] 
    FOREIGN KEY(LoanId)
    REFERENCES Shared.EconomicEvents (EventId)
    ON DELETE NO ACTION
GO

CREATE TABLE Finance.StockSubscriptions
(
    StockId uniqueidentifier NOT NULL PRIMARY KEY default NEWID(),
    FinancierId  uniqueidentifier NOT NULL REFERENCES Finance.Financiers (FinancierId),    
    SharesIssured INT CHECK (SharesIssured >= 0) NOT NULL,
    PricePerShare DECIMAL(18,2) CHECK (PricePerShare >= 0) NOT NULL,
    StockIssueDate DATETIME2(0) NOT NULL,
    UserId  [uniqueidentifier] NOT NULL REFERENCES HumanResources.Users (UserId),
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL
)
GO

CREATE INDEX idx_StockSubscription$FinancierId 
  ON Finance.StockSubscriptions (FinancierId)
GO

CREATE INDEX idx_StockSubscription$UserId 
  ON Finance.StockSubscriptions (UserId)
GO

CREATE UNIQUE INDEX idx_StockSubscription$StockId_FinancierId 
  ON Finance.StockSubscriptions (StockId, FinancierId)
GO

ALTER TABLE Finance.StockSubscriptions WITH CHECK ADD CONSTRAINT [FK_StockSubscriptions$StockId_EconomicEvents$EventId] 
    FOREIGN KEY(StockId)
    REFERENCES Shared.EconomicEvents (EventId)
    ON DELETE NO ACTION
GO

CREATE TABLE Finance.DividendPymtRates
(
    DividendId uniqueidentifier NOT NULL PRIMARY KEY default NEWID(),
    StockId uniqueidentifier NOT NULL REFERENCES Finance.StockSubscriptions (StockId),
    DividendDeclarationDate DATETIME2(0) NOT NULL,
    DividendPerShare DECIMAL(18,2) CHECK (DividendPerShare >= 0) NOT NULL,
    UserId  [uniqueidentifier] NOT NULL REFERENCES HumanResources.Users (UserId),
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL
)
GO

CREATE INDEX idx_DividendPymtRate$StockId 
  ON Finance.DividendPymtRates (StockId)
GO

CREATE UNIQUE INDEX idx_DividendPymtRate$StockId_DividendDeclarationDate_DividendPerShare 
  ON Finance.DividendPymtRates (StockId,DividendDeclarationDate,DividendPerShare)
GO

ALTER TABLE Finance.DividendPymtRates WITH CHECK ADD CONSTRAINT [FK_DividendPymtRates$DividendId_EconomicEvents$EventId] 
    FOREIGN KEY(DividendId)
    REFERENCES Shared.EconomicEvents (EventId)
    ON DELETE NO ACTION
GO



INSERT INTO Shared.EconomicEvents
    (EventId, EventTypeId)
VALUES
    ('41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 2),
    ('09b53ffb-9983-4cde-b1d6-8a49e785177f', 2),
    ('1511c20b-6df0-4313-98a5-7c3561757dc2', 2),    
    ('0a7181c0-3ce9-4981-9559-157fd8e09cfb', 2),    
    ('6d663bb9-763c-4797-91ea-b2d9b7a19ba4', 3),
    ('62d6e2e6-215d-4157-b7ec-1ba9b137c770', 3),
    ('fb39b013-1633-4479-8186-9f9b240b5727', 3),
    ('6632cec7-29c5-4ec3-a5a9-c82bf8f5eae3', 3),
    ('264632b4-20bd-473f-9a9b-dd6f3b6ddbac', 3),    
    ('5997f125-bfca-4540-a144-01e444f6dc25', 3),    
    ('93adf7e5-bf6c-4ec8-881a-bfdf37aaf12e', 4),
    ('f479f59a-5001-47af-9d6c-2eae07077490', 4),
    ('76e6164a-249d-47a2-b47c-f09a332181b6', 4),
    ('caaa8b0c-bd5e-4b74-abe7-437a6e1cde15', 4),
    ('94cf5110-435a-4c30-b9a7-1a0a334528da', 4),
    ('e4860060-778b-4b70-9f92-3b1af108a58d', 4),
    ('710cbc7d-be46-4822-aea6-a5c89213efa3', 4),
    ('769a7c0d-0005-445e-b110-cbfb2321f40e', 4),
    ('43e96119-c4c8-4fe9-a568-4dd3dc569501', 4),
    ('0e04afc1-b006-4ef5-8265-ce24e456c0f8', 4),
    ('b71d5303-6035-4e96-9915-41c3724de721', 4),
    ('cf4279a1-da26-4d10-bff0-cf6e0933661c', 4),
    ('8e804651-5021-4577-bbda-e7ee45a74e44', 4),
    ('97fa51e0-e02a-46c1-9f09-73f72a5519c9', 4),
    ('e4ca6c30-6fd7-44ea-89b5-e11ecfc5989b', 4),
    ('b5c98492-2155-404e-b020-0b8c1481ec73', 4),
    ('eda455e3-1cc9-4d23-8434-37b9da13c71f', 4),
    ('839b2060-3ea5-4f5c-b313-f7a17a0cc0ec', 4),
    ('083082b0-9332-4cae-8522-5af12f3c618d', 4),
    ('08ae781a-27c4-4d43-9c55-d96a956f3418', 4),
    ('22e0ccd6-9308-4c59-a5e8-2d65c40e1974', 4),
    ('4110e43c-b8ca-4ee1-85cc-46ef54d98893', 4),
    ('e673b4ef-1c5c-4a6e-8e4e-253c61c9c85c', 4),
    ('12ad37a2-8bb1-4b10-85d9-5eb9cee15ccc', 4),
    ('2205ebde-58dc-4af7-958b-9124d9645b38', 4),
    ('e82648a3-8744-424f-badd-5a19a979574a', 4),
    ('0801632b-55d5-48fb-99d8-05e6fba1fcaf', 4),
    ('89eb8ba8-5dbb-42b5-8fd5-b733986ea10c', 4),
    ('1fe9c955-b05e-42aa-8770-d36593689790', 4),
    ('7711b4bc-5a44-4c68-8457-f85783f7f57e', 4),
    ('6a61a2eb-08b4-4baf-a6e6-a518b6d3de80', 4),
    ('992c8ad6-6858-44fa-9c97-343ea578f640', 4),
    ('b4a74b84-00cc-4d89-8669-25436309becb', 4),
    ('5696f5fa-3c7a-4401-97aa-bb2bdf425596', 4),
    ('b14fa6a6-740a-437b-9bac-55dd6e7824de', 4),
    ('409e60dc-bbe6-4ca9-95c2-ebf6886e8c4c', 4),
    ('e2a14f74-5261-46a8-bc41-952933925e1d' ,5),
	('ace8a4cd-5f4b-413c-a3b8-4643e9dd0f97' ,5),
	('08117078-5763-46d9-b771-799355d02fa1' ,5),
	('0421396b-4499-4a93-95c2-8f0dba43a25a' ,5),
	('a93ee592-3025-4302-97b3-7a237b0fe6a6' ,5),
	('95949c0f-f68e-4a14-9eaf-c62b4d689048' ,5),
	('e436c1f8-a1eb-4233-a8de-e8dab1812649' ,5),
	('1c2b45e2-f55f-4b93-8cdc-7cb1272210de' ,5),
	('1a14139d-b16f-4aa9-9274-b98a32ac0ebd' ,5),
	('c84dad2c-4aba-4630-8201-6da56743e19e' ,5),
	('b6f60d19-7d05-4625-be94-24e3079ef44f' ,5),
	('6f0c811c-b948-4c03-a6df-49c768dadc49' ,5),
	('aeac44d8-feb3-4823-8b0a-ca79da5bb089' ,5),
	('6f09cd69-4a95-42c7-bcc2-acacbb92270c' ,5),
	('24d6936a-beb5-451b-a950-0f30e3ad463d' ,5)
GO

INSERT INTO Finance.LoanAgreements
    (LoanId, FinancierId, LoanAmount, InterestRate, LoanDate, MaturityDate, PymtsPerYear, UserId)
VALUES
    ('41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', '12998229-7ede-4834-825a-0c55bde75695', 50000.00, 0.08625, '2020-12-02', '2021-11-02', 12, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('09b53ffb-9983-4cde-b1d6-8a49e785177f', '94b1d516-a1c3-4df8-ae85-be1f34966601', 50000.00, 0.08625, '2021-04-02', '2022-03-02', 12, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('1511c20b-6df0-4313-98a5-7c3561757dc2', 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 100000.00, 0.07250, '2021-09-15', '2022-08-15', 12, '4b900a74-e2d9-4837-b9a4-9e828752716e'),
    ('0a7181c0-3ce9-4981-9559-157fd8e09cfb', 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 33000.00, 0.07250, '2021-11-15', '2022-10-15', 12, '4b900a74-e2d9-4837-b9a4-9e828752716e')
GO

INSERT INTO Finance.StockSubscriptions
    (StockId, FinancierId, SharesIssured, PricePerShare, StockIssueDate, UserId)
VALUES
    ('6d663bb9-763c-4797-91ea-b2d9b7a19ba4', '01da50f9-021b-4d03-853a-3fd2c95e207d', 50000, 1.00, '2020-09-03','4b900a74-e2d9-4837-b9a4-9e828752716e'),
    ('62d6e2e6-215d-4157-b7ec-1ba9b137c770', 'bf19cf34-f6ba-4fb2-b70e-ab19d3371886', 50000, 1.00, '2020-09-03','4b900a74-e2d9-4837-b9a4-9e828752716e'),
    ('fb39b013-1633-4479-8186-9f9b240b5727', 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 25000, 1.00, '2020-11-01','660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('6632cec7-29c5-4ec3-a5a9-c82bf8f5eae3', '01da50f9-021b-4d03-853a-3fd2c95e207d', 10000, 1.00, '2020-11-01','660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('264632b4-20bd-473f-9a9b-dd6f3b6ddbac', '12998229-7ede-4834-825a-0c55bde75695', 35000, 3.00, '2021-03-01','660bb318-649e-470d-9d2b-693bfb0b2744'),
    
    ('5997f125-bfca-4540-a144-01e444f6dc25', '12998229-7ede-4834-825a-0c55bde75695', 12567, 0.13, '2021-11-06','660bb318-649e-470d-9d2b-693bfb0b2744')
GO

INSERT INTO Finance.DividendPymtRates
    (DividendId, StockId, DividendDeclarationDate, DividendPerShare, UserId)
VALUES
    ('e2a14f74-5261-46a8-bc41-952933925e1d', '6d663bb9-763c-4797-91ea-b2d9b7a19ba4', '2021-01-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('ace8a4cd-5f4b-413c-a3b8-4643e9dd0f97', '6d663bb9-763c-4797-91ea-b2d9b7a19ba4', '2021-02-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('08117078-5763-46d9-b771-799355d02fa1', '6d663bb9-763c-4797-91ea-b2d9b7a19ba4', '2021-03-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),    
    ('0421396b-4499-4a93-95c2-8f0dba43a25a', '62d6e2e6-215d-4157-b7ec-1ba9b137c770', '2021-01-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('a93ee592-3025-4302-97b3-7a237b0fe6a6', '62d6e2e6-215d-4157-b7ec-1ba9b137c770', '2021-02-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('95949c0f-f68e-4a14-9eaf-c62b4d689048', '62d6e2e6-215d-4157-b7ec-1ba9b137c770', '2021-03-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),    
    ('e436c1f8-a1eb-4233-a8de-e8dab1812649', 'fb39b013-1633-4479-8186-9f9b240b5727', '2021-01-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('1c2b45e2-f55f-4b93-8cdc-7cb1272210de', 'fb39b013-1633-4479-8186-9f9b240b5727', '2021-02-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('1a14139d-b16f-4aa9-9274-b98a32ac0ebd', 'fb39b013-1633-4479-8186-9f9b240b5727', '2021-03-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),    
    ('c84dad2c-4aba-4630-8201-6da56743e19e', '6632cec7-29c5-4ec3-a5a9-c82bf8f5eae3', '2021-01-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('b6f60d19-7d05-4625-be94-24e3079ef44f', '6632cec7-29c5-4ec3-a5a9-c82bf8f5eae3', '2021-02-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('6f0c811c-b948-4c03-a6df-49c768dadc49', '6632cec7-29c5-4ec3-a5a9-c82bf8f5eae3', '2021-03-15', 0.05, '660bb318-649e-470d-9d2b-693bfb0b2744'),    
    ('aeac44d8-feb3-4823-8b0a-ca79da5bb089', '264632b4-20bd-473f-9a9b-dd6f3b6ddbac', '2021-01-15', 0.07, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('6f09cd69-4a95-42c7-bcc2-acacbb92270c', '264632b4-20bd-473f-9a9b-dd6f3b6ddbac', '2021-02-15', 0.07, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('24d6936a-beb5-451b-a950-0f30e3ad463d', '264632b4-20bd-473f-9a9b-dd6f3b6ddbac', '2021-03-15', 0.07, '660bb318-649e-470d-9d2b-693bfb0b2744')
GO 

CREATE TABLE Finance.CashAccounts
(
    CashAccountId uniqueidentifier NOT NULL PRIMARY KEY default NEWID(),
    BankName  NVARCHAR(50) NOT NULL,    
    AccountName NVARCHAR(50) NOT NULL,
    AccountNumber NVARCHAR(50) NOT NULL,
    RoutingTransitNumber NCHAR(9) NOT NULL,
    DateOpened DATETIME2(0) NOT NULL,    
    UserId  [uniqueidentifier] NOT NULL REFERENCES HumanResources.Users (UserId),
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL
)
GO

CREATE UNIQUE INDEX idx_CashAccounts$AccountNumber 
  ON Finance.CashAccounts (AccountNumber)
GO

CREATE INDEX idx_CashAccounts$UserId 
  ON Finance.CashAccounts (UserId)
GO

INSERT INTO Finance.CashAccounts
    (CashAccountId, BankName, AccountName, AccountNumber, RoutingTransitNumber, DateOpened, UserId)
VALUES
    ('417f8a5f-60e7-411a-8e87-dfab0ae62589', 'First Bank and Trust', 'Primary Checking', '36547-9871222', '703452098', '2020-09-03', '4b900a74-e2d9-4837-b9a4-9e828752716e'),
    ('c98ac84f-00bb-463d-9116-5828b2e9f718', 'First Bank and Trust', 'Payroll', '36547-9098812', '703452098', '2020-09-03', '4b900a74-e2d9-4837-b9a4-9e828752716e'),
    ('6a7ed605-c02c-4ec8-89c4-eac6306c885e', 'First Bank and Trust', 'Financing Proceeds', '36547-9888249', '703452098', '2020-09-03', '4b900a74-e2d9-4837-b9a4-9e828752716e'),
    ('765ec2b0-406a-4e42-b831-c9aa63800e76', 'BackAlley Money Washing, LLC', 'Slush Fund', 'XXXXX-XXXXXXX', '703452098', '2020-09-03', '4b900a74-e2d9-4837-b9a4-9e828752716e')
GO

CREATE TABLE Finance.LoanPaymentSchedules
(
    LoanPaymentId uniqueidentifier NOT NULL PRIMARY KEY default NEWID(),
    LoanId uniqueidentifier NOT NULL REFERENCES Finance.LoanAgreements(LoanId),
    PaymentNumber INT CHECK (PaymentNumber >= 1) NOT NULL,
    PaymentDueDate DATETIME2(0) NOT NULL,
    PrincipalAmount DECIMAL(18,2) NOT NULL,
    InterestAmount DECIMAL(18,2) NOT NULL,
    PrincipalRemaining DECIMAL(18,2) NOT NULL,   
    UserId uniqueidentifier NOT NULL REFERENCES HumanResources.Users (UserId),
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL
)
GO

CREATE INDEX idx_LoanPaymentSchedules$LoanId 
  ON Finance.LoanPaymentSchedules (LoanId)
GO
CREATE INDEX idx_LoanPaymentSchedules$UserId
  ON Finance.LoanPaymentSchedules (UserId)
GO
CREATE UNIQUE INDEX iidx_LoanPaymentSchedules$LoanId_PaymentNumber 
  ON Finance.LoanPaymentSchedules (LoanId, PaymentNumber)
GO

ALTER TABLE Finance.LoanPaymentSchedules WITH CHECK ADD CONSTRAINT [FK_LoanPymtSched$LoanPaymentId_EconomicEvents$EventId] 
    FOREIGN KEY(LoanPaymentId)
    REFERENCES Shared.EconomicEvents (EventId)
    ON DELETE NO ACTION
GO

INSERT INTO Finance.LoanPaymentSchedules
	(LoanPaymentId, LoanId, PaymentNumber, PaymentDueDate, InterestAmount, PrincipalAmount, PrincipalRemaining, UserId)
VALUES
	('93adf7e5-bf6c-4ec8-881a-bfdf37aaf12e', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 1, '2021-01-02', 359.38, 4004.51, 45995.49, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('f479f59a-5001-47af-9d6c-2eae07077490', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 2, '2021-02-02', 330.59, 4033.30, 41962.19, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('76e6164a-249d-47a2-b47c-f09a332181b6', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 3, '2021-03-02', 301.60, 4062.29, 37899.90, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('caaa8b0c-bd5e-4b74-abe7-437a6e1cde15', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 4, '2021-04-02', 272.41, 4091.48, 33808.42, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('94cf5110-435a-4c30-b9a7-1a0a334528da', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 5, '2021-05-02', 243.00, 4120.89, 29687.53, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('e4860060-778b-4b70-9f92-3b1af108a58d', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 6, '2021-06-02', 213.38, 4150.51, 25537.02, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('710cbc7d-be46-4822-aea6-a5c89213efa3', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 7, '2021-07-02', 183.55, 4180.34, 21356.68, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('769a7c0d-0005-445e-b110-cbfb2321f40e', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 8, '2021-08-02', 153.50, 4210.39, 17146.29, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('43e96119-c4c8-4fe9-a568-4dd3dc569501', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 9, '2021-09-02', 123.24, 4240.65, 12905.64, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('0e04afc1-b006-4ef5-8265-ce24e456c0f8', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 10, '2021-10-02', 92.76, 4271.13, 8634.51, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('b71d5303-6035-4e96-9915-41c3724de721', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 11, '2021-11-02', 62.06, 4301.83, 4332.68, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('cf4279a1-da26-4d10-bff0-cf6e0933661c', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 12, '2021-12-02', 31.14, 4332.68, 0, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('8e804651-5021-4577-bbda-e7ee45a74e44', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 1, '2021-05-02', 359.38, 4004.51, 45995.49, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('97fa51e0-e02a-46c1-9f09-73f72a5519c9', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 2, '2021-06-02', 330.59, 4033.30, 41962.19, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('e4ca6c30-6fd7-44ea-89b5-e11ecfc5989b', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 3, '2021-07-02', 301.60, 4062.29, 37899.90, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('b5c98492-2155-404e-b020-0b8c1481ec73', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 4, '2021-08-02', 272.41, 4091.48, 33808.42, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('eda455e3-1cc9-4d23-8434-37b9da13c71f', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 5, '2021-09-02', 243.00, 4120.89, 29687.53, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('839b2060-3ea5-4f5c-b313-f7a17a0cc0ec', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 6, '2021-10-02', 213.38, 4150.51, 25537.02, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('083082b0-9332-4cae-8522-5af12f3c618d', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 7, '2021-11-02', 183.55, 4180.34, 21356.68, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('08ae781a-27c4-4d43-9c55-d96a956f3418', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 8, '2021-12-02', 153.50, 4210.39, 12905.64, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('22e0ccd6-9308-4c59-a5e8-2d65c40e1974', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 9, '2022-01-02', 123.24, 4240.65, 12905.64, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('4110e43c-b8ca-4ee1-85cc-46ef54d98893', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 10, '2022-02-02', 92.76, 4271.13, 8634.51, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('e673b4ef-1c5c-4a6e-8e4e-253c61c9c85c', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 11, '2022-03-02', 62.06, 4301.83, 4332.68, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('12ad37a2-8bb1-4b10-85d9-5eb9cee15ccc', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 12, '2022-04-02', 31.14, 4332.68, 0, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('2205ebde-58dc-4af7-958b-9124d9645b38', '1511c20b-6df0-4313-98a5-7c3561757dc2', 1, '2021-10-15', 604.17, 8060.04, 91939.96, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('e82648a3-8744-424f-badd-5a19a979574a', '1511c20b-6df0-4313-98a5-7c3561757dc2', 2, '2021-11-15', 555.47, 8108.74, 83831.22, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('0801632b-55d5-48fb-99d8-05e6fba1fcaf', '1511c20b-6df0-4313-98a5-7c3561757dc2', 3, '2021-12-15', 506.48, 8157.73, 75673.49, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('89eb8ba8-5dbb-42b5-8fd5-b733986ea10c', '1511c20b-6df0-4313-98a5-7c3561757dc2', 4, '2022-01-15', 457.19, 8207.02, 67466.47, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('1fe9c955-b05e-42aa-8770-d36593689790', '1511c20b-6df0-4313-98a5-7c3561757dc2', 5, '2022-02-15', 407.61, 8256.60, 59209.87, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('7711b4bc-5a44-4c68-8457-f85783f7f57e', '1511c20b-6df0-4313-98a5-7c3561757dc2', 6, '2022-03-15', 357.73, 8306.48, 50903.39, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('6a61a2eb-08b4-4baf-a6e6-a518b6d3de80', '1511c20b-6df0-4313-98a5-7c3561757dc2', 7, '2022-04-15', 307.54, 8356.67, 42546.72, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('992c8ad6-6858-44fa-9c97-343ea578f640', '1511c20b-6df0-4313-98a5-7c3561757dc2', 8, '2022-05-15', 257.05, 8407.16, 34139.56, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('b4a74b84-00cc-4d89-8669-25436309becb', '1511c20b-6df0-4313-98a5-7c3561757dc2', 9, '2022-06-15', 206.26, 8457.95, 25681.61, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('5696f5fa-3c7a-4401-97aa-bb2bdf425596', '1511c20b-6df0-4313-98a5-7c3561757dc2', 10, '2022-07-15', 155.16, 8509.05, 17172.56, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('b14fa6a6-740a-437b-9bac-55dd6e7824de', '1511c20b-6df0-4313-98a5-7c3561757dc2', 11, '2022-08-15', 103.75, 8560.46, 8612.10, '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('409e60dc-bbe6-4ca9-95c2-ebf6886e8c4c', '1511c20b-6df0-4313-98a5-7c3561757dc2', 12, '2022-09-15', 52.03, 8612.10, 0, '660bb318-649e-470d-9d2b-693bfb0b2744')
GO 

CREATE TABLE Finance.CashTransactionTypes
(
  CashTransactionTypeId INT IDENTITY PRIMARY KEY CLUSTERED,
  CashTransactionTypeName NVARCHAR(50) NOT NULL UNIQUE
)
GO

INSERT INTO Finance.CashTransactionTypes
    (CashTransactionTypeName)
VALUES
    ('Cash Receipt Sales'),
    ('Cash Receipt Debt Issue Proceeds'),
    ('Cash Receipt Stock Issue Proceeds'),
    ('Cash Disbursement Loan Payment'),    
    ('Cash Disbursement Divident Payment'),
    ('Cash Disbursement TimeCard Payment'),
    ('Cash Disbursement Purchase Receipt'),
    ('Cash Receipt Adjustment'),
    ('Cash Disbursement Adjustment')    
GO

CREATE TABLE Finance.CashAccountTransactions
(
	CashTransactionId INT IDENTITY PRIMARY KEY CLUSTERED,
	CashTransactionTypeId INT NOT NULL REFERENCES Finance.CashTransactionTypes(CashTransactionTypeId),
	CashAccountId uniqueidentifier NOT NULL REFERENCES Finance.CashAccounts(CashAccountId),
	CashAcctTransactionDate DATETIME2(0) NOT NULL,
	CashAcctTransactionAmount DECIMAL(18,2) NOT NULL,
	AgentId uniqueidentifier NOT NULL REFERENCES Shared.ExternalAgents (AgentId),
	EventId uniqueidentifier NOT NULL REFERENCES Shared.EconomicEvents (EventId),
	CheckNumber NVARCHAR(25) NOT NULL,
	RemittanceAdvice NVARCHAR(50) NULL,
	UserId uniqueidentifier NOT NULL REFERENCES HumanResources.Users (UserId),
	CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
	LastModifiedDate datetime2(7) NULL
)
GO

CREATE INDEX idx_CashAcctTransactions$CashTransactionTypeId 
  ON Finance.CashAccountTransactions (CashTransactionTypeId)
GO
CREATE INDEX idx_CashAcctTransactions$CashAccountId 
  ON Finance.CashAccountTransactions (CashAccountId)
GO
CREATE INDEX idx_CashAcctTransactions$AgentId 
  ON Finance.CashAccountTransactions (AgentId)
GO
CREATE INDEX idx_CashAcctTransactions$EventId 
  ON Finance.CashAccountTransactions (EventId)
GO
CREATE INDEX idx_CashAcctTransactions$UserId
  ON Finance.CashAccountTransactions (UserId)
GO

INSERT INTO Finance.CashAccountTransactions
    (CashTransactionTypeId, CashAccountId, CashAcctTransactionDate, CashAcctTransactionAmount, AgentId, EventId, CheckNumber, UserId)
VALUES
	(2, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2020-12-02', 50000, '12998229-7ede-4834-825a-0c55bde75695', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', '65874', '660bb318-649e-470d-9d2b-693bfb0b2744'),
	(2, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2021-09-15', 100000, 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', '1511c20b-6df0-4313-98a5-7c3561757dc2', '100120', '660bb318-649e-470d-9d2b-693bfb0b2744'),
	(2, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2021-04-02', 50000, '94b1d516-a1c3-4df8-ae85-be1f34966601', '09b53ffb-9983-4cde-b1d6-8a49e785177f', '980', '660bb318-649e-470d-9d2b-693bfb0b2744'),
	(3, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2020-09-03', 50000, 'bf19cf34-f6ba-4fb2-b70e-ab19d3371886', '62d6e2e6-215d-4157-b7ec-1ba9b137c770', '114980', '660bb318-649e-470d-9d2b-693bfb0b2744'),
	(3, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2020-11-01', 25000, 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 'fb39b013-1633-4479-8186-9f9b240b5727', '68001', '660bb318-649e-470d-9d2b-693bfb0b2744'),
	(3, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2020-09-03', 50000, '01da50f9-021b-4d03-853a-3fd2c95e207d', '6d663bb9-763c-4797-91ea-b2d9b7a19ba4', '1001', '660bb318-649e-470d-9d2b-693bfb0b2744'),
	(3, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2020-11-01', 10000, '01da50f9-021b-4d03-853a-3fd2c95e207d', '6632cec7-29c5-4ec3-a5a9-c82bf8f5eae3', '180001', '660bb318-649e-470d-9d2b-693bfb0b2744'),
	(3, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2021-03-01', 105000, '12998229-7ede-4834-825a-0c55bde75695', '264632b4-20bd-473f-9a9b-dd6f3b6ddbac', '9800322', '660bb318-649e-470d-9d2b-693bfb0b2744'),
	(4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-01-02', 4363.89, '12998229-7ede-4834-825a-0c55bde75695', '93adf7e5-bf6c-4ec8-881a-bfdf37aaf12e', '2301', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-02-02', 4363.89, '12998229-7ede-4834-825a-0c55bde75695', 'f479f59a-5001-47af-9d6c-2eae07077490', '2302', '660bb318-649e-470d-9d2b-693bfb0b2744'),    
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-03-02', 4363.89, '12998229-7ede-4834-825a-0c55bde75695', '76e6164a-249d-47a2-b47c-f09a332181b6', '2303', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-04-02', 4363.89, '12998229-7ede-4834-825a-0c55bde75695', 'caaa8b0c-bd5e-4b74-abe7-437a6e1cde15', '2305', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-05-02', 4363.89, '12998229-7ede-4834-825a-0c55bde75695', '94cf5110-435a-4c30-b9a7-1a0a334528da', '2306', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-06-02', 4363.89, '12998229-7ede-4834-825a-0c55bde75695', 'e4860060-778b-4b70-9f92-3b1af108a58d', '2308', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-07-02', 4363.89, '12998229-7ede-4834-825a-0c55bde75695', '710cbc7d-be46-4822-aea6-a5c89213efa3', '2310', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-08-02', 4363.89, '12998229-7ede-4834-825a-0c55bde75695', '769a7c0d-0005-445e-b110-cbfb2321f40e', '2311', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-09-02', 4363.89, '12998229-7ede-4834-825a-0c55bde75695', '43e96119-c4c8-4fe9-a568-4dd3dc569501', '2330', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-10-02', 4363.89, '12998229-7ede-4834-825a-0c55bde75695', '0e04afc1-b006-4ef5-8265-ce24e456c0f8', '2331', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-05-02', 4363.89, '94b1d516-a1c3-4df8-ae85-be1f34966601', '94cf5110-435a-4c30-b9a7-1a0a334528da', '2307', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-06-02', 4363.89, '94b1d516-a1c3-4df8-ae85-be1f34966601', '97fa51e0-e02a-46c1-9f09-73f72a5519c9', '2309', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-07-02', 4363.89, '94b1d516-a1c3-4df8-ae85-be1f34966601', 'e4ca6c30-6fd7-44ea-89b5-e11ecfc5989b', '2312', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-08-02', 4363.89, '94b1d516-a1c3-4df8-ae85-be1f34966601', 'b5c98492-2155-404e-b020-0b8c1481ec73', '2313', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-09-02', 4363.89, '94b1d516-a1c3-4df8-ae85-be1f34966601', 'eda455e3-1cc9-4d23-8434-37b9da13c71f', '2332', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-10-02', 4363.89, '94b1d516-a1c3-4df8-ae85-be1f34966601', '839b2060-3ea5-4f5c-b313-f7a17a0cc0ec', '2333', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-10-15', 8664.21, 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', '2205ebde-58dc-4af7-958b-9124d9645b38', '2340', '660bb318-649e-470d-9d2b-693bfb0b2744'),     
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-01-15', 2500.00, 'bf19cf34-f6ba-4fb2-b70e-ab19d3371886', '0421396b-4499-4a93-95c2-8f0dba43a25a', '2303', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-02-15', 2500.00, 'bf19cf34-f6ba-4fb2-b70e-ab19d3371886', 'a93ee592-3025-4302-97b3-7a237b0fe6a6', '2305', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-03-15', 2500.00, 'bf19cf34-f6ba-4fb2-b70e-ab19d3371886', '95949c0f-f68e-4a14-9eaf-c62b4d689048', '2306', '660bb318-649e-470d-9d2b-693bfb0b2744'),    
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-01-15', 1250.00, 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 'e436c1f8-a1eb-4233-a8de-e8dab1812649', '2308', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-02-15', 1250.00, 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', '1c2b45e2-f55f-4b93-8cdc-7cb1272210de', '2310', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-03-15', 1250.00, 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', '1a14139d-b16f-4aa9-9274-b98a32ac0ebd', '2311', '660bb318-649e-470d-9d2b-693bfb0b2744'),    
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-01-15', 2500.00, '01da50f9-021b-4d03-853a-3fd2c95e207d', 'e2a14f74-5261-46a8-bc41-952933925e1d', '2330', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-02-15', 2500.00, '01da50f9-021b-4d03-853a-3fd2c95e207d', 'ace8a4cd-5f4b-413c-a3b8-4643e9dd0f97', '2331', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-03-15', 2500.00, '01da50f9-021b-4d03-853a-3fd2c95e207d', '08117078-5763-46d9-b771-799355d02fa1', '2307', '660bb318-649e-470d-9d2b-693bfb0b2744'),    
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-01-15', 500.00, '01da50f9-021b-4d03-853a-3fd2c95e207d', 'c84dad2c-4aba-4630-8201-6da56743e19e', '2309', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-02-15', 500.00, '01da50f9-021b-4d03-853a-3fd2c95e207d', 'b6f60d19-7d05-4625-be94-24e3079ef44f', '2312', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-03-15', 500.00, '01da50f9-021b-4d03-853a-3fd2c95e207d', '6f0c811c-b948-4c03-a6df-49c768dadc49', '2313', '660bb318-649e-470d-9d2b-693bfb0b2744'),    
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-01-15', 2450.00, '12998229-7ede-4834-825a-0c55bde75695', 'aeac44d8-feb3-4823-8b0a-ca79da5bb089', '2332', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-02-15', 2450.00, '12998229-7ede-4834-825a-0c55bde75695', '6f09cd69-4a95-42c7-bcc2-acacbb92270c', '2333', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    (5, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2021-03-15', 2450.00, '12998229-7ede-4834-825a-0c55bde75695', '24d6936a-beb5-451b-a950-0f30e3ad463d', '2340', '660bb318-649e-470d-9d2b-693bfb0b2744')          
GO




-- Triggers
CREATE TRIGGER HumanResources.SetEmployeeModifiedDate
   ON  HumanResources.Employees
   AFTER UPDATE
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE HumanResources.Employees SET LastModifiedDate = sysdatetime()
    FROM HumanResources.Employees AS t
    WHERE EXISTS (SELECT 1 FROM inserted WHERE EmployeeId = t.EmployeeId);
END
GO

CREATE TRIGGER Shared.SetAddressModifiedDate
   ON  Shared.Addresses
   AFTER UPDATE
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE Shared.Addresses SET LastModifiedDate = sysdatetime()
    FROM Shared.Addresses AS t
    WHERE EXISTS (SELECT 1 FROM inserted WHERE AddressId = t.AddressId);    
END
GO

CREATE TRIGGER Shared.SetContactPersonModifiedDate
   ON  Shared.ContactPersons
   AFTER UPDATE
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE Shared.ContactPersons SET LastModifiedDate = sysdatetime()
    FROM Shared.ContactPersons AS t
    WHERE EXISTS (SELECT 1 FROM inserted WHERE PersonId = t.PersonId);    
END
GO

CREATE TRIGGER Shared.SetExternalModifiedDate
   ON  Shared.ExternalAgents
   AFTER UPDATE
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE Shared.ExternalAgents SET LastModifiedDate = sysdatetime()
    FROM Shared.ExternalAgents AS t
    WHERE EXISTS (SELECT 1 FROM inserted WHERE AgentId = t.AgentId);    
END
GO

CREATE TRIGGER Finance.SetFinanciersModifiedDate
   ON  Finance.Financiers
   AFTER UPDATE
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE Finance.Financiers SET LastModifiedDate = sysdatetime()
    FROM Finance.Financiers AS t
    WHERE EXISTS (SELECT 1 FROM inserted WHERE FinancierID = t.FinancierID);
END
GO

