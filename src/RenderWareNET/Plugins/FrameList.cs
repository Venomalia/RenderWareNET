using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;
using System;
using System.Collections.Generic;
using System.IO;

namespace RenderWareNET.Plugins
{
    public sealed class FrameList : RWPlugin
    {
        public readonly RWFrameList Frames = new RWFrameList();

        public readonly List<Extension> Extensions = new List<Extension>();

        protected override void ReadData(Stream stream)
        {
            Frames.BinaryDeserialize(stream);
            Extensions.Clear();
            Extensions.Capacity = Frames.Count;

            for (int i = 0; i < Frames.Count; i++)
            {
                Extensions.Add(new Extension(stream));
            }
        }

        protected override void WriteData(Stream stream)
        {
            if (Frames.Count != Extensions.Count)
            {
                throw new InvalidOperationException($"{nameof(Frames)} and {nameof(Extensions)} must have the same number of entries.");
            }
            Frames.BinarySerialize(stream);
            foreach (Extension extension in Extensions)
            {
                extension.BinarySerialize(stream);
            }
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.FrameList;
    }
}
