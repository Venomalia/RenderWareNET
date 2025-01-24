using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using System.IO;

namespace RenderWareNET.Plugins
{

    public sealed class MaterialEffectsPLG : RWPlugin
    {
        public MaterialEffectTypes EffectType;

        protected override void ReadData(Stream stream)
        {
            EffectType = stream.Read<MaterialEffectTypes>();
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(EffectType);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.MaterialEffectsPLG;
    }
}
