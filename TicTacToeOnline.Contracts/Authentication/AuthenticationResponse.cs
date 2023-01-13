namespace TicTacToeOnline.Contracts.Authentication
{
    public record AuthenticationResponse
    (
        Guid id,
        string Email,
        string Name,
        string Token
    );
}
