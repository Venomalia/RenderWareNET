using AuroraLib.Core.Buffers;
using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public sealed class RWFrameList : RWPluginList<Frame>
    {
        public RWFrameList()
        { }

        public RWFrameList(RWVersion version, IEnumerable<Frame> materials) : base(version, materials)
        { }

        public RWFrameList(Stream stream) : base(stream)
        { }

        protected override void ReadData(Stream stream)
        {
            Clear();
            Capacity = stream.Read<int>();

            using SpanBuffer<Frame> buffer = new(Capacity);
            stream.Read<Frame>(buffer);
            foreach (Frame item in buffer)
            {
                Add(item);
            }
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Count);
            stream.Write(this);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Struct;
    }
}
