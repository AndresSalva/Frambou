using HospitalDeVehiculosUltimaVersion.Factory;

namespace HospitalDeVehiculosUltimaVersion
{
    public record SolicitudDePago(
        int IdCliente, decimal Total, string Divisa 
    );

    public record ResultadoDePago(
        decimal Total, bool Ok, TipoDePago Tipo    
    );
}