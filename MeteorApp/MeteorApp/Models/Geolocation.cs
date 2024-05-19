namespace MeteorApp.Models
{
    public class Geolocation
    {
        public string Type { get; set; }
        public IEnumerable<double> Coordinates { get; set; }
    }
}
