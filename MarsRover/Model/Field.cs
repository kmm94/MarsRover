using MarsRover.Model.DataType;
using System.Windows.Controls;

namespace MarsRover.Model
{
    public class Field
    {
        public FieldType type { get; set; }
        public Image image { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public Field(FieldType type, int x, int y)
        {
            this.type = type;
            image = new Image();
            this.x = x;
            this.y = y;
        }
    }
}
