using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        
        private  const int k_NumOfWheels = 4;
        private  eCarColor m_CarColor;
        private  eNumOfDoors m_NumOfDoors;

        private readonly  Dictionary<string, string> m_CarDetails;
        
        

        public Car(string i_LicenseNumber, Engine i_Engine)
            
            :base(i_LicenseNumber, i_Engine)
                   

        {
            m_CarDetails = new Dictionary<string, string>();

        }

        public override Dictionary<string,string> GetSpecificKeyVehicleQuestions()
        {
            m_SpecificKeyQuestions.Add("Car Color: ", CarColorOptions());
            m_SpecificKeyQuestions.Add("Number Of Doors: ", "Enter Number Of Doors In The Car (Min:2 , Max:5) : ");
            Engine.InitEngineSpesificKeyQuestions(m_SpecificKeyQuestions);
            return m_SpecificKeyQuestions;
        }

      
        public override void ParseSpecificAnswers(string i_Question, string i_Answer)
        {
            int firstColor = (int)Enum.GetValues(typeof(eCarColor)).Cast<eCarColor>().First();
            int lastColor = (int)Enum.GetValues(typeof(eCarColor)).Cast<eCarColor>().Last();
            int firstDoor = (int)Enum.GetValues(typeof(eNumOfDoors)).Cast<eNumOfDoors>().First();
            int lastDoor = (int)Enum.GetValues(typeof(eNumOfDoors)).Cast<eNumOfDoors>().Last();
            int UsersColorChoise;
            int UserNumOfDoorsChoise;
            float amountOfEnergy;

                switch (i_Question)
                {
                    case "Car Color: ":
                        {
                            if(int.TryParse(m_SpecificVehicleDetails["Car Color: "], out UsersColorChoise) == false) 
                            {
                                throw new FormatException("The input should be a positive integer");
                            }

                            else if(!(UsersColorChoise >= firstColor && UsersColorChoise <= lastColor))
                            {
                                throw new ValueOutOfRangeException(UsersColorChoise, firstColor, lastColor);
                            }

                            
                            m_CarColor = (eCarColor)int.Parse(m_SpecificVehicleDetails["Car Color: "]);
                            
                            break;
                        }

                    case "Number Of Doors: ":
                        {
                        
                             UserNumOfDoorsChoise = int.Parse(i_Answer);

                            if (!(UserNumOfDoorsChoise >= firstDoor && UserNumOfDoorsChoise <= lastDoor))
                            {
                                throw new ValueOutOfRangeException(UserNumOfDoorsChoise, firstDoor, lastDoor);
                            }

                            m_NumOfDoors = (eNumOfDoors)int.Parse(m_SpecificVehicleDetails["Number Of Doors: "]);
                            break;
                        }

                case "Litters Of Fuel In The Tank: ":
                    {

                        if (float.TryParse(i_Answer, out amountOfEnergy) == false)
                        {
                            throw new FormatException("The input is logicali invalid.");

                        }

                        if (float.Parse(i_Answer) < 0 || float.Parse(i_Answer) > m_Engine.GetMaxEnergyAmount())
                        {
                            throw new ValueOutOfRangeException(float.Parse(i_Answer), 0, m_Engine.GetMaxEnergyAmount());

                        }

                        m_Engine.SetInitialEnergy(float.Parse(i_Answer));
                        break;
                    }

                case "Battery Time Left: ":
                    {
                        if (float.TryParse(i_Answer, out amountOfEnergy) == false)
                        {
                            throw new FormatException("The input is logicali invalid.");

                        }

                        if (float.Parse(i_Answer) < 0 || float.Parse(i_Answer) > m_Engine.GetMaxEnergyAmount())
                        {
                            throw new ValueOutOfRangeException(float.Parse(i_Answer), 0, m_Engine.GetMaxEnergyAmount());

                        }

                        m_Engine.SetInitialEnergy(float.Parse(i_Answer));
                        break;
                    }
            }
            
        }


        public override string ToString()
        {
            return string.Format(@" 
{0}
Car color:                        {1}
Number of car doors:              {2}",
base.ToString(),
m_CarColor.ToString(),
m_NumOfDoors.ToString());

        }


        public enum eCarColor
        {
            Red = 1,
            Silver = 2, 
            White = 3,
            Black = 4
        }

        public enum eNumOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        public override string CarColorOptions()
        {
            string[] colors = Enum.GetNames(typeof(eCarColor));
            StringBuilder strColorOptions = new StringBuilder();
            int index = 1;
            strColorOptions.Append("Enter Car Color: ");
            strColorOptions.AppendLine();
           
            foreach (string color in colors)
            {
                strColorOptions.Append(index++);
                strColorOptions.Append(". ");
                strColorOptions.Append(color);
                strColorOptions.AppendLine();
            }

            return strColorOptions.ToString();
        }
    }
}
