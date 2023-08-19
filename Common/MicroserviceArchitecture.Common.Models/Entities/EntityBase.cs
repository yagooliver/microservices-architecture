using System;
namespace MicroserviceArchitecture.Common.Models.Entities
{
    public class EntityBase
    {
        public Guid Id {get; set;}
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy {get; set;}
        public bool IsRemoved {get;}
        public DateTime? RemovedAt {get; set; }
        public Guid? RemovedBy{get; private set;}

        protected EntityBase()
        {
            
        }

        public void MarkAsRemoved(Guid removedBy)
        {
            RemovedBy = removedBy;
        }
    }
}