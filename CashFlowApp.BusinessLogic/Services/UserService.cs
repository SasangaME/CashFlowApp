namespace CashFlowApp.BusinessLogic.Services;

using CashFlowApp.BusinessLogic.Exceptions;
using CashFlowApp.Models.Entities;
using CashFlowApp.Repositories.Repos;
using CashFlowApp.Utils.Security;

public interface IUserService
{
    Task<IEnumerable<User>> FindAll();
    Task<User> FindById(int id);
    Task<User> Create(User user);
    Task<User> Update(int id, User user);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleService _roleService;

    public UserService(IUserRepository userRepository, IRoleService roleService)
    {
        _userRepository = userRepository;
        _roleService = roleService;
    }

    public async Task<IEnumerable<User>> FindAll()
    {
        return await _userRepository.FindAll();
    }

    public async Task<User> FindById(int id)
    {
        var user = await _userRepository.FindById(id);
        return user ?? throw new NotFoundException($"user not found for id: {id}");
    }

    public async Task<User> Create(User user)
    {
        user.CreatedAt = DateTime.Now;
        user.CreatedBy = -1;
        user.Password = PasswordHash.HashPassword(user.Password);
        user.Role = await _roleService.FindById(user.RoleId);
        user.Id = await _userRepository.Create(user);
        return user;
    }

    public async Task<User> Update(int id, User user)
    {
        var existingUser = await FindById(id);
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Password = PasswordHash.HashPassword(user.Password);
        existingUser.UpdatedAt = DateTime.Now;
        existingUser.UpdatedBy = user.Id;
        await _userRepository.Update(existingUser);
        return user;
    }
}