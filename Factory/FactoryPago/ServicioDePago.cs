using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

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

            if(resultado.Ok == false) {  return resultado; }

            Pago pago = new()
            {
                IdCliente = solicitudDePago.IdCliente,
                FechaRegistro = DateTime.UtcNow,
                Divisa = solicitudDePago.Divisa,
                Descuento = 0m,
                Total = solicitudDePago.Total,
                Estado = (byte)(resultado.Ok ? 1 : 0), 
                UltimaActualizacion = DateTime.UtcNow,
                Subtotal =solicitudDePago.Total,
            };

            DbContext.Add(pago);
            DbContext.SaveChanges();
            return resultado;
        }
    }
}
