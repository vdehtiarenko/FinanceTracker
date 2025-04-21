using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.DTO
{
    public class DatePeriodReport
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TotalExpenses { get; set; }

        public List<Transaction> Transactions { get; set; }

        public DatePeriodReport() { }

        public DatePeriodReport(DateTime startDate, DateTime endDate, decimal totalIncome, decimal totalExpenses, List<Transaction> transactions)
        {
            StartDate = startDate;
            EndDate = endDate;
            TotalIncome = totalIncome;
            TotalExpenses = totalExpenses;
            Transactions = transactions;
        }
    }
}
