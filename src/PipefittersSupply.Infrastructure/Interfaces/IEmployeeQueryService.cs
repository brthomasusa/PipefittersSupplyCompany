using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PipefittersSupply.Infrastructure.Queries.HumanResources;
using PipefittersSupply.Domain.HumanResources.Employees;
using static PipefittersSupply.Infrastructure.Queries.HumanResources.ReadModels;
using static PipefittersSupply.Infrastructure.Queries.HumanResources.QueryParameters;

namespace PipefittersSupply.Infrastructure.Interfaces
{
    public interface IEmployeeQueryService
    {
        Task<IEnumerable<EmployeeListItems>> Query(GetEmployees queryParameters);
        Task<IEnumerable<EmployeeListItems>> Query(GetEmployeesSupervisedBy queryParameters);
        Task<IEnumerable<EmployeeListItems>> Query(GetEmployeesOfEmployeeType queryParameters);
        Task<EmployeeDetails> Query(GetEmployee queryParameters);
    }
}