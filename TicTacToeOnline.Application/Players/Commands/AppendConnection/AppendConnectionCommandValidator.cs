using FluentValidation;

namespace TicTacToeOnline.Application.Players.Commands.AppendConnection
{
    public class AppendConnectionCommandValidator : AbstractValidator<AppendConnectionCommand>
    {
        public AppendConnectionCommandValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty().NotNull();
        }
    }
}
