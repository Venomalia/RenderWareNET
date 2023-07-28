using System.Numerics;

namespace RenderWareNET.Structs
{
    public struct BoundingBox
    {
        public Vector3 Min, Max;

        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        public Vector3 GetCenter()
        {
            return (Min + Max) * 0.5f;
        }
    }
}
