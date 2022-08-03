//using System;
//using BlApi;
//using BO;
//namespace ConsoleUI_BL
//{
//    partial class Program
//    {
//        //static BL.BL Bl;
//        enum BaseOptions { Add = 1, Updating = 2, Display = 3, ListView = 4 }
//        static readonly IBL bl;
//        static Program()
//        {
//            bl = BlFactory.GetBl();
//        }
//        static void Main()
//        {
//            Console.WriteLine("Hello World!");
//            int num = 9;
//            int num2;
//            while (num != 5)
//            {
//                Console.WriteLine(" please enter\n" +
//                                  " 1 to: Add options\n" +
//                                  " 2 to: Update options\n" +
//                                  " 3 to: Display options\n" +
//                                  " 4 to: List view options\n" +
//                                  " 5 to: Exit");
//                int.TryParse(Console.ReadLine(), out num);
//                switch ((BaseOptions)num)
//                {
//                    case BaseOptions.Add:
//                        Console.WriteLine(" please enter\n" +
//                                          " 1 to: Add a base station to the list of stations\n" +
//                                          " 2 to: Add a drone to the list of existing skimmers\n" +
//                                          " 3 to: Admission of a new customer to the customer list\n" +
//                                          " 4 to: Receipt of package for shipment");
//                        Int32.TryParse(Console.ReadLine(), out num2);
//                        AddOptions(num2);
//                        break;
//                    case BaseOptions.Updating:
//                        //Updating(num);
//                        //
//                        Console.WriteLine(" please enter\n" +
//                                          " 1 to: Update drone data\n" +
//                                          " 2 to: Update station data\n" +
//                                          " 3 to: Update customer data\n" +
//                                          " 4 to: Send drone to charging\n" +
//                                          " 5 to: Releaseing a drone from charging\n" +
//                                          " 6 to: Assigning a parcel to the drone\n" +
//                                          " 7 to: Collecting a package using a drone\n" +
//                                          " 8 to: Shipping a package using a drone\n");
//                        Int32.TryParse(Console.ReadLine(), out num2);
//                        Updating(num2);
//                        break;
//                    case BaseOptions.Display:
//                        Console.WriteLine(" please enter\n" +
//                                          " 1 to: Base station display\n" +
//                                          " 2 to: drone display\n" +
//                                          " 3 to: Customer display\n" +
//                                          " 4 to: parcels display");
//                        Int32.TryParse(Console.ReadLine(), out num2);
//                        display(num2);
//                        break;
//                    case BaseOptions.ListView:
//                        Console.WriteLine(" please enter\n" +
//                                          " 1 to: Displays a list of base stations\n" +
//                                          " 2 to: Displays the list of drones\n" +
//                                          " 3 to: View the customer list\n" +
//                                          " 4 to: Displays the list of parcel\n" +
//                                          " 5 to: Displays a list of packages that have not yet been assigned to the drone\n" +
//                                          " 6 to: Display base stations with available charging stations");
//                        Int32.TryParse(Console.ReadLine(), out num2);
//                        ListView(num2);

//                        break;
//                    default:
//                        break;
//                }
//            }
//        }
//    }
//}
