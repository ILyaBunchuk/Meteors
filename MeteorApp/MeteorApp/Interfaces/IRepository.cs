using MeteorApp.Models;

namespace MeteorApp.Interfaces
{
    public interface IRepository
    {
        Task<FilterMeteorsView> GetFilterAsync();
        Task<IEnumerable<MeteorView>> GetMeteorsAsync(FilterMeteorsPaginationDTO filter);
        Task<MeteorsTotalView> GetMeteorsTotalAsync(FilterMeteorsDTO filter);
        Task<List<int>> GetExistMeteorIdsAsync();
        Task CreateOrUpdateRangeAsync(IEnumerable<MeteorDB> meteors);
        Task SaveAsync();
    }
}
