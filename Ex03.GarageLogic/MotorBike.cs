using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class MotorBike : Vehicle
    {
        private eMotorBikeLicesneType m_LicenseType; // not readonly , because it can chnage in the future
        private const int k_NumOfWheels = 2;
        private readonly Dictionary<string, string> m_MotorBikeDetails;
        private readonly List<string> m_MotorBikeQuestions;

        public MotorBike(string i_LicensePlateNumber, Engine i_Engine)
            :base(i_LicensePlateNumber, i_Engine)

        {
            
            m_MotorBikeDetails = new Dictionary<string, string>();
            m_MotorBikeQuestions = new List<string>()
            {
                "License Type: ",
            };
        }

        internal StringBuilder licenseTypeToString()
        {
            StringBuilder licenseStr = new StringBuilder();
            string[] licenseNames = Enum.GetNames(typeof(eMotorBikeLicesneType));
            int  index = 1;

            foreach(string licenseType in licenseNames)
            {
                licenseStr.Append(index++ + ". ");
                licenseStr.Append(licenseType);
                licenseStr.Append(Environment.NewLine);
            }

            return licenseStr;
        }

        private void InitMotorBikeDetails(List<string> i_MotorBikeQuestions)
        {
            foreach (string Question in i_MotorBikeQuestions)
            {
                m_MotorBikeDetails.Add(Question, null);
            }
        }


        public override Dictionary<string, string> GetSpecificKeyVehicleQuestions()
        {
            m_SpecificKeyQuestions.Add("License Type: ", LicenseTypeOptions());
            Engine.InitEngineSpesificKeyQuestions(m_SpecificKeyQuestions);
            return m_SpecificKeyQuestions;
        }

        

        public override void ParseSpecificAnswers(string i_Question, string i_Answer)
        {

            int first = (int)Enum.GetValues(typeof(eMotorBikeLicesneType)).Cast<eMotorBikeLicesneType>().First();
            int last = (int)Enum.GetValues(typeof(eMotorBikeLicesneType)).Cast<eMotorBikeLicesneType>().Last();
            int licenseNumber;
            float amountOfEnergy;

            switch (i_Question)
            {
                case "License Type: ":
                    {
                        if (int.TryParse(i_Answer, out licenseNumber) == false)
                        {
                            throw new FormatException("The input is logicali invalid.");

                        }

                        else if (!(int.Parse(i_Answer) >= first && int.Parse(i_Answer) <= last))
                        {
                            throw new ValueOutOfRangeException(int.Parse(i_Answer), first, last);
                        }

                        
                        m_LicenseType = (eMotorBikeLicesneType)int.Parse(m_SpecificVehicleDetails["License Type: "]);
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
            return string.Format(@"{0}
License type:                      {1}",
base.ToString(),
m_LicenseType.ToString());

        }

        private void initSpecificMotorBikeDetails(List<string> i_SpecificMotorBikeInformation)
        {


            foreach (string StringKey in i_SpecificMotorBikeInformation)
            {
                m_SpecificVehicleDetails.Add(StringKey, null);
            }
        }


        public enum eMotorBikeLicesneType
        {
            A = 1,
            B = 2,
           AA = 3,
           BB = 4

        }


        public  string LicenseTypeOptions()
        {
            string[] licenses = Enum.GetNames(typeof(eMotorBikeLicesneType));
            StringBuilder licenseTypeStr = new StringBuilder();
            int index = 1;
            licenseTypeStr.Append("Enter License Type: ");
            licenseTypeStr.AppendLine();


            foreach (string license in licenses)
            {
                licenseTypeStr.Append(index++);
                licenseTypeStr.Append(". ");
                licenseTypeStr.Append(license);
                licenseTypeStr.AppendLine();

            }

            return licenseTypeStr.ToString();
        }

    }
}
