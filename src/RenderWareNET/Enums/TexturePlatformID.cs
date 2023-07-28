namespace RenderWareNET.Enums
{
    public enum TexturePlatformID : int
    {
        Xbox = 5,
        D3D8 = 8,
        D3D9 = 9,
        GC = 0x6000000, // 6 in BigEndian
        PS2 = 0x325350, // PS2 as string
        PSP = 0x505350 // PSP as string
    }
}
