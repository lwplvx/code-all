using System;
using System.ComponentModel.DataAnnotations;

namespace TenantDemo.DB.Entities
{
    public abstract class BaseEntity
    { 
        public Guid Id { get; set; }
         
        public Guid TenantId { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
