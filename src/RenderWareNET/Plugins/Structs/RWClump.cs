using RenderWareNET.Enums;
using RenderWareNET.Interfaces;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public struct RWClump : IHasRWPluginHeader
    {
        /// <inheritdoc/>
        public RWPluginHeader Header { get; set; }
        public int AtomicCount;
        public int LightCount;
        public int CameraCount;

        public RWClump() : this(new(), 0, 0, 0)
        { }

        public RWClump(RWVersion version, int atomicCount, int lightCount, int cameraCount)
        {
            Header = new RWPluginHeader(PluginID.Struct, 12, version);
            AtomicCount = atomicCount;
            LightCount = lightCount;
            CameraCount = cameraCount;
        }
    }
}
