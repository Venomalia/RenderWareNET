using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;
using System.IO;

namespace RenderWareNET.Plugins
{
    public sealed class Atomic : RWPlugin
    {
        public RWAtomic Properties;
        public readonly Extension Extension = new Extension();

        public Atomic()
        { }

        public Atomic(Stream stream) : base(stream)
        { }

        public Atomic(RWVersion version) : base(version)
        {
            Properties.Header = new RWPluginHeader(PluginID.Struct, 16, version);
            Extension = new Extension(version);
        }

        protected override void ReadData(Stream stream)
        {
            Properties = stream.Read<RWAtomic>();
            Extension.BinaryDeserialize(stream);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Properties);
            Extension.BinarySerialize(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Atomic;
    }
}
