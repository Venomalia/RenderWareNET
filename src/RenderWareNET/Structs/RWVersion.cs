namespace RenderWareNET.Structs
{
    /// <summary>
    /// Stores the RenderWare version.
    /// </summary>
    public struct RWVersion
    {
        /// <summary>
        /// known RenderWare Game versions
        /// </summary>
        public static class Common
        {
            public readonly static RWVersion Burnout = 784; //3.0.0.0
            public readonly static RWVersion LibertyCityStories = 67108864; //3.1.0.0
            public readonly static RWVersion GTA3 = 67174400; //3.1.0.1
            public readonly static RWVersion ViceCity = 268632064; //3.4.0.3
            public readonly static RWVersion SonicHeroes = 335544320; //3.5.0.0
            public readonly static RWVersion SanAndreas = 402915327; //3.6.0.3
            public readonly static RWVersion ShadowTheHedgehog = 469893175; //3.7.0.2
            public readonly static RWVersion Madagascar = 469893175; //3.7.0.2
            public readonly static RWVersion Bully = 469893175; //3.7.0.2
        }

        // V is Version
        // J is Major build
        // N is Minor build
        // R is Revision
        // B is build
        // VVJJJJNN NNRRRRRR BBBBBBBB BBBBBBBB
        private ushort _build, _version;


        public RWVersion(byte version, byte major, byte minor, byte revision, ushort build = 0) : this()
        {
            Build = build;
            Version = version;
            Major = major;
            Minor = minor;
            Revision = revision;
        }

        public RWVersion(int vaule)
        {
            _version = (ushort)(vaule >> 16);
            _build = (ushort)vaule;
        }

        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        public byte Version
        {
            get => (byte)((_version >> 14) + 3);
            set => _version = (ushort)((_version & 0x3FFF) | value - 3 << 14);
        }

        /// <summary>
        /// Gets or sets the Major version number.
        /// </summary>
        public byte Major
        {
            get => (byte)((_version & 0x3C00) >> 10);
            set => _version = (ushort)((_version & 0xC3FF) | value << 10);
        }

        /// <summary>
        /// Gets or sets the Minor version number.
        /// </summary>
        public byte Minor
        {
            get => (byte)((_version & 0x3C0) >> 6);
            set => _version = (ushort)((_version & 0xFC3F) | value << 6);
        }

        /// <summary>
        /// Gets or sets the Revision number.
        /// </summary>
        public byte Revision
        {
            get => (byte)(_version & 0x3F);
            set => _version = (ushort)((_version & 0xFFC0) | value);
        }

        /// <summary>
        /// Gets or sets the build number.
        /// </summary>
        public ushort Build
        {
            get => _build;
            set => _build = value;
        }

        public override string ToString()
            => $"{Version}.{Major}.{Minor}.{Revision}-{Build}";

        public static unsafe implicit operator RWVersion(int x)
            => *(RWVersion*)&x;

        public static unsafe implicit operator int(RWVersion x)
            => *(int*)&x;

        public static explicit operator RWVersion(ushort x)
            => new RWVersion(x << 16);

        public static unsafe implicit operator ushort(RWVersion x)
            => x._version;
    }
}
