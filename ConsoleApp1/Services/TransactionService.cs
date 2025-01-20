using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FinanceTracker.Models;

namespace FinanceTracker.Services
{
    public class TransactionService
    {
        private List<Transaction> transactions = new List<Transaction>();
        private const string FilePath = "transactions.csv";

        public TransactionService()
        {
            LoadTransactions();
        }

        public void AddTransaction()
        {
            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Enter amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount. Transaction not added.");
                return;
            }

            Console.Write("Is it Income or Expense (I/E): ");
            string typeInput = Console.ReadLine().ToUpper();
            TransactionType type = typeInput == "I" ? TransactionType.Income : TransactionType.Expense;

            Console.Write("Enter category (e.g., Food, Rent, Salary): ");
            string category = Console.ReadLine();

            var transaction = new Transaction
            {
                Description = description,
                Amount = amount,
                Type = type,
                Category = category,
                Date = DateTime.Now
            };

            transactions.Add(transaction);
            SaveTransactions();

            Console.WriteLine("Transaction added successfully!");
        }

        public void ViewTransactions()
        {
            Console.WriteLine("=== All Transactions ===");
            foreach (var t in transactions)
            {
                Console.WriteLine($"{t.Date:yyyy-MM-dd} | {t.Category} | {t.Description} | {t.Amount:C} | {t.Type}");
            }
        }

        public void GenerateMonthlySummary()
        {
            Console.WriteLine("Enter the month and year (MM/YYYY): ");
            string input = Console.ReadLine();
            if (!DateTime.TryParse($"01/{input}", out DateTime selectedDate))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            var monthlyTransactions = transactions
                .Where(t => t.Date.Month == selectedDate.Month && t.Date.Year == selectedDate.Year);

            decimal totalIncome = monthlyTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            decimal totalExpense = monthlyTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

            Console.WriteLine($"=== Summary for {selectedDate:MMMM yyyy} ===");
            Console.WriteLine($"Total Income: {totalIncome:C}");
            Console.WriteLine($"Total Expense: {totalExpense:C}");
            Console.WriteLine($"Net Savings: {(totalIncome - totalExpense):C}");
        }

        private void SaveTransactions()
        {
            using (var writer = new StreamWriter(FilePath))
            {
                foreach (var t in transactions)
                {
                    writer.WriteLine($"{t.Date},{t.Description},{t.Amount},{t.Type},{t.Category}");
                }
            }
        }

        private void LoadTransactions()
        {
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    transactions.Add(new Transaction
                    {
                        Date = DateTime.Parse(parts[0]),
                        Description = parts[1],
                        Amount = decimal.Parse(parts[2]),
                        Type = (TransactionType)Enum.Parse(typeof(TransactionType), parts[3]),
                        Category = parts[4]
                    });
                }
            }
        }
    }
}
