using MarsRover.Model.DataType;

namespace MarsRover.Model.Interface
{
    interface IRover
    {
        void Move(Field f);
        Field ScanField(Field f);
        void Turn(Direction d);
        bool MoveUsingCommandList(char[] commands, World world);
    }
}
