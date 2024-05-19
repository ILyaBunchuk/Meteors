namespace MeteorApp.Models
{
    public class FilterMeteorsPaginationDTO : FilterMeteorsDTO
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
