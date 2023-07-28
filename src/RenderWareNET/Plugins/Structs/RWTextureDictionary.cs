using RenderWareNET.Enums;
using RenderWareNET.Interfaces;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public struct RWTextureDictionary : IHasRWPluginHeader
    {
        /// <inheritdoc/>
        public RWPluginHeader Header { get; set; }
        public ushort TextureCount;
        public RWPlatformID Platform;

        public RWTextureDictionary(RWVersion version, ushort textureCount = 0, RWPlatformID platform = RWPlatformID.None)
        {
            Header = new RWPluginHeader(PluginID.Struct, 4, version);
            TextureCount = textureCount;
            Platform = version.Major > 5 ? platform : RWPlatformID.None;
        }

        public RWTextureDictionary() : this(new())
        { }
    }
}
