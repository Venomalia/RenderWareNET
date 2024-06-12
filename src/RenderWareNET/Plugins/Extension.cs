using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Interfaces;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class Extension : RWPlugin
    {
        public List<IRWSectionAccess> Sections = new(32);

        public Extension()
        { }

        public Extension(Stream stream) : base(stream)
        { }

        public Extension(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            long sectionEnd = stream.Position + Header.SectionSize;
            while (stream.Position < sectionEnd)
            {
                PluginID next = stream.Peek<PluginID>();
                IRWSectionAccess section = next switch
                {
                    PluginID.BinMeshPLG => new BinMeshPLG(stream),
                    PluginID.NativeDataPLG => new NativeDataPLG(stream),
                    PluginID.CollisionPLG => new RawSection(stream),
                    PluginID.UserDataPLG => new RawSection(stream),
                    PluginID.MaterialEffectsPLG => new RawSection(stream),
                    _ => new RawSection(stream)
                };
                Sections.Add(section);
            }
        }

        protected override void WriteData(Stream dest)
        {
            foreach (IRWSectionAccess section in Sections)
            {
                section.BinarySerialize(dest);
            }
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.Extension;
    }
}
