using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.IntegrationTests.Base;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.FinancierAggregateWriteModels;


namespace PipefittersSupplyCompany.IntegrationTests.Financing
{
    public class FinancierCommandHandlerTests : IntegrationTestBaseEfCore
    {
        private readonly ICommandHandler _financierCmdHdlr;

        public FinancierCommandHandlerTests()
        {
            TestDataInitialization.InitializeData(_dbContext);
            IUnitOfWork unitOfWork = new AppUnitOfWork(_dbContext);
            IFinancierAggregateRepository financierRepo = new FinancierAggregateRepository(_dbContext);

            _financierCmdHdlr = new FinancierAggregateCommandHandler(financierRepo, unitOfWork);
        }

        [Fact]
        public async Task ShouldInsert_Financier_UsingCreateFinancierInfoWriteModel()
        {
            Guid id = Guid.NewGuid();
            var model = new CreateFinancierInfo
            {
                Id = id,
                FinancierName = "ABC Financiers, Inc.",
                Telephone = "214-654-9874",
                IsActive = true,
                UserId = new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            };

            await _financierCmdHdlr.Handle(model);

            Financier result = await _dbContext.Financiers.FindAsync(id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldUpdate_Financier_UsingEditFinancierInfoWriteModel()
        {
            var model = new EditFinancierInfo
            {
                Id = new Guid("01da50f9-021b-4d03-853a-3fd2c95e207d"),
                FinancierName = "ABC Financiers, Inc.",
                Telephone = "214-654-9874",
                IsActive = true,
                UserId = new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            };

            await _financierCmdHdlr.Handle(model);

            Financier result = await _dbContext.Financiers.FindAsync(model.Id);
            Assert.Equal(model.FinancierName, result.FinancierName);
            Assert.Equal(model.Telephone, result.Telephone); ;
        }

        [Fact(Skip = "Delete will fail because of child address and contact person records")]
        public async Task ShouldDelete_Financier_UsingDeleteFinancierInfoModel()
        {
            Financier financier = await _dbContext.Financiers.FindAsync(new Guid("b49471a0-5c1e-4a4d-97e7-288fb0f6338a"));
            Assert.NotNull(financier);

            var model = new DeleteFinancierInfo
            {
                Id = financier.Id,
            };

            await _financierCmdHdlr.Handle(model);

            Financier result = await _dbContext.Financiers.FindAsync(model.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldCreate_FinancierAddress_Using_CreateFinancierAddressInfoWriteModel()
        {
            Financier financier = await _dbContext.Financiers.FindAsync(new Guid("94b1d516-a1c3-4df8-ae85-be1f34966601"));

            var model = new CreateFinancierAddressInfo
            {
                FinancierId = financier.Id,
                AddressLine1 = "32145 Main Street",
                AddressLine2 = "3rd Floor",
                City = "Dallas",
                StateCode = "TX",
                Zipcode = "75021"
            };

            await _financierCmdHdlr.Handle(model);

            var address = (from item in financier.Addresses()
                           where item.AddressDetails.AddressLine1.Equals(model.AddressLine1) &&
                                item.AddressDetails.AddressLine2.Equals(model.AddressLine2) &&
                                item.AddressDetails.City.Equals(model.City) &&
                                item.AddressDetails.StateCode.Equals(model.StateCode) &&
                                item.AddressDetails.Zipcode.Equals(model.Zipcode)
                           select item).SingleOrDefault();

            Assert.NotNull(address);
        }

        [Fact]
        public async Task ShouldUpdate_FinancierAddress_Using_UpdateFinancierAddressInfoCommand()
        {
            Financier financier = await _dbContext.Financiers.FindAsync(new Guid("12998229-7ede-4834-825a-0c55bde75695"));

            var model = new EditFinancierAddressInfo
            {
                AddressId = 10,
                FinancierId = financier.Id,
                AddressLine1 = "32145 Main Street",
                AddressLine2 = "3rd Floor",
                City = "Oxnard",
                StateCode = "CA",
                Zipcode = "93035"
            };

            await _financierCmdHdlr.Handle(model);

            var address = (from item in financier.Addresses()
                           where item.AddressDetails.AddressLine1.Equals(model.AddressLine1) &&
                                item.AddressDetails.AddressLine2.Equals(model.AddressLine2) &&
                                item.AddressDetails.City.Equals(model.City) &&
                                item.AddressDetails.StateCode.Equals(model.StateCode) &&
                                item.AddressDetails.Zipcode.Equals(model.Zipcode)
                           select item).SingleOrDefault();

            Assert.NotNull(address);
        }

        [Fact]
        public async Task ShouldDelete_FinancierAddress_Using_DeleteFinancierAddressInfoCommand()
        {
            Financier financier = await _dbContext.Financiers.FindAsync(new Guid("12998229-7ede-4834-825a-0c55bde75695"));

            var model = new DeleteFinancierAddressInfo
            {
                AddressId = 10,
                FinancierId = financier.Id
            };

            await _financierCmdHdlr.Handle(model);

            var address = (from item in financier.Addresses()
                           where item.Id.Equals(model.AddressId)
                           select item).SingleOrDefault();

            Assert.Null(address);
        }

        [Fact]
        public async Task ShouldCreate_FinancierContact_Using_CreateFinancierContactInfoWriteModel()
        {
            Financier financier = await _dbContext.Financiers.FindAsync(new Guid("94b1d516-a1c3-4df8-ae85-be1f34966601"));

            var model = new CreateFinancierContactInfo
            {
                FinancierId = financier.Id,
                FirstName = "Jane",
                LastName = "DoeZoe",
                MiddleInitial = "Z",
                Telephone = "555-555-5555",
                Notes = "Hello world"
            };

            await _financierCmdHdlr.Handle(model);

            var contact = (from item in financier.ContactPersons()
                           where item.ContactName.FirstName == model.FirstName &&
                               item.ContactName.LastName == model.LastName &&
                               item.ContactName.MiddleInitial == model.MiddleInitial &&
                               item.Telephone == model.Telephone
                           select item).SingleOrDefault();

            Assert.NotNull(contact);
        }

        [Fact]
        public async Task ShouldUpdate_FinancierContact_Using_EditFinancierContactInfoWriteModel()
        {
            Financier financier = await _dbContext.Financiers.FindAsync(new Guid("94b1d516-a1c3-4df8-ae85-be1f34966601"));

            var model = new EditFinancierContactInfo
            {
                PersonId = 12,
                FinancierId = financier.Id,
                FirstName = "Jane",
                LastName = "DoeZoe",
                MiddleInitial = "Z",
                Telephone = "555-555-5555",
                Notes = "Hello world"
            };

            await _financierCmdHdlr.Handle(model);

            var contact = (from item in financier.ContactPersons()
                           where item.ContactName.FirstName == model.FirstName &&
                               item.ContactName.LastName == model.LastName &&
                               item.ContactName.MiddleInitial == model.MiddleInitial &&
                               item.Telephone == model.Telephone
                           select item).SingleOrDefault();

            Assert.NotNull(contact);
        }

        [Fact]
        public async Task ShouldDelete_FinancierContact_Using_DeleteFinancierContactInfoWriteModel()
        {
            Financier financier = await _dbContext.Financiers.FindAsync(new Guid("94b1d516-a1c3-4df8-ae85-be1f34966601"));

            var model = new DeleteFinancierContactInfo
            {
                PersonId = 12,
                FinancierId = financier.Id
            };

            await _financierCmdHdlr.Handle(model);

            var contact = (from item in financier.ContactPersons()
                           where item.Id.Equals(model.PersonId)
                           select item).SingleOrDefault();

            Assert.Null(contact);
        }

    }
}