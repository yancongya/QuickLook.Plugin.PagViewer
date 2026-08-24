namespace QuickLook.Plugin.PagViewer.Native
{
    public enum PagColorType
    {
        Unknown,
        Alpha8,
        Rgba8888,
        Bgra8888,
        Rgb565,
        Gray8,
        RgbaF16,
        Rgba101012
    }

    public enum PagAlphaType
    {
        Unknown,
        Opaque,
        Premultiplied,
        Unpremultiplied
    }

    public enum PagScaleMode
    {
        None,
        Stretch,
        LetterBox,
        Zoom
    }

    public enum PagTimeStretchMode
    {
        None,
        Scale,
        Repeat,
        RepeatInverted
    }

    public enum PagLayerType
    {
        Unknown,
        Null,
        Solid,
        Text,
        Shape,
        Image,
        PreCompose,
        Camera
    }
}
