using FluentValidation;
using MeteorApp.Models;

namespace MeteorApp.Validation
{
    public class FilterMeteorsValidator : AbstractValidator<FilterMeteorsPaginationDTO>
    {
        public FilterMeteorsValidator()
        {
            RuleFor(x => x.YearFrom).InclusiveBetween(0, DateTime.Now.Year);
            RuleFor(x => x.YearTo).InclusiveBetween(0, DateTime.Now.Year);
            RuleFor(x => x.Name).Length(0, 45);
            RuleFor(x => x.Recclass).Length(0, 45);
            RuleFor(x => x.ItemsPerPage).GreaterThanOrEqualTo(-1);
            RuleFor(x => x.Page).GreaterThan(0);
        }
    }
}
