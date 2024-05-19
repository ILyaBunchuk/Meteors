using MeteorApp.Interfaces;
using MeteorApp.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace MeteorApp.Services
{
    public class MeteorService : IMeteorService
    {
        private readonly IRepository _repository;
        private readonly IMemoryCache _memoryCache;
        public MeteorService(IRepository repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }
        public async Task<FilterMeteorsView> GetFilterAsync()
        {
            FilterMeteorsView meteorFilter;
            string cacheKey = "MeteorFilter";

            if (!_memoryCache.TryGetValue(cacheKey, out meteorFilter))
            {
                meteorFilter = await _repository.GetFilterAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(cacheKey, meteorFilter, cacheOptions);
            }

            return meteorFilter;
        }
        public async Task<MeteorsTotalView> GetMeteorsTotalAsync(FilterMeteorsDTO filter)
        {
            MeteorsTotalView meteorsTotal;
            string cacheKey =  JsonConvert.SerializeObject(filter) + "Total";

            if (!_memoryCache.TryGetValue(cacheKey, out meteorsTotal))
            {
                meteorsTotal = await _repository.GetMeteorsTotalAsync(filter);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(cacheKey, meteorsTotal, cacheOptions);
            }

            return meteorsTotal;
        }
        public async Task<IEnumerable<MeteorView>> GetMeteorsAsync(FilterMeteorsPaginationDTO filter)
        {
            IEnumerable<MeteorView> meteorViews;
            string cacheKey = JsonConvert.SerializeObject(filter) + "Meteors";

            if (!_memoryCache.TryGetValue(cacheKey, out meteorViews))
            {
                meteorViews = await _repository.GetMeteorsAsync(filter);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(cacheKey, meteorViews, cacheOptions);
            }

            return meteorViews;
        }
    }
}
