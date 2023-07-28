using RenderWareNET.Structs;

namespace RenderWareNET.Interfaces
{
    /// <summary>
    /// Has a <see cref="RWPluginHeader"/>
    /// </summary>
    public interface IHasRWPluginHeader
    {
        /// <summary>
        /// Represents a RenderWare section Header.
        /// </summary>
        RWPluginHeader Header { get; set; }
    }
}
