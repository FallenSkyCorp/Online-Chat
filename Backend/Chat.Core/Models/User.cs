
namespace Chat.Core.Models
{
    public class User
    {
        public const int MIN_PASSWORD_LENGHT = 3; // поменять на 8
        private User(Guid id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }
        public Guid Id { get; }
        public string Email { get; } = string.Empty;
        public string Password { get; } = string.Empty;

        public static (User User, string Error) Create(Guid id, string email, string password)
        {
            var error = string.Empty;

            if (password.Length < MIN_PASSWORD_LENGHT)
            {
                error += "Слишком короткий пароль.";
            }

            User user = new User(id, email, password);

            return (user, error);
        }
    }
}
