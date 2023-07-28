using AuroraLib.Core.IO;
using RenderWareNET.Plugins;

namespace RenderWareNET.MaterialEffects
{
    public sealed class MEEnvironmentMap : MaterialEffect
    {
        public float ReflectionCoefficient;
        public bool UseFrameBufferAlphaChannel;
        public bool HasEnvironmentMap => EnvironmentMap is not null;
        public Texture? EnvironmentMap;

        public MEEnvironmentMap()
        { }

        public MEEnvironmentMap(Stream stream) : base(stream)
        { }

        public override void Read(Stream stream)
        {
            ReflectionCoefficient = stream.Read<float>();
            UseFrameBufferAlphaChannel = stream.Read<int>() != 0;
            bool hasEnvironmentMap = stream.Read<int>() != 0;
            EnvironmentMap = hasEnvironmentMap ? new Texture(stream) : null;
        }

        public override void Write(Stream stream)
        {
            stream.Write(ReflectionCoefficient);
            stream.Write(UseFrameBufferAlphaChannel ? 1 : 0);
            stream.Write(HasEnvironmentMap ? 1 : 0);
            if (HasEnvironmentMap)
            {
                EnvironmentMap?.Write(stream);
            }
        }
    }
}
