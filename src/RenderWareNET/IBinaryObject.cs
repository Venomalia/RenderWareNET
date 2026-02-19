using System.IO;

namespace RenderWareNET
{
    /// <summary>
    /// Interface for objects that support binary serialization and deserialization.
    /// </summary>
    public interface IBinaryObject
    {
        /// <summary>
        /// Serializes the object to a binary stream.
        /// </summary>
        /// <param name="dest">The destination stream for serialization.</param>
        void BinarySerialize(Stream dest);

        /// <summary>
        /// Deserializes the object from a binary stream.
        /// </summary>
        /// <param name="source">The source stream for deserialization.</param>
        void BinaryDeserialize(Stream source);
    }
}
