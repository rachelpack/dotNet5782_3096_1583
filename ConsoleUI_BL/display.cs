//using DO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleUI_BL
//{

//    partial class Program
//    {
//        enum Display { BaseStation = 1, Dron, Customer, Parcel };

//        static void display(int num)
//        {
//            switch ((Display)num)
//            {
//                case Display.BaseStation:
//                    displayBaseStation();
//                    break;
//                case Display.Dron:
//                    displayDrone();
//                    break;
//                case Display.Customer:
//                    displayCustomer();
//                    break;
//                case Display.Parcel:
//                    displayParcel();
//                    break;
//                default:
//                    break;
//            }

//        }
//        /// <summary>
//        /// method that display a station details
//        /// </summary>
//        private static void displayBaseStation()
//        {
//            Console.WriteLine("Please, enter ID of base station you want");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//                Console.WriteLine(bl.GetBaseStation(id));
//            }
//            catch (TheObjectIDDoesNotExist e)
//            {
//                Console.WriteLine(e.Message); 
//            }
//        }

//        /// <summary>
//        /// method that display a drone details
//        /// </summary>
//        private static void displayDrone()
//        {
//            Console.WriteLine("Please, enter ID of drone you want");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            Console.WriteLine(bl.GetDrone(id));
//        }

//        /// <summary>
//        /// method that display a customer details
//        /// </summary>
//        private static void displayCustomer()
//        {
//            Console.WriteLine("Please, enter ID of custoner you want");
//            Int32.TryParse(Console.ReadLine(), out int id);
//            try
//            {
//            Console.WriteLine(bl.GetCustomer(id));
                
//            }
//            catch (TheObjectIDDoesNotExist)
//            {
//                Console.WriteLine("The drone customer not exist in the system.");
//            }
//        }

//        /// <summary>
//        /// method that display a parcel details
//        /// </summary>
//        private static void displayParcel()
//        {
//            Console.WriteLine("Please, enter ID of parcel you want");
//            int.TryParse(Console.ReadLine(), out int id);
//            Console.WriteLine(bl.GetParcel(id));
//        }


//    }
//}
