using FluentValidation;

namespace TicTacToeOnline.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            //RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.PlayersForStart).InclusiveBetween(2, 8)
                .Must(x => !x.HasValue || x % 2 == 0);
            RuleFor(x => x.PlayerId).NotEmpty();
        }
    }
}
