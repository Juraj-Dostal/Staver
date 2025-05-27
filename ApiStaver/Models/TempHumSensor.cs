namespace ApiStaver.Models
{

    /*
     * This class is used to represent a temperature and humidity sensor DTH11.
     * It contains properties for the sensor's ID, temperature, humidity, datetime and Location.
     */
    public class TempHumSensor
    {

        public int Id { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }

    }

    public enum Location
    {
        LivingRoom,
        Kitchen,
        BedroomN,
        BedroomE,
        BedroomS,
        BathroomUp,
        BathroomDown,
        Office,
        Garage,
    }
}
