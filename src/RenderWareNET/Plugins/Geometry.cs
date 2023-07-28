using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class Geometry : RWPlugin
    {
        public readonly RWGeometry Properties = new();
        public readonly MaterialList Materials = new();
        public readonly Extension Extension = new();

        public Geometry()
        { }

        public Geometry(Stream stream) : base(stream)
        { }

        public Geometry(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            Properties.Read(stream);
            Materials.Read(stream);
            Extension.Read(stream);
        }

        protected override void WriteData(Stream stream)
        {
            Properties.Write(stream);
            Materials.Write(stream);
            Extension.Write(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Geometry;
    }
}
