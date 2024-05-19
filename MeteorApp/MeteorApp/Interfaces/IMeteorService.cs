using MeteorApp.Models;

namespace MeteorApp.Interfaces
{
    public interface IMeteorService
    {
        Task<FilterMeteorsView> GetFilterDataAsync();
        Task<IEnumerable<MeteorView>> GetMeteorsAsync(FilterMeteorsPaginationDTO filter);
        Task<MeteorsTotalDataView> GetMeteorsTotalDataAsync(FilterMeteorsDTO filter);
    }
}
