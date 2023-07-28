using AuroraLib.Core;
using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public sealed partial class RWTextureNative : RWPlugin
    {
        public TexturePlatformID Platform;

        public RWTextureSampling SamplingSettings;

        public int gcnUnknown1;
        public int gcnUnknown2;
        public int gcnUnknown3;
        public int gcnUnknown4;
        public int Unknown5;

        public string TextureName = string.Empty;
        public string AlphaName = string.Empty;

        public ushort Width { get; private set; }
        public ushort Height { get; private set; }
        public bool HasAlpha;
        public bool UseAutoMipMaps;

        public FourCCType Format { get; private set; }
        public FourCCType TLOTFormat { get; private set; }
        public byte Images { get; private set; }

        public byte[] TLOT { get; private set; } = Array.Empty<byte>();
        public byte[] ImageData { get; private set; } = Array.Empty<byte>();

        public int TLOTColors => Format.IsPaletteFormat() ? TLOT.Length / (TLOTFormat.GetBPP() / 8) : 0;

        public RWTextureNative()
        { }

        public RWTextureNative(Stream stream) : base(stream)
        { }

        public RWTextureNative(byte[] imageData, ushort width, ushort height, FourCCType format, byte images = 1)
        {
            Width = width;
            Height = height;
            Format = format;
            Images = images;
            ImageData = imageData;
        }

        public RWTextureNative(byte[] imageData, ushort width, ushort height, FourCCType format, byte[] tlot, FourCCType tlotFormat, byte images = 1) : this(imageData, width, height, format, images)
        {
            TLOT = tlot;
            TLOTFormat = tlotFormat;
        }

        protected override void ReadData(Stream stream)
        {
            Platform = stream.Read<TexturePlatformID>();
            Endian endian = Platform == TexturePlatformID.GC ? Endian.Big : Endian.Little;
            SamplingSettings = stream.Read<int>(endian);
            if (Platform == TexturePlatformID.GC)
            {
                gcnUnknown1 = stream.Read<int>(Endian.Big);
                gcnUnknown2 = stream.Read<int>(Endian.Big);
                gcnUnknown3 = stream.Read<int>(Endian.Big);
                gcnUnknown4 = stream.Read<int>(Endian.Big);
            }
            else if (Platform == TexturePlatformID.PS2 || Platform == TexturePlatformID.PSP)
            {
                ReadPSData(stream);
            }

            TextureName = stream.ReadString(32);
            AlphaName = stream.ReadString(32);
            TextureAttributes attributes = stream.Read<TextureAttributes>(endian);
            UseAutoMipMaps = ((int)attributes & (int)TextureAttributes.AutoMipMaps) != 0;
            if (((int)attributes & (int)TextureAttributes.Native) == 0)
            {
                if (Platform == TexturePlatformID.D3D9)
                {
                    Format = stream.Read<FourCCType>(endian);
                    HasAlpha = true;
                }
                else
                {
                    Format = attributes.ToFourCC(Platform == TexturePlatformID.GC);
                    HasAlpha = stream.Read<int>(endian) != 0;
                }
            }
            TLOTFormat = attributes.IsPaletteFormat() ? attributes.GetPixelFormat().ToFourCC(Platform == TexturePlatformID.GC) : FourCCType.None;
            Width = stream.Read<ushort>(endian);
            Height = stream.Read<ushort>(endian);

            byte bpp = stream.ReadUInt8();
            Images = stream.ReadUInt8();
            byte format = stream.ReadUInt8();
            sbyte SubFormat = stream.ReadInt8();

            if (((int)attributes & (int)TextureAttributes.Native) != 0)
            {
                Format = (FourCCType)format;
                // SubFormat is GC TLOT format
                // -1 none, 0 IA8, 1 RGB565, 2 RGB5A3
                Unknown5 = stream.Read<int>(endian);
            }
            else
            {
                if (SubFormat > 0)
                {
                    Format = Platform switch
                    {
                        TexturePlatformID.Xbox => (SubFormat & 0x3) switch
                        {
                            0 => FourCCType.DXT1,
                            1 => FourCCType.DXT2,
                            2 => FourCCType.DXT3,
                            4 => FourCCType.DXT5,
                            _ => throw new NotImplementedException(),
                        },
                        TexturePlatformID.D3D8 => SubFormat switch
                        {
                            1 => FourCCType.DXT1,
                            2 => FourCCType.DXT2,
                            3 => FourCCType.DXT3,
                            4 => FourCCType.DXT4,
                            8 => FourCCType.DXT5,
                            _ => throw new NotImplementedException(),
                        },
                        TexturePlatformID.D3D9 => Format,
                        TexturePlatformID.GC => (FourCCType)SubFormat,
                        _ => throw new NotImplementedException(),
                    };
                }
            }

            int imagesize = 0;
            if (Platform == TexturePlatformID.Xbox)
            {
                imagesize = stream.Read<int>(endian);
            }

            int PaletteSize = attributes.GetPaletteByteSize();
            if (PaletteSize != 0)
            {
                TLOT = stream.Read(PaletteSize);
            }
            else
            {
                TLOT = Array.Empty<byte>();
            }

            if (Platform != TexturePlatformID.Xbox)
            {
                imagesize = stream.Read<int>(endian);
            }
            ImageData = stream.Read(imagesize);
        }

        private void ReadPSData(Stream stream)
        {
            TextureName = new RWString(stream).Value;
            AlphaName = new RWString(stream).Value;
            RWPluginHeader Header = stream.Read<RWPluginHeader>();
            RWPluginHeader ImageHeader = stream.Read<RWPluginHeader>();
            Width = (ushort)stream.Read<int>();
            Height = (ushort)stream.Read<int>();
            int bpp = stream.Read<int>();
            TextureAttributes attributes = stream.Read<TextureAttributes>();
            UseAutoMipMaps = ((int)attributes & (int)TextureAttributes.AutoMipMaps) != 0;
            throw new NotImplementedException();
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Platform);
            Endian endian = Platform == TexturePlatformID.GC ? Endian.Big : Endian.Little;
            stream.Write((int)SamplingSettings, endian);
            if (Platform == TexturePlatformID.GC)
            {
                stream.Write(gcnUnknown1, Endian.Big);
                stream.Write(gcnUnknown2, Endian.Big);
                stream.Write(gcnUnknown3, Endian.Big);
                stream.Write(gcnUnknown4, Endian.Big);
            }
            else if (Platform == TexturePlatformID.PS2 || Platform == TexturePlatformID.PSP)
            {
                throw new NotImplementedException();
            }
            stream.WriteString(TextureName, 32);
            stream.WriteString(AlphaName, 32);
            TextureAttributes attributes = TextureRasterFormatInfo.Build(Format, TLOTFormat, TLOTColors, Images > 1, Platform == TexturePlatformID.GC, UseAutoMipMaps);
            stream.Write(attributes, endian);

            if (((int)attributes & (int)TextureAttributes.Native) == 0)
            {
                if (Platform == TexturePlatformID.D3D9)
                {
                    stream.Write(Format);
                }
                else
                {
                    stream.Write(HasAlpha ? 1 : 0);
                }
            }

            stream.Write(Width, endian);
            stream.Write(Height, endian);
            stream.Write((byte)Format.GetBPP());
            stream.Write(Images);

            if (((int)attributes & (int)TextureAttributes.Native) != 0)
            {
                stream.Write((byte)Format);
                stream.Write(TextureRasterFormatInfo.GetNativeSubFormat(Format, TLOTFormat));
            }
            else
            {
                stream.Write((byte)4);
                sbyte subFormat = Platform switch
                {
                    TexturePlatformID.Xbox => Format switch
                    {
                        FourCCType.DXT1 => 0xC,
                        FourCCType.DXT2 => 0xD,
                        FourCCType.DXT3 => 0xE,
                        FourCCType.DXT4 => throw new NotImplementedException(),
                        FourCCType.DXT5 => 0xF,
                        _ => 0,
                    },
                    TexturePlatformID.D3D8 => Format switch
                    {
                        FourCCType.DXT1 => 0x1,
                        FourCCType.DXT2 => 0x2,
                        FourCCType.DXT3 => 0x3,
                        FourCCType.DXT4 => 0x4,
                        FourCCType.DXT5 => 0x5,
                        _ => 0,
                    },
                    TexturePlatformID.D3D9 => 8,
                    TexturePlatformID.GC => (sbyte)Format,
                    _ => throw new NotImplementedException(),
                };
                stream.Write(subFormat);
            }

            if (Platform == TexturePlatformID.Xbox)
            {
                stream.Write(ImageData.Length, endian);
            }

            if (Format.IsPaletteFormat())
            {
                int PaletteSize = attributes.GetPaletteByteSize();
                if (TLOT.Length != PaletteSize)
                {
                    Span<byte> bytes = stackalloc byte[PaletteSize];
                    TLOT.AsSpan(0, PaletteSize).CopyTo(bytes);
                    stream.Write(bytes);
                }
                else
                {
                    stream.Write(TLOT);
                }
            }

            if (Platform != TexturePlatformID.Xbox)
            {
                stream.Write(ImageData.Length, endian);
            }
            stream.Write(ImageData);

        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Struct;
    }
}
