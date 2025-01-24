using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;
using System.IO;

namespace RenderWareNET.Plugins
{
    public sealed class ColTree : RWPlugin
    {
        public RWColTree Properties = new RWColTree();

        public ColTree()
        { }

        public ColTree(Stream stream) : base(stream)
        { }

        public ColTree(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
            => Properties.BinaryDeserialize(stream);

        protected override void WriteData(Stream stream)
            => Properties.BinarySerialize(stream);

        protected override PluginID GetExpectedIdentifier()
            => PluginID.ColTree;
    }
}
