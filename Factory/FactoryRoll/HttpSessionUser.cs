using System;
using Microsoft.AspNetCore.Http;

namespace HospitalDeVehiculosUltimaVersion.Factory.FactoryRoll
{
    public class HttpSessionUser : ICurrentUserSession
    {
        private readonly IHttpContextAccessor _http;
        private const string KUserId = "UsuarioId";
        private const string KRole = "Rol";

        public HttpSessionUser(IHttpContextAccessor http) => _http = http;

        public void Set(int userId, UserRole role)
        {
            var s = _http.HttpContext!.Session;
            s.SetInt32(KUserId, userId);
            s.SetString(KRole, role.ToString());

            if (role == UserRole.AdminProg) s.SetInt32("AdminProgId", userId);
            if (role == UserRole.Cliente) s.SetInt32("ClienteId", userId);
        }

        public (int? UserId, UserRole? Role) Get()
        {
            var s = _http.HttpContext!.Session;
            var id = s.GetInt32(KUserId);
            var r = s.GetString(KRole);
            return (id, Enum.TryParse<UserRole>(r, out var role) ? role : null);
        }

        public void Clear() => _http.HttpContext!.Session.Clear();
    }
}
