using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInCharge
    {
        public int Id { get; set; }

        public int StationId { get; set; }
        public double Battey { get; set; }
        public DateTime? DronentryTimeForCharging { get; set; }
        public override string ToString()
        {
            return $"***Drone In Charge***\n" +
                $" Id: {Id}\n" +
                $" StationId: {StationId}\n" +
                $" Status {Battey}\n";
        }
    }
}


