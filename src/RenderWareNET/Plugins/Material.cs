using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class Material : RWPlugin
    {
        public RWMaterial Properties;
        public readonly Texture Texture = new();
        public readonly Extension Extension = new();

        public Material()
        { }

        public Material(Stream stream) : base(stream)
        { }

        public Material(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            Properties = stream.Read<RWMaterial>();
            if (Properties.HasTexture)
            {
                Texture.Read(stream);
            }
            Extension.Read(stream);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Properties);
            if (Properties.HasTexture)
            {
                Texture.Write(stream);
            }
            Extension.Write(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Material;
    }
}
