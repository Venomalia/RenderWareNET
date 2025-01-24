using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;
using System.Collections.Generic;
using System.IO;

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
            stream.ReadCollection(this, Capacity);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Count);
            stream.WriteCollection(this);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Struct;
    }
}
