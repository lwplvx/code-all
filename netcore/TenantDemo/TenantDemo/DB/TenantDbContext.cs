using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TenantDemo.DB.Entities;

namespace TenantDemo.DB
{
    public class TenantDbContext : DbContext
    {
        Guid _tenantId;
        IEntityTypeProvider _entityTypeProvider;
        public TenantDbContext(DbContextOptions<TenantDbContext> options,
            ITenantProvider tenantProvider,
            IEntityTypeProvider entityTypeProvider
            ) : base(options)
        {
            _tenantId = tenantProvider.GetTenantId();
            _entityTypeProvider = entityTypeProvider;




        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    foreach (var property in entityType.GetProperties())
            //    {
            //        if (property.ClrType == typeof(bool))
            //        {
            //            property.SetValueConverter(new BoolToIntConverter());
            //        }
            //    }
            //}



            foreach (var type in _entityTypeProvider.GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });
            }

            base.OnModelCreating(modelBuilder);
        }



        public void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().HasQueryFilter(e => e.TenantId == _tenantId && !e.IsDeleted);
        }

        //  static readonly MethodInfo SetGlobalQueryMethod = typeof(TenantDbContext).GetMethod("SetGlobalQuery");

        static readonly MethodInfo SetGlobalQueryMethod = typeof(TenantDbContext)
            .GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");
    }

    public class BoolToIntConverter : ValueConverter<bool, int>
    {
        public BoolToIntConverter(ConverterMappingHints mappingHints = null)
            : base(
                  v => Convert.ToInt32(v),
                  v => Convert.ToBoolean(v),
                  mappingHints)
        {
        }

        public GuidToStringConverter(ConverterMappingHints mappingHints = null)
           : base(
                 v => Convert.ToString(v),
                 v => Convert.ToBoolean(v),
                 mappingHints)
        {
        }


        public static ValueConverterInfo DefaultInfo { get; }
            = new ValueConverterInfo(typeof(bool), typeof(int), i => new BoolToIntConverter(i.MappingHints));
    }
}