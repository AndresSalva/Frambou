using HospitalDeVehiculosUltimaVersion.Model;
namespace HospitalDeVehiculosUltimaVersion.Factory.FactoryPago
{
    public enum TipoDePago
    {
        TARJETA,
        QR
    }

    public interface IProcesadorDePago
    {
        TipoDePago Tipo { get;  }
        public ResultadoDePago Procesar(SolicitudDePago pago);
    }
}
