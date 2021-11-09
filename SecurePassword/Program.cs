using System;

namespace SecurePassword
{
    class Program
    {
        static void Main(string[] args)
        {
            AccountController acc = new AccountController();
            string username;
            string password;
            Console.WriteLine("Hello, what do you want to do?");
            Console.WriteLine("Type 1 to create account and 2 to login and press enter to confirm");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Type in your username and press enter to confirm.");
                    username = Console.ReadLine();
                    Console.WriteLine("Type in your password and press enter to confirm.");
                    password = Console.ReadLine();
                    Console.WriteLine(acc.CreateAccount(username, password));
                    break;
                case "2":
                    Console.WriteLine("Type in your username and press enter to confirm.");
                    username = Console.ReadLine();
                    Console.WriteLine("Type in your password and press enter to confirm.");
                    password = Console.ReadLine();
                    Console.WriteLine(acc.Login(username, password));
                    break;
                default:
                    break;
            }
        }
    }
}
