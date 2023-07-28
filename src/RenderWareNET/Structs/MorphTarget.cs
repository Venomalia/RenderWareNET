using AuroraLib.Core.IO;
using System.Numerics;

namespace RenderWareNET.Structs
{
    public class MorphTarget
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

        public MorphTarget(Stream stream, int vertices = 0)
        {
            Sphere = stream.Read<Sphere>();
            int hasVertices = stream.Read<int>();
            int hasNormals = stream.Read<int>();

            Vertices = hasVertices != 0 ? stream.Read<Vector3>(vertices) : Array.Empty<Vector3>();
            Normals = hasNormals != 0 ? stream.Read<Vector3>(vertices) : Array.Empty<Vector3>();
        }

        public void Writer(Stream stream)
        {
            stream.Write(Sphere);
            stream.Write(HasVertices ? 1 : 0);
            stream.Write(HasNormals ? 1 : 0);
            stream.Write<Vector3>(Vertices);
            stream.Write<Vector3>(Normals);
        }
    }
}
