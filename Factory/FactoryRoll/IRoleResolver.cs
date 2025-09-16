namespace HospitalDeVehiculosUltimaVersion.Factory.FactoryRoll
{
    public enum UserRole { Desconocido, Cliente, AdminProg }
    public interface IRoleResolver
    {
        Task<UserRole> ResolveAsync(int userId);
    }
}
