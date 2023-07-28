using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Structs;
using System.Runtime.CompilerServices;

namespace RenderWareNET
{
    public static class Common
    {
        /// <summary>
        /// Checks if the given stream represents a RenderWare plugin.
        /// </summary>
        /// <param name="stream">The input stream to check for being a RenderWare plugin.</param>
        /// <param name="pluginID">An out parameter to receive the identifier of the RenderWare plugin.</param>
        /// <returns>True if the stream represents a RenderWare plugin; otherwise, false.</returns>
        public static bool IsRWPlugin(this Stream stream, out PluginID pluginID)
        {
            RWPluginHeader header = stream.At(0,s => s.Read<RWPluginHeader>());
            pluginID = header.Identifier;
            return header.Version.Version == 3 && header.Version.Major < 10 && header.SectionSize == stream.Length - Unsafe.SizeOf<RWPluginHeader>();
        }

        /// <inheritdoc cref="IsRWPlugin(Stream, out PluginID)"/>
        public static bool IsRWPlugin(this Stream stream)
            => IsRWPlugin(stream, out _);
    }
}
