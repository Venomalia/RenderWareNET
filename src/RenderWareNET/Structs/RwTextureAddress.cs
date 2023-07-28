using RenderWareNET.Enums;

namespace RenderWareNET.Structs
{
    public struct RWTextureWrap
    {
        private byte wrapMode;

        public TextureWrapMode U
        {
            get => (TextureWrapMode)((wrapMode & 0xF0) >> 4);
            set => wrapMode = (byte)((wrapMode & 0x0F) | ((byte)value << 4));
        }

        public TextureWrapMode V
        {
            get => (TextureWrapMode)(wrapMode & 0x0F);
            set => wrapMode = (byte)((wrapMode & 0xF0) | (byte)value);
        }

        public RWTextureWrap(TextureWrapMode u = TextureWrapMode.Repeat, TextureWrapMode v = TextureWrapMode.Repeat)
            => wrapMode = (byte)(((byte)v & 0x0F) | ((byte)u << 4));
    }
}
