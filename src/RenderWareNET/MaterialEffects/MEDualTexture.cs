using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins;
using System.IO;

namespace RenderWareNET.MaterialEffects
{
    public sealed class MEDualTexture : MaterialEffect
    {
        public BlendFactorType SourceBlendMode;
        public BlendFactorType DestBlendMode;
        public bool HasTexture => !(Texture is null);
        public Texture? Texture;

        public MEDualTexture()
        { }

        public MEDualTexture(Stream stream) : base(stream)
        { }

        public override void Read(Stream stream)
        {
            SourceBlendMode = stream.Read<BlendFactorType>();
            DestBlendMode = stream.Read<BlendFactorType>();
            bool hasTexture = stream.Read<int>() != 0;
            Texture = hasTexture ? new Texture(stream) : null;
        }

        public override void Write(Stream stream)
        {
            stream.Write(SourceBlendMode);
            stream.Write(DestBlendMode);
            stream.Write(HasTexture ? 1 : 0);
            if (HasTexture)
            {
                Texture?.BinarySerialize(stream);
            }
        }
    }
}
