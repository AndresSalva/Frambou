namespace HospitalDeVehiculosUltimaVersion.Factory.FactoryPago.QrUltils
{
    public interface IQrCodeGenerator
    {
        byte[] Generate(string payload);
    }
}
