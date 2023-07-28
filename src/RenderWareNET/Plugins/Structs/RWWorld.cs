using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;
using System.Numerics;

namespace RenderWareNET.Plugins.Structs
{
    public sealed class RWWorld : RWPlugin
    {
        public int RootIsWorldSector;
        public Vector3 InverseOrigin;
        public uint NumTriangles;
        public uint NumVertices;
        public uint NumPlaneSectors;
        public uint NumAtomicSectors;
        public uint ColSectorSize;
        public GeometryAttributes Flag;

        public BoundingBox Box;

        public RWWorld()
            => Header = new(PluginID.Struct, 0x40);

        public RWWorld(Stream stream) : base(stream)
        { }

        protected override void ReadData(Stream stream)
        {
            RootIsWorldSector = stream.Read<int>();
            InverseOrigin = stream.Read<Vector3>();
            if (Header.SectionSize == 0x34)
            {
                Vector3 Point = stream.Read<Vector3>();
                Box = new(Point, Point);
            }
            NumTriangles = stream.Read<uint>();
            NumVertices = stream.Read<uint>();
            NumPlaneSectors = stream.Read<uint>();
            NumAtomicSectors = stream.Read<uint>();
            ColSectorSize = stream.Read<uint>();
            Flag = stream.Read<GeometryAttributes>();
            if (Header.SectionSize == 0x40)
            {
                Box = stream.Read<BoundingBox>();
            }
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(RootIsWorldSector);
            stream.Write(InverseOrigin);

            if (Header.SectionSize == 0x34)
            {
                stream.Write(Box.GetCenter());
            }
            stream.Write(NumTriangles);
            stream.Write(NumVertices);
            stream.Write(NumPlaneSectors);
            stream.Write(NumAtomicSectors);
            stream.Write(ColSectorSize);
            stream.Write(Flag);
            if (Header.SectionSize == 0x40)
            {
                stream.Write(Box);
            }
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Struct;
    }
}
