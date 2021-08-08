using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate.Events;
using PipefittersSupplyCompany.Core.Exceptions;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class Employee : AggregateRoot
    {
        private static readonly string[] _stateCodes = { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "ME", "MD", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WI", "WV", "WY" };
        private Guid _supervisorID;
        private string _lastName;
        private string _firstName;
        private string _middleInitial;
        private string _line1;
        private string _line2;
        private string _city;
        private string _stateCode;
        private string _zipcode;
        private string _ssn;
        private string _telephone;
        private string _maritalStatus;
        private int _exemptions;
        private decimal _payRate;
        private DateTime _startDate;
        private bool _isActive;


        protected Employee() { }

        public Employee(Guid id, Guid supervisorId, string firstName, string lastName, string mi, string line1,
                        string line2, string city, string stateCode, string zipcode, string ssn, string telephone,
                        string maritalStatus, int exemption, decimal payRate, DateTime startDate, bool isActive)
            : this()
        {
            Apply(new EmployeeEvent.EmployeeCreated
            {
                Id = id,
                SupervisorId = supervisorId,
                LastName = lastName,
                FirstName = firstName,
                MiddleInitial = mi,
                SSN = ssn,
                AddressLine1 = line1,
                AddressLine2 = line2,
                City = city,
                StateProvinceCode = stateCode,
                Zipcode = zipcode,
                Telephone = telephone,
                MaritalStatus = maritalStatus,
                Exemptions = exemption,
                PayRate = payRate,
                StartDate = startDate,
                IsActive = isActive
            });
        }

        public Guid SupervisorId
        {
            get { return _supervisorID; }
            private set
            {
                if (value == default)
                {
                    throw new ArgumentNullException("The supervisor id is required.", nameof(value));
                }

                _supervisorID = value;
            }
        }

        public string FirstName
        {
            get
            { return _firstName; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The employee first name is required.", nameof(value));
                }

                if (value.Length > 25)
                {
                    throw new ArgumentException("The employee first name maximum length is 25 characters.", nameof(value));
                }

                _firstName = value;
            }
        }

        public string LastName
        {
            get
            { return _lastName; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The employee last name is required.", nameof(value));
                }

                if (value.Length > 25)
                {
                    throw new ArgumentException("The employee last name maximum length is 25 characters.", nameof(value));
                }

                _lastName = value;
            }
        }

        public string MiddleInitial
        {
            get
            { return _middleInitial; }

            private set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 1)
                {
                    throw new ArgumentException("The employee middle initial maximum length is 1 character.", nameof(value));
                }

                _middleInitial = value;
            }
        }

        public string AddressLine1
        {
            get
            { return _line1; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The first address line is required.", nameof(value));
                }

                if (value.Length > 30)
                {
                    throw new ArgumentException("Address line 1 has a maximum length of 30 characters.", nameof(value));
                }

                _line1 = value;
            }
        }

        public string AddressLine2
        {
            get
            { return _line2; }

            private set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Length > 30)
                    {
                        throw new ArgumentException("Address line 2 has a maximum length of 30 characters.", nameof(value));
                    }

                    _line2 = value;
                }

            }
        }

        public string City
        {
            get
            { return _city; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The city name is required.", nameof(value));
                }

                if (value.Length > 30)
                {
                    throw new ArgumentException("The city name has a maximum length of 30 characters.", nameof(value));
                }

                _city = value;
            }
        }

        public string StateProvinceCode
        {
            get
            { return _stateCode; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The 2-digit state code is required.", nameof(value));
                }

                if (!Array.Exists(_stateCodes, element => element == value.ToUpper()))
                {
                    throw new ArgumentException("Invalid state code!", nameof(value));
                }

                _stateCode = value.ToUpper();
            }
        }

        public string Zipcode
        {
            get
            { return _zipcode; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("A zip code is required.", nameof(value));
                }

                var usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
                // var caZipRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

                if (!Regex.IsMatch(value, usZipRegEx))
                {
                    throw new ArgumentException("Invalid zip code!", nameof(value));
                }

                _zipcode = value;
            }
        }

        public string SSN
        {
            get
            { return _ssn; }

            private set
            {
                if (!Regex.IsMatch(value, @"^(?!219099999|078051120)(?!666|000|9\d{2})\d{3}(?!00)\d{2}(?!0{4})\d{4}$"))
                {
                    throw new ArgumentException("Invalid social security number!", nameof(value));
                }

                _ssn = value;
            }
        }

        public string Telephone
        {
            get
            { return _telephone; }

            private set
            {
                string rgTelephone = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The telephone number is required.", nameof(value));
                }

                if (!Regex.IsMatch(value, rgTelephone))
                {
                    throw new ArgumentException("Invalid telephone number!", nameof(value));
                }

                _telephone = value;
            }
        }

        public string MaritalStatus
        {
            get
            { return _maritalStatus; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The marital status is required.", nameof(value));
                }

                if (value.ToUpper() != "M" && value.ToUpper() != "S")
                {
                    throw new ArgumentException("Invalid marital status, valid statues are 'S' and 'M'.", nameof(value));
                }

                _maritalStatus = value.ToUpper();
            }
        }

        public int TaxExemption
        {
            get
            { return _exemptions; }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number of exemptions can not be negative.", nameof(value));
                }

                _exemptions = value;
            }
        }

        public decimal PayRate
        {
            get
            { return _payRate; }

            private set
            {
                if (value == 0M)
                {
                    throw new ArgumentException("Invalid pay rate; pay rate can not be zero!", nameof(value));
                }

                if (value < 7.50M || value > 40.00M)
                {
                    throw new ArgumentException("Invalid pay rate, must be between $7.50 and $40.00 (per hour) inclusive!", nameof(value));
                }

                _payRate = value;
            }
        }

        public DateTime StartDate
        {
            get
            { return _startDate; }

            private set
            {
                if (value == default)
                {
                    throw new ArgumentNullException("The employee start date is required.", nameof(value));
                }

                _startDate = value;
            }
        }

        public bool IsActive
        {
            get
            { return _isActive; }

            private set
            {

                _isActive = value;
            }
        }

        public void Activate() { }

        public void Deactivate() { }

        protected override void EnsureValidState()
        {
            var valid = Id != default && SupervisorId != default;

            if (!valid)
            {
                throw new InvalidEntityStateException(this, "Employee validity check failed; the employee id and supervisor id are required.!");
            }
        }

        protected override void When(BaseDomainEvent @event)
        {
            switch (@event)
            {
                case EmployeeEvent.EmployeeCreated evt:
                    Id = evt.Id;
                    SupervisorId = evt.SupervisorId;
                    LastName = evt.LastName;
                    FirstName = evt.FirstName;
                    MiddleInitial = evt.MiddleInitial;
                    SSN = evt.SSN;
                    AddressLine1 = evt.AddressLine1;
                    AddressLine2 = evt.AddressLine2;
                    City = evt.City;
                    StateProvinceCode = evt.StateProvinceCode;
                    Zipcode = evt.Zipcode;
                    Telephone = evt.Telephone;
                    MaritalStatus = evt.MaritalStatus;
                    TaxExemption = evt.Exemptions;
                    PayRate = evt.PayRate;
                    StartDate = evt.StartDate;
                    IsActive = evt.IsActive;
                    CreatedDate = DateTime.Now;
                    break;


            }
        }   // End of When()         
    }
}

