using System;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class ExternalAgent
    {
        public Guid AgentId { get; }

        public ExternalAgent()
        {
            AgentId = Guid.NewGuid();
        }
    }
}