using System.IO;

namespace RenderWareNET.MaterialEffects
{
    public abstract class MaterialEffect
    {
        protected MaterialEffect()
        { }

        protected MaterialEffect(Stream stream)
            => Read(stream);

        public abstract void Read(Stream stream);
        public abstract void Write(Stream stream);
    }
}
