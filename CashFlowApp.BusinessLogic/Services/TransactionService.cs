using CashFlowApp.BusinessLogic.Exceptions;
using CashFlowApp.Models.Entities;
using CashFlowApp.Repositories.Repos;

namespace CashFlowApp.BusinessLogic.Services;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> FindAll(int userId, int pageNumber, int pageSize);
    Task<Transaction> FindById(int id, int userId);
    Task<Transaction> Create(int userId, Transaction transaction);
    Task<Transaction> Update(int id, int userId, Transaction transaction);
}

public class TransactionService(ITransactionRepository transactionRepository, ICategoryService categoryService) : ITransactionService
{
    public async Task<IEnumerable<Transaction>> FindAll(int userId, int pageNumber, int pageSize)
    {
        return await transactionRepository.FindAll(userId, pageNumber, pageSize);
    }

    public async Task<Transaction> FindById(int id, int userId)
    {
        return await transactionRepository.FindById(id, userId) ??
               throw new NotFoundException($"transaction not found for transaction id: {id}");
    }

    public async Task<Transaction> Create(int userId, Transaction transaction)
    {
        transaction.CreatedBy = userId;
        var category = await categoryService.FindById(transaction.CategoryId);
        transaction.Category = category;
        transaction.CreatedAt = DateTime.UtcNow;
        transaction.CreatedBy = userId;
        transaction.Id = await transactionRepository.Create(transaction);
        return transaction;
    }

    public async Task<Transaction> Update(int id, int userId, Transaction transaction)
    {
        var existingTransaction = await transactionRepository.FindById(id, userId);
        var category = await categoryService.FindById(transaction.CategoryId);
        existingTransaction.Category = category;
        existingTransaction.Name = transaction.Name;
        existingTransaction.Description = transaction.Description;
        existingTransaction.Amount = transaction.Amount;
        existingTransaction.IsOutflow = transaction.IsOutflow;
        existingTransaction.UpdatedBy = userId;
        existingTransaction.UpdatedAt = DateTime.Now;
        await transactionRepository.Update(existingTransaction);
        return transaction;
    }
}