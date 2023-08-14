using System;
namespace MicroserviceArchitecture.Common.Models.Entities
{
    public class EntityBase
    {
        public Guid Id {get; protected set;}
        public DateTime CreatedAt { get; protected set; }
        public Guid CreatedBy {get; protected set;}
        public bool IsRemoved {get;}
        public DateTime? RemovedAt {get;}
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