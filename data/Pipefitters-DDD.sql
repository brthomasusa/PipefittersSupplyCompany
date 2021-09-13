-- DROP TABLE HumanResources.UserRoles
-- DROP TABLE HumanResources.Users
-- DROP TABLE Shared.Addresses
-- DROP TABLE Shared.Persons
-- DROP TABLE HumanResources.Employees
-- DROP TABLE Shared.ExternalAgents
-- DROP TABLE HumanResources.Roles
-- DROP TABLE Shared.EconomicEvents
-- DROP TABLE Shared.EconomicEventTypes
-- DROP TABLE Shared.ExternalAgentTypes
-- GO

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
CREATE SCHEMA Shared
GO

CREATE TABLE Shared.EconomicEventTypes
(
    EventTypeId int IDENTITY PRIMARY KEY CLUSTERED,
    EventName NVARCHAR(50) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL     
)

CREATE UNIQUE INDEX idx_EconomicEventTypes$EventName   
   ON Shared.EconomicEventTypes (EventName)   
GO

INSERT INTO Shared.EconomicEventTypes
    (EventName)
VALUES
    ('Sales'),
    ('Debt Issue'),
    ('Stock Issue'),
    ('Labor Acquisition'),
    ('Purchasing'),
    ('Loan Payment'),
    ('Dividend Payment')
GO

CREATE TABLE Shared.ExternalAgentTypes
(
    AgentTypeId int IDENTITY PRIMARY KEY CLUSTERED,
    AgentType NVARCHAR(50) NOT NULL,
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL     
)

CREATE UNIQUE INDEX idx_ExternalAgentTypes$AgentType   
   ON Shared.ExternalAgentTypes (AgentType)   
GO

INSERT INTO Shared.ExternalAgentTypes
    (AgentType)
VALUES
    ('Customer'),
    ('Creditor'),
    ('Stockholder'),
    ('Vendor'),
    ('Employee')
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
    ('604536a1-e734-49c4-96b3-9dfef7417f9a', 5)
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

CREATE TABLE HumanResources.Roles
(
  RoleId UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
  RoleName nvarchar(256) NOT NULL UNIQUE,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL  
)
GO

INSERT INTO HumanResources.Roles
    (RoleId, RoleName)
VALUES
    ('0cceb901-e943-4dac-827e-4e440a7eed46', 'Accountant'),
    ('23098f32-a919-4906-9a0b-6c77f3775df1', 'Maintenance'),
    ('13e7d2d0-3cbe-4066-bc46-ce5c8c377e22', 'Manager'),    
    ('8b9921af-74fe-4c10-bb9a-a59fec0a714f', 'Materials Handler'),
    ('34af757e-666e-4ce6-9fcf-04635b9c5aa9', 'Purchasing Agent'),
    ('a23a1148-603a-4b34-86ec-f3b32b418663', 'Salesperson'),
    ('cad456c3-a6c8-4e7a-8be5-9aa0aedb8ec1', 'System Administrator')
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
  MaritalStatus nchar(1) CHECK (MaritalStatus IN ('M', 'S')) DEFAULT 'S' NOT NULL,
  Exemptions int DEFAULT 0 NOT NULL,
  PayRate decimal(18, 2) CHECK (PayRate >= 7.50 AND PayRate <= 40.00) NOT NULL,
  StartDate datetime2(0) NOT NULL,
  IsActive BIT DEFAULT 1 NOT NULL,
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
    (EmployeeId, SupervisorID, LastName, FirstName, MiddleInitial, SSN, Telephone, MaritalStatus, Exemptions, PayRate, StartDate, IsActive)
VALUES
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', '4B900A74-E2D9-4837-B9A4-9E828752716E','Sanchez', 'Ken', 'J', '123789999', '817-987-1234', 'M', 5, 40.00, '1998-12-02', 1),
    ('5C60F693-BEF5-E011-A485-80EE7300C695', 'e716ac28-e354-4d8d-94e4-ec51f08b1af8','Carter', 'Wayne', 'L', '423789999', '972-523-1234', 'M', 3, 40.00, '1998-12-02', 1),
    ('660bb318-649e-470d-9d2b-693bfb0b2744', '4B900A74-E2D9-4837-B9A4-9E828752716E','Phide', 'Terri', 'M', '638912345', '214-987-1234', 'M', 1, 28.00, '2014-09-22', 1),
    ('9f7b902d-566c-4db6-b07b-716dd4e04340', '4B900A74-E2D9-4837-B9A4-9E828752716E','Duffy', 'Terri', 'L', '699912345', '214-987-1234', 'M', 2, 30.00, '2018-10-22', 1),
    ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', '4B900A74-E2D9-4837-B9A4-9E828752716E','Goldberg', 'Jozef', 'P', '036889999', '469-321-1234', 'S', 1, 29.00, '2013-02-28', 1),
    ('0cf9de54-c2ca-417e-827c-a5b87be2d788', '4B900A74-E2D9-4837-B9A4-9E828752716E','Brown', 'Jamie', 'J', '123700009', '817-555-5555', 'M', 2, 29.00, '2017-12-22', 1),
    ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', '4B900A74-E2D9-4837-B9A4-9E828752716E','Bush', 'George', 'W', '325559874', '214-555-5555', 'M', 5, 30.00, '2016-10-19', 1),
    ('604536a1-e734-49c4-96b3-9dfef7417f9a', '660bb318-649e-470d-9d2b-693bfb0b2744','Rainey', 'Ma', 'A', '775559874', '903-555-5555', 'M', 2, 27.25, '2018-01-05', 1),
    ('e6b86ea3-6479-48a2-b8d4-54bd6cbbdbc5', '4B900A74-E2D9-4837-B9A4-9E828752716E','Beck', 'Jeffery', 'W', '825559874', '214-555-5555', 'M', 5, 30.00, '2016-10-19', 1)
GO

CREATE TABLE HumanResources.Users
(
    UserId UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    UserName NVARCHAR(256) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    EmployeeId UNIQUEIDENTIFIER NOT NULL REFERENCES HumanResources.Employees (EmployeeId),
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL    
)
GO

CREATE INDEX idx_Users$EmployeeId 
  ON HumanResources.Users (EmployeeId)
GO
CREATE UNIQUE INDEX idx_Users$UserName   
   ON HumanResources.Users (UserName)   
GO
CREATE UNIQUE INDEX idx_Users$Email   
   ON HumanResources.Users (Email)   
GO

INSERT INTO HumanResources.Users
    (UserId, UserName, Email, EmployeeId)
VALUES
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', 'ken.j.sanchez@pipefitterssupplycompany.com', 'ken.j.sanchez@pipefitterssupplycompany.com', '4B900A74-E2D9-4837-B9A4-9E828752716E'),
    ('5C60F693-BEF5-E011-A485-80EE7300C695', 'wayne.l.carter@pipefitterssupplycompany.com', 'wayne.l.carter@pipefitterssupplycompany.com', '5C60F693-BEF5-E011-A485-80EE7300C695'),
    ('660bb318-649e-470d-9d2b-693bfb0b2744', 'terri.m.phide@pipefitterssupplycompany.com', 'terri.m.phide@pipefitterssupplycompany.com', '660bb318-649e-470d-9d2b-693bfb0b2744'),
    ('9f7b902d-566c-4db6-b07b-716dd4e04340', 'terri.l.duffy@pipefitterssupplycompany.com', 'terri.l.duffy@pipefitterssupplycompany.com', '9f7b902d-566c-4db6-b07b-716dd4e04340'),
    ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', 'jozef.p.goldberg@pipefitterssupplycompany.com', 'jozef.p.goldberg@pipefitterssupplycompany.com', 'AEDC617C-D035-4213-B55A-DAE5CDFCA366'),
    ('0cf9de54-c2ca-417e-827c-a5b87be2d788', 'jamie.j.brown@pipefitterssupplycompany.com', 'jamie.j.brown@pipefitterssupplycompany.com', '0cf9de54-c2ca-417e-827c-a5b87be2d788'),
    ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', 'george.w.bush@pipefitterssupplycompany.com', 'george.w.bush@pipefitterssupplycompany.com', 'e716ac28-e354-4d8d-94e4-ec51f08b1af8'),
    ('604536a1-e734-49c4-96b3-9dfef7417f9a', 'ma.a.rainey@pipefitterssupplycompany.com', 'ma.a.rainey@pipefitterssupplycompany.com', '604536a1-e734-49c4-96b3-9dfef7417f9a')
GO

CREATE TABLE HumanResources.UserRoles
(
    UserRoleId int IDENTITY PRIMARY KEY CLUSTERED,
    UserId UNIQUEIDENTIFIER NOT NULL REFERENCES HumanResources.Users (UserId),
    RoleId UNIQUEIDENTIFIER NOT NULL REFERENCES HumanResources.Roles (RoleId),
    CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
    LastModifiedDate datetime2(7) NULL     
)
GO

CREATE INDEX idx_UserRoles$UserId 
  ON HumanResources.UserRoles (UserId)
GO
CREATE INDEX idx_UserRoles$RoleId   
   ON HumanResources.UserRoles (RoleId)   
GO
CREATE UNIQUE INDEX idx_UserRoles$UserIdRoleId  
   ON HumanResources.UserRoles (UserId,RoleId)   
GO

INSERT INTO HumanResources.UserRoles
    (UserId, RoleId)
VALUES
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', '13e7d2d0-3cbe-4066-bc46-ce5c8c377e22'),
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', 'cad456c3-a6c8-4e7a-8be5-9aa0aedb8ec1'),
    ('660bb318-649e-470d-9d2b-693bfb0b2744', '13e7d2d0-3cbe-4066-bc46-ce5c8c377e22'),
    ('660bb318-649e-470d-9d2b-693bfb0b2744', '0cceb901-e943-4dac-827e-4e440a7eed46'),
    ('660bb318-649e-470d-9d2b-693bfb0b2744', 'cad456c3-a6c8-4e7a-8be5-9aa0aedb8ec1'),
    ('9f7b902d-566c-4db6-b07b-716dd4e04340', '13e7d2d0-3cbe-4066-bc46-ce5c8c377e22'),
    ('9f7b902d-566c-4db6-b07b-716dd4e04340', '23098f32-a919-4906-9a0b-6c77f3775df1'),
    ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', '13e7d2d0-3cbe-4066-bc46-ce5c8c377e22'),
    ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', '8b9921af-74fe-4c10-bb9a-a59fec0a714f'),
    ('0cf9de54-c2ca-417e-827c-a5b87be2d788', '13e7d2d0-3cbe-4066-bc46-ce5c8c377e22'),
    ('0cf9de54-c2ca-417e-827c-a5b87be2d788', '34af757e-666e-4ce6-9fcf-04635b9c5aa9'),
    ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', '13e7d2d0-3cbe-4066-bc46-ce5c8c377e22'),
    ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', 'a23a1148-603a-4b34-86ec-f3b32b418663'),
    ('604536a1-e734-49c4-96b3-9dfef7417f9a', '8b9921af-74fe-4c10-bb9a-a59fec0a714f'),
    ('5c60f693-bef5-e011-a485-80ee7300c695', 'a23a1148-603a-4b34-86ec-f3b32b418663')
GO

CREATE TABLE Shared.Addresses
(
  AddressId int IDENTITY PRIMARY KEY CLUSTERED,
  AgentId UNIQUEIDENTIFIER NOT NULL REFERENCES Shared.ExternalAgents (AgentId),
  AddressLine1 nvarchar(30) NOT NULL,
  AddressLine2 nvarchar(30) NULL,
  City nvarchar(30) NOT NULL,
  StateCode nchar(2) NOT NULL,
  Zipcode nvarchar(10) NOT NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
);
GO
CREATE INDEX idx_Addresses$AgentId   
   ON Shared.Addresses (AgentId)   
GO
CREATE UNIQUE INDEX idx_Addresses$AgentIdLine1Line2CityStateZipcode   
   ON Shared.Addresses (AgentId,AddressLine1,AddressLine2,City,StateCode,Zipcode)   
GO

INSERT INTO Shared.Addresses
    (AgentId, AddressLine1, AddressLine2, City, StateCode, ZipCode)
VALUES
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', '321 Tarrant Pl', null, 'Fort Worth', 'TX', '78965'),
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', '1 Desoto Plaza', '1st Floor', 'Desoto', 'TX', '75115'),
    ('5C60F693-BEF5-E011-A485-80EE7300C695', '321 Fort Worth Ave', null, 'Dallas', 'TX', '75211'),
    ('660bb318-649e-470d-9d2b-693bfb0b2744', '3455 South Corinth Circle', null, 'Dallas', 'TX', '75224'),
    ('9f7b902d-566c-4db6-b07b-716dd4e04340', '98 Reiger Ave', null, 'Dallas', 'TX', '75214'),
    ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', '6667 Melody Lane', 'Apt 2', 'Dallas', 'TX', '75231'),
    ('0cf9de54-c2ca-417e-827c-a5b87be2d788', '98777 Nigeria Town Rd', null, 'Arlington', 'TX', '78658'),
    ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', '777 Ervay Street', null, 'Dallas', 'TX', '75208'),
    ('604536a1-e734-49c4-96b3-9dfef7417f9a', '1233 Back Alley Rd', null, 'Corsicana', 'TX', '75110') 
GO

CREATE TABLE Shared.ContactPersons
(
  PersonId int IDENTITY PRIMARY KEY CLUSTERED,
  AgentId UNIQUEIDENTIFIER NOT NULL REFERENCES Shared.ExternalAgents (AgentId),
  LastName nvarchar(25) NOT NULL,
  FirstName nvarchar(25) NOT NULL,
  MiddleInitial nchar(1) NULL,
  Telephone nvarchar(14) NOT NULL,
  Notes nvarchar(1024) NULL,
  CreatedDate datetime2(7) DEFAULT sysdatetime() NOT NULL,
  LastModifiedDate datetime2(7) NULL
);
GO
CREATE INDEX idx_ContactPerson$AgentId   
   ON Shared.ContactPersons (AgentId)   
GO
CREATE UNIQUE INDEX idx_ContactPerson$AgentIdLastFirstMiTelephone   
   ON Shared.ContactPersons (AgentId,LastName,FirstName,MiddleInitial,Telephone)   
GO

INSERT INTO Shared.ContactPersons
    (AgentId, LastName, FirstName, MiddleInitial, Telephone)
VALUES
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', 'Sanchez', 'Maria', 'T', '972-412-5688'),
    ('4B900A74-E2D9-4837-B9A4-9E828752716E', 'Harvey', 'Steve', 'T', '972-854-5688'),
    ('5C60F693-BEF5-E011-A485-80EE7300C695', 'Bash', 'Dana', 'D', '214-854-5688'),
    ('660bb318-649e-470d-9d2b-693bfb0b2744', 'Hustle', 'Nipsey', 'T', '469-224-5688'),
    ('9f7b902d-566c-4db6-b07b-716dd4e04340', 'Gutierrez', 'Monica', 'T', '972-854-5688'),
    ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', 'Jones', 'Jim', 'A', '972-854-5688'),
    ('0cf9de54-c2ca-417e-827c-a5b87be2d788', 'Wienstein', 'Harvey', 'T', '817-854-5688'),
    ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', 'Harvey', 'Steve', 'T', '972-854-5688'),
    ('604536a1-e734-49c4-96b3-9dfef7417f9a', 'Harvey', 'Steve', 'T', '903-854-5688')   
GO

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

