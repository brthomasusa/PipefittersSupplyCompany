using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateCommand;

namespace PipefittersSupplyCompany.WebApi.ActionFilters
{
    public class EmployeePatchActionAttribute : IAsyncActionFilter
    {
        private readonly IEmployeeAggregateRepository _employeeRepo;
        private readonly ILoggerManager _logger;

        public EmployeePatchActionAttribute(IEmployeeAggregateRepository repo, ILoggerManager logger)
        {
            _employeeRepo = repo;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var employeeID = (Guid)context.ActionArguments["employeeId"];
            var employee = await _employeeRepo.GetByIdAsync(employeeID);

            if (employee == null)
            {
                _logger.LogInfo($"Employee with id: {employeeID} does not exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                var command = MapToEmployeeCommand(employee);
                context.HttpContext.Items.Add("EditEmployeeInfo", command);
                await next();
            }
        }

        private EditEmployeeInfo MapToEmployeeCommand(Employee employee)
        {
            return new EditEmployeeInfo
            {
                Id = employee.Id,
                SupervisorId = employee.SupervisorId,
                LastName = employee.EmployeeName.LastName,
                FirstName = employee.EmployeeName.FirstName,
                MiddleInitial = employee.EmployeeName.MiddleInitial,
                SSN = employee.SSN,
                Telephone = employee.Telephone,
                MaritalStatus = employee.MaritalStatus,
                Exemptions = employee.TaxExemption,
                PayRate = employee.PayRate,
                StartDate = employee.StartDate,
                IsActive = employee.IsActive
            };
        }
    }
}