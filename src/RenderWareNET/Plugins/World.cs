﻿using AuroraLib.Core.IO;
using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class World : RWPlugin
    {
        public readonly RWWorld Properties = new();
        public readonly MaterialList Materials = new();
        public RWPlugin WorldChunk;
        public readonly Extension Extension = new();

        public World()
        { }

        public World(Stream stream) : base(stream)
        { }

        public World(RWVersion version) : base(version)
        { }

        protected override void ReadData(Stream stream)
        {
            Properties.BinaryDeserialize(stream);
            Materials.BinaryDeserialize(stream);

            PluginID next = stream.Peek<PluginID>();
            WorldChunk = next switch
            {
                PluginID.AtomicSector => new AtomicSector(stream),
                PluginID.PlaneSector => new PlaneSector(stream),
                _ => throw new NotImplementedException(),
            };
            Extension.BinaryDeserialize(stream);
        }

        protected override void WriteData(Stream stream)
        {
            Properties.BinarySerialize(stream);
            Materials.BinarySerialize(stream);
            WorldChunk.BinarySerialize(stream);
            Extension.BinarySerialize(stream);
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.World;
    }
}
