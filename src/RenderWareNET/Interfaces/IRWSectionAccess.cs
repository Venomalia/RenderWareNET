namespace RenderWareNET.Interfaces
{
    /// <summary>
    /// Represents an interface for reading and writing RenderWare sections of data from and to a stream.
    /// </summary>
    public interface IRWSectionAccess : IHasRWPluginHeader
    {
        /// <summary>
        /// Reads the section from the given stream.
        /// </summary>
        /// <param name="stream">The stream from which the section data is read.</param>
        void Read(Stream stream);

        /// <summary>
        /// Writes the section to the given stream.
        /// </summary>
        /// <param name="stream">The stream to which the section data is written.</param>
        void Write(Stream stream);
    }

}
