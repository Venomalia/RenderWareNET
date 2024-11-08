using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class NativeDataPLG : RWPlugin
    {
        public readonly RWNativeData NativeData = new();

        public NativeDataPLG()
        { }

        public NativeDataPLG(Stream stream) : base(stream)
        { }

        public NativeDataPLG(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
            => NativeData.BinaryDeserialize(stream);

        protected override void WriteData(Stream stream)
            => NativeData.BinarySerialize(stream);

        protected override PluginID GetExpectedIdentifier()
            => PluginID.NativeDataPLG;
    }
}
