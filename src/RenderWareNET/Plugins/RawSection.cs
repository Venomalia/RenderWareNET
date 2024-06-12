using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class RawSection : RWPlugin
    {
        public byte[] RawData = Array.Empty<byte>();

        public RawSection()
        { }

        public RawSection(Stream stream) : base(stream)
        { }

        public RawSection(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            RawData = new byte[Header.SectionSize];
            stream.Read(RawData);
        }

        protected override void WriteData(Stream stream)
            => stream.Write(RawData);

        protected override PluginID GetExpectedIdentifier()
            => Header.Identifier;
    }
}
