namespace WebApp.Interfaces
{
    public interface ISwitchable
    {
        public bool IsOn { get; set; }
        public void TurnOff();
        public void TurnOn();
    }
}