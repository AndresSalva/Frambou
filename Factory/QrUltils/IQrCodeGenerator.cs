namespace HospitalDeVehiculosUltimaVersion.Factory.QrUltils
{
    public interface IQrCodeGenerator
    {
        byte[] Generate(string payload);
    }
}
