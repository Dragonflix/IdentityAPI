using IdentityBLL.Models;

namespace IdentityBLL.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleModel>> GetAllAsync();
        Task<RoleModel> GetByIdAsync(Guid id);
        Task AddAsync(RoleModel model);
        Task UpdateAsync(RoleModel model);
        Task DeleteAsync(Guid id);
    }
}
