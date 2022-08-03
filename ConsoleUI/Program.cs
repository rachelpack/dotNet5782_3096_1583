//using IDAL;
//using IDAL.DO;
//using System;
//using System.Collections.Generic;

namespace ConsoleUI
{
   class Program
    {

//        static IDal dal;
//        // enum MainMenu{ Add_options, Update_options, Display_options, List_view_options, Exit }

       static void Main()
       {
//            dal = new DalObject.DalObject();
//            Console.WriteLine(" Hello to our company!");
//            int num;
//            while (true)
//            {
//                Console.WriteLine(
//                    " please enter\n" +
//                 " 1 to: Add options\n" +
//                 " 2 to: Update options\n" +
//                 " 3 to: Display options\n" +
//                 " 4 to: List view options\n" +
//                 " 5 to: Exit");
//                Int32.TryParse(Console.ReadLine(), out num);
//                switch (num)
//                {
//                    case 1:
//                        {
//                            Console.WriteLine(" please enter\n" +
//                    " 1 to: Add a base station to the list of stations\n" +
//                    " 2 to: Add a drone to the list of existing skimmers\n" +
//                    " 3 to: Admission of a new customer to the customer list\n" +
//                    " 4 to: Receipt of package for shipment");
//                            Int32.TryParse(Console.ReadLine(), out num);
//                            switch (num)
//                            {
//                                case 1:
//                                    {
//                                        AddBaseStation();
//                                        break;
//                                    }
//                                case 2:
//                                    {
//                                        AddDrone();
//                                        break;
//                                    }
//                                case 3:
//                                    {
//                                        AddCustomer();
//                                        break;
//                                    }
//                                case 4:
//                                    {
//                                        AddParcel();
//                                        break;
//                                    }
//                                default:
//                                    break;
//                            }
//                            break;
//                        }
//                    case 2:
//                        {
//                            Console.WriteLine(" please enter\n" +
//                    " 1 to: Assigning a package to a drone\n" +
//                    " 2 to: Collect a parcel by a drone\n" +
//                    " 3 to: Delivery of a package to the destination\n" +
//                    " 4 to: Sending a skimmer for charging at a base station\n" +
//                    " 5 to: Release skimmer from charging at base station");
//                            Int32.TryParse(Console.ReadLine(), out num);
//                            switch (num)
//                            {
//                                case 1:
//                                    {
//                                        AssigningAParcelToADrone();
//                                        break;
//                                    }
//                                case 2:
//                                    {
//                                        CollectAParcelByADrone();
//                                        break;
//                                    }
//                                case 3:
//                                    {
//                                        DeliveryOfAParcelToTheDestination();
//                                        break;
//                                    }
//                                case 4:
//                                    {
//                                        SendingADroneForChargingAtABaseStation();
//                                        break;
//                                    }
//                                case 5:
//                                    {
//                                        ReleaseDroneFromChargingAtBaseStation();
//                                        break;
//                                    }
//                                default:
//                                    break;
//                            }
//                            break;
//                        }
//                    case 3:
//                        {
//                            Console.WriteLine(" please enter\n" +
//                    " 1 to: Base station view\n" +
//                    " 2 to: Skimmer display\n" +
//                    " 3 to: Customer view\n" +
//                    " 4 to: Package view");
//                            Int32.TryParse(Console.ReadLine(), out num);
//                            switch (num)
//                            {
//                                case 1:
//                                    {
//                                        DisplayBaseStation();
//                                        break;
//                                    }
//                                case 2:
//                                    {
//                                        DisplayDrone();
//                                        break;
//                                    }
//                                case 3:
//                                    {
//                                        DisplayCustomer();
//                                        break;
//                                    }
//                                case 4:
//                                    {
//                                        DisplayParcel();
//                                        break;
//                                    }
//                                default:
//                                    break;
//                            }
//                            break;
//                        }
//                    case 4:
//                        {
//                            Console.WriteLine(" please enter\n" +
//                    " 1 to: Displays a list of base stations\n" +
//                    " 2 to: Displays the list of skimmers\n" +
//                    " 3 to: View the customer list\n" +
//                    " 4 to: Displays the list of packages\n" +
//                    " 5 to: Displays a list of packages that have not yet been assigned to the glider\n" +
//                    " 6 to: Display base stations with available charging stations");
//                            Int32.TryParse(Console.ReadLine(), out num);
//                            switch (num)
//                            {
//                                case 1:
//                                    {
//                                        DisplayBaseStationsList();
//                                        break;
//                                    }
//                                case 2:
//                                    {
//                                        DisplayDronesList();
//                                        break;
//                                    }
//                                case 3:
//                                    {
//                                        DisplayTheCustomerList();
//                                        break;
//                                    }
//                                case 4:
//                                    {
//                                        DisplayTheParcelsList();
//                                        break;
//                                    }
//                                case 5:
//                                    {
//                                        DisplayTheListOfParcelsNotYetBeenAssignedToTheDrone();
//                                        break;
//                                    }
//                                case 6:
//                                    {
//                                        DisplayBaseStationsWithAvailableChargingStations();
//                                        break;
//                                    }
//                                default:
//                                    break;
//                            }
//                            break;
//                        }
//                    case 5:
//                        {
//                            return;
//                        }
//                    default:
//                        break;
//                }
//            }
//        }


//        private static void DisplayBaseStationsList()
//        {
//            dal.DisplayBaseStationsList();
//            List<BaseStation> baseStations = (List<BaseStation>)dal.DisplayBaseStationsList();
//            foreach (var item in baseStations)
//            {
//                Console.WriteLine(item.ToString());
//            }
//        }

//        private static void DisplayDronesList()
//        {
//            List<Drone> drones = (List < Drone >)dal.DisplayTheListOfDrone();
//            foreach (var item in drones)
//            {
//                Console.WriteLine(item.ToString());
//            }

//        }

//        private static void DisplayTheCustomerList()
//        {
//            List<Customer> customers = (List<Customer>)dal.DisplayTheCustomerList();
//            foreach (var item in customers)
//            {
//                Console.WriteLine(item.ToString());
//            }
//        }


//        private static void DisplayTheParcelsList()
//        {
//            List<Parcel> parcels = (List<Parcel>)dal.DisplayTheParcels();
//            foreach (var item in parcels)
//            {
//                Console.WriteLine(item.ToString());
//            }
//        }

//        private static void DisplayTheListOfParcelsNotYetBeenAssignedToTheDrone()
//        {
//            List<Parcel> parcels =(List<Parcel>) dal.DisplayTheListOfParcelsNotYetBeenAssignedToTheDrone();
//            foreach (var item in parcels)
//            {
//                if (item.Id != 0)
//                {
//                    Console.WriteLine(item.ToString());
//                }
//            }
//        }

//        private static void DisplayBaseStationsWithAvailableChargingStations()
//        {
//           List<BaseStation> baseStations = (List<BaseStation>)dal.DisplayBaseStationsWithAvailableChargingStations();
//            foreach (var item in baseStations)
//            {
//                if (item.Id != 0)
//                {
//                    Console.WriteLine(item.ToString());
//                }
//            }

//        }
//        /// <summary>
//        ///  method that display a base station details
//        /// </summary>
//        private static void DisplayBaseStation()
//        {
//            Console.WriteLine("Please, enter ID of base station you want");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                BaseStation tempStation = new BaseStation();
//                tempStation = dal.DisplayStation(id);
//                Console.WriteLine(tempStation.ToString());
//            }
//            catch (Exception)
//            {

//                Console.WriteLine("base station not exist!");
//            }

//        }
//        /// <summary>
//        /// method that display a drone details
//        /// </summary>
//        private static void DisplayDrone()
//        {
//            Console.WriteLine("Please, enter ID of drone you want");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                Drone drone = new Drone();
//                drone = dal.DisplayDrone(id);
//                Console.WriteLine(drone.ToString());
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("drone not exist!");
//            }

//        }
//        /// <summary>
//        /// method that display a customer details
//        /// </summary>
//        private static void DisplayCustomer()
//        {
//            Console.WriteLine("Please, enter ID of customer you want");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                Customer tempCustomer = new Customer();
//                tempCustomer = dal.DisplayCustomer(id);
//                tempCustomer.ToString();
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("customer not exist!");
//            }
//        }
//        /// <summary>
//        ///  method that display a parcel details
//        /// </summary>
//        private static void DisplayParcel()
//        {
//            Console.WriteLine("Please, enter ID of parcel you want");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                Parcel tempParcel = new Parcel();
//                tempParcel = dal.DisplayParcel(id);
//                tempParcel.ToString();
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("parcel not exist!");
//            }
//        }

//        private static void AssigningAParcelToADrone()
//        {
//            Console.WriteLine("enter id of your parcel");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                dal.AssigningAParcelToADrone(id);
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("There are currently no drone available.");
//            }
//        }

//        private static void CollectAParcelByADrone()
//        {
//            int id;
//            Console.WriteLine("enter id of your parcel");
//            Int32.TryParse(Console.ReadLine(), out id);
//            try
//            {
//                dal.CollectAParcelByADrone(id);
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("One of the data is incorrect");
//            }
//        }

//        private static void DeliveryOfAParcelToTheDestination()
//        {
//            int id;
//            Console.WriteLine("enter id of your parcel");
//            Int32.TryParse(Console.ReadLine(), out id);
//            try
//            {
//                dal.DeliveryOfAParcelToTheDestination(id);
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("One of the data is incorrect");
//            }

//        }
//        /// <summary>
//        /// The function of pulling a drone to a charging station
//        /// </summary>
//        private static void SendingADroneForChargingAtABaseStation()
//        {
//            Console.WriteLine("please, enter drone id to charge");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                dal.SendingADroneForChargingAtABaseStation(id);
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("Some of the data you entered is invalid, please enter it again.");
//                SendingADroneForChargingAtABaseStation();
//            }
//        }
//        /// <summary>
//        /// The function releases a skimmer from a charger
//        /// </summary>
//        private static void ReleaseDroneFromChargingAtBaseStation()
//        {
//            Console.WriteLine("please, enter id");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                dal.ReleaseDroneFromChargingAtBaseStation(id);

//            }
//            catch (Exception)
//            {
//                Console.WriteLine("Some of the data you entered is invalid, please enter it again.");
//                ReleaseDroneFromChargingAtBaseStation();
//            }
//        }
//        /// <summary>
//        /// Method that add a drone to the arr of the drones
//        /// </summary>

//        private static void AddDrone()
//        {
//            Console.WriteLine("Pleas enter id of the drone.");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            Console.WriteLine("Pleas enter the drone's model");
//            string model = Console.ReadLine();
//            Console.WriteLine("Pleas enter the maximum weight the drone can carry(1- Easy ,2- Medium,3- Heavy).");
//            int.TryParse(Console.ReadLine(), out int weight);
//            try
//            {
//                dal.AddDrone(id, model, weight);
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("Some of the data you entered is invalid, please enter it again.");
//                AddDrone();
//            }

//        }

//        /// <summary>
//        /// Method that add a station to the arr of the stations
//        /// </summary>
//        private static void AddBaseStation()
//        {
//            int id;
//            double longitude, latitude;
//            string name;
//            Console.WriteLine("Pleas enter id, name,longitude,and latitude:");
//            Int32.TryParse(Console.ReadLine(), out id);
//            name = Console.ReadLine();
//            double.TryParse(Console.ReadLine(), out longitude);
//            double.TryParse(Console.ReadLine(), out latitude);
//            try
//            {
//                dal.AddStation(id, name, longitude, latitude);
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("Some of the data you entered is invalid, please enter it again.");
//                AddBaseStation();
//            }
//        }
//        /// <summary>
//        /// Method that add a p parcelo the arr of the parcels
//        /// </summary>
//        private static void AddParcel()
//        {
//            int idSend, idGiv, weight, priority;
//            Console.WriteLine("Pleas enter the sender's id");
//            int.TryParse(Console.ReadLine(), out idSend);
//            Console.Write("pleas enter the recipient's id");
//            int.TryParse(Console.ReadLine(), out idGiv);
//            Console.WriteLine("Pleas enter the weight of the parcel(1 - Easy, 2 - Medium, 3- Heavy).");
//            int.TryParse(Console.ReadLine(), out weight);
//            Console.WriteLine("Pleas enter the priority of the package(1 - Normal, 2 - Fast, 3 - Emergency).");
//            int.TryParse(Console.ReadLine(), out priority);
//            try
//            {
//                dal.AddAParcel(idGiv, idSend, weight, priority);
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("Some of the data you entered is invalid, please enter it again.");
//                AddParcel();
//            }

//        }

//        private static void AddCustomer()
//        {
//            int id, phone;
//            string name;
//            double longitude, latitude;
//            Console.WriteLine("Pleas enter the customer's id.");
//            int.TryParse(Console.ReadLine(), out id);
//            Console.WriteLine("Pleas enter the customer's name.");
//            name = Console.ReadLine();
//            Console.WriteLine("Pleas enter the customer's phone.");
//            int.TryParse(Console.ReadLine(), out phone);
//            Console.WriteLine("Pleas enter the customer's longitudevand latitude");
//            double.TryParse(Console.ReadLine(), out longitude);
//            double.TryParse(Console.ReadLine(), out latitude);
//            try
//            {
//                dal.AddCustomer(id, name, phone, longitude, latitude);
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("Some of the data you entered is invalid, please enter it again.");
//                AddCustomer();
//            }
     }

    }
}
