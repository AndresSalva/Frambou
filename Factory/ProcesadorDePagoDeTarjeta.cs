using HospitalDeVehiculosUltimaVersion.Model;

namespace HospitalDeVehiculosUltimaVersion.Factory
{
    public class ProcesadorDePagoDeTarjeta : IProcesadorDePago
    {
        public TipoDePago Tipo => TipoDePago.TARJETA;

        public ResultadoDePago Procesar(SolicitudDePago pago)
        {
            return new ResultadoDePago(pago.Total, true, Tipo);
        }
    }
}
