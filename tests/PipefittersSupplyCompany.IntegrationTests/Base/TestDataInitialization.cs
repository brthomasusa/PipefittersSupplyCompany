using System;
using Microsoft.EntityFrameworkCore;
using PipefittersSupplyCompany.Infrastructure.Data;

namespace PipefittersSupplyCompany.IntegrationTests.Base
{
    public static class TestDataInitialization
    {
        private static void ResetIdentity(AppDbContext ctx)
        {
            var tables = new[]
            {
                "HumanResources.UserRoles"
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
            ctx.Database.ExecuteSqlRaw("DELETE FROM HumanResources.UserRoles");
            ctx.Database.ExecuteSqlRaw("DELETE FROM HumanResources.Users");
            ctx.Database.ExecuteSqlRaw("DELETE FROM HumanResources.Employees");
            ctx.Database.ExecuteSqlRaw("DELETE FROM HumanResources.Roles");

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

        private static void InsertEmployees(AppDbContext ctx)
        {
            string sql =
            @"
            INSERT INTO HumanResources.Employees
                (EmployeeId, SupervisorID, LastName, FirstName, MiddleInitial, SSN, AddressLine1, AddressLine2, City, StateCode, ZipCode, Telephone, MaritalStatus, Exemptions, PayRate, StartDate, IsActive)
            VALUES
                ('4B900A74-E2D9-4837-B9A4-9E828752716E', '4B900A74-E2D9-4837-B9A4-9E828752716E','Sanchez', 'Ken', 'J', '123789999', '321 Tarrant Pl', null, 'Fort Worth', 'TX', '78965', '817-987-1234', 'M', 5, 40.00, '1998-12-02', 1),
                ('5C60F693-BEF5-E011-A485-80EE7300C695', '5C60F693-BEF5-E011-A485-80EE7300C695','Carter', 'Wayne', 'L', '423789999', '321 Fort Worth Ave', null, 'Dallas', 'TX', '75211', '972-523-1234', 'M', 3, 40.00, '1998-12-02', 1),
                ('660bb318-649e-470d-9d2b-693bfb0b2744', '4B900A74-E2D9-4837-B9A4-9E828752716E','Phide', 'Terri', 'M', '638912345', '3455 South Corinth Circle', null, 'Dallas', 'TX', '75224', '214-987-1234', 'M', 1, 28.00, '2014-09-22', 1),
                ('9f7b902d-566c-4db6-b07b-716dd4e04340', '4B900A74-E2D9-4837-B9A4-9E828752716E','Duffy', 'Terri', 'L', '599912345', '98 Reiger Ave', null, 'Dallas', 'TX', '75214', '214-987-1234', 'M', 2, 30.00, '2018-10-22', 1),
                ('AEDC617C-D035-4213-B55A-DAE5CDFCA366', '4B900A74-E2D9-4837-B9A4-9E828752716E','Goldberg', 'Jozef', 'P', '036889999', '6667 Melody Lane', 'Apt 2', 'Dallas', 'TX', '75231', '469-321-1234', 'S', 1, 29.00, '2013-02-28', 1),
                ('0cf9de54-c2ca-417e-827c-a5b87be2d788', '4B900A74-E2D9-4837-B9A4-9E828752716E','Brown', 'Jamie', 'J', '123700009', '98777 Nigeria Town Rd', null, 'Arlington', 'TX', '78658', '817-555-5555', 'M', 2, 29.00, '2017-12-22', 1),
                ('e716ac28-e354-4d8d-94e4-ec51f08b1af8', '4B900A74-E2D9-4837-B9A4-9E828752716E','Bush', 'George', 'W', '325559874', '777 Ervay Street', null, 'Dallas', 'TX', '75208', '214-555-5555', 'M', 5, 30.00, '2016-10-19', 1),
                ('604536a1-e734-49c4-96b3-9dfef7417f9a', '660bb318-649e-470d-9d2b-693bfb0b2744','Rainey', 'Ma', 'A', '775559874', '1233 Back Alley Rd', null, 'Corsicana', 'TX', '75110', '903-555-5555', 'M', 2, 27.25, '2018-01-05', 1)         
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
                InsertRoles(ctx);
                InsertEmployees(ctx);
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