using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MarsRover.Model.Interface
{
    interface IRoverController
    {
        void KeyboardHandler(KeyEventArgs e);
        World GetWorld();
        bool MoveRoverUsingCommandList(char[] commands, World world);
    }
}
