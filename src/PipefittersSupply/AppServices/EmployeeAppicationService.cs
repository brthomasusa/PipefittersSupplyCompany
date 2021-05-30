using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.Interfaces;
using PipefittersSupply.Domain.Lookup;
using PipefittersSupply.Domain.Repository;
using static PipefittersSupply.Contracts.HumanResources.EmployeeCommand;

namespace PipefittersSupply.AppServices
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
                V1.UpdateEmployeeTypeId cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateEmployeeTypeId(EmployeeTypeIdentifier.FromInterger(cmd.EmployeeTypeId))
                    ),
                V1.UpdateSupervisorId cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateSupervisorId(new EmployeeId(cmd.SupervisorId))
                    ),
                V1.UpdateEmployeeLastName cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateLastName(EmployeeLastName.FromString(cmd.LastName))
                    ),
                V1.UpdateEmployeeFirstName cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateFirstName(EmployeeFirstName.FromString(cmd.FirstName))
                    ),
                V1.UpdateEmployeeMiddleInitial cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateMiddleInitial(EmployeeMiddleInitial.FromString(cmd.MiddleInitial))
                    ),
                V1.UpdateEmployeeSSN cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateSSN(EmployeeSSN.FromString(cmd.SSN))
                    ),
                V1.UpdateAddressLine1 cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateAddressLine1(AddressLine1.FromString(cmd.AddressLine1))
                    ),
                V1.UpdateAddressLine2 cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateAddressLine2(AddressLine2.FromString(cmd.AddressLine2))
                    ),
                V1.UpdateCity cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateCity(City.FromString(cmd.City))
                    ),
                V1.UpdateStateProvinceCode cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateStateProvinceCode(StateProvinceCode.FromString(cmd.StateProvinceCode, _stateCodeLkup))
                    ),
                V1.UpdateZipcode cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateZipcode(Zipcode.FromString(cmd.Zipcode))
                    ),
                V1.UpdateTelephone cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateTelephone(Telephone.FromString(cmd.Telephone))
                    ),
                V1.UpdateMaritalStatus cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateMaritalStatus(MaritalStatus.FromString(cmd.MaritalStatus))
                    ),
                V1.UpdateTaxExemption cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateTaxExemption(TaxExemption.FromInterger(cmd.Exemptions))
                    ),
                V1.UpdateEmployeePayRate cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdatePayRate(EmployeePayRate.FromDecimal(cmd.PayRate))
                    ),
                V1.UpdateEmployeeStartDate cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateStartDate(EmployeeStartDate.FromDateTime(cmd.StartDate))
                    ),
                V1.UpdateIsActive cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateIsActive(IsActive.FromBoolean(cmd.IsActive))
                    ),
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

        private async Task HandleUpdate(int employeeID, Action<Employee> operation)
        {
            var employee = await _employeeRepo.Load(employeeID.ToString());

            if (employee == null)
            {
                throw new InvalidOperationException($"Entity with id {employeeID} could not be found!");
            }

            operation(employee);

            await _unitOfWork.Commit();
        }
    }
}