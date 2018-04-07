using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlugin
{
    internal class BusinessLayer
    {
        public bool CostumerNameIsValid(string name)
        {
            return name.Length > 5; 
        }

        public bool ArriveDateIsValid(DateTime? arriveTime, DateTime? leaveTime)
        {
            //TODO: Check if user input is valid
            return true;
        }

        public bool LeaveDateIsValid(DateTime? arriveTime, DateTime? leaveTime)
        {
            //TODO: Check if user input is valid
            return true;
        }
    }
}
