using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Drone
    {
        /// <summary>
        /// drone's id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// drone's model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// max weight the drone can carry.
        /// </summary>
        public WeightCategory MaxWeight { get; set; }
        /// <summary>
        /// Is the station active(not deleted)
        /// </summary>
        public bool IsAvailable { get; set; }
        public override string ToString()
        {
            return $"***Drone***\n" +
                $" Id: {Id}\n" +
                $" Modal: {Model}\n" +
                $" MaxWeight: {MaxWeight}\n";
        }
    }
}
