using MarsRover.Model.DataType;

namespace MarsRover.Model
{
    public class World
    {
        public Field[,] map;
        public int size { get; set; } // HxB

        public World(int size)
        {
            map = new Field[size,size];
            this.size = size;
            InitializeMap(size);
        }

        private void InitializeMap(int size)
        {
            for (var i=0; i<size; i++)
            {
                for (var j=0; j<size; j++)
                {
                    if (j%3 == 0 && i%3 == 0)
                    {
                        map[j, i] = new Field(FieldType.STONE, j, i);
                    }
                    else
                    {
                        map[j, i] = new Field(FieldType.DIRT, j, i);
                    }
                }
            }
            map[0, 0] = new Field(FieldType.DIRT, 0, 0);

            map[size - 1, size - 1] = new Field(FieldType.EXIT, size - 1 , size - 1);
        }

        public Field GetField(int x, int y)
        {
            if (x >= size)
            {
                x = 0;
            }
            if (x < 0)
            {
                x = size - 1;
            }
            if (y >= size)
            {
                y = 0;
            }
            if (y < 0)
            {
                y = size - 1;
            }

            return map[x, y];
        }
    }
}
