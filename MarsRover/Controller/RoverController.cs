using System;
using System.Windows;
using System.Windows.Input;
using MarsRover.Exception;
using MarsRover.Model;
using MarsRover.Model.DataType;
using MarsRover.Model.Interface;

namespace MarsRover.Controller
{
    class RoverController : IRoverController
    {
        public World world;
        public RoverController(int size)
        {
            this.world = new World(size);
        }
        /// <summary>
        /// lytter til hvilken key der bliver trykket på.
        /// </summary>
        /// <param name="e"></param>
        public void KeyboardHandler(KeyEventArgs e)
        {
         
            switch (e.Key) // her vælger man hvilken vej billede vender
            {
                case Key.L:
                {
                    if (Rover.Instance.CurrentDirection == Direction.S)
                    {
                        Rover.Instance.Turn(Direction.E);
                        
                    }else if (Rover.Instance.CurrentDirection == Direction.W)
                    {
                        Rover.Instance.Turn(Direction.S);
                    }
                        else if (Rover.Instance.CurrentDirection == Direction.N)
                        {
                            Rover.Instance.Turn(Direction.W);
                        }
                        else if (Rover.Instance.CurrentDirection == Direction.E)
                        {
                            Rover.Instance.Turn(Direction.N);
                        }
                    return;
                }
                case Key.R:
                {
                    if (Rover.Instance.CurrentDirection == Direction.N)
                    {
                        Rover.Instance.Turn(Direction.E);
                    } else
                    if (Rover.Instance.CurrentDirection == Direction.E)
                    {
                        Rover.Instance.Turn(Direction.S);
                    } else
                    if (Rover.Instance.CurrentDirection == Direction.S)
                    {
                        Rover.Instance.Turn(Direction.W);
                    } else
                    if (Rover.Instance.CurrentDirection == Direction.W)
                    {
                        Rover.Instance.Turn(Direction.N);
                    }
                    return;
                }

                case Key.F:
                {
                    Field clearPath = null;

                    switch (Rover.Instance.CurrentDirection) // koordinatsystemet er spejlvent her flytter man marsrover
                    {
                        case Direction.N:
                            clearPath = Rover.Instance.ScanField(world.GetField(Rover.Instance.x, Rover.Instance.y - 1));
                            break;
                        case Direction.E:
                            clearPath = Rover.Instance.ScanField(world.GetField(Rover.Instance.x+1, Rover.Instance.y));
                            break;
                        case Direction.S:
                            clearPath = Rover.Instance.ScanField(world.GetField(Rover.Instance.x, Rover.Instance.y+1));
                            break;
                        case Direction.W:
                            clearPath = Rover.Instance.ScanField(world.GetField(Rover.Instance.x - 1, Rover.Instance.y));
                            break;
                    }
                    if (clearPath != null)
                    {
                        try
                        {
                            Rover.Instance.Move(clearPath);
                        }
                        catch (FieldNotClearException ex)
                        {

 
                         }
                    }
                    break;
                }

                case Key.B:
                    {
                        Field clearPath = null;

                        switch (Rover.Instance.CurrentDirection) // koordinatsystemet er spejlvent her flytter man marsrover
                        {
                            case Direction.N:
                                clearPath = Rover.Instance.ScanField(world.GetField(Rover.Instance.x, Rover.Instance.y + 1));
                                break;
                            case Direction.E:
                                clearPath = Rover.Instance.ScanField(world.GetField(Rover.Instance.x - 1, Rover.Instance.y));
                                break;
                            case Direction.S:
                                clearPath = Rover.Instance.ScanField(world.GetField(Rover.Instance.x, Rover.Instance.y - 1));
                                break;
                            case Direction.W:
                                clearPath = Rover.Instance.ScanField(world.GetField(Rover.Instance.x + 1, Rover.Instance.y));
                                break;
                        }
                        if (clearPath != null)
                        {
                            try
                            {
                                Rover.Instance.Move(clearPath);
                            }
                            catch (FieldNotClearException ex)
                            {
                                MessageBoxResult confirmation = MessageBox.Show("ugyldit move", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        break;
                    }
                case Key.Enter:
                {
                    char[] commands = new char[2];
                    commands[0] = 'N';
                    commands[1] = 'W';
                    try
                    {
                        Rover.Instance.MoveUsingCommandList(commands, this.world);
                    }
                   
                        catch (InvalidMoveException ex)
                    {

                    }

                        catch (FieldNotClearException ex)
                    {

                    }
                    break;
                }
            }
        }

        public World GetWorld()
        {
            return this.world;
        }

        public bool MoveRoverUsingCommandList(char[] commands, World world)
        {
            return Rover.Instance.MoveUsingCommandList(commands, world);
        }
    }
}
