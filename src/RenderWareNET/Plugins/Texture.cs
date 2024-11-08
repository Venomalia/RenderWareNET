using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class Texture : RWPlugin
    {
        public RWTexture Properties;
        public readonly RWString DiffuseName = new();
        public readonly RWString AlphaName = new();
        public readonly Extension Extension = new();

        public Texture() : base()
        { }

        public Texture(Stream stream) : base(stream)
        { }

        public Texture(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            Properties = stream.Read<RWTexture>();
            DiffuseName.BinaryDeserialize(stream);
            AlphaName.BinaryDeserialize(stream);
            Extension.BinaryDeserialize(stream);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Properties);
            DiffuseName.BinarySerialize(stream);
            AlphaName.BinarySerialize(stream);
            Extension.BinarySerialize(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Texture;
    }
}
