using IdentityBLL.Models;

namespace IdentityBLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(Guid id);
        Task AddAsync(UserModel model);
        Task UpdateAsync(UserModel model);
        Task DeleteAsync(Guid id);
        Task<string> AuthorizeAsync(string email, string password);
    }
}
