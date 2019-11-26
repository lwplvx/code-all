using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TenantDemo.DB.Entities
{
     

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {

            // table
            //builder.ToTable("Posts");

            // key
            builder.HasKey(k => k.Id);

            // properties
            builder.Property(p => p.Id).HasColumnType("CHAR(36)");
            builder.Property(p => p.TenantId).HasColumnType("CHAR(36)");
            builder.Property(p => p.Title);
            builder.Property(p => p.Content);

            // filter
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
