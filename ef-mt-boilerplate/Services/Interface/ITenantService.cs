namespace ef_mt_boilerplate.Services.Interface
{
    public interface ITenantService
    {
        int GetCurrentTenantId();
        void SetCurrentTenantId(int tenantId);

    }
}
