using RenderWareNET.Interfaces;
using RenderWareNET.Structs;

namespace RenderWareNET.Plugins.Structs
{
    public struct RWPlane : IHasRWPluginHeader
    {
        /// <inheritdoc/>
        public RWPluginHeader Header { get; set; }

        public int Type;
        public float Value;
        private int leftIsAtomic;
        private int rightIsAtomic;
        public float LeftValue;
        public float RightValue;

        public bool LeftIsAtomic
        {
            get => leftIsAtomic != 0;
            set => leftIsAtomic = value ? 1 : 0;
        }

        public bool RightIsAtomic
        {
            get => rightIsAtomic != 0;
            set => rightIsAtomic = value ? 1 : 0;
        }
    }
}
