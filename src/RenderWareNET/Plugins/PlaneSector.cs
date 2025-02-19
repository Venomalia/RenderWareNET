﻿using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;
using System.IO;

namespace RenderWareNET.Plugins
{
    public sealed class PlaneSector : RWPlugin
    {
        public RWPlane Properties;
        private RWPlugin left = new AtomicSector();
        private RWPlugin right = new AtomicSector();

        public RWPlugin Left
        {
            get => left;
            set
            {
                Properties.LeftIsAtomic = value is AtomicSector;
                left = value;
            }
        }

        public RWPlugin Right
        {
            get => right;
            set
            {
                Properties.RightIsAtomic = value is AtomicSector;
                right = value;
            }
        }

        public PlaneSector()
        { }

        public PlaneSector(Stream stream) : base(stream)
        { }

        public PlaneSector(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            Properties = stream.Read<RWPlane>();

            if (Properties.LeftIsAtomic)
                left = new AtomicSector(stream);
            else
                left = new PlaneSector(stream);

            if (Properties.RightIsAtomic)
                right = new AtomicSector(stream);
            else
                right = new PlaneSector(stream);
        }

        protected override void WriteData(Stream stream)
        {
            stream.Write(Properties);
            Left.BinarySerialize(stream);
            Right.BinarySerialize(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.PlaneSector;
    }
}
