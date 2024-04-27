using Microsoft.AspNetCore.Http;

namespace VideoGameStore.Services
{
    // Implementación de la interfaz ISessionService para gestionar la sesión del usuario
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string UserIdSessionKey = "UserId";

        // Constructor que recibe el IHttpContextAccessor para acceder a la sesión
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Establece el nombre de usuario en la sesión
        public void SetUsername(string username)
        {
            _httpContextAccessor.HttpContext.Session.SetString("Username", username);
        }

        // Obtiene el nombre de usuario de la sesión
        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("Username");
        }

        // Establece el estado de administrador en la sesión
        public void SetIsAdmin(bool isAdmin)
        {
            _httpContextAccessor.HttpContext.Session.SetString("IsAdmin", isAdmin.ToString());
        }

        // Obtiene el estado de administrador de la sesión
        public bool GetIsAdmin()
        {
            var isAdminString = _httpContextAccessor.HttpContext.Session.GetString("IsAdmin");
            return isAdminString != null && bool.Parse(isAdminString);
        }

        // Borra la sesión actual
        public void ClearSession()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        // Establece el ID de usuario en la sesión
        public void SetUserId(int userId)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32("UserId", userId);
        }

        // Obtiene el ID de usuario de la sesión
        public int GetUserId()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("UserId") ?? 0;
        }
    }
}
