using HospitalDeVehiculosUltimaVersion.Model;

namespace HospitalDeVehiculosUltimaVersion.Factory.FactoryPago
{
    public abstract class ServicioDePago(HospitalDeVehiculosContext dbContext )
    {
        HospitalDeVehiculosContext DbContext { get; set; } = dbContext;
        public abstract TipoDePago Tipo { get; }
        public abstract IProcesadorDePago CrearProcesador();
        public abstract string TipoDePagoFormato();
        public  ResultadoDePago ProcesarPago( SolicitudDePago solicitudDePago)
        {
            IProcesadorDePago procesadorDePago = CrearProcesador();
            ResultadoDePago resultado = procesadorDePago.Procesar(solicitudDePago);

            Pago pago = new()
            {
                IdCliente = solicitudDePago.IdCliente,
                FechaRegistro = DateTime.UtcNow,
                Divisa = solicitudDePago.Divisa,
                Subtotal = solicitudDePago.Total,
                Descuento = 0m,
                Total = solicitudDePago.Total,
                Estado = (byte)(resultado.Ok ? 1 : 0), 
                UltimaActualizacion = DateTime.UtcNow,
                // colocar TTPO 
            };

            DbContext.Add(pago);
            DbContext.SaveChanges();
            return resultado;
        }
    }
}
