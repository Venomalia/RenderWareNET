using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class BinMeshPLG : RWPluginList<Mesh>
    {
        public BinMeshHeaderFlags Flag;

        public int TotalIndicesCount
        {
            get
            {
                int total = 0;
                foreach (Mesh mesh in this)
                {
                    total += mesh.Count;
                }

                return total;
            }
        }

        public BinMeshPLG()
        { }

        public BinMeshPLG(Stream stream) : base(stream)
        { }

        public BinMeshPLG(RWVersion version, IEnumerable<Mesh> materials) : base(version, materials)
        { }

        public bool IsNativeData { get; set; }

        protected override void ReadData(Stream stream)
        {
            Flag = stream.Read<BinMeshHeaderFlags>();
            int count = stream.Read<int>(); //Count
            _ = stream.Read<int>(); //TotalIndicesCount

            Capacity = count;

            IsNativeData = Header.SectionSize == 12 + 8 * Capacity;

            for (int i = 0; i < count; i++)
            {
                Add(new(stream, IsNativeData));
            }
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Flag);
            stream.Write(Count);
            stream.Write(TotalIndicesCount);
            for (int i = 0; i < Count; i++)
            {
                this[i].Write(stream);
            }
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.BinMeshPLG;
    }
}
