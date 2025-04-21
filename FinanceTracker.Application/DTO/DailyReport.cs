using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.DTO
{
    public class DailyReport
    {
        public DateTime Date { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TotalExpenses { get; set; }

        public List<Transaction> Transactions { get; set; }

        public DailyReport() { }
                
        public DailyReport(DateTime date, decimal totalIncome, decimal totalExpenses, List<Transaction> transactions)
        {
            Date = date;
            TotalIncome = totalIncome;
            TotalExpenses = totalExpenses;
            Transactions = transactions;
        }
    }
}
