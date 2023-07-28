namespace RenderWareNET.Enums
{
    [Flags]
    public enum GeometryAttributes : int
    {
        none = 0x0,
        Triangle = 0x1,
        VertexPositions = 0x2,
        TextCoords = 0x4,
        VertexColors = 0x8,
        Normals = 0x10,
        Lights = 0x20,
        ModulateMaterialColors = 0x40,
        MultipleTextCoords = 0x80,

        NativeGeometry = 0x1000000,
        NativeInstance = 0x2000000,
        SectorsOverlap = 0x40000000
    }
}
