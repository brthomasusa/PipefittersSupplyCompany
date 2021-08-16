using System;
using System.Text.RegularExpressions;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class Address : Entity<int>
    {
        private static readonly string[] _stateCodes = { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "ME", "MD", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WI", "WV", "WY" };
        private string _addressLine1;
        private string _addressLine2;
        private string _city;
        private string _stateCode;
        private string _zipcode;

        protected Address() { }

        public Address(ExternalAgent agent, string line1, string line2, string city, string stateCode, string zipcode)
            : this()
        {
            if (agent == null)
            {
                throw new ArgumentNullException("External agent is required.");
            }

            if (string.IsNullOrEmpty(line1))
            {
                throw new ArgumentNullException("Address line 1 is required.");
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentNullException("The city name is required.");
            }

            if (string.IsNullOrEmpty(stateCode))
            {
                throw new ArgumentNullException("The 2-digit state code is required.");
            }

            if (string.IsNullOrEmpty(zipcode))
            {
                throw new ArgumentNullException("The zipcode is required.");
            }

            Agent = agent;
            AddressLine1 = line1;
            AddressLine2 = line2;
            City = city;
            StateCode = stateCode;
            ZipCode = zipcode;
        }

        public string AddressLine1
        {
            get
            { return _addressLine1; }

            private set
            {
                if (value.Length > 50)
                {
                    throw new ArgumentException("The maximum length of address line 1 is 50 characters.");
                }

                _addressLine1 = value;
            }
        }

        public string AddressLine2
        {
            get
            { return _addressLine2; }

            private set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 50)
                {
                    throw new ArgumentException("The maximum length of address line 1 is 50 characters.");
                }

                _addressLine2 = value;
            }
        }

        public string City
        {
            get
            { return _city; }

            private set
            {
                if (value.Length > 50)
                {
                    throw new ArgumentException("The city name has a maximum length of 50 characters.");
                }

                _city = value;
            }
        }

        public string StateCode
        {
            get
            { return _stateCode; }

            private set
            {
                if (!Array.Exists(_stateCodes, element => element == value.ToUpper()))
                {
                    throw new ArgumentException("Invalid state code!");
                }

                _stateCode = value;
            }
        }
        public string ZipCode
        {
            get
            { return _zipcode; }

            private set
            {
                var usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";

                if (!Regex.IsMatch(value, usZipRegEx))
                {
                    throw new ArgumentException("Invalid zip code!");
                }

                _zipcode = value;
            }
        }

        public virtual ExternalAgent Agent { get; private set; }

    }
}