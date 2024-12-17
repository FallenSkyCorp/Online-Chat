using Chat.Core.Abstractions;
using Chat.Core.Models;
using Users.DataAccess.Repositories;

namespace Users.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository) 
        { 
            _usersRepository = usersRepository; 
        }

        public async Task<Guid> CreateUser(User user)
        {
            return await _usersRepository.Create(user);
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _usersRepository.Delete(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.GetAll();
        }

        public async Task<Guid> UpdateUser(Guid id, string email, string password)
        {
            return await _usersRepository.Update(id, email, password);
        }
    }
}
