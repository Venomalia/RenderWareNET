using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;
using System.IO;

namespace RenderWareNET.Plugins
{
    public sealed class TextureNative : RWPlugin
    {
        public readonly RWTextureNative Properties = new RWTextureNative();
        public readonly Extension Extension = new Extension();

        public TextureNative()
        { }

        public TextureNative(Stream stream) : base(stream)
        { }

        public TextureNative(RWVersion version) : base(version)
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
            => PluginID.TextureNative;
    }
}
