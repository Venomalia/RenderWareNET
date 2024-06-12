using AuroraLib.Core.Interfaces;
using AuroraLib.Core.IO;
using System.Numerics;

namespace RenderWareNET.Structs
{
    public class MorphTarget : IBinaryObject
    {
        public Sphere Sphere;
        public bool HasVertices { get => Vertices.Length != 0; }
        public bool HasNormals { get => Normals.Length != 0; }

        public Vector3[] Vertices;
        public Vector3[] Normals;

        public MorphTarget(Sphere sphere, Vector3[] vertices, Vector3[] normals)
        {
            Sphere = sphere;
            Vertices = vertices ?? Array.Empty<Vector3>();
            Normals = normals ?? Array.Empty<Vector3>();
        }

        public MorphTarget(Stream source, int vertices = 0)
            => BinaryDeserialize(source, vertices);

        /// <inheritdoc/>
        public void BinaryDeserialize(Stream source)
            => BinaryDeserialize(source, 0);

        /// <inheritdoc cref="BinaryDeserialize(Stream)"/>
        public void BinaryDeserialize(Stream source, int vertices)
        {
            Sphere = source.Read<Sphere>();
            bool hasVertices = source.Read<int>() != 0;
            bool hasNormals = source.Read<int>() != 0;

            Vertices = new Vector3[vertices];
            Normals = new Vector3[vertices];
            if (hasVertices)
                source.Read<Vector3>(Vertices);
            if (hasNormals)
                source.Read<Vector3>(Normals);
        }

        /// <inheritdoc/>
        public void BinarySerialize(Stream dest)
        {
            dest.Write(Sphere);
            dest.Write(HasVertices ? 1 : 0);
            dest.Write(HasNormals ? 1 : 0);
            dest.Write<Vector3>(Vertices);
            dest.Write<Vector3>(Normals);
        }
    }
}
