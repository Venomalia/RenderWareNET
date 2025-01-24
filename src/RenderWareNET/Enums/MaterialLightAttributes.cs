using System;

namespace RenderWareNET.Enums
{
    [Flags]
    public enum MaterialLightAttributes
    {
        None = 0,
        Lightmap = 1,
        Emitter = 2,
        Sky = 8,
        Flat = 16,
    }
}
