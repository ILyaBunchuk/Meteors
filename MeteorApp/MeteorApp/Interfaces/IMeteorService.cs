using MeteorApp.Models;

namespace MeteorApp.Interfaces
{
    public interface IMeteorService
    {
        Task<FilterMeteorsView> GetFilterAsync();
        Task<IEnumerable<MeteorView>> GetMeteorsAsync(FilterMeteorsPaginationDTO filter);
        Task<MeteorsTotalView> GetMeteorsTotalAsync(FilterMeteorsDTO filter);
    }
}
