using HospitalDeVehiculosUltimaVersion.Factory.FactoryPago.QrUltils;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.Identity.Client;

namespace HospitalDeVehiculosUltimaVersion.Factory.FactoryPago
{
    public class ServicioPagoDeQr(HospitalDeVehiculosContext dbContext) : ServicioDePago(dbContext)
    {
        public override TipoDePago Tipo => TipoDePago.QR;

        public IQrCodeGenerator QrCodeGenerator { get; set; }

        public void SetQrCodeGenerator(IQrCodeGenerator qrCodeGenerator)
        {
            QrCodeGenerator = qrCodeGenerator;
        }

        public override IProcesadorDePago CrearProcesador()
        {
            return new ProcesadorDePagoDeQr();
        }

        public byte[] CreateQrCode( SolicitudDePago solicitudDePago )
        {
            if(QrCodeGenerator == null)
            {
                throw new InvalidOperationException("QrCodeGenerator no ha sido configurado.");
            }
            string qrPayload = $"{solicitudDePago.Total}";
            return QrCodeGenerator.Generate(qrPayload);
        }

        public override string TipoDePagoFormato()
        {
            return "QR";
        }
    }
}
