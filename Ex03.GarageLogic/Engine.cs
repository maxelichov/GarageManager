using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelType = Ex03.GarageLogic.FuelEngine.eFuelType;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_CurrentEnergyPercent;
        private Dictionary<string, string> m_EngineDetails;
        private readonly List<string> m_EngineInQuestions;


        internal abstract float GetMaxEnergyAmount();

        public Engine()
        {

            m_EngineDetails = new Dictionary<string, string>();
            m_EngineDetails = new Dictionary<string, string>(); 
        }


        public override string ToString()
        {
            string fuelOrTime;
            string littersOrHours;
            string spaces;

            if (this is FuelEngine)
            {
                fuelOrTime = "fule";
                littersOrHours = "liter";
                spaces = "     ";
            }
            else
            {
                fuelOrTime = "battery";
                littersOrHours = "Hours";
                spaces = "  ";
            }

            return string.Format(@"
Current {0} percent:{4}        {1}%
Maximum {0} amount:{4}         {3} {2}",
fuelOrTime,
m_CurrentEnergyPercent,
littersOrHours,
GetMaxEnergyAmount(),
spaces);
        }


        internal void InitEngineSpesificKeyQuestions(Dictionary<string,string> m_SpecificKeyQuestions)
        {
            if (this is FuelEngine)
            {
                   m_SpecificKeyQuestions.Add("Litters Of Fuel In The Tank: ", "Enter Current Litters Of Fuel In The Tank: ");
            }

            else
            {
                m_SpecificKeyQuestions.Add("Battery Time Left: ", "Enter Time Left In The Battery: ");
            }
        }

         internal abstract void IncreaseEnergyLevelInTheEngine(float i_TimeToAdd);
         public abstract void SetInitialEnergy(float i_Amount);

        private void initEngineDetails(List<string> i_EngineInformation)
        {
            foreach(string EngineStringInfo in i_EngineInformation)
            {
                m_EngineDetails.Add(EngineStringInfo, null);
            }
        }
        
    }

}
