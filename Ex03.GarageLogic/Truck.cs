using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelType = Ex03.GarageLogic.FuelEngine.eFuelType;


namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool  m_IsCarryingHazardousMaterials;
        private float m_MaxCarryingWeight; // not readonly beacse it can change in the future.
        private const int k_NumberOfWheels = (int)Wheel.eNumberOfWheels.Truck;
        
        public Truck(string i_LicensePlateNumber, Engine i_Engine)
            :base(i_LicensePlateNumber, i_Engine)
        {
            
        }


        public override string ToString()
        {
            return string.Format(@"
{0}
Carrying hazardous materials:     {1}
Max cargo volume:                 {2}",
base.ToString(),
m_IsCarryingHazardousMaterials.ToString(),
m_MaxCarryingWeight);
        }


        internal static List<string> GetTruckQuestions()
        {
            List<string> truckQuestions = new List<string>()
            {
                "Carrying Hazardous Matirials: ",
                "Max Cargo Weight: "

            };

            return truckQuestions;
        }

        public override void ParseSpecificAnswers(string i_Question, string i_Answer)
        {
            bool isCarryingHazardousMatirials;
            float currentLittersInTheTank;

            switch (i_Question)
            {
                case "Carrying Hazardous Matirials: ":
                    {
                        if (bool.TryParse(i_Answer, out isCarryingHazardousMatirials) == false)
                        {
                            throw new FormatException("The input is logicali invalid.");

                        }

                        m_IsCarryingHazardousMaterials = isCarryingHazardousMatirials;
                        break;
                    }

                case "Litters Of Fuel In The Tank: ":
                    {

                        if (float.TryParse(i_Answer, out currentLittersInTheTank) == false)
                        {
                            throw new FormatException("The input is logicali invalid.");

                        }

                        if (float.Parse(i_Answer) < 0 || float.Parse(i_Answer) > m_Engine.GetMaxEnergyAmount())
                        {
                            throw new ValueOutOfRangeException(float.Parse(i_Answer), 0, m_Engine.GetMaxEnergyAmount());

                        }

                        m_Engine.SetInitialEnergy(currentLittersInTheTank);
                        break;
                    }

                    case "Max Cargo Weight: ":
                    {
                        if (float.TryParse(i_Answer, out currentLittersInTheTank) == false)
                        {
                            throw new FormatException("The input is logicali invalid.");

                        }

                        m_MaxCarryingWeight = float.Parse(i_Answer);
                        break;
                    }
            }

        }


        public override Dictionary<string, string> GetSpecificKeyVehicleQuestions()
        {
            m_SpecificKeyQuestions.Add("Carrying Hazardous Matirials: ", getHazardousMatirialsQuestion());
            m_SpecificKeyQuestions.Add("Litters Of Fuel In The Tank: ", "Enter Current Litters Of Fuel In The Tank: ");
            m_SpecificKeyQuestions.Add("Max Cargo Weight: ", "Enter Max Cargo Weight: ");
            
            return m_SpecificKeyQuestions;
        }

        private string getHazardousMatirialsQuestion()
        {
            return "Enter 'true' If The Truck Is Carrying Hazardous Matirials , Enter 'false' Other Wise: ";
        }

    }
}
