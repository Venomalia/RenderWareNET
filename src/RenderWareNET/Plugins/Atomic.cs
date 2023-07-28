using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class Atomic : RWPlugin
    {
        public RWAtomic Properties;
        public readonly Extension Extension = new();

        public Atomic()
        { }

        public Atomic(Stream stream) : base(stream)
        { }

        public Atomic(RWVersion version) : base(version)
        {
            Properties.Header = new(PluginID.Struct, 16, version);
            Extension = new(version);
        }

        protected override void ReadData(Stream stream)
        {
            Properties = stream.Read<RWAtomic>();
            Extension.Read(stream);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Properties);
            Extension.Write(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Atomic;
    }
}
