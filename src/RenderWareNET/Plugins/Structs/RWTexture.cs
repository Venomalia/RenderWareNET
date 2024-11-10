using RenderWareNET.Enums;
using RenderWareNET.Interfaces;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public struct RWTexture : IHasRWPluginHeader
    {
        /// <inheritdoc/>
        public RWPluginHeader Header { get; set; }
        public RWTextureSampling Sampling;

        public RWTexture(RWVersion version = new(), RWTextureSampling sampling = new())
        {
            Header = new RWPluginHeader(PluginID.Struct, 4, version);
            Sampling = sampling;
        }
    }
}
