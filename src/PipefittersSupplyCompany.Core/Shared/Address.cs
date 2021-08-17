using System;
using System.Text.RegularExpressions;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class Address : Entity<int>
    {
        protected Address() { }

        public Address(ExternalAgent agent, AddressVO addressDetails)
            : this()
        {
            if (agent == null)
            {
                throw new ArgumentNullException("External agent is required.");
            }

            Agent = agent;
            AddressDetails = addressDetails;
        }

        public virtual AddressVO AddressDetails { get; private set; }

        public virtual ExternalAgent Agent { get; private set; }

    }
}