using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using Question = Ex03.GarageLogic.Garage.eQuestion;
using StatusInTheGarage = Ex03.GarageLogic.Garage.eStatusInTheGarage;


namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private readonly Garage m_GarageManager = new Garage();
        private const int k_NumberOfOptionsInTheGarage = 8;
        private readonly int m_FirstMenuOption = (int)Enum.GetValues(typeof(eGarageOptions)).Cast<eGarageOptions>().First();
        private readonly int m_LastMenuOption = (int)Enum.GetValues(typeof(eGarageOptions)).Cast<eGarageOptions>().Last();
        private eGarageOptions m_MenuChoise; 
        
            private readonly string[] k_GarageStringOptions = {"1. Insert new vehicle to the garage.",
                                                           "2. Show plate's number of all the vehicle in the garage.",
                                                           "3. Change specific vehicle status.",
                                                           "4. Inflate wheels of specific vehicle to the maximum.",
                                                           "5. Refule a vehicle.",
                                                           "6. Charge an electric vehicle.",
                                                           "7. Show all details for a specific vehicle.",
                                                           "8. Exit." };
        

        internal enum eGarageOptions
        {
            AddNewVehicle = 1,
            ShowAllGarageVehicleLicencePlateNumber = 2,
            ChangeVehicleStatus = 3,
            InflateWheelsOfVehicleToMaximum = 4,
            ReFuleAVehicle = 5,
            ChargeAnElectricVehicle = 6,
            ShowSpecificVehicleDetails = 7,
            Exit = 8
        }

        

        private Garage.eVehicleType displayAndGetVehicleOption()
        {
            Console.Clear();
            bool validChoise = false;
            int VehicleChosenByTheUser = 0;
            int first = (int)Enum.GetValues(typeof(Garage.eVehicleType)).Cast<Garage.eVehicleType>().First();
            int last = (int)Enum.GetValues(typeof(Garage.eVehicleType)).Cast<Garage.eVehicleType>().Last();
            Console.WriteLine("Choose Vehicle Type:");
            displayVehicleTypes();

            do
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out VehicleChosenByTheUser) == false)
                    {
                        throw new IndexOutOfRangeException("The input is logicali invalid.");

                    }

                    else if (!(VehicleChosenByTheUser >= first && VehicleChosenByTheUser <= last))
                    {
                        throw new ValueOutOfRangeException(VehicleChosenByTheUser, first, last);
                    }

                    validChoise = true;
                }

                catch(IndexOutOfRangeException execption)
                {
                    Console.WriteLine(execption.Message);
                    Console.Write("Enter Choise: ");
                    
                }
                
                catch(ValueOutOfRangeException execption)
                {
                    Console.WriteLine(execption.Message);
                    Console.Write("Enter Choise: ");
                }


            } while (validChoise == false);

            return (Garage.eVehicleType)VehicleChosenByTheUser;
        }

        private void displayVehicleTypes()
        {
            StringBuilder vehicleTypesToDisplay = new StringBuilder();

            string[] vehicleAsString  = Enum.GetNames(typeof(Garage.eVehicleType));
            int index = 1;

            foreach (string vehicleType in vehicleAsString)
            {
                vehicleTypesToDisplay.Append(index++);
                vehicleTypesToDisplay.Append(". ");
                vehicleTypesToDisplay.Append(vehicleType);
                vehicleTypesToDisplay.Append(Environment.NewLine);
            }

            Console.WriteLine(vehicleTypesToDisplay);
        }

        private void showGarageMenu()
        {
            Console.Clear();
            Thread.Sleep(150);
            Console.WriteLine("Choose one of the options below:\n");

            foreach (string garageOptions in k_GarageStringOptions)
            {
                Console.WriteLine(garageOptions);
            }
        }

        private eGarageOptions getAndCehckGarageOption()
        {
            newLine();
            bool validMenuChoise = false;
            int menuChoise = 0;

            do
            {
                Console.Write("Menu choise: ");

                try
                {
                    if (int.TryParse(Console.ReadLine(), out menuChoise) == false)
                    {
                        //input logic is invalid
                        throw new FormatException();
                    }


                    else if (menuChoise < m_FirstMenuOption || menuChoise > m_LastMenuOption)
                    {
                        //input range is invalid.
                        throw new ValueOutOfRangeException(menuChoise, 1, k_NumberOfOptionsInTheGarage);
                    }

                    validMenuChoise = true;
                }

                catch (FormatException)
                {
                    string msg = string.Format(@"Invalid input.the input should be between {0} - {1}", 1, k_NumberOfOptionsInTheGarage);
                    Console.WriteLine(msg);
                }

                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }

                m_MenuChoise = (eGarageOptions)menuChoise;

            } while (validMenuChoise == false);

            return m_MenuChoise;
        }

        public void EnterGarage()
        {
            Console.WriteLine("Welcome to Max and Niv's Garage!\n");
            Thread.Sleep(500);
            Console.Clear();
            showGarageMenu();
            eGarageOptions menuChoise = getAndCehckGarageOption();
            applyMenuChoise(menuChoise);

        }

        private string getLincenseNumberFromUser()
        {
            
            Console.Write("Enter Vehicle License Plate Number: ");
            string licenseNumber = Console.ReadLine();

            while(string.IsNullOrEmpty(licenseNumber))
            {
                Console.WriteLine("Invalid Licesnse Number.");
                Console.Write("Licesne Number: ");
                licenseNumber =  Console.ReadLine();
            }

            return licenseNumber;
        }

        private void applyMenuChoise(eGarageOptions i_MenuChoise)
        {
            Console.Clear();
           
            while(i_MenuChoise != eGarageOptions.Exit)
            {
                switch (i_MenuChoise)
                {

                    case eGarageOptions.AddNewVehicle:
                        {
                            addNewVehicleToGarage(); 
                            break;
                        }

                    case eGarageOptions.ChangeVehicleStatus:
                        {
                            changeVehcileStatusInTheGarage();
                            break;
                        }

                    case eGarageOptions.ChargeAnElectricVehicle:
                        {
                            chargeAnElectricVehicle();
                            break;
                        }
                    case eGarageOptions.ReFuleAVehicle:
                        {
                            refuelVehicle();
                            break;
                        }
                    case eGarageOptions.ShowAllGarageVehicleLicencePlateNumber:
                        {
                            showLicensePlatesInTheGarage();
                            break;
                        }
                    case eGarageOptions.ShowSpecificVehicleDetails:
                        {
                            showVehicleDetails();
                            break;
                        }

                    case eGarageOptions.InflateWheelsOfVehicleToMaximum:
                        {
                            inflatVehicleWheelsToMax();
                            break;
                        }

                    case eGarageOptions.Exit:
                        {
                            
                            break;
                        }
                }

            }
        }


        private void inflatVehicleWheelsToMax()
        {
            string licensePlateNumber = string.Empty;
            licensePlateNumber = getLincenseNumberFromUser();

            if (m_GarageManager.CheckIfVehicleIsInTheGarage(licensePlateNumber) == true)
            {
                Console.WriteLine(m_GarageManager.InflateVehicleWheelsToMax(licensePlateNumber));
            }
            else
            {
                Console.WriteLine(string.Format("Vehicle with license plate {0} is not in the garage.", licensePlateNumber));
                Thread.Sleep(3500);
            }

            
            
            EnterGarage();
        }


        private void showVehicleDetails()
        {
            string licensePlateNumber = string.Empty;
            licensePlateNumber = getLincenseNumberFromUser();

            if (m_GarageManager.CheckIfVehicleIsInTheGarage(licensePlateNumber) == true)
            {
                Console.WriteLine(m_GarageManager.GetVehicleDetails(licensePlateNumber));
            }
            else
            {
                Console.WriteLine(string.Format("Vehicle with license plate {0} is not in the garage.", licensePlateNumber));
                Thread.Sleep(3500);
            }

            EnterGarage();
        }

        private void showLicensePlatesInTheGarage()
        {
            Console.Clear();
            Console.WriteLine("Licesnse Plates In The Garage:");
            Thread.Sleep(1500);
            Console.WriteLine(m_GarageManager.ShowAllLicensePlatesInTheGarage());
            EnterGarage();
        }

        private void changeVehcileStatusInTheGarage()
        {
            string licensePlateNumber = string.Empty;
            licensePlateNumber = getLincenseNumberFromUser();

            if (m_GarageManager.CheckIfVehicleIsInTheGarage(licensePlateNumber) == true)
            {
                StatusInTheGarage newstatus = getStatusFromUser();
                m_GarageManager.ChangeVehicleStatus(licensePlateNumber, newstatus);
            }
            else
            {
                Console.WriteLine(string.Format("Vehicle with license plate {0} is not in the garage.", licensePlateNumber));
                Thread.Sleep(3500);
            }

            EnterGarage();
        }


        private StatusInTheGarage getStatusFromUser()
        {

            int firstStatus = (int)Enum.GetValues(typeof(StatusInTheGarage)).Cast<StatusInTheGarage>().First();
            int lastStatus = (int)Enum.GetValues(typeof(StatusInTheGarage)).Cast<StatusInTheGarage>().Last();
            int statusChoise = 0;
            bool validInput = false;
            Console.Write("Choose Garage Status: ");
            Console.WriteLine(Garage.DisplayGarageStatusOptions());

            do
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out statusChoise) == false)
                    {
                        throw new FormatException("The input is logicali invalid.");
                    }

                    else if (!(statusChoise >= firstStatus && statusChoise <= lastStatus))
                    {
                        throw new ValueOutOfRangeException(statusChoise, firstStatus, lastStatus);
                    }

                    validInput = true;
                }

                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.Write("Status In The Garage: ");
                }

                catch(ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }

            } while (validInput == false) ;

                return (StatusInTheGarage)statusChoise;
        }           





        private void refuelVehicle()
        {
            string licensePlateNumber = string.Empty;
            licensePlateNumber = getLincenseNumberFromUser();
            
   
            try
            {
                if (m_GarageManager.CheckIfVehicleIsInTheGarage(licensePlateNumber) == true)
                {
                    Console.WriteLine("Choose Fuel Type: ");
                    float amountOfFuel = getAmountOfFuelToAdd();
                    string fuelType = Garage.GetFuelType();
                    m_GarageManager.CheckingFuelTypeInput(licensePlateNumber, fuelType);
                    m_GarageManager.AddEnergyToVehicle(licensePlateNumber, amountOfFuel);
                }

                else
                {
                    Console.WriteLine(string.Format("Vehicle with license plate {0} is not in the garage.", licensePlateNumber));
                    Thread.Sleep(3500);
                }

            }
            catch(FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }

            EnterGarage();
        }


        private void chargeAnElectricVehicle()
        {
            string licensePlateNumber = string.Empty;
            licensePlateNumber = getLincenseNumberFromUser();

            if (m_GarageManager.CheckIfVehicleIsInTheGarage(licensePlateNumber) == true)
            {
                
                float amountToCharge = getAmountOfTimeCharge();
                m_GarageManager.AddEnergyToVehicle(licensePlateNumber, amountToCharge);
            }
            else
            {
                Console.WriteLine(string.Format("Vehicle with license plate {0} is not in the garage.", licensePlateNumber));
                Thread.Sleep(3500);
            }

            
            EnterGarage();
        }

        private float getAmountOfFuelToAdd()
        {
            float amountOfFuelToAdd = 0;
            bool validInput = false;
            Console.Write("Amount Of Fuel To Add: ");

            do
            {
                try
                {
                    if (float.TryParse(Console.ReadLine(), out amountOfFuelToAdd) == false)
                    {
                        throw new FormatException("The input is logicali invalid.");
                    }

                    validInput = true;
                }

                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.Write("Amount Of Fuel To Add: ");
                }

            } while (validInput == false);

            return amountOfFuelToAdd;
        }

        private float getAmountOfTimeCharge()
        {
            float amountToCharge = 0;
            bool validInput = false;
            Console.Write("Amount Of Time To Charge: ");

            do
            {
                try
                {
                    if (float.TryParse(Console.ReadLine(), out amountToCharge) == false)
                    {
                        throw new FormatException("The input is logicali invalid.");
                    }

                    validInput = true;
                }

                catch(FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.Write("Amount Of Time To Charge: ");
                }

            } while (validInput == false);

            return amountToCharge;
      }

        private void addNewVehicleToGarage()
        {
            string licensePlateNumber = string.Empty;
            Garage.eVehicleType vehicleType;
            licensePlateNumber = getLincenseNumberFromUser();

            if (m_GarageManager.CheckIfVehicleIsInTheGarageAndChangeStatusIfTrue(licensePlateNumber))
            {

                Console.WriteLine("Vehicle Is Already In The Garage. Vehicle Status Chnaged To 'In Repair'.");
                Thread.Sleep(3500);
            }

            else
            {
                vehicleType = displayAndGetVehicleOption();

                Vehicle newVehicleToInsert = m_GarageManager.CreateNewVehicle(licensePlateNumber, vehicleType);
                getVehicleDetailsFromUser(newVehicleToInsert);


                Console.Clear();
                Console.WriteLine(m_GarageManager.AddVehicleToGarage(licensePlateNumber, newVehicleToInsert));
                Thread.Sleep(3500);
            }
            
            EnterGarage();
        }

        private static void getVehicleDetailsFromUser(Vehicle i_newVehicleToInsert)
        {
            Console.Clear();
           
            Dictionary<string, string> genericVehicleQuestions =  i_newVehicleToInsert.GeneralVehicleQuestions;
            Dictionary<string, string>  vehicleAnswers = i_newVehicleToInsert.GetVehicleAnswers;

            Dictionary<string,string> specificVehicleQuesiotns = i_newVehicleToInsert.GetSpecificKeyVehicleQuestions();
            Dictionary<string, string> SpecificVehicleAnswers = i_newVehicleToInsert.GetSpecificVehicleAnswers();

            Console.WriteLine("Enter Vehicle Details:");

            Thread.Sleep(500);

            displayQuestionsAndGetAnswers(genericVehicleQuestions, vehicleAnswers, i_newVehicleToInsert,
                Question.General);
                
            displayQuestionsAndGetAnswers(specificVehicleQuesiotns, SpecificVehicleAnswers
                , i_newVehicleToInsert, Question.Specific);
        }

        private  static void displayQuestionsAndGetAnswers(Dictionary<string,string> i_VehicleQuestions,
            Dictionary<string, string> vehicleAnswers, Vehicle i_VehicleInProcces, Question i_QuestionType)
        {
            
            string userAnswer = string.Empty;
            bool validAnswer = false;

            foreach (string keyQuestion in i_VehicleQuestions.Keys)
            {
                validAnswer = false;
                string QuestionToDisplay = string.Empty;
                
                do
                {
                    try
                    {
                        if((i_VehicleQuestions.TryGetValue(keyQuestion, out QuestionToDisplay)))
                        {
                            Console.Write(QuestionToDisplay);
                        }

                        userAnswer = Console.ReadLine();
                        vehicleAnswers[keyQuestion] = userAnswer;
                        Garage.CheckValidationAndSetAnswers(keyQuestion, userAnswer, i_VehicleInProcces, i_QuestionType);
                        validAnswer = true;
                    }

                    catch (FormatException exception)
                    {
                        Console.WriteLine(exception.Message);
                        
                    }
                    catch (ValueOutOfRangeException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }

                } while (validAnswer == false);
                
            }
        }

        private  string getLicesnsePlateFromUser()
        {
            string licensePlate = string.Empty;
            Console.Write("Enter Vehicle License Plate Number: ");
            licensePlate =  Console.ReadLine();

            while (string.IsNullOrEmpty(licensePlate) == true) 
            {
                Console.WriteLine("Invalid License Plate Number.");
                Console.Write("Enter New License Plate Number: ");
                licensePlate = Console.ReadLine();
            }

            return licensePlate;
        }

        private void newLine()
        {
            Console.WriteLine();
        }
    }
}
