using MicroserviceArchitecture.Common.Models.Entities;
using MicroserviceArchitecture.Services.Customer.Model.ValueObjects;

namespace MicroserviceArchitecture.Services.Customer.Model.Entities
{
    public class Client : EntityBase
    {
        public string FirstName {get;}
        public string LastName { get;}
        public string Email {get;}

        private Client()
        {

        }
        
        public Client(Guid id)
        {
            Id = id;
        }
        public Client(Guid createdBy, string firstName, string lastName, string email)
        {
            FirstName = firstName; 
            LastName = lastName;
            Email = email;
            CreatedBy = createdBy;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}