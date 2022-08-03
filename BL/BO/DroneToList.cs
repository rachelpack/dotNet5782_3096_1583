using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneToList
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategory Weight { get; set; }
        public double Battery { get; set; }
        public DroneStatuses Statuses { get; set; }
        public Location Location { get; set; }
        public int ParcelId { get; set; }

        public override string ToString()
        {
            return $"***DroneToList***\n" +
                $" Id: {Id}\n" +
                $" Model: {Model}\n" +
                $" Weight: {Weight}\n" +
                $" Battery: {Battery}\n" +
                $" Statuses: {Statuses}\n" +
                $" Location:\n{Location}" +
                $" ParcelId: {ParcelId}\n";
        }
    }
}
