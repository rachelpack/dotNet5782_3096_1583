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
//        enum DisplayLists { BaseStation = 1, Dron, Customer, Parcel, PackagesNotRelated, ChargingStationsWhithEmpty }

//        static void ListView(int num)
//        {
//            switch ((DisplayLists)num)
//            {
//                case DisplayLists.BaseStation:
//                    displayBaseStationList();
//                    break;
//                case DisplayLists.Dron:
//                    foreach (var item in bl.GetDroneList())
//                    {
//                        Console.WriteLine(item);
//                    }
//                    break;
//                case DisplayLists.Customer:
//                    foreach (var item in bl.GetCustomerList())
//                    {
//                        Console.WriteLine(item);
//                    }
//                    break;
//                case DisplayLists.Parcel:
//                    foreach (var item in bl.GetParcelList())
//                    {
//                        Console.WriteLine(item);
//                    }
//                    break;
//                case DisplayLists.PackagesNotRelated:
//                    foreach (var item in bl.GetParcelssNotRelated())
//                    {
//                        Console.WriteLine(item);
//                    }
//                    break;
//                case DisplayLists.ChargingStationsWhithEmpty:
//                    foreach (var item in bl.ChargingStationsWhithEmptyChargeSlots())
//                    {
//                        Console.WriteLine(item);
//                    }
//                    break; 
//                default:
//                    break;
//            }

//        }
//        private static void displayBaseStationList()
//        {
//            try
//            {
//                List<BO.BaseStationForList> stations =(List<BO.BaseStationForList>)bl.GetBaseStationList();
//                foreach (var item in stations)
//                {
//                    Console.WriteLine(item);
//                }
//            }
//            catch (TheObjectIDDoesNotExist e)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }
//    }
//}
