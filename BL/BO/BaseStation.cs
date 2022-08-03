using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BaseStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int NumOfChargeSlots { get; set; }
        public IEnumerable<DroneInCharge> DronesInCharge { get; set; }

        public override string ToString()
        {
            return $"***Base Station***\n" +
                $" Id: {Id}\n" +
                $" Name: {Name}\n" +
                $" Location:\n {Location}\n" +
                $" Number Of Charge Slots: {NumOfChargeSlots}\n";
        }
    }
}
