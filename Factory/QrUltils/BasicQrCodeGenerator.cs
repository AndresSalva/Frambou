using QRCoder;
using System.Xml.Linq;
namespace HospitalDeVehiculosUltimaVersion.Factory.QrUltils
{
    public class BasicQrCodeGenerator : IQrCodeGenerator
    {
        public byte[] Generate(string payload)
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData data = qrCodeGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode code = new PngByteQRCode(data);
            return code.GetGraphic(40);
        }
    }
}
