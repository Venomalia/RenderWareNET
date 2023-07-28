using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class TextureDictionary : RWPluginList<TextureNative>
    {
        public RWPlatformID Platform;
        public readonly Extension Extension = new();

        public TextureDictionary()
        { }

        public TextureDictionary(Stream stream) : base(stream)
        { }

        public TextureDictionary(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            RWTextureDictionary properties = stream.Read<RWTextureDictionary>();
            Platform = properties.Platform;
            Capacity = properties.TextureCount;

            for (int i = 0; i < properties.TextureCount; i++)
            {
                Add(new(stream));
            }
            Extension.Read(stream);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(new RWTextureDictionary(Header.Version, (ushort)Count, Platform));
            foreach (TextureNative texture in this)
            {
                texture.Write(stream);
            }
            Extension.Write(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.TextureDictionary;
    }
}
