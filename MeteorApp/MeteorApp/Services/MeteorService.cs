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
        public async Task<FilterMeteorsView> GetFilterDataAsync()
        {
            FilterMeteorsView meteorFilterData;
            string cacheKey = "MeteorFilterData";

            if (!_memoryCache.TryGetValue(cacheKey, out meteorFilterData))
            {
                meteorFilterData = await _repository.GetFilterDataAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(cacheKey, meteorFilterData, cacheOptions);
            }

            return meteorFilterData;
        }
        public async Task<MeteorsTotalDataView> GetMeteorsTotalDataAsync(FilterMeteorsDTO filter)
        {
            MeteorsTotalDataView meteorsTotalData;
            string cacheKey =  JsonConvert.SerializeObject(filter) + "TotalData";

            if (!_memoryCache.TryGetValue(cacheKey, out meteorsTotalData))
            {
                meteorsTotalData = await _repository.GetMeteorsTotalDataAsync(filter);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(cacheKey, meteorsTotalData, cacheOptions);
            }

            return meteorsTotalData;
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
