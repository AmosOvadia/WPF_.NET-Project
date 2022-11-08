using System;

namespace Stage0
{
   partial class program
   {   
        static void Main(string[] args)
        {
            Welcome1_5646();
            Welcome2_5646();
            Console.ReadKey();
        }
        static partial void Welcome2_5646();
        private static void Welcome1_5646()
        {
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console applications", userName);
        }
    }
}
