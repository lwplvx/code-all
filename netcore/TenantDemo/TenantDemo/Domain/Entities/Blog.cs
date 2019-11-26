using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TenantDemo.DB.Entities
{

    public class Blog:BaseEntity
    {   
        public string Name { get; set; } 
        public string Url { get; set; }
        public DateTime CreatedTime { get; set; }
        public IEnumerable<Post> Posts { get; set; } 
    }
     

}