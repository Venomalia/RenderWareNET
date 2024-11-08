using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class GeometryList : RWPluginList<Geometry>
    {
        public GeometryList()
        { }

        public GeometryList(Stream stream) : base(stream)
        { }

        public GeometryList(RWVersion version) : base(version)
        { }

        public GeometryList(RWVersion version, IEnumerable<Geometry> materials) : base(version, materials)
        { }

        protected override void ReadData(Stream stream)
        {
            RWGeometryList properties = stream.Read<RWGeometryList>();

            Clear();
            Capacity = properties.Count;
            for (int i = 0; i < properties.Count; i++)
            {
                Add(new(stream));
            }
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(new RWGeometryList(Header.Version, Count));

            foreach (Geometry geometry in this)
            {
                geometry.BinarySerialize(stream);
            }
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.GeometryList;
    }
}
