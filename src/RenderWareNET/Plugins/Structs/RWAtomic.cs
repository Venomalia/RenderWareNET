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

        public RWAtomic(RWVersion version) : this()
            => Header = new(PluginID.Struct, 16, version);

        public RWAtomic()
        {
            Header = new(PluginID.Struct, 16);
            FrameIndex = GeometryIndex = Unused = 0;
            Flag = AtomicAttributes.None;
        }
    }
}
