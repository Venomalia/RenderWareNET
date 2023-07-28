using AuroraLib.Core;
using AuroraLib.Core.Cryptography;

namespace RenderWareNET.Structs
{
    public struct RGBA32
    {
        public byte R, G, B, A;

        public RGBA32(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static bool operator ==(RGBA32 c1, RGBA32 c2)
        {
            return Equals(c1, c2);
        }

        public static bool operator !=(RGBA32 c1, RGBA32 c2)
        {
            return !Equals(c1, c2);
        }

        public override bool Equals(object obj)
        {
            if (obj is RGBA32 c) return (GetHashCode() == c.GetHashCode());
            else return false;
        }

        public override int GetHashCode()
        {
            Span<byte> bytes = stackalloc byte[4];
            BitConverterX.TryWriteBytes(bytes, this);
            return (int)XXHash32.Generate(bytes);
        }
    }
}
