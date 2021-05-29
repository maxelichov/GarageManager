using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        private readonly float r_MaximumBatteryTimeInHours;
        private float m_BatteryTimeRemainingInHours;
        private Dictionary<string, string> m_EngineDetails;
        private List<string> m_EngineInQuestions;

        public ElectricEngine(float i_MaxBatteryTime)
        {
            r_MaximumBatteryTimeInHours = i_MaxBatteryTime;

            m_EngineInQuestions = new List<string>()
           {
               "Battery Time Left: "
           };
        }

        internal void InitEngineDetails(List<string> i_EngineQuestions)
        {
            foreach(string Question in i_EngineQuestions)
            {
                m_EngineDetails.Add(Question, null);
            }
        }


        internal override void IncreaseEnergyLevelInTheEngine(float i_EnergyToAdd)
        {
            if(m_BatteryTimeRemainingInHours + i_EnergyToAdd > r_MaximumBatteryTimeInHours)
            {
                m_BatteryTimeRemainingInHours = r_MaximumBatteryTimeInHours;
                m_CurrentEnergyPercent = 100;
            }
            else
            {
                m_BatteryTimeRemainingInHours += i_EnergyToAdd;
                m_CurrentEnergyPercent = (m_BatteryTimeRemainingInHours / r_MaximumBatteryTimeInHours) * 100;
            }
            
        }

        internal override float GetMaxEnergyAmount()
        {
            return r_MaximumBatteryTimeInHours;
        }

        public override void SetInitialEnergy(float i_BatteryTimeLeft)
        {
            m_BatteryTimeRemainingInHours = i_BatteryTimeLeft;
            m_CurrentEnergyPercent = (m_BatteryTimeRemainingInHours / r_MaximumBatteryTimeInHours) * 100;
        }


    }
}
