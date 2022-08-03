using System;

namespace Targil0
{
    partial class Program
    {
        static void Main()
        {
            welcome1538();
            welcome3096();
            Console.ReadKey();
        }
        static partial void welcome3096();
        private static void welcome1538()
        {
            Console.WriteLine("hello");
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine($"{name}, welcome to my first console application");
        }
    }
}
