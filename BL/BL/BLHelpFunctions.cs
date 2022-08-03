using System;
using System.Collections.Generic;
//using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BO
{
    partial class BL
    {


        /// <summary>
        /// A help function that calculate the distance between two locations
        /// </summary>
        /// <param name="l1">The first location</param>
        /// <param name="l2">The second location</param>
        /// <returns>The distance between the locations</returns>
        internal static double Distance(Location l1, Location l2)
        {
            int R = 6371 * 1000; // metres
            double phi1 = l1.Latitude * Math.PI / 180; // φ, λ in radians
            double phi2 = l2.Latitude * Math.PI / 180;
            double deltaPhi = (l2.Latitude - l1.Latitude) * Math.PI / 180;
            double deltaLambda = (l2.Longitude - l1.Longitude) * Math.PI / 180;

            double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                       Math.Cos(phi1) * Math.Cos(phi2) *
                       Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c / 1000; // in kilometres
            return d;
        }

        //----------------------------------------------   Near Stations  --------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// A help function that found the nearest station with available chargeslots
        /// </summary>
        /// <param name="dorneLocation">The location of the drone that need charge</param>
        /// <returns>The nearest station with available chargeslot</returns>
        internal BaseStation NearStationWithAvailableChargeSlots(Location dorneLocation)
        {
            BaseStation nearStation;
            IEnumerable<DO.BaseStation> baseStations;
            lock (dal)
            {
                baseStations = dal.GetStationsList();
                Location location = new() { Latitude = baseStations.FirstOrDefault().Latitude, Longitude = baseStations.FirstOrDefault().Longitude };
                nearStation = ConvertDalStationToBLStaion(baseStations.FirstOrDefault());
                double minDistance = Distance(dorneLocation, location);
                double distance;
                foreach (DO.BaseStation station in baseStations)
                {
                    location = new() { Latitude = station.Latitude, Longitude = station.Longitude };
                    distance = Distance(location, dorneLocation);
                    if (distance < minDistance && station.ChargeSlots != 0)
                    {
                        minDistance = distance;
                        nearStation = new BaseStation() { Id = station.Id, Location = location, Name = station.Name, NumOfChargeSlots = station.ChargeSlots };
                    }
                }
                return nearStation.NumOfChargeSlots == 0 ? throw new DroneCanNotBeSent("there is not available chargeslot") : nearStation;
            }
        }

        ///// <summary>
        ///// A help function that found the nearest station to location's drone 
        ///// </summary>
        ///// <param name="receive">The location of the receive</param>
        ///// <returns>The nearesr station</returns>
        //private BaseStation NearStation(Location receive)

        //------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------


        //-------------------------------------------------   Baterry  -----------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// A help function that checks if drone have enough battery to fly
        /// </summary>
        /// <param name="d">The desired drone</param>
        /// <param name="p">The parcel</param>
        /// <returns>True, if the drone have enough battery, else, return false</returns>
        internal bool IsEnoughBattery(DroneToList d, int senderId, int receiveId, WeightCategory weight)
        {
            double batteryConsumption;
            Location sender = LocationOfSomeone(senderId);
            batteryConsumption = BatteryConsumption(d, Distance(d.Location, sender), 0);
            Location receive = LocationOfSomeone(receiveId);
            batteryConsumption += BatteryConsumption(d, Distance(sender, receive), weight);
            batteryConsumption += BatteryConsumption(d, Distance(receive, NearStationWithAvailableChargeSlots(receive).Location), 0);
            return batteryConsumption < d.Battery;
        }

        /// <summary>
        /// A help function that calculate the battery consumption that drone need to over the distance 
        /// </summary>
        /// <param name="d">The drone </param>
        /// <param name="distance">The distance ohat the drone need to over</param>
        /// <returns>The BatteryConsumption that drone need</returns>
        private double BatteryConsumption(DroneToList d, double distance, WeightCategory weight)
        {
            double power = 0;
            if (d.Statuses == DroneStatuses.AVAILABLE)
            {
                power = PowerDroneAvailable;
            }
            else if (d.Statuses == DroneStatuses.DALIVERY)
            {
                switch (weight)
                {
                    case WeightCategory.EASY:
                        power = PowerDroneEasy;
                        break;
                    case WeightCategory.MEDIUM:
                        power = PowerDroneMedium;
                        break;
                    case WeightCategory.HEAVY:
                        power = PowerDroneHeavy;
                        break;
                    default:
                        break;
                }
            }
            return power * distance;
        }

        /// <summary>
        ///How much battery will the glider need to get to the nearest charging station 
        /// </summary>
        /// <param name="location">The drone location.</param>
        /// <returns></returns>
        private double MinBatteryForAvailAble(Location location)
        {
            try
            {
                BaseStation station = NearStationWithAvailableChargeSlots(location);
                double power = Distance(location, new() { Latitude = station.Location.Latitude, Longitude = station.Location.Longitude }) * PowerDroneAvailable;
                return power > FULLBATTRY ? MININITBATTARY : power;
            }
            catch (DroneCanNotBeSent)
            {
                throw new DroneCanNotBeSent("there is not available chargeslot");
            }

        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------


        //------------------------------------------------   Locations  ----------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// A help function that return the location of customer that he have parcels that assigning to him
        /// </summary>
        /// <returns>Customer location</returns>
        //private Location LocationOfCustomerAssigningParcelsToThem()
        //{
        //    IEnumerable<int> idOfCustomers = dal.GetParcelsList()
        //        .Where(p => p.TargetId != 0)
        //        .Select(p => p.TargetId);
        //    if (idOfCustomers.Count() == 0)
        //        return new Location();
        //    int j = rand.Next(idOfCustomers.Count());
        //    int id = idOfCustomers.ElementAt(j);
        //    return LocationOfSomeone(id);
        //}

        /// <summary>
        /// A help function that return a location of desired someone
        /// </summary>
        /// <param name="desiredId">The id of the desired</param>
        /// <returns>The desured location</returns>
        private Location LocationOfSomeone(int desiredId)
        {
            Location location = new();
            lock (dal)
            {
                location.Latitude = dal.GetCustomer(desiredId).Latitude;
                location.Longitude = dal.GetCustomer(desiredId).Longitude;
            }
            return location;
        }

        /// <summary>
        /// The fuction get id of 2 customer and return Their location.
        /// </summary>
        /// <param name="id1">The id of the first customer</param>
        /// <param name="id2">The id of the second cusromer</param>
        /// <returns>The arr with the  location of the 2 customers</returns>
        private Location[] GetLocationOf2Customers(int id1, int id2)
        {
            int[] arr = new int[2] { id1, id2 };
            Location[] locations = new Location[2];
            DO.Customer customer;
            for (int i = 0; i < 2; i++)
            {
                lock (dal)
                    customer = dal.GetCustomersList().FirstOrDefault(item => item.Id == arr[i]);
                locations[i] = new Location() { Longitude = customer.Longitude, Latitude = customer.Latitude };
            }
            return locations;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------
    }
}