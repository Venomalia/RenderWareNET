using System;

namespace RenderWareNET.Enums
{
    [Flags]
    internal enum TextureAttributes : int
    {
        /// <inheritdoc cref="FourCCType.LUM8A8"/>
        LUM8A8 = 0,

        /// <summary>
        /// Use for most GC
        /// </summary>
        Native = 4,

        /// <inheritdoc cref="FourCCType.C1555"/>
        C1555 = 0x0100,

        /// <inheritdoc cref="FourCCType.C565"/>
        C565 = 0x0200,

        /// <inheritdoc cref="FourCCType.C4444"/>
        C4444 = 0x0300,

        /// <inheritdoc cref="FourCCType.LUM8"/>
        LUM8 = 0x0400,

        /// <inheritdoc cref="FourCCType.C8888"/>
        C8888 = 0x0500,

        /// <inheritdoc cref="FourCCType.C888"/>
        C888 = 0x0600,

        /// <summary>
        /// A 16-bit texture format used for depth buffer purposes.
        /// </summary>
        D16 = 0x0700,

        /// <summary>
        /// A 24-bit texture format used for depth buffer purposes.
        /// </summary>
        D24 = 0x0800,

        /// <summary>
        /// A 32-bit texture format used for depth buffer purposes.
        /// </summary>
        D32 = 0x0900,

        /// <inheritdoc cref="FourCCType.C555"/>
        C555 = 0x0A00,

        /// <summary>
        /// A flag indicating that the texture uses automatic mipmap generation.
        /// </summary>
        AutoMipMaps = 0x1000,

        /// <summary>
        /// A palette texture format with 256 colors.
        /// </summary>
        PAL8 = 0x2000,

        /// <summary>
        /// A palette texture format with 16 colors.
        /// </summary>
        PAL4 = 0x4000,

        /// <summary>
        /// A flag indicating that the texture uses mipmapping.
        /// </summary>
        MipMaps = 0x8000,

        unk = 0x20000
    }

    public static class TextureRasterFormatInfo
    {
        internal static bool IsPaletteFormat(this TextureAttributes format)
            => ((int)format & 0x6000) != 0;

        public static bool IsPaletteFormat(this FourCCType format)
            => format == FourCCType.PAL || format == FourCCType.C4 || format == FourCCType.C8;

        internal static int GetPaletteCollors(this TextureAttributes format)
            => format.IsPaletteFormat() ? (((int)format & (int)TextureAttributes.PAL8) != 0 ? 256 : 16) : 0;

        internal static TextureAttributes GetPixelFormat(this TextureAttributes format)
            => (TextureAttributes)((int)format & 0xF00);

        internal static int GetBPP(this TextureAttributes format)
            => format.GetPixelFormat().ToFourCC().GetBPP();

        internal static int GetPaletteByteSize(this TextureAttributes format)
            => (format.GetPaletteCollors() * format.GetBPP()) / 8;

        public static int GetBPP(this FourCCType format) => format switch
        {
            FourCCType.DXT1 => 16,
            FourCCType.DXT2 => 16,
            FourCCType.DXT3 => 16,
            FourCCType.DXT4 => 16,
            FourCCType.DXT5 => 16,
            FourCCType.C8888 => 32,
            FourCCType.C888 => 24,
            FourCCType.C565 => 16,
            FourCCType.C555 => 16,
            FourCCType.C1555 => 16,
            FourCCType.C4444 => 16,
            FourCCType.PAL => 8,
            FourCCType.LUM8 => 8,
            FourCCType.LUM8A8 => 16,
            FourCCType.I8 => 8,
            FourCCType.IA8 => 16,
            FourCCType.RGB565 => 16,
            FourCCType.RGB5A3 => 16,
            FourCCType.RGBA32 => 32,
            FourCCType.C4 => 4,
            FourCCType.C8 => 8,
            FourCCType.CMPR => 4,
            _ => throw new NotImplementedException(),
        };

        internal static FourCCType ToFourCC(this TextureAttributes format, bool isGC = false)
        {
            if (format.IsPaletteFormat())
            {
                format = (TextureAttributes)((int)format & 0x6000);
            }
            else
            {
                format = format.GetPixelFormat();
            }

            if (isGC)
            {
                return format switch
                {
                    TextureAttributes.C1555 => FourCCType.RGB5A3,
                    TextureAttributes.C4444 => FourCCType.RGB5A3, //
                    TextureAttributes.C565 => FourCCType.RGB565,
                    TextureAttributes.LUM8 => FourCCType.I8,
                    TextureAttributes.C8888 => FourCCType.RGBA32,
                    TextureAttributes.PAL8 => FourCCType.C8,
                    TextureAttributes.PAL4 => FourCCType.C4,
                    TextureAttributes.LUM8A8 => FourCCType.IA8,
                    _ => throw new NotImplementedException(),
                };
            }
            else
            {
                return format switch
                {
                    TextureAttributes.C1555 => FourCCType.C1555,
                    TextureAttributes.C565 => FourCCType.C565,
                    TextureAttributes.C4444 => FourCCType.C4444,
                    TextureAttributes.LUM8 => FourCCType.LUM8,
                    TextureAttributes.C8888 => FourCCType.C8888,
                    TextureAttributes.C888 => FourCCType.C888,
                    TextureAttributes.C555 => FourCCType.C555,
                    TextureAttributes.PAL8 => FourCCType.PAL,
                    TextureAttributes.PAL4 => FourCCType.PAL,
                    TextureAttributes.LUM8A8 => FourCCType.LUM8A8,
                    _ => throw new NotImplementedException(),
                };
            }
        }

        internal static TextureAttributes FromFourCC(this FourCCType format, int colors = 0) => format switch
        {
            FourCCType.DXT1 => TextureAttributes.C1555,
            FourCCType.DXT2 => TextureAttributes.C565,
            FourCCType.DXT3 => TextureAttributes.C565,
            FourCCType.DXT4 => TextureAttributes.C565,
            FourCCType.DXT5 => TextureAttributes.C565,
            FourCCType.C8888 => TextureAttributes.C8888,
            FourCCType.C888 => TextureAttributes.C888,
            FourCCType.C565 => TextureAttributes.C565,
            FourCCType.C555 => TextureAttributes.C555,
            FourCCType.C1555 => TextureAttributes.C1555,
            FourCCType.C4444 => TextureAttributes.C4444,
            FourCCType.PAL => colors > 16 ? TextureAttributes.PAL8 : TextureAttributes.PAL4,
            FourCCType.LUM8 => TextureAttributes.LUM8,
            FourCCType.LUM8A8 => TextureAttributes.LUM8A8,

            //GC WII
            FourCCType.I8 => TextureAttributes.LUM8,
            FourCCType.IA8 => TextureAttributes.LUM8A8,
            FourCCType.RGB565 => TextureAttributes.C565,
            FourCCType.RGB5A3 => TextureAttributes.C1555,
            FourCCType.RGBA32 => TextureAttributes.C8888,
            FourCCType.C4 => TextureAttributes.PAL4,
            FourCCType.C8 => TextureAttributes.PAL8,
            FourCCType.CMPR => TextureAttributes.LUM8A8,
            _ => throw new NotImplementedException(),
        };

        internal static TextureAttributes Build(FourCCType format, FourCCType tlotformat, int colors, bool hasMipmaps, bool IsNative, bool useAutoMipMaps)
            => (TextureAttributes)
            (
                (IsNative ? (int)TextureAttributes.Native : 0x0) |
                (hasMipmaps ? (int)TextureAttributes.MipMaps : 0x0) |
                (useAutoMipMaps ? (int)TextureAttributes.AutoMipMaps : 0x0) |
                (int)format.FromFourCC(colors) |
                (format.IsPaletteFormat() ? (int)tlotformat.FromFourCC(colors) : 0x0)
            );

        internal static sbyte GetNativeSubFormat(FourCCType format, FourCCType tlotformat)
            => (sbyte)(format.IsPaletteFormat() ? tlotformat switch
            {
                FourCCType.IA8 => 0,
                FourCCType.RGB565 => 1,
                FourCCType.RGB5A3 => 2,
                _ => throw new NotImplementedException(),
            } : -1);
    }

}