using FinanceTracker.Application.DTO;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TransactionDto>> GetTransactionsAsync(DateTime startDate, DateTime endDate)
        {
            var transactions = await _dbContext.Transactions
                .Include(t => t.TransactionType)
                .Where(t => t.Date >= startDate && t.Date <= endDate)  
                .ToListAsync();

            var transactionDtos = transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Date = t.Date,
                TransactionTypeId = t.TransactionTypeId,
                TransactionType = t.TransactionType
            }).ToList();

            return transactionDtos;
        }

        public async Task<DailyReport> GetDailyReportAsync(DateTime date)
        {
            var transactions = await _dbContext.Transactions
                .Where(t => t.Date == date)
                .Include(t => t.TransactionType)
                .ToListAsync();

            var totalIncome = transactions
               .Where(t => t.TransactionType.Category == TransactionCategory.Income)
               .Sum(t => t.Amount);

            var totalExpenses = transactions
                .Where(t => t.TransactionType.Category == TransactionCategory.Expense)
                .Sum(t => t.Amount);

            var dailyReport = new DailyReport(date, totalIncome, totalExpenses, transactions);

            return dailyReport;
        }

        public async Task<DatePeriodReport> GetDatePeriodReportAsync(DateTime startDate, DateTime endDate)
        {
            var transactions = await _dbContext.Transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .Include(t => t.TransactionType)
                .ToListAsync();

            var totalIncome = transactions
                .Where(t => t.TransactionType.Category == TransactionCategory.Income)
                .Sum(t => t.Amount);

            var totalExpenses = transactions
                .Where(t => t.TransactionType.Category == TransactionCategory.Expense)
                .Sum(t => t.Amount);

            var periodReport = new DatePeriodReport(startDate, endDate, totalIncome, totalExpenses, transactions);

            return periodReport;
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(Guid transactionId)
        {
            if (transactionId == Guid.Empty)
            {
                throw new ArgumentException("The transaction ID cannot be empty.");
            }

            var existingTransaction = await _dbContext.Transactions
                .Include(t => t.TransactionType)  
                .FirstOrDefaultAsync(t => t.Id == transactionId);

            if (existingTransaction is null)
            {
                throw new ArgumentException("The transaction with the specified ID does not exist.");
            }

            var transactionDto = new TransactionDto
            {
                Id = existingTransaction.Id,
                Amount = existingTransaction.Amount,
                Date = existingTransaction.Date,
                TransactionTypeId = existingTransaction.TransactionTypeId,
            };

            return transactionDto;
        }

        public async Task<TransactionDto> CreateTransactioneAsync(CreateTransactionDto createTransactionDto)
        {
            if (createTransactionDto.TransactionTypeId == Guid.Empty)
            {
                throw new ArgumentException("The transaction type ID cannot be empty.");
            }

            var existingTransactionType = await _dbContext.TransactionTypes
                .FirstOrDefaultAsync(t => t.Id == createTransactionDto.TransactionTypeId);

            if (existingTransactionType is null)
            {
                throw new ArgumentException("The transaction type for creating a transaction was not found.");
            }

            var id = Guid.NewGuid();

            var transaction = new Transaction(id, createTransactionDto.Amount, createTransactionDto.Date, createTransactionDto.TransactionTypeId);

            await _dbContext.Transactions.AddAsync(transaction);

            await _dbContext.SaveChangesAsync();

            var createdTransaction = await _dbContext.Transactions
                .FirstOrDefaultAsync(t => t.Id == id);

            if (createdTransaction is null)
            {
                throw new InvalidOperationException("Failed to retrieve the created transaction.");
            }

            var transactionDto = new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Date = transaction.Date,
                TransactionTypeId = transaction.TransactionTypeId
            };

            return transactionDto;
        }

        public async Task<TransactionDto> UpdateTransactionAsync(UpdateTransactionDto updateTransactionDto)
        {
            if (updateTransactionDto.Id == Guid.Empty)
            {
                throw new ArgumentException("The transaction ID cannot be empty.");
            }

            var existingTransaction = await _dbContext.Transactions
                .FirstOrDefaultAsync(t => t.Id == updateTransactionDto.Id);

            if (existingTransaction is null)
            {
                throw new InvalidOperationException("The transaction with the specified ID does not exist.");
            }

            existingTransaction.Amount = updateTransactionDto.Amount;
            existingTransaction.Date = updateTransactionDto.Date;
            existingTransaction.TransactionTypeId = updateTransactionDto.TransactionTypeId;

            await _dbContext.SaveChangesAsync();

            var updatedTransaction = await _dbContext.Transactions
                .FirstOrDefaultAsync(t => t.Id == updateTransactionDto.Id);

            if (updatedTransaction is null)
            {
                throw new InvalidOperationException("Failed to retrieve the updated transaction.");
            }

            var transactionDto = new TransactionDto
            {
                Id = updatedTransaction.Id,
                Amount = updatedTransaction.Amount,
                Date = updatedTransaction.Date,
                TransactionTypeId = updatedTransaction.TransactionTypeId
            };

            return transactionDto;
        }

        public async Task DeleteTransactionAsync(Guid transactionId)
        {
            if (transactionId == Guid.Empty)
            {
                throw new ArgumentException("The transaction ID cannot be empty.");
            }

            var existingTransaction = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);

            if (existingTransaction is null)
            {
                throw new InvalidOperationException("The transaction with the specified ID does not exist.");
            }

            _dbContext.Transactions.Remove(existingTransaction);

            await _dbContext.SaveChangesAsync();
        }
    }
}
