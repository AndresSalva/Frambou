namespace HospitalDeVehiculosUltimaVersion.Factory
{
    public class ProcesadorDePagoDeQr : IProcesadorDePago
    {
        public TipoDePago Tipo => TipoDePago.QR;

        public ResultadoDePago Procesar(SolicitudDePago pago)
        {
            return new ResultadoDePago(pago.Total, true, Tipo);
        }
    }
}
