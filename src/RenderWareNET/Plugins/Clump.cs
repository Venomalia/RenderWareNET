using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class Clump : RWPlugin
    {
        public RWClump Properties;
        public readonly FrameList FrameList = new();
        public readonly GeometryList GeometryList = new();
        public readonly List<Atomic> AtomicList = new();
        public readonly Extension Extension = new();

        public Clump()
        { }

        public Clump(Stream stream) : base(stream)
        { }

        public Clump(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            Properties = stream.Read<RWClump>();
            FrameList.BinaryDeserialize(stream);
            GeometryList.BinaryDeserialize(stream);

            AtomicList.Clear();
            AtomicList.Capacity = Properties.AtomicCount;
            for (int i = 0; i < Properties.AtomicCount; i++)
            {
                AtomicList.Add(new(stream));
            }
            Extension.BinaryDeserialize(stream);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Properties);
            FrameList.BinarySerialize(stream);
            GeometryList.BinarySerialize(stream);
            foreach (Atomic atomic in AtomicList)
            {
                atomic.BinarySerialize(stream);
            }
            Extension.BinarySerialize(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Clump;
    }
}
