using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TenantDemo.DB.Entities
{

    

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {

        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            // table
            // builder.ToTable("Blogs");

            // key
            builder.HasKey(k => k.Id);

            // properties 
            builder.Property(p=>p.Id).HasColumnType("CHAR(36)");
            builder.Property(p => p.TenantId).HasColumnType("CHAR(36)");
            builder.Property(p => p.Url).HasColumnType("VARCHAR(500)");
            builder.Property(p => p.Name).HasColumnType("VARCHAR(200)");
            builder.Property(p => p.CreatedTime)
                  .HasColumnType("DATETIME");//.HasDefaultValueSql("GATEDATE()"); 

            // filter
            builder.HasQueryFilter(p => !p.IsDeleted);

            // relationship
            builder.HasMany(p => p.Posts)
                .WithOne(o => o.Blog)
                .HasForeignKey(k => k.BlogId);


        }
    }

}