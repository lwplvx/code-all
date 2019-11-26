using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TenantDemo.DB.Entities
{
    public class Post:BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual Guid BlogId { get; set; }
        public virtual Blog Blog { get; set; } 
    }

     
}
