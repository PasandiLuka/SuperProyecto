using SuperProyecto.Core.IServices;

using QRCoder;
using SkiaSharp;

namespace SuperProyecto.Services.Service;

public class QrService : IQrService
{
    readonly IUrlConstructorService _urlConstructor;

    public QrService(IUrlConstructorService urlConstructor)
    {
        _urlConstructor = urlConstructor;
    }
    
    public string GenerarQrUrl(int idEntrada)
    {
        return _urlConstructor.GenerarQrUrl(idEntrada);
    }

    public byte[] CrearQR(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("La URL no puede estar vacía.", nameof(url));

        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
        QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        BitmapByteQRCode qRCode = new BitmapByteQRCode(qRCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);

        // Convertir a PNG usando SkiaSharp
        using var bitmap = SKBitmap.Decode(qrCodeBytes);
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);

        return data.ToArray();
    }
}