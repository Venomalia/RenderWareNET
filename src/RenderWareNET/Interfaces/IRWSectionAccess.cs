using AuroraLib.Core.IO;

namespace RenderWareNET.Interfaces
{
    /// <summary>
    /// Represents an interface for reading and writing RenderWare sections of data from and to a stream.
    /// </summary>
    public interface IRWSectionAccess : IHasRWPluginHeader, IBinaryObject
    { }

}
