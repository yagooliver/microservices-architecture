using MicroserviceArchitecture.Common.Models.Entities;
using MicroserviceArchitecture.Services.Customer.Model.ValueObjects;

namespace MicroserviceArchitecture.Services.Customer.Model.Entities
{
    public class Client : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {get;set;}

        public Client()
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