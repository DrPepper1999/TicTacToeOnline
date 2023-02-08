using FluentValidation;
using TicTacToeOnline.Domain.Common.Enums;

namespace TicTacToeOnline.Application.Games.Commands.MakeMove
{
    public class MakeMoveCommandValidator : AbstractValidator<MakeMoveCommand>
    {
        public MakeMoveCommandValidator()
        {
            RuleFor(x => x.Move).NotEmpty();
            RuleFor(x => x.TeamId).NotEmpty();
            RuleFor(x => x.GameId).NotEmpty();
            RuleFor(x => x.Mark)
                .NotEmpty()
                .IsInEnum()
                .Must(x => x != Mark.Empty);
        }
    }
}
