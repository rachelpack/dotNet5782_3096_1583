using System;
using BO;
using System.Threading;
using System.Linq;
using static System.Math;
using DO;
using DlApi;
using System.Collections.Generic;

namespace BO
{
    internal class DroneSimulator
    {
        enum Maintenance { STARTING, GOING, CHARGING }
        private const double VELOCITY = 1.0;
        //private const int DELAY = 1;
        private const int DELAY = 1000;
        private const double TIME_STEP = DELAY / 1000.0;
        private const double STEP = VELOCITY / TIME_STEP;
        // private const double STEP = 2000;
        /// <summary>
        /// A loop that works until the simulator stops and checks if the skimmer is available, tries to assign it a package if there is no suitable package and no 100% battery, sends it for further charging, it waits until it finds a suitable package.
        // takes care of sending it for loading, loading and unloading and also collects the packages of the skimmer and sends them to the recipient
        /// </summary>
        /// <param name="ibl"></param>
        /// <param name="droneId"></param>
        /// <param name="updateDrone"></param>
        /// <param name="checkStop"></param>
        public DroneSimulator(BL ibl, int droneId, Action updateDrone, Func<bool> checkStop)
        {
            BL bl = ibl;
            IDal dal = DalFactory.GetDAL();
            DroneToList drone = bl.GetDroneList().FirstOrDefault(drone => drone.Id == droneId);
            int? parcelId = null;
            BaseStation bs = null;
            double distance = 0.0;
            int batteryUsage = 0;
            Parcel parcel = null;
            bool pickedUp = false;
            Customer customer = null;
            Maintenance maintenance = Maintenance.STARTING;
            //initilaze the parcel the drone send new..
            void initDelivery(int id)
            {
                parcel = bl.GetParcel(id);
                batteryUsage = (int)Enum.Parse(typeof(BatteryUsage), parcel?.Weight.ToString());
                pickedUp = parcel.CollectingTime is not null;
                customer = bl.ConvertDalCustomerToBlCustomer(dal.GetCustomer(pickedUp ? parcel.Receives.Id : parcel.Sender.Id));
            }

            do
            {
                switch (drone)
                {
                    case DroneToList { Statuses: DroneStatuses.AVAILABLE }:
                        if (!SleepDelayTime())
                        {
                            break;
                        }

                        lock (bl)
                        {
                            try
                            {
                                //Search for a package that a glider can send
                                parcelId = bl.ParcelToDrone(drone.Id);
                                initDelivery((int)parcelId);
                                drone.Statuses = DroneStatuses.DALIVERY;
                            }
                            catch (ThereIsNotEnoughBattery)
                            {
                                if (drone.Battery < 100)
                                {
                                    try
                                    {
                                        //In case he does not have enough battery to send the package - send the skimmer for charging
                                        bl.AmountOfBatteryDroneNeedsToBeShippedForCharging(drone);
                                        maintenance = Maintenance.STARTING;
                                    }
                                    catch (Exception)
                                    {
                                        //Placing the skimmer at a charging station
                                        BaseStation station;
                                        lock (bl)
                                            station = bl.NearStationWithAvailableChargeSlots(drone.Location);
                                        drone.Statuses = DroneStatuses.MAINTENANCE;
                                        drone.Battery = 0;
                                        drone.Location = new Location() { Latitude = station.Location.Latitude, Longitude = station.Location.Longitude };
                                        maintenance = Maintenance.GOING;
                                    }
                                }
                                break;
                            }
                            catch (DroneCanNotBeSent)
                            {
                                //In case he does not have enough battery to send the package - send the skimmer for charging
                                if (drone.Battery < 100)
                                {
                                    bl.AmountOfBatteryDroneNeedsToBeShippedForCharging(drone);
                                    maintenance = Maintenance.STARTING;
                                }
                                break;
                            }
                        }

                        break;

                    case DroneToList { Statuses: DroneStatuses.MAINTENANCE }:
                        switch (maintenance)
                        {
                            case Maintenance.STARTING:
                                lock (bl)//Search for a skimmer loading station
                                {
                                    IEnumerable<DroneInCharge> droneInCharges = bl.GetDroneChargingList();
                                    if (droneInCharges.Any(drone => drone.Id == drone.Id))
                                    {
                                        maintenance = Maintenance.CHARGING;
                                        bs = bl.GetBaseStation(bl.GetDroneChargingList().First(d => d.Id == drone.Id).StationId);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            bs = bl.NearStationWithAvailableChargeSlots(drone.Location);
                                        }
                                        catch (DroneCanNotBeSent)
                                        {
                                            throw;
                                        }
                                        distance = BL.Distance(bs.Location, drone.Location);
                                        maintenance = Maintenance.GOING;
                                    }
                                }
                                break;

                            case Maintenance.GOING:
                                if (distance < 0.01)
                                {
                                    lock (bl)// send the skimmer for charging
                                    {
                                        drone.Location = bs.Location;
                                        maintenance = Maintenance.CHARGING;
                                        try
                                        {
                                            lock (dal)
                                            {
                                                dal.SendingADroneForChargingAtABaseStation(drone.Id, bs.Id);
                                            }
                                        }
                                        catch (DO.TheObjectIDDoesNotExist ex)
                                        {
                                            throw new TheObjectIDDoesNotExist("The drone does not exist in the system.", ex);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!SleepDelayTime())
                                    {
                                        break;
                                    }

                                    lock (bl)
                                    {
                                        double delta = distance < STEP ? distance : STEP;
                                        distance -= delta;
                                        drone.Battery = Max(0.0, drone.Battery - delta * dal.GetData()[0]);
                                    }
                                }
                                break;

                            case Maintenance.CHARGING:
                                if (drone.Battery == 100)
                                {
                                    lock (bl)// Release from charging
                                    {
                                        try
                                        {
                                            bl.DroneFromCharging(drone.Id);
                                            maintenance = Maintenance.STARTING;
                                        }
                                        catch (DroneCanNotBeSent)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!SleepDelayTime())
                                    {
                                        break;
                                    }

                                    lock (bl)
                                    {
                                        drone.Battery = Min(100, drone.Battery + dal.GetData()[4] * TIME_STEP);
                                    }
                                }
                                break;
                            default:
                                throw new BadStatusException("Internal error: wrong maintenance substate");
                        }
                        break;

                    case BO.DroneToList { Statuses: DroneStatuses.DALIVERY }:
                        lock (bl)
                        {
                            try
                            {
                                if (parcelId == null)
                                {
                                    initDelivery(drone.ParcelId);
                                }
                            }
                            catch (TheObjectIDDoesNotExist ex) { throw new BadStatusException("Internal error getting parcel", ex); }
                            distance = BL.Distance(customer.Location, drone.Location);
                        }

                        if (distance < 0.01 || drone.Battery == 0.0)
                        {
                            lock (bl)
                            {
                                drone.Location = customer.Location;
                                if (pickedUp)
                                {
                                    bl.ShippingPackage(drone.Id);
                                    drone.Statuses = DroneStatuses.AVAILABLE;
                                }
                                else
                                {
                                    bl.CollectingPackage(drone.Id);
                                    customer = bl.ConvertDalCustomerToBlCustomer(dal.GetCustomer(parcel.Sender.Id));
                                    pickedUp = true;
                                }
                            }
                        }
                        else

                        {
                            if (!SleepDelayTime()) break;
                            lock (bl)
                            {
                                double delta = distance < STEP ? distance : STEP;
                                double proportion = delta / distance;
                                drone.Battery = Max(0.0, drone.Battery - delta * dal.GetData()[pickedUp ? batteryUsage : 0]);
                                double lat = drone.Location.Latitude + (customer.Location.Latitude - drone.Location.Latitude) * proportion;
                                double lon = drone.Location.Longitude + (customer.Location.Longitude - drone.Location.Longitude) * proportion;
                                drone.Location = new() { Latitude = lat, Longitude = lon };
                            }
                        }
                        break;

                    default:
                        throw new BadStatusException("Internal error: not available after Delivery...");

                }
                updateDrone();
            } while (!checkStop());
        }

        private static bool SleepDelayTime()
        {
            try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
            return true;
        }
    }
}




