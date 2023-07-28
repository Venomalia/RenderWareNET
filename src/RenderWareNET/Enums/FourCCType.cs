namespace RenderWareNET.Enums
{
    public enum FourCCType : int
    {
        None = -1,
        /// <summary>
        /// A block-compressed texture format with 1-bit alpha.
        /// </summary>
        DXT1 = 827611204,

        /// <summary>
        /// A block-compressed texture format with explicit alpha.
        /// </summary>
        DXT2 = 844388420,

        /// <summary>
        /// A block-compressed texture format with 4-bit alpha.
        /// </summary>
        DXT3 = 861165636,

        /// <summary>
        /// A block-compressed texture format with interpolated alpha.
        /// </summary>
        DXT4 = 877942852,

        /// <summary>
        /// A block-compressed texture format with 8-bit alpha.
        /// </summary>
        DXT5 = 894720068,

        /// <summary>
        /// A 32-bit texture format where 8 bits are allocated for each of Red, Green, Blue, and Alpha channels.
        /// </summary>
        C8888 = 0x15,

        /// <summary>
        /// A 24-bit texture format where 8 bits are allocated for each of Red, Green, and Blue channels.
        /// </summary>
        C888 = 0x16,

        /// <summary>
        /// A 16-bit texture format where 5 bits are allocated for Red and Blue channels, and 6 bits are allocated for the Green channel.
        /// </summary>
        C565 = 0x17,

        /// <summary>
        /// A 16-bit texture format where 5 bits are allocated for each of Red, Green, and Blue channels.
        /// </summary>
        C555 = 0x18,

        /// <summary>
        /// A 16-bit texture format where 5 bits are allocated for each of Red, Green, and Blue channels, and 1 bit is allocated for the Alpha channel.
        /// </summary>
        C1555 = 0x19,

        /// <summary>
        /// A 16-bit texture format where 4 bits are allocated for each of Red, Green, Blue, and Alpha channels.
        /// </summary>
        C4444 = 0x1A,

        /// <summary>
        /// A 8-bit palette texture format, Up to 256 different colors.
        /// </summary>
        PAL = 0x29,

        /// <summary>
        /// A 8-bit luminance texture format where each pixel stores a single luminance value.
        /// </summary>
        LUM8 = 0x32,

        /// <summary>
        /// A 16-bit texture format where 8 bits are allocated for each luminance and alpha channels.
        /// </summary>
        LUM8A8 = 0x33,

        //GC WII

        //I4 = 0x00, // Invalid?

        /// <inheritdoc cref="FourCCType.LUM8"/>
        I8 = 0x01,

        //IA4 = 0x02, // Invalid?

        /// <inheritdoc cref="FourCCType.LUM8A8"/>
        IA8 = 0x03,

        /// <inheritdoc cref="FourCCType.C565"/>
        RGB565 = 0x04,

        /// <summary>
        ///  16-bit texture format either 5 bits are allocated for each of Red, Green, and Blue channels or
        ///  4 bits are allocated for each of Red, Green, Blue, and 3-bit for Alpha channel.
        /// </summary>
        RGB5A3 = 0x05,

        /// <inheritdoc cref="FourCCType.C8888"/>
        RGBA32 = 0x06,

        /// <summary>
        /// A 4-bit palette texture format, Up to 16 different colors.
        /// </summary>
        C4 = 0x08,

        /// <inheritdoc cref="FourCCType.PAL"/>
        C8 = 0x09,

        //C14X2 = 0x0A, // Invalid

        /// <inheritdoc cref="FourCCType.DXT1"/>
        CMPR = 0x0E
    }
}
