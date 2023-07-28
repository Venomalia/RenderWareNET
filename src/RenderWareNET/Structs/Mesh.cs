using AuroraLib.Core.Buffers;
using AuroraLib.Core.IO;

namespace RenderWareNET.Structs
{
    public class Mesh : List<int>
    {
        public int MaterialIndex; // material index
        public int vertexIndices; // material index

        public Mesh(Stream stream, bool IsNative = false)
        {
            Capacity = stream.Read<int>();
            MaterialIndex = stream.Read<int>();

            if (!IsNative)
            {
                using SpanBuffer<int> buffer = new(Capacity);
                stream.Read<int>(buffer);
                foreach (int item in buffer)
                {
                    Add(item);
                }
            }
        }

        public void Write(Stream stream)
        {
            stream.Write(Count);
            stream.Write(MaterialIndex);
            stream.Write(this);
        }
    }
}
