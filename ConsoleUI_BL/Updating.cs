//using BO;
//using DO;
//using System;


//namespace ConsoleUI_BL
//{
//    enum updating { Drone = 1, Staion, Customer, SendDroneToCharging, DroneFromCharging, ParcelToDrone, CollectingPackage, ShippingPackage };

//    partial class Program
//    {
//        static void Updating(int num)
//        {
//            switch ((updating)num)
//            {
//                case updating.Drone:
//                    droneUpdate();
//                    break;
//                case updating.Staion:
//                    stationUpdae();
//                    break;
//                case updating.Customer:
//                    customerUpdate();
//                    break;
//                case updating.SendDroneToCharging:
//                    sendDroneToCharging();
//                    break;
//                case updating.DroneFromCharging:
//                    droneFromCharging();
//                    break;
//                case updating.ParcelToDrone:
//                    parcelToDrone();
//                    break;
//                case updating.CollectingPackage:
//                    collectingPackage();
//                    break;
//                case updating.ShippingPackage:
//                    shippingPackage();
//                    break;
//                default:
//                    break;
//            }
//        }

//        private static void droneUpdate()
//        {
//            Console.WriteLine("Pleas enter the id of the drone you to update.");
//            int.TryParse(Console.ReadLine(), out int id);
//            Console.WriteLine("Pleas enter the new model.");
//            string model = Console.ReadLine();
//            try
//            {
//                bl.DroneUpdating(id, model);
//                Console.WriteLine("The drone was successfully updated");
//            }
//            catch (TheObjectIDDoesNotExist e)
//            {

//                Console.WriteLine(e.Message);
//            }
//        }

//        private static void stationUpdae()
//        {
//            Console.WriteLine("pleas enter the station number and the  total amount of charging stations.If you dont wont to update one of this details enter 0.");
//            int.TryParse(Console.ReadLine(), out int id);
//            string name = Console.ReadLine();
//            Console.WriteLine("Pleas enter the ");
//            int.TryParse(Console.ReadLine(), out int numOfCa);
//            try
//            {
//                bl.UpdateStation(id, numOfCa, name);
//                Console.WriteLine("The station was successfully updated");
//            }
//            catch (TheObjectIDDoesNotExist e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch (OutOfRangeValue e)
//            {
//                Console.WriteLine(e.Message);
//            }


//        }

//        private static void customerUpdate()
//        {
//            Console.WriteLine("pleas enter the customer id.");
//            int.TryParse(Console.ReadLine(), out int id);
//            Console.WriteLine("pleas enter the new name and/or the new phone.If you dont wont to update one of this details enter 0.");
//            string name = Console.ReadLine();
//            int.TryParse(Console.ReadLine(), out int phone);
//            try
//            {
//                bl.UpdatCustomer(id, name, phone);
//                Console.WriteLine("The customer was successfully updated");
//            }
//            catch (TheObjectIDDoesNotExist e)
//            {
//                Console.WriteLine(e.Message);
//            }

//        }

//        private static void sendDroneToCharging()
//        {
//            Console.WriteLine("Pleas enter the id of the drone.");
//            int.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                bl.SendDroneToCharging(id);
//                Console.WriteLine("The drone was successfully sent to charge");
//            }
//            catch (TheObjectIDDoesNotExist e)
//            {

//                Console.WriteLine(e.Message);
//            }
//            catch (ThereIsNotEnoughBattery e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch (DroneCanNotBeSent e)
//            {
//                Console.WriteLine(e.Message);
//            }

//        }

//        private static void droneFromCharging()
//        {
//            Console.WriteLine("Pleas enter the id of the drone.");
//            int.TryParse(Console.ReadLine(), out int id);
//            Console.WriteLine("pleas enter the Charging time.");
//            double.TryParse(Console.ReadLine(), out double chTime);
//            try
//            {
//                bl.DroneFromCharging(id, chTime);
//                Console.WriteLine("The drone relesed from charging in successfully");
//            }
//            catch (TheObjectIDDoesNotExist e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch (DroneCanNotBeSent e)
//            {
//                Console.WriteLine(e.Message);
//            }

//        }

//        private static void parcelToDrone()
//        {
//            Console.WriteLine("Pleas enter the id of the drone.");
//            int.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                bl.ParcelToDrone(id);
//                Console.WriteLine("The parcel assigning was successfully");
//            }
//            catch (ThisActionIsNotPossible e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch (TheObjectIDDoesNotExist e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch (DroneCanNotBeSent e)
//            {
//                Console.WriteLine(e.Message);
//            }

//        }

//        private static void collectingPackage()
//        {
//            Console.WriteLine("Pleas enter the id of the drone.");
//            int.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                bl.CollectingPackage(id);
//                Console.WriteLine("The parcel was successfully collected");
//            }
//            catch (TheObjectIDDoesNotExist e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch (ThereIsNotEnoughBattery e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch (ThisActionIsNotPossible e)
//            {
//                Console.WriteLine(e.Message);
//            }

//        }

//        private static void shippingPackage()
//        {
//            Console.WriteLine("Pleas enter the id of the drone.");
//            int.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                bl.ShippingPackage(id);
//                Console.WriteLine("The parcel was successfully shipping");
//            }
//            catch (ThisActionIsNotPossible e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch (ThereIsNotEnoughBattery e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch (TheObjectIDDoesNotExist e)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }


//    }
//}
