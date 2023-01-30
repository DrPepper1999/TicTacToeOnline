using FluentValidation;

namespace TicTacToeOnline.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.PlayersForStart)
                .NotEmpty()
                .InclusiveBetween(2, 8);
            RuleFor(x => x.MapSize)
                .NotEmpty()
                .InclusiveBetween(3, 12)
                .Must(x => x % 3 == 0);
            RuleFor(x => x.PlayerId).NotEmpty();
        }
    }
}
