using RenderWareNET.Enums;
using RenderWareNET.Interfaces;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public struct RWGeometryList : IHasRWPluginHeader
    {
        /// <inheritdoc/>
        public RWPluginHeader Header { get; set; }

        public int Count;

        public RWGeometryList(RWVersion version = default, int count = 0)
        {
            Header = new RWPluginHeader(PluginID.Struct, 4, version);
            Count = count;
        }
    }
}
