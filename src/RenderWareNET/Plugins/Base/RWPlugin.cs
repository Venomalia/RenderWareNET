using AuroraLib.Core;
using AuroraLib.Core.Exceptions;
using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Interfaces;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Base
{
    /// <summary>
    /// Represents a base abstract class for read-write RenderWare plugins of data.
    /// </summary>
    public abstract class RWPlugin : IRWSectionAccess
    {
        public RWPluginHeader Header { get; set; }

        public RWPlugin() : this(new RWVersion())
        { }

        public RWPlugin(Stream stream)
            => Read(stream);

        public RWPlugin(RWVersion version) : base()
            => Header = new(GetExpectedIdentifier(), 4, version);

        public virtual void Read(Stream stream)
        {
            Header = stream.Read<RWPluginHeader>();
            if (Header.Identifier != GetExpectedIdentifier())
            {
                throw new InvalidIdentifierException(new Identifier32((uint)GetExpectedIdentifier()), new Identifier32((uint)Header.Identifier));
            }
            long sectionStart = stream.Position;
            long sectionEnd = sectionStart + Header.SectionSize;
            ReadData(stream);
            if (stream.Position != sectionEnd)
            {
                Console.Error.WriteLine($"This Plugin {Header.Identifier} was not read properly. Read until {stream.Position}-{sectionEnd}");
                while (stream.Position < sectionEnd)
                {
                    RWPluginHeader test = stream.Read<RWPluginHeader>();
                    if (test.Version != Header.Version)
                    {
                        break;
                    }

                    Console.Error.WriteLine($"Sub Plugin {test.Identifier} found, At {stream.Position}, Size {test.SectionSize} was not read.");
                    stream.Seek(test.SectionSize, SeekOrigin.Current);
                }

                stream.Seek(sectionEnd, SeekOrigin.Begin);
            }
        }

        public virtual void Write(Stream stream)
        {
            stream.Write(Header);
            long sectionStart = stream.Position;
            WriteData(stream);
            int sectionSize = (int)(stream.Position - sectionStart);
            stream.At(sectionStart - 8, s => s.Write(sectionSize));
        }

        /// <summary>
        /// Reads the specific section data from the given stream.
        /// </summary>
        /// <param name="stream">The stream from which the section data is read.</param>
        protected abstract void ReadData(Stream stream);

        /// <summary>
        /// Writes the specific section data to the given stream.
        /// </summary>
        /// <param name="stream">The stream to which the section data is written.</param>
        protected abstract void WriteData(Stream stream);

        /// <summary>
        /// Gets the expected identifier for this plugin.
        /// </summary>
        /// <returns>The expected identifier.</returns>
        protected abstract PluginID GetExpectedIdentifier();


        public static implicit operator RWPluginHeader(RWPlugin x) => x.Header;
        public static implicit operator RWVersion(RWPlugin x) => x.Header.Version;
    }
}
