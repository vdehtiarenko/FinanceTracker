using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.DAL;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TransactionTypeDto>> GetTransactionTypesAsync()
        {
            var transactionTypes = await _dbContext.TransactionTypes.ToListAsync();

            var transactionTypeDtos = transactionTypes.Select(t => new TransactionTypeDto
            {
                Id = t.Id,
                Name = t.Name,
                Category = t.Category,
                Description = t.Description
            }).ToList();

            return transactionTypeDtos;
        }

        public async Task<TransactionTypeDto> GetTransactionTypeByIdAsync(Guid transactionTypeId)
        {
            if (transactionTypeId == Guid.Empty)
            {
                throw new ArgumentException("The transaction type ID cannot be empty.");
            }

            var existingTransactionType = await _dbContext.TransactionTypes
                .FirstOrDefaultAsync(t => t.Id == transactionTypeId);

            if (existingTransactionType is null)
            {
                throw new ArgumentException("The transaction type with the specified ID does not exist.");
            }

            var transactionTypeDto = new TransactionTypeDto
            {
                Id = existingTransactionType.Id,
                Name = existingTransactionType.Name,
                Category = existingTransactionType.Category,
                Description = existingTransactionType.Description
            };

            return transactionTypeDto;
        }

        public async Task<TransactionTypeDto> CreateTransactionTypeAsync(CreateTransactionTypeDto createDto)
        {
            if (string.IsNullOrEmpty(createDto.Name))
            {
                throw new ArgumentException("The transaction type name cannot be empty.");
            }

            if (string.IsNullOrEmpty(createDto.Description))
            {
                throw new ArgumentException("The transaction type description cannot be empty.");
            }

            var existingTransactionType = await _dbContext.TransactionTypes
                .FirstOrDefaultAsync(t => string.Equals(t.Name.Trim().ToUpper(), createDto.Name.Trim().ToUpper())
                && t.Category == createDto.Category
                && string.Equals(t.Description.Trim().ToUpper(), createDto.Description.Trim().ToUpper()));

            if (existingTransactionType != null)
            {
                var transactionTypeDto = new TransactionTypeDto
                {
                    Id = existingTransactionType.Id,
                    Name = existingTransactionType.Name,
                    Category = existingTransactionType.Category,
                    Description = existingTransactionType.Description
                };

                return transactionTypeDto;
            }

            var id = Guid.NewGuid();

            var transactionType = new TransactionType(id, createDto.Name, createDto.Category, createDto.Description);

            await _dbContext.TransactionTypes.AddAsync(transactionType);

            await _dbContext.SaveChangesAsync();

            var createdTransactionType = await _dbContext.TransactionTypes
                .FirstOrDefaultAsync(t => t.Id == id);

            if (createdTransactionType is null)
            {
                throw new InvalidOperationException("Failed to retrieve the created transaction type.");
            }

            var newTransactionTypeDto = new TransactionTypeDto
            {
                Id = transactionType.Id,
                Name = transactionType.Name,
                Category = transactionType.Category,
                Description = transactionType.Description
            };

            return newTransactionTypeDto;
        }

        public async Task<TransactionTypeDto> UpdateTransactionTypeAsync(UpdateTransactionTypeDto updateDto)
        {
            if (string.IsNullOrEmpty(updateDto.Name))
            {
                throw new ArgumentException("The transaction type name cannot be empty.");
            }

            if (string.IsNullOrEmpty(updateDto.Description))
            {
                throw new ArgumentException("The transaction type description cannot be empty.");
            }

            var existingTransactionType = await _dbContext.TransactionTypes
                .FirstOrDefaultAsync(t => t.Id == updateDto.Id);

            if (existingTransactionType is null)
            {
                throw new InvalidOperationException("The transaction type with the specified ID does not exist.");
            }

            existingTransactionType.Name = updateDto.Name.Trim();
            existingTransactionType.Category = updateDto.Category;
            existingTransactionType.Description = updateDto.Description.Trim();

            await _dbContext.SaveChangesAsync();

            var updatedTransactionType = await _dbContext.TransactionTypes
                .FirstOrDefaultAsync(t => t.Id == updateDto.Id);

            if (updatedTransactionType is null)
            {
                throw new InvalidOperationException("Failed to retrieve the updated transaction type.");
            }

            var updatedTransactionTypeDto = new TransactionTypeDto
            {
                Id = existingTransactionType.Id,
                Name = existingTransactionType.Name,
                Category = existingTransactionType.Category,
                Description = existingTransactionType.Description
            };

            return updatedTransactionTypeDto;
        }

        public async Task DeleteTransactionTypeAsync(Guid transactionTypeId)
        {
            if (transactionTypeId == Guid.Empty)
            {
                throw new ArgumentException("The transaction type ID cannot be empty.");
            }

            var existingTransactionType = await _dbContext.TransactionTypes
                .FirstOrDefaultAsync(t => t.Id == transactionTypeId);

            if (existingTransactionType is null)
            {
                throw new InvalidOperationException("The transaction type with the specified ID does not exist.");
            }

            _dbContext.TransactionTypes.Remove(existingTransactionType);

            await _dbContext.SaveChangesAsync();
        }
    }
}
