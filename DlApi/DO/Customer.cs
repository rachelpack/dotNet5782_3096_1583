using System;


namespace DO
{
    public struct Customer
    {
        /// <summary>
        /// Id of the customer
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// customer's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// customer's phone.
        /// </summary>
        public int Phone { get; set; }
        /// <summary>
        /// customer's Longitude.
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// customer's LonLatitudegitude.
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Is the station active(not deleted)
        /// </summary>
        public bool IsAvailable { get; set; }
        public override string ToString()
        {
            return $"***Customer***\n" +
                $" Id: {Id}\n" +
                $" Name: {Name}\n" +
                $" Phone: {Phone}\n" +
                $" Longitude: {Longitude}\n" +
                $" Latitude: {Latitude}\n";
        }
    }
}



