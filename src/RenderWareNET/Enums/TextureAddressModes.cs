namespace RenderWareNET.Enums
{
    /// <summary>
    /// Specifies how texture coordinates outside the range [0,1] are handled.
    /// </summary>
    public enum TextureWrapMode : byte
    {
        /// <summary>
        /// No tiling. Texture coordinates outside the range are not tiled or repeated.
        /// </summary>
        None = 0,

        /// <summary>
        /// Tiles the texture, creating a repeating pattern.
        /// </summary>
        Repeat = 1,

        /// <summary>
        /// Tiles the texture, creating a repeating pattern by mirroring it at every integer boundary.
        /// </summary>
        MirrorRepeat = 2,

        /// <summary>
        /// Clamps the texture to the last pixel at the edge.
        /// </summary>
        Clamp = 3,

        /// <summary>
        /// Specifies a border color for texture coordinates outside the range.
        /// </summary>
        Border = 4
    }
}
