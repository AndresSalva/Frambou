using HospitalDeVehiculosUltimaVersion.Model;

namespace HospitalDeVehiculosUltimaVersion.Factory
{
    public class ServicioPagoDeTarjeta(HospitalDeVehiculosContext dbContext) : ServicioDePago(dbContext)
    {
        public override TipoDePago Tipo => TipoDePago.TARJETA;

        public override IProcesadorDePago CrearProcesador()
        {
            return new ProcesadorDePagoDeTarjeta();
        }
        public override string GetFormatoDeTipoDePago()
        {
            return "TARJETA";
        }
    }
}
