using HospitalDeVehiculosUltimaVersion.Factory.QrUltils;
using HospitalDeVehiculosUltimaVersion.Model;

namespace HospitalDeVehiculosUltimaVersion.Factory
{
    public class ServicioPagoDeQr(HospitalDeVehiculosContext dbContext) : ServicioDePago(dbContext)
    {
        public override TipoDePago Tipo => throw new NotImplementedException();

        public IQrCodeGenerator? QrCodeGenerator { get; set; }

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
            if(QrCodeGenerator is null)
            {
                throw new NullReferenceException();
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
