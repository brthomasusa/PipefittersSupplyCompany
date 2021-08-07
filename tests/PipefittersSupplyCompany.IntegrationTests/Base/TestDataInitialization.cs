using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using PipefittersSupplyCompany.Core.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Infrastructure.Data;
using PipefittersSupplyCompany.IntegrationTests.Data;

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
#pragma warning disable EF1000  // Possible Sql injection vulnerability  
                ctx.Database.ExecuteSqlInterpolated($"DBCC CHECKIDENT (\"{table}\", RESEED, 0);");
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

        private static void SeedData(AppDbContext ctx)
        {
            try
            {
                ctx.Roles.AddRange(HumanResourcesTestData.GetRoles());
                ctx.Employees.AddRange(HumanResourcesTestData.GetEmployees());
                ctx.Users.AddRange(HumanResourcesTestData.GetUsers());
                ctx.UserRoles.AddRange(HumanResourcesTestData.GetUserRoles());
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