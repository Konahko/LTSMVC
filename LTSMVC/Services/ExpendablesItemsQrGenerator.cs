using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using LTSMVC.Models;
using QRCoder;

namespace LTSMVC.Services
{
    public class ExpendablesItemsQrGenerator
    {
        private readonly Color _backgroundColor = Color.White;
        private const string FontFamily = "Arial";
        private const string AreaName = "СЛД 58 Юдино-Казанский";
        private readonly FontStyle _fontStyle = FontStyle.Bold;
        
        private readonly Dictionary<QrCodeSize, QrImageProperties> _qrProperties = new Dictionary<QrCodeSize, QrImageProperties>
        {
            {QrCodeSize.Small, new QrImageProperties(340, 228, 4, 72, 27, 200, 200)},
            {QrCodeSize.Medium, new QrImageProperties(656, 452, 6, 19, 55, 350, 350)}
        };

        public MemoryStream GetQrImageStream(ExpendablesItem item, QrCodeSize size)
        {
            using var qrGenerator = new QRCodeGenerator();
            //using (var g = new QRCodeGenerator())

            var qrProperties = _qrProperties[size];
            var image = new Bitmap(qrProperties.Width, qrProperties.Height);
            using var g = Graphics.FromImage(image);
            
            g.Clear(_backgroundColor);
            
            var dateTime = DateTime.Now;
            using var qrCodeData = qrGenerator.CreateQrCode(item.GetQrText(dateTime), QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(qrProperties.Ppm);
            
            g.DrawImage(qrCodeImage, qrProperties.QrMarginX, qrProperties.QrMarginY, qrProperties.Width, qrProperties.Height);
            
            var stream = new MemoryStream();
 
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

            return stream;
        }

        private void AddTextToImage(Graphics g, QrCodeSize size, int itemId, string itemName, DateTime dateTime)
        {
            switch (size)
            {
                case QrCodeSize.Small:
                {
                    using var font = new Font(FontFamily, 18, _fontStyle);
                    g.DrawString(AreaName, font, Brushes.Black, 18, 6);
                    break;
                }
                case QrCodeSize.Medium:
                {
                    using var titleFont = new Font(FontFamily, 35, _fontStyle);
                    g.DrawString(AreaName, titleFont, Brushes.Black, 19, 6);
                    
                    using var font = new Font(FontFamily, 19, _fontStyle);
                    g.DrawString(itemId.ToString(), font, Brushes.Black, 350, 70);
                    g.DrawString(itemName, font, Brushes.Black, 350, 104);
                    g.DrawString(dateTime.ToShortDateString(), font, Brushes.Black, 350, 138);

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }   
        }
    }
}