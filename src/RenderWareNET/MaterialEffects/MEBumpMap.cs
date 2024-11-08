using AuroraLib.Core.IO;
using RenderWareNET.Plugins;

namespace RenderWareNET.MaterialEffects
{
    public class MEBumpMap : MaterialEffect
    {
        public float Intensity;
        public bool HasBumpMap => BumpMap is not null;
        public bool HasHeightMap => HeightMap is not null;
        public Texture? BumpMap;
        public Texture? HeightMap;

        public MEBumpMap()
        { }

        public MEBumpMap(Stream stream) : base(stream)
        { }

        public override void Read(Stream stream)
        {
            Intensity = stream.Read<float>();
            bool hasBumpMap = stream.Read<int>() != 0;
            BumpMap = hasBumpMap ? new Texture(stream) : null;
            bool hasHeightMap = stream.Read<int>() != 0;
            HeightMap = hasHeightMap ? new Texture(stream) : null;
        }

        public override void Write(Stream stream)
        {
            stream.Write(Intensity);
            stream.Write(HasBumpMap ? 1 : 0);
            if (HasBumpMap)
            {
                BumpMap?.BinarySerialize(stream);
            }
            stream.Write(HasHeightMap ? 1 : 0);
            if (HasHeightMap)
            {
                HeightMap?.BinaryDeserialize(stream);
            }
        }
    }
}
