using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class ExternalAgentId : ValueObject
    {
        public Guid Value { get; }

        protected ExternalAgentId() { }

        private ExternalAgentId(Guid agentID)
            : this()
        {
            Value = agentID;
        }

        public static implicit operator Guid(ExternalAgentId self) => self.Value;

        public static ExternalAgentId Create(Guid agentID)
        {
            CheckValidity(agentID);
            return new ExternalAgentId(agentID);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The agent Id is required; it can not be a default Guid.", nameof(value));
            }
        }
    }
}