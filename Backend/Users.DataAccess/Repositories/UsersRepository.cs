using Chat.Core.Models;
using Microsoft.EntityFrameworkCore;
using Users.DataAccess.Entities;

namespace Users.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersDbContext _context;
        public UsersRepository(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            var userEntities = await _context.Users
                .AsNoTracking().
                ToListAsync();

            var users = userEntities
                .Select(u => User.Create(u.Id, u.Email, u.Password).User)
                .ToList();

            return users;
        }

        public async Task<Guid> Create(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
            };

            await _context.Users.AddAsync(userEntity);

            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string email, string password)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Email, u => email)
                    .SetProperty(u => u.Password, u => password));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
