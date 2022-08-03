using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace Dal
{
    static class DataSource
    {
        const int DRONES_SIZE = 5;
        const int BASESTATIONS_SIZE = 2;
        const int CUSTOMERS_SIZE = 10;
        const int PARCELS_SIZE = 10;
        const int ENUM_SIZE = 3;
        internal static List<Drone> Drones = new();
        internal static List<BaseStation> BaseStations = new();
        internal static List<Customer> Customers = new();
        internal static List<Parcel> Parcels = new();
        internal static List<DroneCharge> DroneCharges = new();
        internal static List<User> Users = new();
        internal static User Manager = new() { Password = "1234", UserName = "manager" };
        static readonly Random rand = new();
        internal class Config
        {
            internal static int createIdParcel = 10;
            internal static double PowerConsumptionByDroneAvailable { get; } = 0.01;
            internal static double PowerConsumptionByDroneCarryEasyWeight { get; } = 0.02;
            internal static double PowerConsumptionByDroneCarryMediumWeight { get; } = 0.03;
            internal static double PowerConsumptionByDroneCarryheavyWeight { get; } = 0.04;
            internal static double DroneLoadingRate { get; } = 2;

        }


        /// <summary>
        /// A function that initialize tha arrays.
        /// </summary>
        internal static void Initialize()
        {
            for (int i = 1; i <= BASESTATIONS_SIZE; i++)
            {
                RandomBaseStation(i);
            }

            for (int i = 1; i <= DRONES_SIZE; i++)
            {
                RandomDrone(i);
            }

            for (int i = 1; i <= CUSTOMERS_SIZE; i++)
            {
                RandomCustomer(i);
            }

            for (int i = 1; i <= PARCELS_SIZE; ++i)
            {
                RandParcel(i);
            }
        }

        /// <summary>
        /// Randome the station's details
        /// </summary>
        /// <param name="stationId">The station id</param>
        private static void RandomBaseStation(int stationId)
        {
            BaseStation baseStation = new()
            {
                Id = stationId,
                Name = "BaseStation_" + stationId,
                ChargeSlots = 7,
                Latitude = rand.Next(30, 40) + rand.NextDouble(),
                Longitude = rand.Next(30, 40) + rand.NextDouble(),
                IsAvailable = true
            };
            BaseStations.Add(baseStation);
        }

        /// <summary>
        /// Randome the drone's details
        /// </summary>
        /// <param name="droneId">The drone id</param>
        private static void RandomDrone(int droneId)
        {
            Drone drone = new()
            {
                Id = droneId,
                Model = "Drone_" + droneId,
                MaxWeight = (WeightCategory)rand.Next(ENUM_SIZE),
                IsAvailable = true
            };
            Drones.Add(drone);
        }

        /// <summary>
        /// Randome the customer's details
        /// </summary>
        /// <param name="customerId">The customer id</param>
        private static void RandomCustomer(int customerId)
        {
            Customer customer = new()
            {
                Id = customerId,
                Name = "customer_" + customerId,
                Phone = rand.Next(1000000, 1000000000),
                Latitude = rand.Next(30, 40) + rand.NextDouble(),
                Longitude = rand.Next(30, 40) + rand.NextDouble(),
                IsAvailable = true
            };
            Customers.Add(customer);
        }


        /// <summary>
        /// Assign parcel to drone
        /// </summary>
        /// <param name="weight">The parcel weight.</param>
        /// <returns>The id of the drone that can to assign</returns>
        public static int AssignParcelDrone(WeightCategory weight)
        {
            Drone tmpDrone = Drones.FirstOrDefault(item => weight <= item.MaxWeight);
            if (tmpDrone.Equals(default(Drone)))
            {
                return 0;
            }
            Parcel parcel = Parcels.FirstOrDefault(parcel => parcel.DroneId == tmpDrone.Id);
            if (parcel.Equals(default(Parcel)))
            {
                return tmpDrone.Id;
            }
            return 0;
        }

        /// <summary>
        /// Randome the parcel's details
        /// </summary>
        /// <param name="parcelId">The parcel id</param>
        private static void RandParcel(int parcelId)
        {
            Parcel newParcel = new();
            newParcel.Id = parcelId;
            newParcel.SenderId = Customers[rand.Next(1, Customers.Count)].Id;
            do
            {
                newParcel.TargetId = Customers[rand.Next(1, Customers.Count)].Id;
            } while (newParcel.TargetId == newParcel.SenderId);
            newParcel.Weight = (WeightCategory)rand.Next(ENUM_SIZE);
            newParcel.Priority = (Priorities)rand.Next(ENUM_SIZE);
            newParcel.Requested = DateTime.Now; ;
            newParcel.Scheduled = null;
            newParcel.PickedUp = null;
            newParcel.Delivered = null;
            newParcel.DroneId = 0;
            newParcel.IsAvailable = true;
            int state = rand.Next(4);
            if (state != 0)
            {
                newParcel.DroneId = AssignParcelDrone(newParcel.Weight);
                if (newParcel.DroneId != 0)
                {
                    newParcel.Scheduled = DateTime.Now;
                    if (state == 2)
                    {
                        newParcel.PickedUp = DateTime.Now;
                    }
                    else if (state == 3)
                    {
                        newParcel.PickedUp = DateTime.Now;
                        newParcel.Delivered = DateTime.Now;
                    }
                }
            }
            Parcels.Add(newParcel);
        }
    }
}
