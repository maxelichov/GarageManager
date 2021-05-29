using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly float r_FuelTankCapacity;
        private readonly eFuelType r_FuelType;
        private float m_CurrentAmountOfLittersInTheTank;
        private Dictionary<string, string> m_EngineDetails;
        private List<string> m_EngineInQuestions;


        public FuelEngine(eFuelType i_FuelType, float i_FuelTankCapacity)
        {
            r_FuelType = i_FuelType;
            r_FuelTankCapacity = i_FuelTankCapacity;
        
            m_EngineDetails = new Dictionary<string, string>();
            m_EngineInQuestions = new List<string>();
        }

        internal override void IncreaseEnergyLevelInTheEngine(float i_EnergyToAdd)
        {
           
            if (m_CurrentAmountOfLittersInTheTank + i_EnergyToAdd > r_FuelTankCapacity)
            {
                m_CurrentAmountOfLittersInTheTank = r_FuelTankCapacity;
                m_CurrentEnergyPercent = 100;
            }
            else
            {
                m_CurrentAmountOfLittersInTheTank += i_EnergyToAdd;
                m_CurrentEnergyPercent = (m_CurrentAmountOfLittersInTheTank / r_FuelTankCapacity) * 100;
            }
        }

        internal override float GetMaxEnergyAmount()
        {
            return r_FuelTankCapacity;
        }

        public override string ToString()
        {
            return string.Format(@"{0}
Fuel type:                        {1}",
base.ToString(),
r_FuelType.ToString());
        }


        public override void SetInitialEnergy(float i_LittersInTank)
        {
            m_CurrentAmountOfLittersInTheTank = i_LittersInTank;
            m_CurrentEnergyPercent = (m_CurrentAmountOfLittersInTheTank / r_FuelTankCapacity) * 100;
        }

        private void InitEngineDetails(List<string> i_EngineQuestions)
        {
            foreach (string Question in i_EngineQuestions)
            {
                m_EngineDetails.Add(Question, null);
            }
        }


        internal eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }
        
        public enum eFuelType
        {
            Soler = 1,
            Octan95 = 2,
            Octan96 = 3,
            Octan98 = 4
        }

        internal enum eFuelCapacity
        {
            MotorBike = 6,
            Car = 45,
            Truck = 120
        }
    }
}


    