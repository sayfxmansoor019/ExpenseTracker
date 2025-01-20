using System;
using FinanceTracker.Services;

namespace FinanceTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactionService = new TransactionService();
            string choice;

            do
            {
                Console.WriteLine("== Finance Tracker ===");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Transactions");
                Console.WriteLine("3. Generate Monthly Summary");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        transactionService.AddTransaction();
                        break;
                    case "2":
                        transactionService.ViewTransactions();
                        break;
                    case "3":
                        transactionService.GenerateMonthlySummary();
                        break;
                    case "4":
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            } while (choice != "4");
        }
    }
}
