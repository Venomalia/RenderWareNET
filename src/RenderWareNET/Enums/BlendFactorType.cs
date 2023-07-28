namespace RenderWareNET.Enums
{
    public enum BlendFactorType : int
    {
        None = 0x00,
        Zero = 0x01,
        One = 0x02,
        SourceColor = 0x03,
        InverseSourceColor = 0x04,
        SourceAlpha = 0x05,
        InverseSourceAlpha = 0x06,
        DestinationAlpha = 0x07,
        InverseDestinationAlpha = 0x08,
        DestinationColor = 0x09,
        InverseDestinationColor = 0x0A,
        SourceAlphaSaturated = 0x0B
    }
}
