using AuroraLib.Core.Interfaces;
using System.ComponentModel;

namespace RenderWareNET.Interfaces
{
    /// <summary>
    /// Represents an interface for reading and writing RenderWare sections of data from and to a stream.
    /// </summary>
    public interface IRWSectionAccess : IHasRWPluginHeader, IBinaryObject
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("deprecated, please use BinaryDeserialize instead.")]
        void Read(Stream stream);


        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("deprecated, please use BinarySerialize instead.")]
        void Write(Stream stream);
    }

}
