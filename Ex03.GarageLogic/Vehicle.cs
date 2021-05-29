using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic = Ex03.GarageLogic.Garage;
using CarColor = Ex03.GarageLogic.Car.eCarColor;
using VehicleStatus = Ex03.GarageLogic.Garage.eStatusInTheGarage;



namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_Model;
        private readonly string r_LicenceNumber;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        protected Engine m_Engine;
        private readonly List<Wheel> m_Wheels;
        private VehicleStatus m_CurrentStatus = VehicleStatus.InRepair;
        protected readonly Dictionary<string, string> m_GeneralVehicleDetails;
        protected readonly Dictionary<string, string> m_SpecificVehicleDetails;
        protected readonly Dictionary<string, string> m_GeneralKeyQuestions;
        protected readonly Dictionary<string, string> m_SpecificKeyQuestions;

      

        public Vehicle(string i_LicensePlateNumber, Engine i_Engine)
        {
            m_Engine = i_Engine;
            r_LicenceNumber = i_LicensePlateNumber;
            m_Wheels = new List<Wheel>();
            m_GeneralVehicleDetails = new Dictionary<string, string>();
            m_SpecificVehicleDetails = new Dictionary<string, string>();
            m_GeneralKeyQuestions = new Dictionary<string, string>();
            m_SpecificKeyQuestions = new Dictionary<string, string>();
            initKeyQuestion();
        }

        private void initKeyQuestion()
        {
            Garage.eVehicleType vehicleType;

            if(this is Car)
            {
                vehicleType = Garage.eVehicleType.ElectricCar;
            }
            else if(this is MotorBike)
            {
                vehicleType = Garage.eVehicleType.ElectricMotorBike;
            }
            else
            {
                vehicleType = Garage.eVehicleType.Truck;
            }
                
                    
            m_GeneralKeyQuestions.Add("Owner Name: ", "Enter Owner Name: ");
            m_GeneralKeyQuestions.Add("Owner Phone Number: ", "Enter Owner Phone Number (10 digits): ");
            m_GeneralKeyQuestions.Add("Vehicle Model Name: ", "Enter Vehicle Model Name: ");
            Wheel.InitWheelsKeyQuestions(m_GeneralKeyQuestions, vehicleType);


            m_GeneralVehicleDetails.Add("Owner Name: ", null);
            m_GeneralVehicleDetails.Add("Owner Phone Number: ", null);
            m_GeneralVehicleDetails.Add("Vehicle Model Name: ", null);
            Wheel.InitWheelsKeyQuestions(m_GeneralVehicleDetails, vehicleType);

        }

        

        public void ParseGeneralAnswers(string i_Qeustion, string i_Answer)
        {
            float validAirPressure;

            switch (i_Qeustion)
            {
                case "Owner Name: ":
                    {
                        m_OwnerName = i_Answer;
                        break;
                    }

                case "Owner Phone Number: ":
                    {
                        m_OwnerPhoneNumber = i_Answer;
                        break;
                    }
                case "Vehicle Model Name: ":
                    {
                        m_Model = i_Answer;
                        break;
                    }

                case "Wheels Manufacturer Name: ":
                    {
                       
                        initWheelsManufaturerName(i_Answer);
                        break;
                    }

                case "Current Air Presure: ":
                    {
                        //todo : validation
                        if(float.Parse(i_Answer) < 0 || float.Parse(i_Answer) > m_Wheels[0].MaxAirPressure)
                        {
                            throw new ValueOutOfRangeException(float.Parse(i_Answer), 0, m_Wheels[0].MaxAirPressure);
                        }

                        if(float.TryParse(i_Answer, out validAirPressure) == false)
                        {
                            throw new FormatException(string.Format("Air pressure should bee a positive number between 0 - {0}", m_Wheels[0].MaxAirPressure));
                        }

                        initWheelsCurrentAirPressure(float.Parse(i_Answer));
                        break;
                    }     
            }
        }

        public void AddTimeToBattery(float i_TimeToAdd)
        {
            m_Engine.IncreaseEnergyLevelInTheEngine(i_TimeToAdd);
        }
        

        private void initWheelsCurrentAirPressure(float i_CurrentAirPressure)
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.AirPressure = i_CurrentAirPressure;
            }
        }

        private void initWheelsManufaturerName(string i_Answer)
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.Manufacturer = i_Answer;
            }
        }

        public string GetGeneralAnswer(string i_Question)
        {
            return m_GeneralVehicleDetails[i_Question];
        }

        public void AddFuelToEngine(float i_LittersToAdd)
        {
            m_Engine.IncreaseEnergyLevelInTheEngine(i_LittersToAdd);
        }

        public abstract void ParseSpecificAnswers(string i_Question, string i_Answer);
        
        public VehicleStatus Status
        {
            get
            {
                return m_CurrentStatus;
            }
            set
            {
                m_CurrentStatus = value;
            }
        }

        public   Dictionary<string, string> Answers
        {
            get
            {
                return m_GeneralVehicleDetails;
            }
        }

        private void checkPercentOfEnergyRemainingInput(string i_userInput)
        {
            float userInputAsFloat;

            if(float.TryParse(i_userInput, out userInputAsFloat) == false)
            {
                throw new FormatException("Input should be a positive number");
            }

            if (!(userInputAsFloat >= (int)Energy.MinEnergyLevel && userInputAsFloat <= (int)Energy.MaxEnergyLevel))
            {
                throw new ValueOutOfRangeException(userInputAsFloat, (int)Energy.MinEnergyLevel, (int)Energy.MaxEnergyLevel);
            }
        }

       

        public virtual Dictionary<string, string> GetSpecificKeyVehicleQuestions()
        {
            
            return m_SpecificKeyQuestions;
        }


        public override string ToString()
        {
            string energySource;
            string spaces;

            if (m_Engine is FuelEngine)
            {
                energySource = "Fule";
                spaces = "   ";
            }
            else
            {
                energySource = "Battery";
                spaces = "";
            }

            return string.Format(@"
Licennse number:                  {0}
Owner name:                       {1}
Owner phone Number:               {2}
Vehicle status:                   {3}
Model name:                       {4}
                                  {6}{7}
{8}",
r_LicenceNumber,
m_OwnerName,
m_OwnerPhoneNumber,
m_CurrentStatus.ToString(),
m_Model,
energySource,
m_Engine.ToString(),
m_Wheels[0].ToString(),
spaces
);
        }


         internal string InflateVehicleWheelsToMax()
         {
           foreach(Wheel wheelToInflate in m_Wheels)
            {
                wheelToInflate.AirPressure = wheelToInflate.MaxAirPressure;
            }

            return string.Format("All The Wheels Of Vehicle With License Plate Number {0} had been Inflated To Maximum", r_LicenceNumber);
        }


        public virtual string CarColorOptions()
        {
            return null;
        }

        public string GetColorQuestion()
        {

            string colorQuestion = "Car Color: ";
            return colorQuestion;
        }

        public Dictionary<string, string>  GetSpecificVehicleAnswers()
        {
            return m_SpecificVehicleDetails;
        }

        public Dictionary<string, string> GetVehicleAnswers
        {
            get
            {
                return m_GeneralVehicleDetails;
            }
        }

        public Dictionary<string, string> GeneralVehicleQuestions
        {
            get
            {
                return m_GeneralKeyQuestions;
            }

        }

        public Dictionary<string,string> SpecificVehicleQuestions
        {
            get
            {
                return m_SpecificKeyQuestions;
            }

        }

        public string Model
        {
            get
            {
                return m_Model;
            }
        }

        public string LicensePlateNumber
        {
            get
            {
                return r_LicenceNumber;
            }
        }


        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }
            set
            {
                m_OwnerPhoneNumber = value;
            }
        }

        internal List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
            
        }

        internal Engine Engine
        {
            get
            {
                return m_Engine;
            }

            set
            {
                m_Engine = value;
            }
        }
         
        internal enum Energy
        {
            MinEnergyLevel = 0,
            MaxEnergyLevel = 100
        }

    }
}
