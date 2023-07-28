using AuroraLib.Core.Buffers;
using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public sealed class RWMaterialList : RWPluginList<int>
    {
        public RWMaterialList()
        { }

        public RWMaterialList(Stream stream) : base(stream)
        { }

        public RWMaterialList(RWVersion version) : base(version)
        { }

        public RWMaterialList(RWVersion version, IEnumerable<int> materials) : base(version, materials)
        { }

        protected override void ReadData(Stream stream)
        {
            Capacity = stream.Read<int>();

            using SpanBuffer<int> buffer = new(Capacity);
            stream.Read<int>(buffer);
            foreach (int item in buffer)
            {
                Add(item);
            }
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Count);
            stream.Write(this);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Struct;
    }
}
