using System;

namespace FinanceTracker.Models
{
    public class Transaction
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }

    public enum TransactionType
    {
        Income,
        Expense
    }
}
