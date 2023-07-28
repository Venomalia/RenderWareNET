using System.Numerics;

namespace RenderWareNET.Structs
{
    public struct Frame
    {
        public Vector3 Right;
        public Vector3 Up;
        public Vector3 Forward;
        public Vector3 Position;
        public int ParentFrame;
        public int Unknown;
    }
}
