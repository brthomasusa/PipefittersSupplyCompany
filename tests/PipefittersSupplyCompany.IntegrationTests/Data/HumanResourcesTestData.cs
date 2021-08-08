using System;
using System.Collections.Generic;
using PipefittersSupplyCompany.Core.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;

namespace PipefittersSupplyCompany.IntegrationTests.Data
{
    public class HumanResourcesTestData
    {
        public static IList<Role> GetRoles() => new List<Role>
        {
            new Role(new Guid("0cceb901-e943-4dac-827e-4e440a7eed46"), "Accountant"),
            new Role(new Guid("23098f32-a919-4906-9a0b-6c77f3775df1"), "Maintenance"),
            new Role(new Guid("13e7d2d0-3cbe-4066-bc46-ce5c8c377e22"), "Manager"),
            new Role(new Guid("8b9921af-74fe-4c10-bb9a-a59fec0a714f"), "Materials Handler"),
            new Role(new Guid("34af757e-666e-4ce6-9fcf-04635b9c5aa9"), "Purchasing Agent"),
            new Role(new Guid("a23a1148-603a-4b34-86ec-f3b32b418663"), "Sales Person"),
            new Role(new Guid("cad456c3-a6c8-4e7a-8be5-9aa0aedb8ec1"), "System Administrator")
        };

        public static IList<Employee> GetEmployees() => new List<Employee>
        {
            new Employee // Owner
            (
                new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E"),
                new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E"),
                "Sanchez",
                "Ken",
                "J",
                "321 Tarrant Pl",
                null,
                "Fort Worth",
                "TX",
                "78965",
                "123789999",
                "817-987-1234",
                "M",
                5,
                40.00M,
                new DateTime(1998,12,2),
                true
            ),
            new Employee    // Owner
            (
                new Guid("5C60F693-BEF5-E011-A485-80EE7300C695"),
                new Guid("5C60F693-BEF5-E011-A485-80EE7300C695"),
                "Carter",
                "Wayne",
                "L",
                "321 Fort Worth Ave",
                null,
                "Dallas",
                "TX",
                "75211",
                "423789999",
                "972-523-1234",
                "M",
                3,
                40.00M,
                new DateTime(1998,12,2),
                true
            ),
            new Employee // Accounting Manager
            (
                new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"),
                new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E"),
                "Phide",
                "Terri",
                "M",
                "3455 South Corinth Circle",
                null,
                "Dallas",
                "TX",
                "75224",
                "638912345",
                "214-987-1234",
                "M",
                1,
                28.00M,
                new DateTime(2014,9,22),
                true
            ),
            new Employee // Maintenance Manager
            (
                new Guid("9f7b902d-566c-4db6-b07b-716dd4e04340"),
                new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E"),
                "Duffy",
                "Terri",
                "L",
                "98 Reiger Ave",
                null,
                "Dallas",
                "TX",
                "75214",
                "399912345",
                "214-987-1234",
                "M",
                1,
                30.00M,
                new DateTime(2018,10,22),
                true
            ),
            new Employee    // Materials Handler Manager
            (
                new Guid("AEDC617C-D035-4213-B55A-DAE5CDFCA366"),
                new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E"),
                "Goldberg",
                "Jozef",
                "P",
                "6667 Melody Lane",
                "Apt 2",
                "Dallas",
                "TX",
                "75231",
                "036889999",
                "469-321-1234",
                "S",
                1,
                29.00M,
                new DateTime(2013,2,28),
                true
            ),
            new Employee    // Purchasing Manager
            (
                new Guid("0cf9de54-c2ca-417e-827c-a5b87be2d788"),
                new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E"),
                "Brown",
                "Jamie",
                "J",
                "98777 Nigeria Town Rd",
                null,
                "Arlington",
                "TX",
                "78658",
                "123700009",
                "817-555-5555",
                "M",
                2,
                29.00M,
                new DateTime(2017,12,22),
                true
            ),
            new Employee    // Sales Manager
            (
                new Guid("e716ac28-e354-4d8d-94e4-ec51f08b1af8"),
                new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E"),
                "Bush",
                "George",
                "W",
                "777 Ervay Street",
                null,
                "Dallas",
                "TX",
                "75208",
                "325559874",
                "972-555-5555",
                "M",
                5,
                30.00M,
                new DateTime(2016,10,19),
                true
            ),
            new Employee    // Accountant
            (
                new Guid("604536a1-e734-49c4-96b3-9dfef7417f9a"),
                new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"),
                "Rainey",
                "Ma",
                "A",
                "1233 Back Alley Rd",
                null,
                "Corsicana",
                "TX",
                "75110",
                "775559874",
                "903-555-5555",
                "M",
                2,
                27.25M,
                new DateTime(2018,1,5),
                true
            )

        };

        public static IList<User> GetUsers() => new List<User>
        {
            new User(new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E"), "ken.j.sanchez@pipefitterssupplycompany.com", "ken.j.sanchez@pipefitterssupplycompany.com", GetEmployees()[0]),
            new User(new Guid("517e8a39-6fb4-4aa3-931d-d512e59066e7"), "wayne.l.carter@pipefitterssupplycompany.com", "wayne.l.carter@pipefitterssupplycompany.com", GetEmployees()[1]),
            new User(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"), "terri.m.phide@pipefitterssupplycompany.com", "terri.m.phide@pipefitterssupplycompany.com", GetEmployees()[2]),
            new User(new Guid("9f7b902d-566c-4db6-b07b-716dd4e04340"), "terri.l.duffy@pipefitterssupplycompany.com", "terri.l.duffy@pipefitterssupplycompany.com", GetEmployees()[3]),
            new User(new Guid("AEDC617C-D035-4213-B55A-DAE5CDFCA366"), "jozef.p.goldberg@pipefitterssupplycompany.com", "jozef.p.goldberg@pipefitterssupplycompany.com", GetEmployees()[4]),
            new User(new Guid("0cf9de54-c2ca-417e-827c-a5b87be2d788"), "jamie.j.brown@pipefitterssupplycompany.com", "jamie.j.brown@pipefitterssupplycompany.com", GetEmployees()[5]),
            new User(new Guid("e716ac28-e354-4d8d-94e4-ec51f08b1af8"), "george.w.bush@pipefitterssupplycompany.com", "george.w.bush@pipefitterssupplycompany.com", GetEmployees()[6]),
            new User(new Guid("2624b03c-901d-4618-9303-7d560d0e4507"), "ma.a.rainey@pipefitterssupplycompany.com", "ma.a.rainey@pipefitterssupplycompany.com", GetEmployees()[7])
        };

        public static IList<UserRole> GetUserRoles() => new List<UserRole>
        {
            new UserRole(1, GetUsers()[0], GetRoles()[2]),
            new UserRole(2, GetUsers()[0], GetRoles()[6]),
            new UserRole(3, GetUsers()[1], GetRoles()[2]),
            new UserRole(4, GetUsers()[1], GetRoles()[6]),
            new UserRole(5, GetUsers()[2], GetRoles()[2]),
            new UserRole(6, GetUsers()[2], GetRoles()[0]),
            new UserRole(7, GetUsers()[2], GetRoles()[6]),
            new UserRole(8, GetUsers()[3], GetRoles()[2]),
            new UserRole(9, GetUsers()[3], GetRoles()[1]),
            new UserRole(10, GetUsers()[4], GetRoles()[2]),
            new UserRole(11, GetUsers()[4], GetRoles()[3]),
            new UserRole(12, GetUsers()[5], GetRoles()[2]),
            new UserRole(13, GetUsers()[5], GetRoles()[4]),
            new UserRole(14, GetUsers()[6], GetRoles()[2]),
            new UserRole(15, GetUsers()[6], GetRoles()[5]),
            new UserRole(16, GetUsers()[7], GetRoles()[0]),
        };
    }
}