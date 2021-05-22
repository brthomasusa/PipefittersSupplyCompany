using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.Lookup;
using PipefittersSupply.Domain.Repository;
using PipefittersSupply.Framework;
using static PipefittersSupply.Contracts.HumanResources.EmployeeCommand;

namespace PipefittersSupply.Api
{
    public class EmployeeAppicationService : IApplicationService
    {
        private readonly IStateProvinceLookup _stateCodeLkup;
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeAppicationService(IStateProvinceLookup lkup, IEmployeeRepository repo)
        {
            _stateCodeLkup = lkup;
            _employeeRepo = repo;
        }

        public Task Handle(object command) =>
            command switch
            {
                V1.Create cmd =>
                    HandleCreate(cmd),
                _ => Task.CompletedTask
            };

        private async Task HandleCreate(V1.Create cmd)
        {
            if (await _employeeRepo.Exists(cmd.Id.ToString()))
            {
                throw new InvalidOperationException($"Entity with Id {cmd.Id} already exists!");
            }

            var employee = new Employee
            (
                new EmployeeId(cmd.Id),
                EmployeeTypeIdentifier.FromInterger(cmd.EmployeeTypeId),
                EmployeeLastName.FromString(cmd.LastName),
                EmployeeFirstName.FromString(cmd.FirstName),
                EmployeeMiddleInitial.FromString(cmd.MiddleInitial),
                EmployeeSSN.FromString(cmd.SSN),
                AddressLine1.FromString(cmd.AddressLine1),
                AddressLine2.FromString(cmd.AddressLine2),
                City.FromString(cmd.City),
                StateProvinceCode.FromString(cmd.StateProvinceCode, _stateCodeLkup),
                Zipcode.FromString(cmd.Zipcode),
                Telephone.FromString(cmd.Telephone),
                MaritalStatus.FromString(cmd.MaritalStatus),
                TaxExemption.FromInterger(cmd.Exemptions),
                EmployeePayRate.FromDecimal(cmd.PayRate),
                EmployeeStartDate.FromDateTime(cmd.StartDate),
                IsActive.FromBoolean(cmd.IsActive)
            );

            await _employeeRepo.Save(employee);
        }
    }
}