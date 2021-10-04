using System;

namespace PipefittersSupplyCompany.SharedKernel.CommonValueObjects
{
    public class OrganizationName : ValueObject
    {
        protected OrganizationName() { }

        private OrganizationName(string orgName)
            : this()
        {
            OrgName = orgName;
        }

        public string OrgName { get; }

        public static OrganizationName Create(string orgName)
        {
            CheckValidity(orgName);
            return new OrganizationName(orgName);
        }

        private static void CheckValidity(string orgName)
        {
            if (string.IsNullOrEmpty(orgName))
            {
                throw new ArgumentNullException("An organization name is required.", nameof(orgName));
            }

            if (orgName.Trim().Length > 25)
            {
                throw new ArgumentOutOfRangeException("Maximum length of the organization name is 50 characters.", nameof(orgName));
            }
        }
    }
}