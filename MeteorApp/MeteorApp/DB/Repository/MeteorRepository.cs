using MeteorApp.DB.Predicate;
using MeteorApp.ExtensionStarter;
using MeteorApp.Interfaces;
using MeteorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MeteorApp.DB.Repository
{
    public class MeteorRepository : IRepository
    {
        private readonly MeteorContext _context;
        private readonly PredicateMeteorBuilder _predicate;

        public MeteorRepository(MeteorContext context, PredicateMeteorBuilder predicate)
        {
            _context = context;
            _predicate = predicate;
        }
        public async Task<FilterMeteorsView> GetFilterAsync()
        {
            var minYear = await _context.Meteors.Select(meteor => meteor.Year.Year).MinAsync();
            var recclasses = await _context.Meteors.GroupBy(meteor => meteor.Recclass).Select(group => group.Key).ToListAsync();

            var filterMeteorsView = new FilterMeteorsView()
            {
                MinYear = minYear,
                Recclasses = recclasses
            };

            return filterMeteorsView;
        }
        public async Task<MeteorsTotalView> GetMeteorsTotalAsync(FilterMeteorsDTO filter)
        {
            var meteorPredicate = _predicate.Build(filter);

            var query = await _context.Meteors.Where(meteorPredicate).ToListAsync();

            var meteorsTotal = new MeteorsTotalView()
            {
                Count = query.Count(),
                RowsCount = query.GroupBy(meteor => meteor.Year.Year).Count(),
                Mass = query.Select(meteor => meteor.Mass).Sum()
            };

            return meteorsTotal;
        }
        public async Task<IEnumerable<MeteorView>> GetMeteorsAsync(FilterMeteorsPaginationDTO filter)
        {
            var meteorPredicate = _predicate.Build(filter);

            var query = _context.Meteors.Where(meteorPredicate)
                .GroupBy(meteor => meteor.Year.Year)
                .Select(group => new MeteorView()
                {
                    Year = group.Key,
                    Count = group.Count(),
                    Mass = group.Select(meteor => meteor.Mass).ToArray().Sum()
                });

            query = filter.SortField switch
            {
                "year" => query.OrderBy(group => group.Year, filter.SortAsc),
                "count" => query.OrderBy(group => group.Count, filter.SortAsc),
                "mass" => query.OrderBy(group => group.Mass, filter.SortAsc),
                _ => query
            };

            if (filter.ItemsPerPage > 0 && filter.Page > 0)
            {
                query = query.Skip((filter.Page - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage);
            }


            return await query.ToListAsync();
        }
        public async Task<List<int>> GetExistMeteorIdsAsync()
        {
            return await _context.Meteors.Select(meteor => meteor.Id).ToListAsync();
        }
        public async Task CreateOrUpdateRangeAsync(IEnumerable<MeteorDB> meteors)
        {
            IEnumerable<int> existMeteorIds = await GetExistMeteorIdsAsync();

            var meteorsForAdd = meteors.ExceptBy(existMeteorIds, meteor => meteor.Id);
            var meteorsForUpdate = meteors.IntersectBy(existMeteorIds, meteor => meteor.Id);

            foreach (var meteorForUpdate in meteorsForUpdate)
            {
                await _context.Meteors.AddAsync(meteorForUpdate);
                _context.Entry(meteorForUpdate).State = EntityState.Modified;
            }

            if (meteorsForAdd.Count() > 0)
            {
                await _context.Meteors.AddRangeAsync(meteorsForAdd);
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
