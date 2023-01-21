using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.GameAggregate.Enums;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.GameAggregate.Entities
{
    public class Map : Entity<MapId>
    {
        private readonly Mark[,] _fields;
        public int Size { get; private set; }
        public bool IsAllCellFilled => Size * Size == _fillCellCount;
        private int _fillCellCount = 0;

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        public Map(MapId id, int size) : base(id)
        {
            Size = size;
            _fields = new Mark[size, size];
        }

        public static Map Create(int size)
        {
            return new Map(MapId.CreateUnique(), size);
        }

        #pragma warning disable CS8618
        private Map()
        {
        }
        #pragma warning disable CS8618

        public Mark this[int x, int y]
        {
            get => _fields[x, y];
            set
            {
                if (!InBounds(x, y)) throw new ArgumentException($"кординаты должны быть больше 0 и меньше {Size}");
                if (value == Mark.Empty) throw new ArgumentException("Поле не может быть пустым");
                _fields[x, y] = value;
                _fillCellCount++;
            }
        }
        public Mark[,] GetMap()
        {
            return _fields;
        }

        private bool InBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x <= Size && y <= Size;
        }
    }
}
