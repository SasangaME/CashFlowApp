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

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryService _categoryService;

    public TransactionService(ITransactionRepository transactionRepository, ICategoryService categoryService)
    {
        _transactionRepository = transactionRepository;
        _categoryService = categoryService;
    }

    public async Task<IEnumerable<Transaction>> FindAll(int userId, int pageNumber, int pageSize)
    {
        return await _transactionRepository.FindAll(userId, pageNumber, pageSize);
    }

    public async Task<Transaction> FindById(int id, int userId)
    {
        return await _transactionRepository.FindById(id, userId) ??
               throw new NotFoundException($"transaction not found for transaction id: {id}");
    }

    public async Task<Transaction> Create(int userId, Transaction transaction)
    {
        transaction.CreatedBy = userId;
        var category = await _categoryService.FindById(transaction.CategoryId);
        transaction.Category = category;
        transaction.Id = await _transactionRepository.Create(transaction);
        return transaction;
    }

    public async Task<Transaction> Update(int id, int userId, Transaction transaction)
    {
        var existingTransaction = await _transactionRepository.FindById(id, userId);
        var category = await _categoryService.FindById(transaction.CategoryId);
        existingTransaction.Category = category;
        existingTransaction.Name = transaction.Name;
        existingTransaction.Description = transaction.Description;
        existingTransaction.Amount = transaction.Amount;
        existingTransaction.IsOutflow = transaction.IsOutflow;
        existingTransaction.UpdatedBy = userId;
        existingTransaction.UpdatedAt = DateTime.Now;
        await _transactionRepository.Update(existingTransaction);
        return transaction;
    }
}