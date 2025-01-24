using AuroraLib.Core.Buffers;
using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace RenderWareNET.Plugins.Structs
{
    public sealed class RWGeometry : RWPlugin
    {
        public GeometryAttributes GeometryFlag;

        public int NumTriangles => Triangles.Length;
        public int NumVertices;
        public int NumMorphTargets => MorphTargets.Count;

        public SurfaceProperties Surface;

        public RGBA32[] VertexColors = Array.Empty<RGBA32>();
        public Vector2[] TextCoords = Array.Empty<Vector2>();
        public Vector2[] TextCoords2 = Array.Empty<Vector2>();
        public Triangle[] Triangles = Array.Empty<Triangle>();
        public readonly List<MorphTarget> MorphTargets = new List<MorphTarget>();

        public RWGeometry() : base()
        { }

        public RWGeometry(Stream stream) : base(stream)
        { }

        public RWGeometry(RWVersion version) : base(version)
        {
        }

        protected override void ReadData(Stream stream)
        {
            MorphTargets.Clear();
            GeometryFlag = stream.Read<GeometryAttributes>();

            int numTriangles = stream.Read<int>();
            NumVertices = stream.Read<int>();
            int numMorphTargets = stream.Read<int>();

            if (Header.Version.Version == 3 && Header.Version.Major == 4)
            {
                Surface = stream.Read<SurfaceProperties>();
            }

            if ((GeometryFlag & GeometryAttributes.NativeGeometry) != 0)
            {
                MorphTargets.Add(new MorphTarget(stream));
                return;
            }

            VertexColors = new RGBA32[NumVertices];
            if ((GeometryFlag & GeometryAttributes.VertexColors) != 0)
                stream.Read<RGBA32>(VertexColors);

            TextCoords = new Vector2[NumVertices];
            if ((GeometryFlag & GeometryAttributes.TextCoords) != 0)
                stream.Read<Vector2>(TextCoords);

            TextCoords2 = new Vector2[NumVertices];
            if ((GeometryFlag & GeometryAttributes.MultipleTextCoords) != 0)
                stream.Read<Vector2>(TextCoords2);

            Triangles = new Triangle[numTriangles];
            stream.Read<Triangle>(Triangles);
            ReversTriangle(Triangles);

            MorphTargets.Capacity = numMorphTargets;
            for (int i = 0; i < numMorphTargets; i++)
            {
                MorphTargets[i] = new MorphTarget(stream, NumVertices);
            }
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(GeometryFlag);

            stream.Write(NumTriangles);
            stream.Write(NumVertices);
            stream.Write(NumMorphTargets);

            if (Header.Version.Version == 3 && Header.Version.Major == 4)
            {
                stream.Write(Surface);
            }

            if ((GeometryFlag & GeometryAttributes.NativeGeometry) != 0)
            {
                MorphTargets[0].BinarySerialize(stream);
                return;
            }

            if ((GeometryFlag & GeometryAttributes.VertexColors) != 0)
            {
                stream.Write<RGBA32>(VertexColors);
            }

            if ((GeometryFlag & GeometryAttributes.TextCoords) != 0)
            {
                stream.Write<Vector2>(TextCoords);
            }
            if ((GeometryFlag & GeometryAttributes.MultipleTextCoords) != 0)
            {
                stream.Write<Vector2>(TextCoords2);
            }

            using SpanBuffer<Triangle> Buffer = new SpanBuffer<Triangle>(Triangles);
            ReversTriangle(Buffer);
            stream.Write<Triangle>(Triangles);

            for (int i = 0; i < MorphTargets.Count; i++)
            {
                MorphTargets[i].BinarySerialize(stream);
            }
        }

        private static void ReversTriangle(Span<Triangle> triangles)
        {
            for (int i = 0; i < triangles.Length; i++)
            {
                (triangles[i].materialIndex, triangles[i].vertex2) = (triangles[i].vertex2, triangles[i].materialIndex);
            }
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Struct;
    }
}
