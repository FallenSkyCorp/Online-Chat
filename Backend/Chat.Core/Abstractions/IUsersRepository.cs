using Chat.Core.Models;

namespace Users.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<Guid> Create(User user);
        Task<Guid> Delete(Guid id);
        Task<List<User>> GetAll();
        Task<Guid> Update(Guid id, string email, string password);
    }
}