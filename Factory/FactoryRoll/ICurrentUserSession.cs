namespace HospitalDeVehiculosUltimaVersion.Factory.FactoryRoll
{
    public interface ICurrentUserSession
    {
        void Set(int userId, UserRole role);
        (int? UserId, UserRole? Role) Get();
    }
}
