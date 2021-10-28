# PipefittersSupplyCompany

An ASP.NET Core 5.0 and SQL Server 15 implementation of the accounting information system from the textbook 'Building Accounting Systems using Access 2010' by James T. Perry and Richard Newmark. At the application level, the following three design patterns are implemented: Domain-Driven-Design (DDD), Command Query Responsibility Segregation (CQRS), and Clean Architecture (Hexagonal).

Development will be performed over five iterations (these are the top-level bounded contexts):

-   Financing (financing business process)
-   HumanResources (human resource business process which includes labor acquisition and payroll)
-   Purchasing (inventory acquisition business process)
-   Sales (sales/collection business process)
-   General (roughly analagous to a general ledger in a double-entry accounting system)

In the solution folder there is a folder named data. In this folder is the database creation and seed script for the 'Pipefitters_Test' database. For now, ignore the other scripts in the folder. Create the database and then run this script from within. The tests in the PipefittersSupplyCompany.IntegrationTests folder reseed the database Before each test run (look in the file 'Base/TestDataInitialization.cs'). There are two appsettings.json file: one in the root of WebApi project and the other in root of the Integration test project. The connection string info in those files need to be updated to reflect your database environment.
