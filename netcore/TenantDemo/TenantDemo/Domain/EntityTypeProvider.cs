using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace TenantDemo.DB.Entities
{
    public interface IEntityTypeProvider
    {
        IEnumerable<Type> GetEntityTypes();
    }
    public class EntityTypeProvider:IEntityTypeProvider
    {
        static IList<Type> _entityTypeCache;

        public IEnumerable<Type> GetEntityTypes()
        {
            if (_entityTypeCache!=null)
            {
                return _entityTypeCache.ToList(); 
            }
            _entityTypeCache = (from a in GetReferencingAssemblies()
                                from t in a.DefinedTypes
                                where t.BaseType == typeof(BaseEntity)
                                select t.AsType()).ToList();

            return _entityTypeCache;
        }

        static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblites = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblites.Add(assembly);
                }
                catch (FileNotFoundException ex)
                {

                }
            }

            return assemblites;
        }
    }
}
