namespace ef_mt_boilerplate.Services
{
    using ef_mt_boilerplate.Services.Interface;
    public class TenantService: ITenantService   
    {
        private int _currentTenantId;

        public int GetCurrentTenantId()
        {
            return _currentTenantId;
        }

        public void SetCurrentTenantId(int tenantId)
        {
            _currentTenantId = tenantId;
        }
    }
}
