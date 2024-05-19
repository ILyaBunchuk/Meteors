namespace MeteorApp.Models
{
    public class MeteorDB
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameType { get; set; }
        public string Recclass { get; set; }
        public double Mass { get; set; }
        public string Fall { get; set; }
        public DateTime Year { get; set; }
        public double Reclat { get; set; }
        public double Reclong { get; set; }
        public string GeolocationJson { get; set; }
    }
}
