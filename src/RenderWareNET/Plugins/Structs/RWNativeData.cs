using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public sealed class RWNativeData : RWPlugin
    {
        public Enums.RWPlatformID DataType;
        public int HeaderLenght;
        public int DataLenght;
        public byte[] RawData = Array.Empty<byte>();

        public RWNativeData()
        { }

        public RWNativeData(Stream stream) : base(stream)
        { }

        public RWNativeData(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            DataType = stream.Read<Enums.RWPlatformID>();
            HeaderLenght = stream.Read<int>();
            DataLenght = stream.Read<int>();
            RawData = new byte[Header.SectionSize];
            stream.Read(RawData);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(DataType);
            stream.Write(HeaderLenght);
            stream.Write(DataLenght);
            stream.Write(RawData);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Struct;
    }
}
