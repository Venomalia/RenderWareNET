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

        public RWClump(RWVersion version = default, int atomicCount = 0, int lightCount = 0, int cameraCount = 0)
        {
            Header = new RWPluginHeader(PluginID.Struct, 12, version);
            AtomicCount = atomicCount;
            LightCount = lightCount;
            CameraCount = cameraCount;
        }
    }
}
