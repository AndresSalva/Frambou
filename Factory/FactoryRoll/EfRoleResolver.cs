using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Factory.FactoryRoll
{
    public class EfRoleResolver : IRoleResolver
    {
        private readonly HospitalDeVehiculosContext _ctx;
        public EfRoleResolver(HospitalDeVehiculosContext ctx) => _ctx = ctx;

        public async Task<UserRole> ResolveAsync(int userId)
        {
            var esAdminProg = await _ctx.Set<AdministradorDeProgramacion>()
                                        .AsNoTracking()
                                        .AnyAsync(a => a.Id == userId);

            if (esAdminProg) return UserRole.AdminProg;

            var esCliente = await _ctx.Set<Cliente>()
                                      .AsNoTracking()
                                      .AnyAsync(c => c.Id == userId);

            if (esCliente) return UserRole.Cliente;

            return UserRole.Desconocido;
        }
    }
}
