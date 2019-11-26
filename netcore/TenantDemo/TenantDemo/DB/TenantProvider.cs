using System;
namespace TenantDemo.DB
{
    public interface ITenantProvider
    {
        Guid GetTenantId();
    }
    public class TenantProvider : ITenantProvider
    {
        static Guid id1 = Guid.Parse("e7d695fa-aaaa-11e9-b55b-0242ac110005");
        static Guid id2 = Guid.Parse("411fc813-aa41-11e9-b55b-0242ac110005");
        static Guid tenantId = id1;

        public Guid GetTenantId()
        {
            if (tenantId == id1)
            {
                tenantId = id2; 
            }
            else
            {
                tenantId = id1;
            }
            return tenantId;
        }
    }
}
