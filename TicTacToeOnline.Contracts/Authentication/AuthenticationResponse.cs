namespace TicTacToeOnline.Contracts.Authentication
{
    public record AuthenticationResponse
    (
        Guid Id,
        string Email,
        string Name,
        string Token
    );
}
