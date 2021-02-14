namespace LTSMVC.Services
{
    public readonly struct QrImageProperties
    {
        public int Width { get; }
        public int Height { get; }
        public int Ppm { get; }
        public int QrMarginX { get; }
        public int QrMarginY { get; }
        public int QrWidth { get; }
        public int QrHeight { get; }

        public QrImageProperties(int width, int height, int ppm, int qrMarginX, int qrMarginY, int qrWidth, int qrHeight)
        {
            Width = width;
            Height = height;
            Ppm = ppm;
            QrMarginX = qrMarginX;
            QrMarginY = qrMarginY;
            QrWidth = qrWidth;
            QrHeight = qrHeight;
        }
    }
}