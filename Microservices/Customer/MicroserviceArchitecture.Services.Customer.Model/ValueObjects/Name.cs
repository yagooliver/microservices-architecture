using System;
using MicroserviceArchitecture.Common.Models.ValueObjects;

namespace MicroserviceArchitecture.Services.Customer.Model.ValueObjects
{
    public class Name : ValueObject<Name>
    {
        public Name(string firstName, string lastName)
        {
            this.FirstName = firstName;
            LastName = lastName;
        }
        public string FirstName { get; }
        public string LastName { get; }

        protected override bool EqualsCore(Name other)
        {
            return true;
        }

        protected override int GetHashCodeCore()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}