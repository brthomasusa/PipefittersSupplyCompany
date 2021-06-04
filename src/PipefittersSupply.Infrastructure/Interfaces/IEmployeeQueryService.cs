using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PipefittersSupply.Infrastructure.Queries.HumanResources;

namespace PipefittersSupply.Infrastructure.Interfaces
{
    public interface IEmployeeQueryService
    {
        Task<IEnumerable<ReadModels.EmployeeListItems>> GetEmployees(int page, int pageSize);
    }
}