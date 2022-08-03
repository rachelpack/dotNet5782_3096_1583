
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct DroneCharge
    {
        /// <summary>
        /// drone's id
        /// </summary>
        public int DroneId { get; set; }
        /// <summary>
        ///  station's id
        /// </summary>
        public int StationId { get; set; }
        /// <summary>
        /// The time the drone went into charging 
        /// </summary>
        public DateTime? DronentryTimeForCharging { get; set; }
        /// <summary>
        /// Is the station active(not deleted)
        /// </summary>
        public bool IsAvailable { get; set; }
        public override string ToString()
        {
            return $"***DroneCharge***\n" +
                $" DroneId: {DroneId}\n" +
                $" StationId: {StationId}\n";
        }
    }
}
