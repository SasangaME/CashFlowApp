namespace CashFlowApp.BusinessLogic.Services;

using CashFlowApp.BusinessLogic.Exceptions;
using CashFlowApp.Models.Entities;
using CashFlowApp.Repositories.Repos;

public interface IRoleService
{
    Task<IEnumerable<Role>> FindAll();
    Task<Role> FindById(int id);
    Task<Role> Create(Role role);
    Task<Role> Update(int id, Role role);
}

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<Role>> FindAll()
    {
        return await _roleRepository.FindAll();
    }

    public async Task<Role> FindById(int id)
    {
        var role = await _roleRepository.FindById(id);
        return role ?? throw new NotFoundException($"role not found for id: {id}");
    }

    public async Task<Role> Create(Role role)
    {
        if (await _roleRepository.IsRoleExists(role.RoleEnum))
        {
            throw new ValidationException("role already exists");
        }

        role.CreatedAt = DateTime.Now;
        role.CreatedBy = -1;
        role.Id = await _roleRepository.Create(role);
        return role;
    }

    public async Task<Role> Update(int id, Role role)
    {
        var existingRole = await FindById(id);
        existingRole.Name = role.Name;
        existingRole.Description = role.Description;
        existingRole.UpdatedAt = DateTime.Now;
        existingRole.UpdatedBy = -1;
        await _roleRepository.Update(existingRole);
        return existingRole;
    }
}