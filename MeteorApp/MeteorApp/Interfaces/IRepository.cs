using MeteorApp.Models;

namespace MeteorApp.Interfaces
{
    public interface IRepository
    {
        Task<FilterMeteorsView> GetFilterDataAsync();
        Task<IEnumerable<MeteorView>> GetMeteorsAsync(FilterMeteorsPaginationDTO filter);
        Task<MeteorsTotalDataView> GetMeteorsTotalDataAsync(FilterMeteorsDTO filter);
        Task<List<int>> GetExistMeteorIdsAsync();
        Task CreateOrUpdateRangeAsync(IEnumerable<MeteorDB> meteors);
        Task SaveAsync();
    }
}
