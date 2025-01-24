using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using System.Collections.Generic;
using System.IO;

namespace RenderWareNET.Plugins
{
    public sealed class MaterialList : RWPlugin
    {
        public readonly RWMaterialList List = new RWMaterialList();
        public readonly List<Material> Materials = new List<Material>();

        protected override void ReadData(Stream stream)
        {
            long sectionEnd = stream.Position + Header.SectionSize;
            List.BinaryDeserialize(stream);
            Materials.Clear();
            Materials.Capacity = List.Count;

            while (stream.Position < sectionEnd)
            {
                Materials.Add(new Material(stream));
            }
        }

        protected override void WriteData(Stream stream)
        {
            List.BinarySerialize(stream);
            foreach (Material material in Materials)
            {
                material.BinarySerialize(stream);
            }
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.MaterialList;
    }
}
