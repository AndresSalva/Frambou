using HospitalDeVehiculosUltimaVersion.Factory.QrUltils;
using HospitalDeVehiculosUltimaVersion.Model;

namespace HospitalDeVehiculosUltimaVersion.Factory
{
    public class ServicioPagoDeQr(HospitalDeVehiculosContext dbContext, IQrCodeGenerator qrCodeGenerator) : ServicioDePago(dbContext)
    {
        public override TipoDePago Tipo => throw new NotImplementedException();

        public IQrCodeGenerator QrCodeGenerator { get; set; } = qrCodeGenerator;

        public override IProcesadorDePago CrearProcesador()
        {
            return new ProcesadorDePagoDeQr();
        }

        public byte[] CreateQrCode( SolicitudDePago solicitudDePago )
        {
            string qrPayload = $"{solicitudDePago.Total}";
            return QrCodeGenerator.Generate(qrPayload);
        }

        public override string TipoDePagoFormato()
        {
            return "QR";
        }
    }
}
