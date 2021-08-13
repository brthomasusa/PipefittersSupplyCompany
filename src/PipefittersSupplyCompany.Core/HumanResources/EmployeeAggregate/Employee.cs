using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate.Events;
using PipefittersSupplyCompany.Core.Exceptions;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.Core.Shared;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class Employee : AggregateRoot
    {
        private static readonly string[] _stateCodes = { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "ME", "MD", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WI", "WV", "WY" };
        private Guid _supervisorID;
        private string _lastName;
        private string _firstName;
        private string _middleInitial;
        private string _ssn;
        private string _telephone;
        private string _maritalStatus;
        private int _exemptions;
        private decimal _payRate;
        private DateTime _startDate;
        private bool _isActive;
        private readonly IList<Address> _addresses = new List<Address>();
        private readonly IList<ContactPerson> _contactPersons = new List<ContactPerson>();


        protected Employee() { }

        public Employee(ExternalAgent agent, Guid supervisorId, string lastName, string firstName, string mi, string ssn,
                        string telephone, string maritalStatus, int exemption, decimal payRate, DateTime startDate, bool isActive)
            : this()
        {
            if (agent == null)
            {
                throw new ArgumentNullException("The external agent is required.");
            }

            ExternalAgent = agent;

            Apply(new EmployeeEvent.EmployeeCreated
            {
                Id = agent.Id,
                SupervisorId = supervisorId,
                LastName = lastName,
                FirstName = firstName,
                MiddleInitial = mi,
                SSN = ssn,
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

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

        public virtual ExternalAgent ExternalAgent { get; private set; }

        public virtual IReadOnlyList<Address> Addresses => _addresses.ToList();

        public virtual IReadOnlyList<ContactPerson> ContactPersons => _contactPersons.ToList();

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

