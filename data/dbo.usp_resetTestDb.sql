-- select NEWID()
CREATE OR ALTER Proc dbo.usp_resetTestDb
as
BEGIN
    BEGIN TRAN
        BEGIN TRY            
            Delete from Finance.CashAccountTransactions;
            Delete from Finance.CashAccounts;
            Delete from Finance.LoanInstallments;
            Delete from Finance.LoanAgreements;
            Delete from Finance.StockSubscriptions;
            Delete from Finance.Financiers;
            Delete from Shared.DomainUsers;
            Delete from HumanResources.Employees;
            Delete from Shared.ExternalAgents; 
            Delete from Shared.EconomicEvents;

            DBCC CHECKIDENT ("Finance.CashAccountTransactions", RESEED, 0);
            
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
                ('e6b86ea3-6479-48a2-b8d4-54bd6cbbdbc5', 5),
                ('12998229-7ede-4834-825a-0c55bde75695', 6),
                ('94b1d516-a1c3-4df8-ae85-be1f34966601', 6),
                ('bf19cf34-f6ba-4fb2-b70e-ab19d3371886', 6),
                ('b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 6),
                ('01da50f9-021b-4d03-853a-3fd2c95e207d', 6),
                ('84164388-28ff-4b47-bd63-dd9326d32236', 6) 
            
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
                ('e6b86ea3-6479-48a2-b8d4-54bd6cbbdbc5', '4B900A74-E2D9-4837-B9A4-9E828752716E','Beck', 'Jeffery', 'W', '825559874', '214-555-5555', '321 Fort Worth Ave', null, 'Dallas', 'TX', '75211', 'M', 5, 30.00, '2016-10-19', 1, 0);
            
            INSERT INTO Shared.DomainUsers
                (UserId, UserName, Email)
            VALUES
                ('660bb318-649e-470d-9d2b-693bfb0b2744', 'tphide', 'terri.phide@pipefitterssupplycompany.com') 

            INSERT INTO Finance.Financiers
                (FinancierID, FinancierName, Telephone, AddressLine1, AddressLine2, City, StateCode, Zipcode, ContactLastName, ContactFirstName, ContactMiddleInitial, ContactTelephone, IsActive, UserId)
            VALUES
                ('12998229-7ede-4834-825a-0c55bde75695', 'Arturo Sandoval', '888-719-8128', '5232 Outriggers Way', 'Ste 401', 'Oxnard', 'CA', '93035', 'Sandoval', 'Arturo', 'T', '888-719-8128', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('94b1d516-a1c3-4df8-ae85-be1f34966601', 'Paul Van Horn Enterprises', '415-328-9870', '825 Mandalay Beach Rd', 'Level 2', 'Oxnard', 'CA', '94402', 'Crocker', 'Patrick', 'T', '415-328-9870', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('bf19cf34-f6ba-4fb2-b70e-ab19d3371886', 'New World Tatoo Parlor', '630-321-9875', '1690 S. El Camino Real', 'Room 2C', 'San Mateo', 'CA', '75224', 'Jozef Jr.', 'JoJo', 'D', '630-321-9875', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 'Bertha Mae Jones Innovative Financing', '886-587-0001', '12333 Menard Heights Blvd', 'Ste 1001', 'Palo Alto', 'CA', '94901', 'Sinosky', 'Betty', 'L', '886-587-0001', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('01da50f9-021b-4d03-853a-3fd2c95e207d', 'Pimps-R-US Financial Management, Inc.', '415-912-5570', '96541 Sunset Rise Plaza', 'Ste 2', 'Oxnard', 'CA', '93035', 'Daniels', 'Javier', 'A', '888-719-8100', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('84164388-28ff-4b47-bd63-dd9326d32236', 'I Exist-Only-To-Be-Deleted', '415-912-5570', '985211 Highway 78 East', null, 'Oxnard', 'CA', '93035', 'Gutierrez', 'Monica', 'T', '415-912-5570', 1, '660bb318-649e-470d-9d2b-693bfb0b2744')                                              

            INSERT INTO Shared.EconomicEvents
                (EventId, EventTypeId)
            VALUES
                ('41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 2),
                ('09b53ffb-9983-4cde-b1d6-8a49e785177f', 2),
                ('1511c20b-6df0-4313-98a5-7c3561757dc2', 2),        
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
                ('978aca0b-8e59-42ec-8ee7-b81455d2d1f2', 4),
                ('35744ee4-fee6-4273-8b0a-21f66410bb95', 4),
                ('4cdf2a3e-369f-4559-95b2-7320a0a68441', 4),
                ('a1aa2b37-bab9-4618-a17a-6d2b9273aacd', 4),
                ('2218e030-f884-4335-93d2-729c86c1cbd3', 4),
                ('dcd0a758-a2eb-4a5a-865f-1357e7faff4c', 4),
                ('acf3ed19-13ca-445c-811f-b2439f6589d1', 4),
                ('b9c1db2e-274a-4312-bdfd-b59afc8e8d9a', 4),
                ('6b48dcd5-e83d-451f-8da8-170ebe597137', 4),
                ('08e124ea-e7f8-454d-a9db-c0bc81f3b41e', 4),
                ('e63b4dd1-32a7-49df-8300-64c88d2121e0', 4),                 
                ('de4de926-924f-42f2-97ed-4ed031ab1cb8', 4),
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

            INSERT INTO Finance.LoanAgreements
                (LoanId, FinancierId, LoanAmount, InterestRate, LoanDate, MaturityDate, NumberOfInstallments, UserId)
            VALUES
                ('41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', '12998229-7ede-4834-825a-0c55bde75695', 25000.00, 0.08625, '2022-01-05', '2023-01-05', 12, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('09b53ffb-9983-4cde-b1d6-8a49e785177f', '94b1d516-a1c3-4df8-ae85-be1f34966601', 30000.00, 0.08625, '2022-02-02', '2024-02-02', 24, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('1511c20b-6df0-4313-98a5-7c3561757dc2', 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 10000.00, 0.07250, '2022-03-15', '2023-03-15', 12, '660bb318-649e-470d-9d2b-693bfb0b2744')

            INSERT INTO Finance.LoanInstallments
                (LoanInstallmentId, LoanId, InstallmentNumber, PaymentDueDate, EqualMonthlyInstallment, InterestAmount, PrincipalAmount, PrincipalRemaining, UserId)
            VALUES
                ('93adf7e5-bf6c-4ec8-881a-bfdf37aaf12e', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 1, '2022-02-05', 2186.28, 187.28, 1999.00, 23001.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('f479f59a-5001-47af-9d6c-2eae07077490', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 2, '2022-03-05', 2186.28, 172.28, 2014.00, 20987.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('76e6164a-249d-47a2-b47c-f09a332181b6', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 3, '2022-04-05', 2186.28, 158.28, 2028.00, 18959.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('caaa8b0c-bd5e-4b74-abe7-437a6e1cde15', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 4, '2022-05-05', 2186.28, 141.28, 2045.00, 16914.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('94cf5110-435a-4c30-b9a7-1a0a334528da', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 5, '2022-06-05', 2186.28, 127.28, 2059.00, 14855.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('e4860060-778b-4b70-9f92-3b1af108a58d', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 6, '2022-07-05', 2186.28, 111.28, 2075.00, 12780.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('710cbc7d-be46-4822-aea6-a5c89213efa3', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 7, '2022-08-05', 2186.28, 96.28, 2090.00, 10690.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('769a7c0d-0005-445e-b110-cbfb2321f40e', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 8, '2022-09-05', 2186.28, 80.28, 2106.00, 8584.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('43e96119-c4c8-4fe9-a568-4dd3dc569501', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 9, '2022-10-05', 2186.28, 64.28, 2122.00, 6462.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('0e04afc1-b006-4ef5-8265-ce24e456c0f8', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 10, '2022-11-05', 2186.28, 48.28, 2138.00, 4324.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('b71d5303-6035-4e96-9915-41c3724de721', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 11, '2022-12-05', 2186.28, 32.28, 2154.00, 2170.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('cf4279a1-da26-4d10-bff0-cf6e0933661c', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', 12, '2023-01-05', 2186.28, 16.28, 2170.00, 0, '660bb318-649e-470d-9d2b-693bfb0b2744'),

                ('8e804651-5021-4577-bbda-e7ee45a74e44', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 1, '2022-03-02', 1370.54, 224.54, 1146.00, 28854.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('97fa51e0-e02a-46c1-9f09-73f72a5519c9', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 2, '2022-04-02', 1370.54, 216.54, 1154.00, 27700.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('e4ca6c30-6fd7-44ea-89b5-e11ecfc5989b', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 3, '2022-05-02', 1370.54, 208.54, 1162.00, 26538.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('b5c98492-2155-404e-b020-0b8c1481ec73', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 4, '2022-06-02', 1370.54, 198.54, 1172.00, 25366.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('eda455e3-1cc9-4d23-8434-37b9da13c71f', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 5, '2022-07-02', 1370.54, 190.54, 1180.00, 24186.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('839b2060-3ea5-4f5c-b313-f7a17a0cc0ec', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 6, '2022-08-02', 1370.54, 181.54, 1189.00, 22997.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('083082b0-9332-4cae-8522-5af12f3c618d', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 7, '2022-09-02', 1370.54, 172.54, 1198.00, 21799.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('08ae781a-27c4-4d43-9c55-d96a956f3418', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 8, '2022-10-02', 1370.54, 162.54, 1208.00, 20591.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('22e0ccd6-9308-4c59-a5e8-2d65c40e1974', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 9, '2022-11-02', 1370.54, 154.54, 1216.00, 19375.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('4110e43c-b8ca-4ee1-85cc-46ef54d98893', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 10, '2022-12-02', 1370.54, 145.54, 1225.00, 18150.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('e673b4ef-1c5c-4a6e-8e4e-253c61c9c85c', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 11, '2023-01-02', 1370.54, 136.54, 1234.00, 16916.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('12ad37a2-8bb1-4b10-85d9-5eb9cee15ccc', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 12, '2023-02-02', 1370.54, 126.54, 1244.00, 15672.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('2205ebde-58dc-4af7-958b-9124d9645b38', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 13, '2023-03-02', 1370.54, 117.54, 1253.00, 14419.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('978aca0b-8e59-42ec-8ee7-b81455d2d1f2', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 14, '2023-04-02', 1370.54, 108.54, 1262.00, 13157.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('35744ee4-fee6-4273-8b0a-21f66410bb95', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 15, '2023-05-02', 1370.54, 98.54, 1272.00, 11885.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('4cdf2a3e-369f-4559-95b2-7320a0a68441', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 16, '2023-06-02', 1370.54, 88.54, 1282.00, 10603.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('a1aa2b37-bab9-4618-a17a-6d2b9273aacd', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 17, '2023-07-02', 1370.54, 79.54, 1291.00, 9312.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('2218e030-f884-4335-93d2-729c86c1cbd3', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 18, '2023-08-02', 1370.54, 70.54, 1300.00, 8012.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('dcd0a758-a2eb-4a5a-865f-1357e7faff4c', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 19, '2023-09-02', 1370.54, 59.54, 1311.00, 6701.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('acf3ed19-13ca-445c-811f-b2439f6589d1', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 20, '2023-10-02', 1370.54, 50.54, 1320.00, 5381.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('b9c1db2e-274a-4312-bdfd-b59afc8e8d9a', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 21, '2023-11-02', 1370.54, 40.54, 1330.00, 4051.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('6b48dcd5-e83d-451f-8da8-170ebe597137', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 22, '2023-12-02', 1370.54, 30.54, 1340.00, 2711.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('08e124ea-e7f8-454d-a9db-c0bc81f3b41e', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 23, '2024-01-02', 1370.54, 19.54, 1351.00, 1360.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('e63b4dd1-32a7-49df-8300-64c88d2121e0', '09b53ffb-9983-4cde-b1d6-8a49e785177f', 24, '2024-02-02', 1370.54, 10.54, 1360.00, 0, '660bb318-649e-470d-9d2b-693bfb0b2744'),    

                ('de4de926-924f-42f2-97ed-4ed031ab1cb8', '1511c20b-6df0-4313-98a5-7c3561757dc2', 1, '2022-04-15', 865.26, 58.26, 807.00, 9193.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('e82648a3-8744-424f-badd-5a19a979574a', '1511c20b-6df0-4313-98a5-7c3561757dc2', 2, '2022-05-15', 865.26, 53.26, 812.00, 8381.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('0801632b-55d5-48fb-99d8-05e6fba1fcaf', '1511c20b-6df0-4313-98a5-7c3561757dc2', 3, '2022-06-15', 865.26, 49.26, 816.00, 7565.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('89eb8ba8-5dbb-42b5-8fd5-b733986ea10c', '1511c20b-6df0-4313-98a5-7c3561757dc2', 4, '2022-07-15', 865.26, 44.26, 821.00, 6744.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('1fe9c955-b05e-42aa-8770-d36593689790', '1511c20b-6df0-4313-98a5-7c3561757dc2', 5, '2022-08-15', 865.26, 39.26, 826.00, 5918.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('7711b4bc-5a44-4c68-8457-f85783f7f57e', '1511c20b-6df0-4313-98a5-7c3561757dc2', 6, '2022-09-15', 865.26, 34.26, 831.00, 5087.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('6a61a2eb-08b4-4baf-a6e6-a518b6d3de80', '1511c20b-6df0-4313-98a5-7c3561757dc2', 7, '2022-10-15', 865.26, 30.26, 835.00, 4252.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('992c8ad6-6858-44fa-9c97-343ea578f640', '1511c20b-6df0-4313-98a5-7c3561757dc2', 8, '2022-11-15', 865.26, 24.26, 841.00, 3411.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('b4a74b84-00cc-4d89-8669-25436309becb', '1511c20b-6df0-4313-98a5-7c3561757dc2', 9, '2022-12-15', 865.26, 20.26, 845.00, 2566.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('5696f5fa-3c7a-4401-97aa-bb2bdf425596', '1511c20b-6df0-4313-98a5-7c3561757dc2', 10, '2023-01-15', 865.26, 15.26, 850.00, 1716.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('b14fa6a6-740a-437b-9bac-55dd6e7824de', '1511c20b-6df0-4313-98a5-7c3561757dc2', 11, '2023-02-15', 865.26, 9.26, 856.00, 860.00, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('409e60dc-bbe6-4ca9-95c2-ebf6886e8c4c', '1511c20b-6df0-4313-98a5-7c3561757dc2', 12, '2023-03-15', 865.26, 5.26, 860.00, 0, '660bb318-649e-470d-9d2b-693bfb0b2744')

            INSERT INTO Finance.StockSubscriptions
                (StockId, FinancierId, SharesIssured, PricePerShare, StockIssueDate, UserId)
            VALUES
                ('6d663bb9-763c-4797-91ea-b2d9b7a19ba4', '01da50f9-021b-4d03-853a-3fd2c95e207d', 15000, 1.00, '2022-01-03','660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('62d6e2e6-215d-4157-b7ec-1ba9b137c770', 'bf19cf34-f6ba-4fb2-b70e-ab19d3371886', 10000, 1.00, '2022-01-03','660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('fb39b013-1633-4479-8186-9f9b240b5727', 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 12000, 1.00, '2022-01-11','660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('6632cec7-29c5-4ec3-a5a9-c82bf8f5eae3', '01da50f9-021b-4d03-853a-3fd2c95e207d', 10000, 1.00, '2022-01-13','660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('264632b4-20bd-473f-9a9b-dd6f3b6ddbac', '12998229-7ede-4834-825a-0c55bde75695', 5000, 3.00, '2022-02-01','660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('5997f125-bfca-4540-a144-01e444f6dc25', '12998229-7ede-4834-825a-0c55bde75695', 12500, 1.25, '2022-04-02','660bb318-649e-470d-9d2b-693bfb0b2744')

            INSERT INTO Finance.CashAccounts
                (CashAccountId, BankName, AccountName, AccountNumber, RoutingTransitNumber, DateOpened, UserId)
            VALUES
                ('417f8a5f-60e7-411a-8e87-dfab0ae62589', 'First Bank and Trust', 'Primary Checking', '36547-9871222', '703452098', '2020-09-03', '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('c98ac84f-00bb-463d-9116-5828b2e9f718', 'First Bank and Trust', 'Payroll', '36547-9098812', '703452098', '2020-09-03', '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('6a7ed605-c02c-4ec8-89c4-eac6306c885e', 'First Bank and Trust', 'Financing Proceeds', '36547-9888249', '703452098', '2020-09-03', '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('765ec2b0-406a-4e42-b831-c9aa63800e76', 'BackAlley Money Washing, LLC', 'Slush Fund', 'XXXXX-XXXXXXX', '703452098', '2020-09-03', '660bb318-649e-470d-9d2b-693bfb0b2744')

            INSERT INTO Finance.CashAccountTransactions       -- Receipt of debt issue proceeds
                (CashTransactionTypeId, CashAccountId, CashAcctTransactionDate, CashAcctTransactionAmount, AgentId, EventId, CheckNumber, UserId)
            VALUES
            (3, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2022-01-10', 10000.00, 'bf19cf34-f6ba-4fb2-b70e-ab19d3371886', '62d6e2e6-215d-4157-b7ec-1ba9b137c770', '114980', '660bb318-649e-470d-9d2b-693bfb0b2744'),
            (3, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2022-01-13', 15000.00, '01da50f9-021b-4d03-853a-3fd2c95e207d', '6d663bb9-763c-4797-91ea-b2d9b7a19ba4', '1001', '660bb318-649e-470d-9d2b-693bfb0b2744'),
            (3, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2022-01-13', 10000.00, '01da50f9-021b-4d03-853a-3fd2c95e207d', '6632cec7-29c5-4ec3-a5a9-c82bf8f5eae3', '180001', '660bb318-649e-470d-9d2b-693bfb0b2744'),
            (2, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2022-01-15', 25000.00, '12998229-7ede-4834-825a-0c55bde75695', '41ca2b0a-0ed5-478b-9109-5dfda5b2eba1', '65874', '660bb318-649e-470d-9d2b-693bfb0b2744'),
            (3, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2022-01-18', 12000.00, 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 'fb39b013-1633-4479-8186-9f9b240b5727', '68001', '660bb318-649e-470d-9d2b-693bfb0b2744'),
            (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2022-02-05', 2186.28, '12998229-7ede-4834-825a-0c55bde75695', '93adf7e5-bf6c-4ec8-881a-bfdf37aaf12e', '2301', '660bb318-649e-470d-9d2b-693bfb0b2744'),
            (2, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2022-02-10', 30000.00, 'b49471a0-5c1e-4a4d-97e7-288fb0f6338a', '1511c20b-6df0-4313-98a5-7c3561757dc2', '100120', '660bb318-649e-470d-9d2b-693bfb0b2744'),
            (3, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2022-02-28', 15000.00, '12998229-7ede-4834-825a-0c55bde75695', '264632b4-20bd-473f-9a9b-dd6f3b6ddbac', '9800322', '660bb318-649e-470d-9d2b-693bfb0b2744'),
            (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2022-03-02', 1370.54, '94b1d516-a1c3-4df8-ae85-be1f34966601', '8e804651-5021-4577-bbda-e7ee45a74e44', '2302', '660bb318-649e-470d-9d2b-693bfb0b2744'),
            (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2022-03-05', 2186.28, '12998229-7ede-4834-825a-0c55bde75695', 'f479f59a-5001-47af-9d6c-2eae07077490', '2307', '660bb318-649e-470d-9d2b-693bfb0b2744'), 		
            (2, '6a7ed605-c02c-4ec8-89c4-eac6306c885e', '2022-03-19', 10000.00, '94b1d516-a1c3-4df8-ae85-be1f34966601', '09b53ffb-9983-4cde-b1d6-8a49e785177f', '980', '660bb318-649e-470d-9d2b-693bfb0b2744'),          
            (4, '417f8a5f-60e7-411a-8e87-dfab0ae62589', '2022-04-02', 1370.54, '94b1d516-a1c3-4df8-ae85-be1f34966601', '97fa51e0-e02a-46c1-9f09-73f72a5519c9', '2309', '660bb318-649e-470d-9d2b-693bfb0b2744')         

            COMMIT TRANSACTION
        END TRY
        BEGIN CATCH
                -- if error, roll back any chanegs done by any of the sql statements
                ROLLBACK TRANSACTION
        END CATCH    
END








         


         