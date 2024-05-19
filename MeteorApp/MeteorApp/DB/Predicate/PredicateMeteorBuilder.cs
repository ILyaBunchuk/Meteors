using LinqKit;
using MeteorApp.Models;

namespace MeteorApp.DB.Predicate
{
    public class PredicateMeteorBuilder
    {
        public ExpressionStarter<MeteorDB> Build(FilterMeteorsDTO filter)
        {
            var meteorPredicate = PredicateBuilder.New<MeteorDB>(
                meteor => meteor.Year.Year >= filter.YearFrom);

            if (filter.YearTo > 0)
            {
                meteorPredicate.And(meteor => meteor.Year.Year <= filter.YearTo);
            }            
            
            if (!string.IsNullOrEmpty(filter.Recclass) && !filter.Recclass.Equals("All"))
            {
                meteorPredicate.And(meteor => meteor.Recclass == filter.Recclass);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                meteorPredicate.And(meteor => meteor.Name.Contains(filter.Name));
            }

            return meteorPredicate;
        } 
    }
}
