using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuel = Ex03.GarageLogic.FuelEngine.eFuelType;

namespace Ex03.GarageLogic
{
    public class Garage
    {

       private readonly VehicleGenerator m_VehicleGenerator;
       private readonly Dictionary<string, Vehicle> m_VehiclesInTheGarage;





        public Garage()
        {
            m_VehiclesInTheGarage = new Dictionary<string, Vehicle>();
            m_VehicleGenerator = new VehicleGenerator();
        }

        public enum eQuestion
        {
            General = 1,
            Specific = 2
        }

        public enum eVehicleType
        {
            FuelCar = 1,
            ElectricCar = 2,
            FuelMotorBike = 3,
            ElectricMotorBike = 4,
            Truck = 5
        }

        internal enum eVehicleStatus
        {
            InRepair = 1,
            Fixed = 2,
            Paid = 3
        }

        public Vehicle CreateNewVehicle(string i_LicensePlateNumber, eVehicleType i_VehicleType)
        {

            Vehicle NewVehicleCreated = m_VehicleGenerator.GenerateVehicle(i_LicensePlateNumber, i_VehicleType);
            return NewVehicleCreated;
        }

        public string AddVehicleToGarage(string i_LicensePlateNumber, Vehicle i_Vehicle)
        {
         
                m_VehiclesInTheGarage.Add(i_LicensePlateNumber, i_Vehicle);
                return string.Format("Vehilce With License Plate Number {0} Has Seccesfuly Entered The Garage.", i_LicensePlateNumber);
        }

        public bool CheckIfVehicleIsInTheGarageAndChangeStatusIfTrue(string i_LicensePlateNumber)
        {
            bool vehicleFound = false;

            if (m_VehiclesInTheGarage.ContainsKey(i_LicensePlateNumber) == true)
            {
                m_VehiclesInTheGarage[i_LicensePlateNumber].Status = eStatusInTheGarage.InRepair;
                vehicleFound = true;
            }

            return vehicleFound;
        }

        public static void CheckValidationAndSetAnswers(string i_Question, string i_UserAnswer,
                                                                               Vehicle i_VehicleInProcess,eQuestion i_QuestionType )      
        {
            if(i_QuestionType == eQuestion.General)
            {
                i_VehicleInProcess.ParseGeneralAnswers(i_Question, i_UserAnswer);
            }
            else
            {
                i_VehicleInProcess.ParseSpecificAnswers(i_Question, i_UserAnswer);
            }
        }

        public void AddEnergyToVehicle(string i_LicensePlateNumber, float i_EnergyToAdd)
        {
            Vehicle vehicle = m_VehiclesInTheGarage[i_LicensePlateNumber];

            if((vehicle.Engine is ElectricEngine))
            {
                vehicle.AddTimeToBattery(i_EnergyToAdd);
            }
            else
            {
                vehicle.AddFuelToEngine(i_EnergyToAdd);
            }
            
        }

        public void ChangeVehicleStatus(string i_licensePlateNumber,eStatusInTheGarage i_newstatus)
        {
            Vehicle vehicle = m_VehiclesInTheGarage[i_licensePlateNumber];

            //validation

            vehicle.Status = i_newstatus;
        }

        public static StringBuilder DisplayGarageStatusOptions()
        { 

            StringBuilder statusString = new StringBuilder();
            statusString.AppendLine();
            string[] statusNames = Enum.GetNames(typeof(eStatusInTheGarage));
            int  index = 1;

            foreach (string status in statusNames)
            {
                statusString.Append(index++);
                statusString.Append(". ");
                statusString.Append(status);
                statusString.AppendLine();

            }
            return statusString;
        }

        public enum eStatusInTheGarage
        {
            InRepair = 1,
            Fixed = 2,
            Paid = 3
        }

        public StringBuilder GetVehicleDetails(string i_LicensePlateNumber)
        {
            Vehicle vehicle = m_VehiclesInTheGarage[i_LicensePlateNumber];
            StringBuilder vehicleDetails = new StringBuilder(vehicle.ToString());
            return vehicleDetails;
        }

        public bool CheckIfVehicleIsInTheGarage(string i_LicensePlateNumber)
        {
            bool vehiceInTheGarage = false;

            if(m_VehiclesInTheGarage.ContainsKey(i_LicensePlateNumber) == true)
            {
                vehiceInTheGarage = true;
            }

            return vehiceInTheGarage;
        }

        public string InflateVehicleWheelsToMax(string i_LicensePlateNumber)
        {
            Vehicle vehicle = m_VehiclesInTheGarage[i_LicensePlateNumber];
            string msg = vehicle.InflateVehicleWheelsToMax();
            return msg;
        }

        public StringBuilder ShowAllLicensePlatesInTheGarage()
        {
            StringBuilder licensePlates = new StringBuilder();
            int index = 1;

           foreach(string licensePlate in m_VehiclesInTheGarage.Keys)
            {
                licensePlates.Append(index++);
                licensePlates.Append(". ");
                licensePlates.Append(licensePlate);
                licensePlates.AppendLine();
            }

            return licensePlates;
        }




        

    public void CheckingFuelTypeInput(string i_LicensePlateNumber, string i_FuelType)
        {
            Vehicle vehicleToCheck;

            m_VehiclesInTheGarage.TryGetValue(i_LicensePlateNumber, out vehicleToCheck);

            if (vehicleToCheck.Engine is FuelEngine)
            {
                FuelEngine FuelTankTochek = vehicleToCheck.Engine as FuelEngine;

                FuelEngine.eFuelType fuelTypeToFill = ParseFromString(i_FuelType);

                if(FuelTankTochek.FuelType != fuelTypeToFill)
                {
                    fuelTypeToFill.ToString();
                    throw new FormatException(string.Format("Vehicle with license number {0} does not support {1} fuel type.", i_LicensePlateNumber,
                                                 fuelTypeToFill));
                }
            }
        }


        public static Fuel ParseFromString(string i_NumToParse)
        {
            int userInput = int.Parse(i_NumToParse);
            int firstFuelType = (int)Enum.GetValues(typeof(Fuel)).Cast<Fuel>().First();
            int lastFuelType = (int)Enum.GetValues(typeof(Fuel)).Cast<Fuel>().Last();

            if((userInput >= firstFuelType && userInput <= lastFuelType) == false)
            {
                throw new ValueOutOfRangeException(userInput, firstFuelType, lastFuelType);
            }

            return (Fuel)userInput;
        }



        public static string GetFuelType()
        {
            string input = string.Empty;
            Console.WriteLine(fuleTypesToString());
            input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                throw new FormatException();
            }
            return input;

        }


        private  static StringBuilder fuleTypesToString()
        {
            StringBuilder fuelTypesStr = new StringBuilder();
            string[] fuelTypes = Enum.GetNames(typeof(Fuel));
            int index = 1;

            foreach(string fuleType in fuelTypes)
            {
                fuelTypesStr.Append(index++);
                fuelTypesStr.Append(". ");
                fuelTypesStr.Append(fuleType);
                fuelTypesStr.AppendLine();
            }

            return fuelTypesStr;
        }


    }
}
