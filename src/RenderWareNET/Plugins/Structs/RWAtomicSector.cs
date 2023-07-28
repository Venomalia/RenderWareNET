using AuroraLib.Core.Buffers;
using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;
using System.Numerics;

namespace RenderWareNET.Plugins.Structs
{
    public sealed class RWAtomicSector : RWPlugin
    {
        public int MatListWindowBase;

        public BoundingBox Box;
        public int CollSectorPresent;
        public int Unused;

        public readonly List<Vector3> Vertexs = new();
        public readonly List<RGBA32> Colors = new();
        public readonly List<RGBA32> Colors2 = new();
        public readonly List<TexCoords> UVs = new();
        public readonly List<Triangle> Triangles = new();

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
            Read(stream);
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

            Vertexs.Capacity = numVertices;
            using (SpanBuffer<Vector3> buffer = new(numVertices))
            {
                stream.Read<Vector3>(buffer);
                foreach (Vector3 item in buffer)
                {
                    Vertexs.Add(item);
                }
            }

            long trianglesPosition = startSectionPosition + Header.SectionSize - 8 * numTriangles;
            IsCollision = stream.Position == trianglesPosition;

            if (!IsCollision)
            {
                Colors.Capacity = numVertices;
                using (SpanBuffer<RGBA32> buffer = new(numVertices))
                {
                    stream.Read<RGBA32>(buffer);
                    foreach (RGBA32 item in buffer)
                    {
                        Colors.Add(item);
                    }

                    //two color Arrays?
                    long uvPosition = startSectionPosition + Header.SectionSize - 8 * numTriangles - 8 * numVertices;
                    if (uvPosition - stream.Position == 4 * numVertices)
                    {
                        Colors2.Capacity = numVertices;
                        stream.Read<RGBA32>(buffer);
                        foreach (RGBA32 item in buffer)
                        {
                            Colors2.Add(item);
                        }
                    }
                    stream.Seek(uvPosition, SeekOrigin.Begin);
                }

                UVs.Capacity = numVertices;
                using (SpanBuffer<TexCoords> buffer = new(numVertices))
                {
                    stream.Read<TexCoords>(buffer);
                    foreach (TexCoords item in buffer)
                    {
                        UVs.Add(item);
                    }
                }
            }

            stream.Seek(trianglesPosition, SeekOrigin.Begin);

            Triangles.Capacity = numTriangles;
            using (SpanBuffer<Triangle> buffer = new(numTriangles))
            {
                stream.Read<Triangle>(buffer);
                if (IsShadow)
                {
                    ReversTriangle(buffer);
                }
                foreach (Triangle item in buffer)
                {
                    Triangles.Add(item);
                }
            }
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(MatListWindowBase);
            stream.Write(Triangles.Count);
            stream.Write(Vertexs.Count);
            stream.Write(Box);
            stream.Write(CollSectorPresent);
            stream.Write(Unused);

            stream.Write(Vertexs);
            if (!IsCollision)
            {
                stream.Write(Colors);
                stream.Write(Colors2);
                stream.Write(UVs);
            }

            if (IsShadow)
            {
                using SpanBuffer<Triangle> Buffer = new(Triangles);
                ReversTriangle(Buffer);
                stream.Write<Triangle>(Buffer);
            }
            else
            {
                stream.Write(Triangles);
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
