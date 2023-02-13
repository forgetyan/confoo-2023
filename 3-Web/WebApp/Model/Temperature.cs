namespace WebApp.Model
{
    public class Temperature
    {
        public bool AlarmHigh { get; set; }
        public bool AlarmLow { get; set; }
        public bool IsActive { get; set; }
        public double TemperatureCelcius { get; set; }
    }
}