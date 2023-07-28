using RenderWareNET.Enums;

namespace RenderWareNET.Structs
{
    public struct RWTextureSampling
    {
        public RWTextureFilterMode FilterMode;
        public RWTextureWrap WrapMode;
        public ushort UseMipLevels;

        public RWTextureSampling(RWTextureFilterMode filterMode = RWTextureFilterMode.Nearest, RWTextureWrap wrapMode = new(), ushort useMipLevels = 0)
        {
            FilterMode = filterMode;
            WrapMode = wrapMode;
            UseMipLevels = useMipLevels;
        }
        public static unsafe implicit operator RWTextureSampling(int x)
            => *(RWTextureSampling*)&x;

        public static unsafe implicit operator int(RWTextureSampling x)
            => *(int*)&x;
    }
}
