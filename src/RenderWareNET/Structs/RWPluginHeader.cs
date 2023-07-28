using RenderWareNET.Enums;

namespace RenderWareNET.Structs
{
    /// <summary>
    /// Represents a RenderWare section of data.
    /// </summary>
    public readonly struct RWPluginHeader
    {
        /// <inheritdoc/>
        public readonly PluginID Identifier;

        /// <inheritdoc/>
        public readonly uint SectionSize;

        /// <inheritdoc/>
        public readonly RWVersion Version;

        public RWPluginHeader(PluginID identifier, uint sectionSize) : this()
        {
            Identifier = identifier;
            SectionSize = sectionSize;
        }

        public RWPluginHeader(PluginID identifier, uint sectionSize, RWVersion version) : this(identifier, sectionSize)
            => Version = version;
    }
}
