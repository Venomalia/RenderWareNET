﻿using System.Numerics;

namespace RenderWareNET.Structs
{
    public struct Sphere
    {
        public Vector3 Position;
        public float Radius;

        public Sphere(Vector3 position, float radius)
        {
            Position = position;
            Radius = radius;
        }
    }
}
