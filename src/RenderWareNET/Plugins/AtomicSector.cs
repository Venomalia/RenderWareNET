using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;
using System.IO;

namespace RenderWareNET.Plugins
{
    public sealed class AtomicSector : RWPlugin
    {
        public readonly RWAtomicSector Properties = new RWAtomicSector();
        public readonly Extension Extension = new Extension();

        public AtomicSector()
        { }

        public AtomicSector(Stream stream) : base(stream)
        { }

        public AtomicSector(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            Properties.BinaryDeserialize(stream);
            Extension.BinaryDeserialize(stream);
        }

        protected override void WriteData(Stream stream)
        {
            Properties.BinarySerialize(stream);
            Extension.BinarySerialize(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.AtomicSector;
    }
}
