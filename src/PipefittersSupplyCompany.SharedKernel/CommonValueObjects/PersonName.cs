using System;
using Ardalis.GuardClauses;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.SharedKernel.CommonValueObjects
{
    public class PersonName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleInitial { get; }

        protected PersonName() { }

        private PersonName(string first, string last, string mi)
            : this()
        {
            FirstName = first;
            LastName = last;
            MiddleInitial = mi;
        }

        private static void CheckValidity(string first, string last, string mi)
        {
            if (string.IsNullOrEmpty(first))
            {
                throw new ArgumentNullException("A first name is required.", nameof(first));
            }

            if (string.IsNullOrEmpty(last))
            {
                throw new ArgumentNullException("A last name is required.", nameof(last));
            }


        }
    }
}