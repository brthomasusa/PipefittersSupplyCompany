using System.Collections.Generic;
using PipefittersSupplyCompany.Core.ProjectAggregate;

namespace PipefittersSupplyCompany.Web.Endpoints.ProjectEndpoints
{
    public class ProjectListResponse
    {
        public List<ProjectRecord> Projects { get; set; } = new();
    }
}
