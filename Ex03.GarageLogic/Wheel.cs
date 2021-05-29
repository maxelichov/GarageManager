using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private  string m_Manufacturer;
        private readonly float r_MaxAirPressureInTheWheels;
        private float m_CurrentPresure;
        private Dictionary<string, string> m_WheelDetails;

        public string ToString()
        {
            return string.Format(@"
Wheels information: 
Manufacturer name:                {0}
Current air pressure:             {1}
Maximum manufacturer air pressure:{2}",
m_Manufacturer,
m_CurrentPresure,
r_MaxAirPressureInTheWheels);

        }

        internal float AirPressure
        {
            get
            {
                return m_CurrentPresure;
            }

            set
            {
                m_CurrentPresure = value;
            }
        }   

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressureInTheWheels = i_MaxAirPressure;
            m_WheelDetails = new Dictionary<string, string>();
        }

        internal static List<string> GetWheelQuestions()
        {
            List<string> WheelQuestions = new List<string>()
            {
                "Wheels Manufacturer Name: ",
                "Current Air Presure: "
            };

            return WheelQuestions;

        }

        internal string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }

            set
            {
                m_Manufacturer = value;
            }
        }

        private void initWheelsDetails(List<string> i_WheelQuestions)
        {
            foreach(string Question in i_WheelQuestions)
            {
                m_WheelDetails.Add(Question, null);
            }
        }


        public enum eNumberOfWheels
        {
            MotorBike = 2,
            Car = 4,
            Truck = 16
        }


        public enum eMaxAirPressure
        {
            MotorBike = 30,
            Car = 32,
            Truck = 26
        }

        internal static void InitWheelsKeyQuestions(Dictionary<string,string>  i_GeneralKeyQuestions , Garage.eVehicleType i_VehicleType)
        {
            float maxAirPressure = 0;

            if (i_VehicleType == Garage.eVehicleType.FuelCar || i_VehicleType == Garage.eVehicleType.ElectricCar)
            {
                maxAirPressure = (float)eMaxAirPressure.Car;
            }

            else if(i_VehicleType == Garage.eVehicleType.FuelMotorBike || i_VehicleType == Garage.eVehicleType.ElectricMotorBike)
            {
                maxAirPressure = (float)eMaxAirPressure.MotorBike;
            }

            else
            {
                maxAirPressure = (float)eMaxAirPressure.Truck;
            }

            i_GeneralKeyQuestions.Add("Wheels Manufacturer Name: ", "Enter Wheels Manufacturer Name: ");
            i_GeneralKeyQuestions.Add("Current Air Presure: ", string.Format("Enter Current Air Presure (should be between 0 - {0}) : ", maxAirPressure));
             
        }

        internal Dictionary<string, string> GetWheelsDetails()
        {
            return m_WheelDetails;
        }

        internal float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressureInTheWheels;
            }
        }


    }
}
