using System;

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

        public static PersonName Create(string first, string last, string mi)
        {
            CheckValidity(first, last, mi);
            return new PersonName(first, last, mi);
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

            first = first.Trim();
            last = last.Trim();

            if (first.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Maximum length of the first name is 25 characters.", nameof(first));
            }

            if (last.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Maximum length of the last name is 25 characters.", nameof(last));
            }

            if (!string.IsNullOrEmpty(mi))
            {
                mi = mi.Trim();
                if (mi.Length > 1)
                {
                    throw new ArgumentOutOfRangeException("Maximum length of middle initial is 1 character.", nameof(mi));
                }
            }
        }
    }
}