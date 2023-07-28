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

        public RWGeometryList() : this(new())
        { }

        public RWGeometryList(RWVersion version, int count = 0)
        {
            Header = new(PluginID.Struct, 4, version);
            Count = count;
        }
    }
}
