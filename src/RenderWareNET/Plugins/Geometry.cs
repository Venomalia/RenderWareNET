using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;
using System.IO;

namespace RenderWareNET.Plugins
{
    public sealed class Geometry : RWPlugin
    {
        public readonly RWGeometry Properties = new RWGeometry();
        public readonly MaterialList Materials = new MaterialList();
        public readonly Extension Extension = new Extension();

        public Geometry()
        { }

        public Geometry(Stream stream) : base(stream)
        { }

        public Geometry(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            Properties.BinaryDeserialize(stream);
            Materials.BinaryDeserialize(stream);
            Extension.BinaryDeserialize(stream);
        }

        protected override void WriteData(Stream stream)
        {
            Properties.BinarySerialize(stream);
            Materials.BinaryDeserialize(stream);
            Extension.BinaryDeserialize(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Geometry;
    }
}
