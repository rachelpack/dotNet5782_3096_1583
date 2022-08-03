using System;

namespace DO
{
    public struct BaseStation
    {
        /// <summary>
        /// Id of the station
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// Name of the station
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// Number of the empty ChargeSlots in the srarion.
        /// </summary>
        public int ChargeSlots { set; get; }
        /// <summary>
        /// The Longitude of the station.
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// The Latitude of the station.
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Is the station active(not deleted)
        /// </summary>
        public bool IsAvailable { get; set; }
        public override string ToString()
        {
            return $"***Staion***\n Id: {Id}\n Name: {Name}\n ChargeSlots: {ChargeSlots}\n Longitude: {Longitude}\n Latitude: {Latitude}\n";
        }
    }
}
