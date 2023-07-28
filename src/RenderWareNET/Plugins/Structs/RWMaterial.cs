using RenderWareNET.Enums;
using RenderWareNET.Interfaces;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public struct RWMaterial : IHasRWPluginHeader
    {
        /// <inheritdoc/>
        public RWPluginHeader Header { get; set; }

        public int UnusedFlags;
        public RGBA32 Color;
        public int UnusedInt2;
        private int hasTexture;
        public float Ambient;
        public float Specular;
        public float Diffuse;

        public bool HasTexture
        {
            get => hasTexture != 0;
            set => hasTexture = value ? 1 : 0;
        }

        public RWMaterial(RWVersion version) : this()
        {
            Header = new RWPluginHeader(PluginID.Struct, 28, version);
        }
    }
}
