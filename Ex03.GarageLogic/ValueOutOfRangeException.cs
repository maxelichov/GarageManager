using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException(float i_UserInput,float i_MinValue, float i_MaxValue)
            : base(
                string.Format("The input '{0}' is invalid. input should be between {1} - {2}", i_UserInput, i_MinValue,
                    i_MaxValue))


        {

            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

    }
}
