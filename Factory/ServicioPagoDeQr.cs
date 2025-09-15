using HospitalDeVehiculosUltimaVersion.Model;

namespace HospitalDeVehiculosUltimaVersion.Factory
{
    public class ServicioPagoDeQr(HospitalDeVehiculosContext dbContext) : ServicioDePago(dbContext)
    {
        public override TipoDePago Tipo => throw new NotImplementedException();

        public override IProcesadorDePago CrearProcesador()
        {
            return new ProcesadorDePagoDeQr();
        }

        public override string TipoDePagoFormato()
        {
            return "QR";
        }
    }
}
