using System.Runtime.CompilerServices;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
     partial class DalObject
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] GetData()
        {
            double[] powers = new double[5];
            powers[0] = DataSource.Config.PowerConsumptionByDroneAvailable;
            powers[1] = DataSource.Config.PowerConsumptionByDroneCarryEasyWeight;
            powers[2] = DataSource.Config.PowerConsumptionByDroneCarryMediumWeight;
            powers[3] = DataSource.Config.PowerConsumptionByDroneCarryheavyWeight;
            powers[4] = DataSource.Config.DroneLoadingRate;
            return powers;
        }


    }
}
