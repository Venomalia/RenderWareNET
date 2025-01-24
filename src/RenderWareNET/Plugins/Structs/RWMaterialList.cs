using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;
using System.Collections.Generic;
using System.IO;

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
            stream.ReadCollection(this, Capacity);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Count);
            stream.WriteCollection(this);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Struct;
    }
}
