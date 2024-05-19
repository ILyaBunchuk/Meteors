namespace MeteorApp.Models
{
    public class FilterMeteorsDTO
    {
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
        public string? Recclass { get; set; }
        public string? Name { get; set; }
        public bool SortAsc { get; set; }
        public string? SortField { get; set; }
    }
}
