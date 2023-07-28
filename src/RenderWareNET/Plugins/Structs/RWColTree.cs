using AuroraLib.Core.Buffers;
using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public sealed class RWColTree : RWPlugin
    {
        public bool UseMap;
        public BoundingBox Box;

        public readonly List<Split> Splits = new();
        public readonly List<ushort> Triangles = new();

        public RWColTree()
        { }

        public RWColTree(Stream stream) : base(stream)
        { }

        public RWColTree(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            UseMap = stream.Read<int>() != 0;
            Box = stream.Read<BoundingBox>();
            int numTriangles = stream.Read<int>();
            int numSplits = stream.Read<int>();

            Splits.Clear();
            Triangles.Clear();
            Splits.Capacity = numSplits;
            Splits.Capacity = numTriangles;

            using (SpanBuffer<Split> buffer = new(numSplits))
            {
                stream.Read<Split>(buffer);
                foreach (Split item in buffer)
                {
                    Splits.Add(item);
                }
            }
            using (SpanBuffer<ushort> buffer = new(numTriangles))
            {
                stream.Read<ushort>(buffer);
                foreach (ushort item in buffer)
                {
                    Triangles.Add(item);
                }
            }
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(UseMap ? 1 : 0);
            stream.Write(Box);
            stream.Write(Triangles.Count);
            stream.Write(Splits.Count);
            stream.Write(Splits);
            stream.Write(Triangles);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Struct;

        public struct Split
        {
            public Sector negativeSector;
            public Sector positiveSector;
        }

        public struct Sector
        {
            public SectorOrientation type;
            public byte triangleAmount;
            public ushort referenceIndex;
            public float splitPosition;
        }
    }
}
