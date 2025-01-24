using AuroraLib.Core.Buffers;
using AuroraLib.Core.Collections;
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
    public sealed class RWAtomicSector : RWPlugin
    {
        public int MatListWindowBase;

        public BoundingBox Box;
        public int CollSectorPresent;
        public int Unused;

        public readonly List<Vector3> Vertexs = new List<Vector3>();
        public readonly List<RGBA32> Colors = new List<RGBA32>();
        public readonly List<RGBA32> Colors2 = new List<RGBA32>();
        public readonly List<TexCoords> UVs = new List<TexCoords>();
        public readonly List<Triangle> Triangles = new List<Triangle>();

        public bool IsCollision = false;
        public bool IsShadow = false;

        public RWAtomicSector()
        { }

        public RWAtomicSector(Stream stream) : base(stream)
        { }

        public RWAtomicSector(RWVersion version) : base(version)
        {
        }

        public void Read(Stream stream, bool isShadow)
        {
            IsShadow = isShadow;
            BinaryDeserialize(stream);
        }

        protected override void ReadData(Stream stream)
        {
            long startSectionPosition = stream.Position;

            MatListWindowBase = stream.Read<int>();
            int numTriangles = stream.Read<int>();
            int numVertices = stream.Read<int>();
            Box = stream.Read<BoundingBox>();
            CollSectorPresent = stream.Read<int>();
            Unused = stream.Read<int>();

            Vertexs.Clear();
            Colors.Clear();
            Colors2.Clear();
            UVs.Clear();
            Triangles.Clear();

            stream.ReadCollection(Vertexs, numVertices);

            long trianglesPosition = startSectionPosition + Header.SectionSize - 8 * numTriangles;
            IsCollision = stream.Position == trianglesPosition;
            if (!IsCollision)
            {
                stream.ReadCollection(Colors, numVertices);

                //two color Arrays?
                long uvPosition = startSectionPosition + Header.SectionSize - 8 * numTriangles - 8 * numVertices;
                if (uvPosition - stream.Position == 4 * numVertices)
                {
                    stream.ReadCollection(Colors2, numVertices);
                }
                stream.Seek(uvPosition, SeekOrigin.Begin);
                stream.ReadCollection(UVs, numVertices);
            }

            stream.Seek(trianglesPosition, SeekOrigin.Begin);
            stream.ReadCollection(Triangles, numTriangles);
            if (IsShadow)
            {
                ReversTriangle(Triangles.UnsafeAsSpan());
            }
        }

        protected override void WriteData(Stream stream)
        {
            if (!IsCollision)
            {
                if (Triangles.Count != Colors.Count)
                    throw new InvalidOperationException("Triangles.Count and Colors.Count must match.");

                if (Triangles.Count != UVs.Count)
                    throw new InvalidOperationException("Triangles.Count and UVs.Count must match.");

                if (Colors2.Count != 0 && Triangles.Count != Colors2.Count)
                    throw new InvalidOperationException("If Colors2 is used, its count must match Triangles.Count.");
            }

            stream.Write(MatListWindowBase);
            stream.Write(Triangles.Count);
            stream.Write(Vertexs.Count);
            stream.Write(Box);
            stream.Write(CollSectorPresent);
            stream.Write(Unused);

            stream.WriteCollection(Vertexs);
            if (!IsCollision)
            {
                stream.WriteCollection(Colors);
                if (Colors2.Count != 0)
                    stream.WriteCollection(Colors2);
                stream.WriteCollection(UVs);
            }

            if (IsShadow)
            {
                using SpanBuffer<Triangle> Buffer = new SpanBuffer<Triangle>(Triangles.UnsafeAsSpan());
                ReversTriangle(Buffer);
                stream.Write<Triangle>(Buffer);
            }
            else
            {
                stream.WriteCollection(Triangles);
            }
        }

        private static void ReversTriangle(Span<Triangle> triangles)
        {
            for (int i = 0; i < triangles.Length; i++)
            {
                (triangles[i].materialIndex, triangles[i].vertex1, triangles[i].vertex2, triangles[i].vertex3) = (triangles[i].vertex1, triangles[i].vertex2, triangles[i].vertex3, triangles[i].materialIndex);
            }
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Struct;
    }
}
