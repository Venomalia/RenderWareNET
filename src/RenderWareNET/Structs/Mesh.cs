using AuroraLib.Core.IO;
using System.Collections.Generic;
using System.IO;

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
                stream.ReadCollection(this, Capacity);
            }
        }

        public void Write(Stream stream)
        {
            stream.Write(Count);
            stream.Write(MaterialIndex);
            if (this.Count != 0)
                stream.WriteCollection(this);
        }
    }
}
