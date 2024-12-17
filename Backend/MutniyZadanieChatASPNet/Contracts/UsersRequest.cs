namespace MutniyZadanieChatASPNet.API.Contracts
{
    public record class UsersRequest
    {
        public UsersRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; init; } = "";
        public string Password { get; init; } = "";

    }
}
