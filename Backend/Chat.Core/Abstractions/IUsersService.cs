using Chat.Core.Models;

namespace Chat.Core.Abstractions
{
    public interface IUsersService
    {
        Task<Guid> CreateUser(User user);

        Task<Guid> DeleteUser(Guid id);

        Task<List<User>> GetAllUsers();

        Task<Guid> UpdateUser(Guid id, string email, string password);
    }
}
