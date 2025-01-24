using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Structs;
using System;
using System.IO;

namespace RenderWareNET.Plugins.Structs
{
    public sealed class RWString : RWPlugin
    {
        public string Value
        {
            get => data;
            set
            {
                data = value ?? string.Empty;
                Header = new RWPluginHeader(PluginID.String, (uint)StreamEx.AlignPosition(data.Length, 4), Header.Version);
            }
        }
        private string data = string.Empty;

        public RWString() : base()
        { }

        public RWString(Stream stream) : base(stream)
        { }

        public RWString(RWVersion version) : base(version)
        { }

        public RWString(RWVersion version, string data) : base(version)
            => Value = data;

        public RWString(string data) : base()
            => Value = data;

        protected override void ReadData(Stream stream)
            => data = stream.ReadString((int)Header.SectionSize, 0);

        protected override void WriteData(Stream stream)
            => stream.WriteString(data.AsSpan(), (int)Header.SectionSize, 0);

        protected override PluginID GetExpectedIdentifier()
            => PluginID.String;

        public static implicit operator string(RWString x) => x.Value;
        public static explicit operator RWString(string x) => new RWString(x);
    }
}
