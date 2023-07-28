namespace RenderWareNET.Enums
{
    [Flags]
    public enum AtomicAttributes : int
    {
        None = 0,
        CollisionTest = 1,
        Render = 4,
        CollisionTestAndRender = 5
    }
}
