using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;
using System.Collections.Generic;
using System.IO;

namespace RenderWareNET.Plugins.Structs
{
    public sealed class RWColTree : RWPlugin
    {
        public bool UseMap;
        public BoundingBox Box;

        public readonly List<Split> Splits = new List<Split>();
        public readonly List<ushort> Triangles = new List<ushort>();

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

            stream.ReadCollection(Splits, numSplits);
            stream.ReadCollection(Triangles, numTriangles);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(UseMap ? 1 : 0);
            stream.Write(Box);
            stream.Write(Triangles.Count);
            stream.Write(Splits.Count);
            stream.WriteCollection(Splits);
            stream.WriteCollection(Triangles);
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
