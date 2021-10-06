using System;
using Microsoft.EntityFrameworkCore;
using PipefittersSupplyCompany.Infrastructure.Persistence;

namespace PipefittersSupplyCompany.IntegrationTests.Base
{
    public static class TestDataInitialization
    {
        private static void ResetIdentity(AppDbContext ctx)
        {
            var tables = new[]
            {
                "HumanResources.UserRoles",
                "Shared.Addresses",
                "Shared.ContactPersons"
            };

            foreach (var table in tables)
            {
                var rawSqlString = $"DBCC CHECKIDENT (\"{table}\", RESEED, 0);";

#pragma warning disable EF1000  // Possible Sql injection vulnerability                
                ctx.Database.ExecuteSqlRaw(rawSqlString);
#pragma warning restore EF1000
            }
        }

        private static void ClearData(AppDbContext ctx)
        {
            ctx.Database.ExecuteSqlRaw("DELETE FROM Finance.Financiers");
            ctx.Database.ExecuteSqlRaw("DELETE FROM HumanResources.UserRoles");
            ctx.Database.ExecuteSqlRaw("DELETE FROM HumanResources.Users");
            ctx.Database.ExecuteSqlRaw("DELETE FROM Shared.ContactPersons");
            ctx.Database.ExecuteSqlRaw("DELETE FROM Shared.Addresses");
            ctx.Database.ExecuteSqlRaw("DELETE FROM HumanResources.Employees");
            ctx.Database.ExecuteSqlRaw("DELETE FROM HumanResources.Roles");
            ctx.Database.ExecuteSqlRaw("DELETE FROM Shared.ExternalAgents");
            ctx.Database.ExecuteSqlRaw("DELETE FROM Shared.EconomicEvents");

            ResetIdentity(ctx);
        }

        private static void InsertRoles(AppDbContext ctx)
        {
            string sql =
            @"
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
            ";

            ctx.Database.ExecuteSqlRaw(sql);
        }

        private static void InsertExternalAgents(AppDbContext ctx)
        {
            string sql =
            @"
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
                ('01da50f9-021b-4d03-853a-3fd2c95e207d', 6)                         
            ";

            ctx.Database.ExecuteSqlRaw(sql);
        }

        private static void InsertEmployees(AppDbContext ctx)
        {
            string sql =
            @"
            INSERT INTO HumanResources.Employees
                (EmployeeId, SupervisorID, LastName, FirstName, MiddleInitial, SSN, Telephone, MaritalStatus, Exemptions, PayRate, StartDate, IsActive)
            VALUES
                ('4B900A74-E2D9-4837-B9A4-9E828752716E', '4B900A74-E2D9-4837-B9A4-9E828752716E','Sanchez', 'Ken', 'J', '123789999', '817-987-1234', 'M', 5, 40.00, '1998-12-02', 1),
                ('5C60F693-BEF5-E011-A485-80EE7300C695', '5C60F693-BEF5-E011-A485-80EE7300C695','Carter', 'Wayne', 'L', '423789999', '972-523-1234', 'M', 3, 40.00, '1998-12-02', 1),
                ('660bb318-649e-470d-9d2b-693bfb0b2744', '4B900A74-E2D9-4837-B9A4-9E828752716E','Phide', 'Terri', 'M', '638912345', '214-987-1234', 'M', 1, 28.00, '2014-09-22', 1),
                ('9f7b902d-566c-4db6-b07b-716dd4e04340', '4B900A74-E2D9-4837-B9A4-9E828752716E','Duffy', 'Terri', 'L', '619912345', '214-987-1234', 'M', 2, 30.00, '2018-10-22', 1),
                ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', '4B900A74-E2D9-4837-B9A4-9E828752716E','Goldberg', 'Jozef', 'P', '036889999', '469-321-1234', 'S', 1, 29.00, '2013-02-28', 1),
                ('0cf9de54-c2ca-417e-827c-a5b87be2d788', '4B900A74-E2D9-4837-B9A4-9E828752716E','Brown', 'Jamie', 'J', '123700009', '817-555-5555', 'M', 2, 29.00, '2017-12-22', 1),
                ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', '4B900A74-E2D9-4837-B9A4-9E828752716E','Bush', 'George', 'W', '325559874', '214-555-5555', 'M', 5, 30.00, '2016-10-19', 1),
                ('604536a1-e734-49c4-96b3-9dfef7417f9a', '660bb318-649e-470d-9d2b-693bfb0b2744','Rainey', 'Ma', 'A', '775559874', '903-555-5555', 'M', 2, 27.25, '2018-01-05', 1),
                ('e6b86ea3-6479-48a2-b8d4-54bd6cbbdbc5', '4B900A74-E2D9-4837-B9A4-9E828752716E','Beck', 'Jeffery', 'W', '825559874', '214-555-5555', 'M', 5, 30.00, '2016-10-19', 1)         
            ";

            ctx.Database.ExecuteSqlRaw(sql);
        }

        private static void InsertFinanciers(AppDbContext ctx)
        {
            string sql =
            @"
            INSERT INTO Finance.Financiers
                (FinancierID, FinancierName, Telephone, IsActive, UserId)
            VALUES
                ('12998229-7ede-4834-825a-0c55bde75695', 'Arturo Sandoval', '888-719-8128', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('94b1d516-a1c3-4df8-ae85-be1f34966601', 'Paul Van Horn Enterprises', '415-328-9870', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('bf19cf34-f6ba-4fb2-b70e-ab19d3371886', 'New World Tatoo Parlor', '630-321-9875', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 'Bertha Mae Jones Innovative Financing', '886-587-0001', 1, '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('01da50f9-021b-4d03-853a-3fd2c95e207d', 'Pimps-R-US Financial Management, Inc.', '415-912-5570', 1, '660bb318-649e-470d-9d2b-693bfb0b2744')       
            ";

            ctx.Database.ExecuteSqlRaw(sql);
        }

        private static void InsertAddresses(AppDbContext ctx)
        {
            string sql =
            @"
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
                ('604536a1-e734-49c4-96b3-9dfef7417f9a', '1233 Back Alley Rd', null, 'Corsicana', 'TX', '75110'),
                ('12998229-7ede-4834-825a-0c55bde75695', '5232 Outriggers Way', 'Ste 401', 'Oxnard', 'CA', '93035'),
                ('12998229-7ede-4834-825a-0c55bde75695', '985211 Highway 78 East', null, 'Oxnard', 'CA', '93035'),
                ('94b1d516-a1c3-4df8-ae85-be1f34966601', '825 Mandalay Beach Rd', 'Level 2', 'Oxnard', 'CA', '94402'),
                ('bf19cf34-f6ba-4fb2-b70e-ab19d3371886', '1690 S. El Camino Real', 'Room 2C', 'San Mateo', 'CA', '75224'),
                ('b49471a0-5c1e-4a4d-97e7-288fb0f6338a', '12333 Menard Heights Blvd', 'Ste 1001', 'Palo Alto', 'CA', '94901'),
                ('01da50f9-021b-4d03-853a-3fd2c95e207d', '96541 Sunset Rise Plaza', 'Ste 2', 'Oxnard', 'CA', '93035')                        
            ";

            ctx.Database.ExecuteSqlRaw(sql);
        }

        private static void InsertContactPersons(AppDbContext ctx)
        {
            string sql =
            @"
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
                ('604536a1-e734-49c4-96b3-9dfef7417f9a', 'Harvey', 'Steve', 'T', '903-854-5688'),
                ('12998229-7ede-4834-825a-0c55bde75695', 'Sandoval', 'Arturo', 'T', '888-719-8128'),
                ('12998229-7ede-4834-825a-0c55bde75695', 'Daniels', 'Javier', 'A', '888-719-8100'),
                ('94b1d516-a1c3-4df8-ae85-be1f34966601', 'Crocker', 'Patrick', 'T', '415-328-9870'),
                ('bf19cf34-f6ba-4fb2-b70e-ab19d3371886', 'Jozef Jr.', 'JoJo', 'D', '630-321-9875'),
                ('b49471a0-5c1e-4a4d-97e7-288fb0f6338a', 'Sinosky', 'Betty', 'L', '886-587-0001'),
                ('01da50f9-021b-4d03-853a-3fd2c95e207d', 'Gutierrez', 'Monica', 'T', '415-912-5570')                       
            ";

            ctx.Database.ExecuteSqlRaw(sql);
        }

        private static void InsertUsers(AppDbContext ctx)
        {
            string sql =
            @"
            INSERT INTO HumanResources.Users
                (UserId, UserName, Email, EmployeeId)
            VALUES
                ('4B900A74-E2D9-4837-B9A4-9E828752716E', 'ken.j.sanchez@pipefitterssupplycompany.com', 'ken.j.sanchez@pipefitterssupplycompany.com', '4B900A74-E2D9-4837-B9A4-9E828752716E'),
                ('517e8a39-6fb4-4aa3-931d-d512e59066e7', 'wayne.l.carter@pipefitterssupplycompany.com', 'wayne.l.carter@pipefitterssupplycompany.com', '5C60F693-BEF5-E011-A485-80EE7300C695'),
                ('660bb318-649e-470d-9d2b-693bfb0b2744', 'terri.m.phide@pipefitterssupplycompany.com', 'terri.m.phide@pipefitterssupplycompany.com', '660bb318-649e-470d-9d2b-693bfb0b2744'),
                ('9f7b902d-566c-4db6-b07b-716dd4e04340', 'terri.l.duffy@pipefitterssupplycompany.com', 'terri.l.duffy@pipefitterssupplycompany.com', '9f7b902d-566c-4db6-b07b-716dd4e04340'),
                ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', 'jozef.p.goldberg@pipefitterssupplycompany.com', 'jozef.p.goldberg@pipefitterssupplycompany.com', 'AEDC617C-D035-4213-B55A-DAE5CDFCA366'),
                ('0cf9de54-c2ca-417e-827c-a5b87be2d788', 'jamie.j.brown@pipefitterssupplycompany.com', 'jamie.j.brown@pipefitterssupplycompany.com', '0cf9de54-c2ca-417e-827c-a5b87be2d788'),
                ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', 'george.w.bush@pipefitterssupplycompany.com', 'george.w.bush@pipefitterssupplycompany.com', 'e716ac28-e354-4d8d-94e4-ec51f08b1af8'),
                ('2624b03c-901d-4618-9303-7d560d0e4507', 'ma.a.rainey@pipefitterssupplycompany.com', 'ma.a.rainey@pipefitterssupplycompany.com', '604536a1-e734-49c4-96b3-9dfef7417f9a')           
            ";

            ctx.Database.ExecuteSqlRaw(sql);
        }

        private static void InsertUserRoles(AppDbContext ctx)
        {
            string sql =
            @"
            INSERT INTO HumanResources.UserRoles
                (UserId, RoleId)
            VALUES
                ('4B900A74-E2D9-4837-B9A4-9E828752716E', '13e7d2d0-3cbe-4066-bc46-ce5c8c377e22'),
                ('4B900A74-E2D9-4837-B9A4-9E828752716E', 'cad456c3-a6c8-4e7a-8be5-9aa0aedb8ec1'),
                ('517e8a39-6fb4-4aa3-931d-d512e59066e7', '13e7d2d0-3cbe-4066-bc46-ce5c8c377e22'),
                ('517e8a39-6fb4-4aa3-931d-d512e59066e7', 'cad456c3-a6c8-4e7a-8be5-9aa0aedb8ec1'),
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
                ('2624b03c-901d-4618-9303-7d560d0e4507', '0cceb901-e943-4dac-827e-4e440a7eed46')          
            ";

            ctx.Database.ExecuteSqlRaw(sql);
        }


        private static void SeedData(AppDbContext ctx)
        {
            try
            {
                InsertExternalAgents(ctx);      // For employees, creditors, stockholders, vendors, and customers
                InsertEmployees(ctx);
                InsertFinanciers(ctx);
                InsertAddresses(ctx);           // add additional ones for creditors, stockholders, vendors, and customers
                InsertContactPersons(ctx);      // add additional ones for creditors, stockholders, vendors, and customers                
                InsertRoles(ctx);
                InsertUsers(ctx);
                InsertUserRoles(ctx);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static void InitializeData(AppDbContext ctx)
        {
            ClearData(ctx);
            SeedData(ctx);
        }



    }
}