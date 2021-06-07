using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Infrastructure.Interfaces;
using PipefittersSupply.Domain.Lookup;
using PipefittersSupply.Domain.Repository;
using static PipefittersSupply.Infrastructure.Application.Commands.HumanResources.EmployeeCommand;

namespace PipefittersSupply.Infrastructure.Application.Services
{
    public class EmployeeAppicationService : IApplicationService
    {
        private readonly IStateProvinceLookup _stateCodeLkup;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeAppicationService(IStateProvinceLookup lkup, IEmployeeRepository repo, IUnitOfWork unitOfWork)
        {
            _stateCodeLkup = lkup;
            _employeeRepo = repo;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(object command) =>
            command switch
            {
                V1.CreateEmployee cmd =>
                    HandleCreate(cmd),
                V1.UpdateEmployee cmd =>
                    HandleUpdate(cmd),
                _ => Task.CompletedTask
            };

        private async Task HandleCreate(V1.CreateEmployee cmd)
        {
            if (await _employeeRepo.Exists(cmd.Id.ToString()))
            {
                throw new InvalidOperationException($"Entity with Id {cmd.Id} already exists!");
            }

            var employee = new Employee
            (
                new EmployeeId(cmd.Id),
                EmployeeTypeIdentifier.FromInterger(cmd.EmployeeTypeId),
                new EmployeeId(cmd.SupervisorId),
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

            await _employeeRepo.Add(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(V1.UpdateEmployee cmd)
        {
            var id = new EmployeeId(cmd.Id);
            var employee = await _employeeRepo.Load(id);

            if (employee == null)
            {
                throw new InvalidOperationException($"Entity with id {id} could not be found!");
            }

            employee.UpdateEmployee
            (
                EmployeeTypeIdentifier.FromInterger(cmd.EmployeeTypeId),
                new EmployeeId(cmd.SupervisorId),
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

            await _unitOfWork.Commit();
        }
    }
}