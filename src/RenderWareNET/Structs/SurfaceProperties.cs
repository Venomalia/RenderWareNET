namespace RenderWareNET.Structs
{
    public struct SurfaceProperties
    {
        public float Ambient;
        public float Specular;
        public float Diffuse;

        public SurfaceProperties(float ambient, float specular, float diffuse)
        {
            Ambient = ambient;
            Specular = specular;
            Diffuse = diffuse;
        }
    }
}
