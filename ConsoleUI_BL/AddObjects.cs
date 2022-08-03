//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BO;
//using DO;

//namespace ConsoleUI_BL
//{
//    enum AddOptions { AddStation = 1, AddDrone, ReceivingCustomer, ReceivingAPackageForDelivery };

//    partial class Program
//    {
//        /*למחוק */
        
//        static void AddOptions(int option)
//        {
//            switch ((AddOptions)option)
//            {
//                case ConsoleUI_BL.AddOptions.AddStation:
//                    addStation();
//                    break;
//                case ConsoleUI_BL.AddOptions.AddDrone:
//                    addDrone();
//                    break;
//                case ConsoleUI_BL.AddOptions.ReceivingCustomer:
//                    receivingCustomer();
//                    break;
//                case ConsoleUI_BL.AddOptions.ReceivingAPackageForDelivery:
//                    receivingAPackageForDelivery();
//                    break;
//                default:
//                    break;
//            }
//        }

//        /// <summary>
//        /// Method that add a station to the list of the stations
//        /// </summary>
//        private static void addStation()
//        {
//            Console.WriteLine("please enter id.");
//            int.TryParse(Console.ReadLine(), out int id);
//            Console.WriteLine("please enter the name of the station.");
//            string model = Console.ReadLine();
//            Console.WriteLine("please enter the location. Longitude and Latitudes");
//            double.TryParse(Console.ReadLine(), out double longitude);//אורך 180
//            double.TryParse(Console.ReadLine(), out double latitudes);
//            Console.WriteLine("please enter the several charging stations are available");
//            int.TryParse(Console.ReadLine(), out int availableStation);
//            try
//            {
//                bl.AddStation(id, model, longitude, latitudes, availableStation);
//                Console.WriteLine("The station was successfully added");
//            }
//            catch (TheLatitudeValueOutOfRange)
//            {
//                Console.WriteLine("The latitude value out of range");
//            }
//            catch (TheLongitudeValueOutOfRange)
//            {
//                Console.WriteLine("The longitude value out of range");
//            }
//            catch(TheObjectIdAlreadyExist e)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }

//        /// <summary>
//        /// Method that add a drone to the list of the drones
//        /// </summary>
//        private static void addDrone()
//        {
//            Console.WriteLine("Pleas enter id of the drone.");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            Console.WriteLine("Pleas enter the dron's model.");
//            string model = Console.ReadLine();
//            Console.WriteLine("Pleas enter the maximum weight the drone can carry(1- Easy ,2- Medium,3- Heavy).");
//            int.TryParse(Console.ReadLine(), out int weight);
//            Console.WriteLine("pleas enter the station number for inserting the drone for initial charging");
//            int.TryParse(Console.ReadLine(), out int stationNumber);
//            try
//            {
//                bl.AddDrone(id, model, weight, stationNumber);
//                Console.WriteLine("The drone was successfully added");
//            }
//            catch (TheObjectIdAlreadyExist e)
//            {
//                Console.WriteLine(e.Message); 
//            }
//            catch(WrongWeight e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch(TheObjectIDDoesNotExist e)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }

//        /// <summary>
//        /// Method that add a customer to the list of the customers
//        /// </summary>
//        private static void receivingCustomer()
//        {
//            Console.WriteLine("Pleas enter the customer's id.");
//            int.TryParse(Console.ReadLine(), out int id );
//            Console.WriteLine("Pleas enter the customer's name.");
//            string name = Console.ReadLine();
//            Console.WriteLine("Pleas enter the customer's phone.");
//            int.TryParse(Console.ReadLine(), out int phone);
//            Console.WriteLine("pleas enter the location. Longitude and Latitudes");
//            double.TryParse(Console.ReadLine(), out double longitude);
//            double.TryParse(Console.ReadLine(), out double latitudes);
//            try
//            {
//                bl.AddCustomer(id, name, phone, longitude, latitudes);
//                Console.WriteLine("The customer was successfully added");
//            }
//            catch (WorngPhone e)
//            {
//                Console.WriteLine(e.Message);    
//            }
//            catch (TheLatitudeValueOutOfRange)
//            {
//                Console.WriteLine("The latitude value out of range");
//            }
//            catch (TheLongitudeValueOutOfRange)
//            {
//                Console.WriteLine("The longitude value out of range");
//            }
//            catch(TheObjectIdAlreadyExist e)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }

//        /// <summary>
//        /// Method that add a parcel to the list of the parcels
//        /// </summary>
//        private static void receivingAPackageForDelivery()
//        {
//            Console.WriteLine("Pleas enter the sender's id");
//            int.TryParse(Console.ReadLine(), out int idSend);
//            Console.WriteLine("pleas enter the recipient's id");
//            int.TryParse(Console.ReadLine(), out int idGiv);
//            Console.WriteLine("Pleas enter the weight of the parcel(1 - Easy, 2 - Medium, 3- Heavy).");
//            int.TryParse(Console.ReadLine(), out int weight);
//            Console.WriteLine("Pleas enter the priority of the package(1 - Normal, 2 - Fast, 3 - Emergency).");
//            int.TryParse(Console.ReadLine(), out int priority);
//            try
//            {
//                bl.ReceivingAPackageForDelivery(idSend, idGiv, weight, priority);
//                Console.WriteLine("The parcel was successfully added");
//            }

//            catch (TheObjectIDDoesNotExist e)
//            {
//                Console.WriteLine(e.Message);    
//            }

//            catch (WrongPriority e)
//            {
//                Console.WriteLine(e.Message);
//            }

//            catch (WrongWeight e)
//            {
//                Console.WriteLine(e.Message);
//            }
//            catch(TheObjectIdAlreadyExist e)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }
//    }
//}
