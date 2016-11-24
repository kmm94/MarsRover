using MarsRover.Model.Interface;
using MarsRover.Model.DataType;
using System.Windows.Media;
using Image = System.Windows.Controls.Image;
using System;
using System.Windows;
using MarsRover.Exception;

namespace MarsRover.Model
{
    class Rover : IRover
    {
        public int x { get; set; } //x koordinater    
        public int y { get; set; } //y koordinater 
        public Image image { get; set; }

        // hvor meget billedet skal rotere
        private readonly int rotateWest = 90;
        private readonly int rotateNorth = 180;
        private readonly int rotateEast = 270;
        private readonly int rotateSouth = 360;

        public Direction CurrentDirection { get; set; } // rotre

        private static readonly Rover instance = new Rover();

        private Rover()
        {
            x = 0;
            y = 0;

            image = new Image();

            CurrentDirection = Direction.S;
        }

        public static Rover Instance
        {
            get
            {
                return instance;
            }
        }

        public void Move(Field f)
        {
            x = f.x;
            y = f.y;
        }

        public Field ScanField(Field f)
        {
            if (f != null && (f.type == FieldType.DIRT || f.type == FieldType.EXIT))
            {
                return f;
            }
            else
            {              
                y = 0;
                x = 0;
                MessageBoxResult confirmation = MessageBox.Show("ugyldit move", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return null;
        }

        public void Turn(Direction d)
        {
            RotateTransform rotate;

            switch (d)
            {
                case Direction.N:
                    rotate = new RotateTransform(rotateNorth, (image.ActualWidth/2), (image.ActualHeight/2));
                    image.RenderTransform = rotate;
                    break;
                case Direction.E:
                    rotate = new RotateTransform(rotateEast, (image.ActualWidth/2), (image.ActualHeight/2));
                    image.RenderTransform = rotate;
                    break;
                case Direction.S:
                    rotate = new RotateTransform(rotateSouth, (image.ActualWidth/2), (image.ActualHeight/2));
                    image.RenderTransform = rotate;
                    break;
                case Direction.W:
                    rotate = new RotateTransform(rotateWest, (image.ActualWidth/2), (image.ActualHeight/2));
                    image.RenderTransform = rotate;
                    break;
            }
            CurrentDirection = d;
        }

        public bool MoveUsingCommandList(char[] commands, World world)
        {
            RotateTransform rotate;            

            foreach (char c in commands)
            {
                switch (Char.ToUpper(c))
                {
                    case 'N':
                        Turn(Direction.N);
                        break;
                    case 'E':
                        Turn(Direction.E);
                        break;
                    case 'S':
                        Turn(Direction.S);
                        break;
                    case 'W':
                        Turn(Direction.W);
                        break;
                    default:
                        throw new InvalidMoveException();
                }

                Field clearPath = null;
                switch (CurrentDirection)
                {
                    case Direction.N:
                        clearPath = ScanField(world.GetField(x, y - 1));
                        break;
                    case Direction.E:
                        clearPath = ScanField(world.GetField(x + 1, y));
                        break;
                    case Direction.S:
                        clearPath = ScanField(world.GetField(x, y + 1));
                        break;
                    case Direction.W:
                        clearPath = ScanField(world.GetField(x - 1, y));
                        break;
                }
                if (clearPath != null)
                    Move(clearPath);
                else
                    throw new FieldNotClearException();
            }
            return world.GetField(x, y).type == FieldType.EXIT;
        }
    }
}
