using System.Numerics;

namespace RenderWareNET.Structs
{
    public struct Line
    {
        public Vector3 Start, End;

        public Line(Vector3 start, Vector3 end)
        {
            Start = start;
            End = end;
        }
    }
}
