using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BaseStationForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeveralChargingStationsAreAvailable { get; set; }
        public int SeveralChargingStationsAreOccupied { get; set; }
        public override string ToString()
        {
            return $"***Base Station For List***\n" +
                $" Id: {Id}\n" +
                $" Name: {Name}\n" +
                $" Several Charging Stations Are Available: {SeveralChargingStationsAreAvailable}\n" +
                $" Several Charging Stations Are Occupied: {SeveralChargingStationsAreOccupied}\n";
        }
    }
}
