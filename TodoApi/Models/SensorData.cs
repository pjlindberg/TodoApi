namespace TodoApi.Models
{
    public class SensorData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public ushort Soil { get; set; }
        public ushort Motion { get; set; }
    }
}