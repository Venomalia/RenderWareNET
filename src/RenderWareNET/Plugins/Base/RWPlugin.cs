using AuroraLib.Core;
using AuroraLib.Core.Exceptions;
using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Interfaces;
using RenderWareNET.Structs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace RenderWareNET.Plugins.Base
{
    /// <summary>
    /// Represents a base abstract class for read-write RenderWare plugins of data.
    /// </summary>
    public abstract class RWPlugin : IRWSectionAccess
    {
        /// <inheritdoc/>
        public RWPluginHeader Header { get; set; }

        public RWPlugin() : this(new RWVersion())
        { }

        public RWPlugin(Stream source)
            => BinaryDeserialize(source);

        public RWPlugin(RWVersion version) : base()
            => Header = new RWPluginHeader(GetExpectedIdentifier(), 4, version);


        /// <inheritdoc/>
        public void BinaryDeserialize(Stream source)
        {
            Header = source.Read<RWPluginHeader>();
            if (Header.Identifier != GetExpectedIdentifier())
            {
                throw new InvalidIdentifierException(GetExpectedIdentifier().ToString(), Header.Identifier.ToString());
            }
            long sectionStart = source.Position;
            long sectionEnd = sectionStart + Header.SectionSize;
            ReadData(source);
            if (source.Position != sectionEnd)
            {
                Trace.WriteLine($"This Plugin {Header.Identifier} was not read properly. Read until {source.Position}-{sectionEnd}");
                while (source.Position < sectionEnd)
                {
                    RWPluginHeader test = source.Read<RWPluginHeader>();
                    if (test.Version != Header.Version)
                    {
                        break;
                    }

                    Trace.WriteLine($"Sub Plugin {test.Identifier} found, At {source.Position}, Size {test.SectionSize} was not read.");
                    source.Seek(test.SectionSize, SeekOrigin.Current);
                }

                source.Seek(sectionEnd, SeekOrigin.Begin);
            }
        }

        /// <inheritdoc/>
        public void BinarySerialize(Stream dest)
        {
            dest.Write(Header);
            long sectionStart = dest.Position;
            WriteData(dest);
            int sectionSize = (int)(dest.Position - sectionStart);
            dest.At(sectionStart - 8, s => s.Write(sectionSize));
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("deprecated, please use BinaryDeserialize instead.")]
        public void Read(Stream source)
            => BinaryDeserialize(source);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("deprecated, please use BinarySerialize instead.")]
        public void Write(Stream dest)
            => BinarySerialize(dest);

        /// <summary>
        /// Reads the specific section data from the given stream.
        /// </summary>
        /// <param name="source">The stream from which the section data is read.</param>
        protected abstract void ReadData(Stream source);

        /// <summary>
        /// Writes the specific section data to the given stream.
        /// </summary>
        /// <param name="dest">The stream to which the section data is written.</param>
        protected abstract void WriteData(Stream dest);

        /// <summary>
        /// Gets the expected identifier for this plugin.
        /// </summary>
        /// <returns>The expected identifier.</returns>
        protected abstract PluginID GetExpectedIdentifier();

        public static implicit operator RWPluginHeader(RWPlugin x) => x.Header;
        public static implicit operator RWVersion(RWPlugin x) => x.Header.Version;
    }
}
