using ErrorOr;
using System.Drawing;
using TicTacToeOnline.Domain.Common.Enums;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.GameAggregate.Entities
{
    public class Map : Entity<MapId>
    {
        private readonly Mark[,] _fields;
        public int Size { get; private set; }
        public bool IsAllCellFilled => Size * Size == _fillCellCount;
        private int _fillCellCount;

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        public Map(MapId id, int size) : base(id)
        {
            Size = size;
            _fillCellCount = 0;
            _fields = new Mark[size, size];
        }

        public static Map Create(int size)
        {
            return new Map(MapId.CreateUnique(), size);
        }

        public bool InBounds(Point point) =>
            point.X < 0 || point.Y < 0 && point.X >= Size || point.Y >= Size;

        public ErrorOr<Mark> this[int x, int y]
        {
            get
            {
                if (InBounds(new Point(x, y)))
                {
                    return Errors.Map.OutOfBoundsMap;
                }

                return _fields[x, y];
            }
            private set
            {
                _fields[x, y] = value.Value;
                _fillCellCount++;
            }
        }

        public Error? SetField(Point point, Mark mark)
        {
            if (InBounds(point))
            {
                return Errors.Map.OutOfBoundsMap;
            }

            if (mark == Mark.Empty)
            {
                return Errors.Common.MarkCannotBeEmpty;
            }

            this[point.X, point.Y] = mark;

            // TODO менять currentMoveMark;
            return null;
        }

        public Error? SetField(int x, int y, Mark mark) => SetField(new Point(x, y), mark);

        public Mark[,] GetFields()
        {
            return _fields;
        }

#pragma warning disable CS8618
        private Map()
        {
        }
#pragma warning disable CS8618
    }
}
