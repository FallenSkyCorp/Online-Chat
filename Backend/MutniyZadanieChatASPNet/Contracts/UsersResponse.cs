namespace MutniyZadanieChatASPNet.API.Contracts
{
    public record class UsersResponse(
        Guid id,
        string email,
        string password);
}
