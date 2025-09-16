namespace HospitalDeVehiculosUltimaVersion.Factory.FactoryPago
{
    public enum MetaDato
    {
        NUMERO_TARJETA,
        FECHA_VENCIMIENTO,
        CVV
    }
    public class SolicitudDePago
    {
        public int IdCliente;
        public decimal Total;
        public string? Divisa;
        public Dictionary<MetaDato, string> MetaDatos = new();

        public SolicitudDePago() { }
        public SolicitudDePago(int idCliente, decimal total, string divisa) 
        { 
            this.IdCliente = idCliente;
            this.Total = total;
            this.Divisa = divisa;
        }

        public void AgregarMetaDato( MetaDato metaDato, string valor )
        {
            this.MetaDatos.Add( metaDato, valor );
        }
    }

    public class ResultadoDePago
    {
        public decimal Total { get; set; }
        public bool Ok { get; set; }
        public TipoDePago Tipo { get; set; }

        public ResultadoDePago( decimal total, bool ok, TipoDePago tipoDePago)
        {
            this.Total = total;
            this.Ok = ok;
            this.Tipo = tipoDePago;
        }
    }
        
}