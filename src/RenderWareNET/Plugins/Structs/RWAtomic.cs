using RenderWareNET.Enums;
using RenderWareNET.Interfaces;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public struct RWAtomic : IHasRWPluginHeader
    {
        /// <inheritdoc/>
        public RWPluginHeader Header { get; set; }
        public int FrameIndex;
        public int GeometryIndex;
        public AtomicAttributes Flag;
        public int Unused;

        public RWAtomic(RWVersion version = default)
        {
            Header = new RWPluginHeader(PluginID.Struct, 16, version);
            FrameIndex = GeometryIndex = Unused = 0;
            Flag = AtomicAttributes.None;
        }
    }
}
