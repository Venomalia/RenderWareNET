namespace RenderWareNET.Enums
{

    /// <summary>
    /// FilterMode specifies what type of filtering the file should use.
    /// </summary>
    public enum RWTextureFilterMode : byte
    {
        /// <summary>
        /// filtering is disabled
        /// </summary>
        None = 0,

        /// <summary>
        /// Point Sampling, No Mipmap
        /// </summary>
        Nearest = 1,

        /// <summary>
        /// Bilinear Filtering, No Mipmap
        /// </summary>
        Linear = 2,

        /// <summary>
        /// Point Sampling, Discrete Mipmap
        /// </summary>
        NearestMipmapNearest = 3,

        /// <summary>
        /// Bilinear Filtering, Discrete Mipmap
        /// </summary>
        NearestMipmapLinear = 4,

        /// <summary>
        /// Point Sampling, Linear MipMap
        /// </summary>
        LinearMipmapNearest = 5,

        /// <summary>
        /// Trilinear Filtering
        /// </summary>
        LinearMipmapLinear = 6
    }
}
