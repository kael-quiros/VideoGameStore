using VideoGameStore.Services;

namespace VideoGameStore.Services
{
    // Interfaz que define los métodos para administrar la sesión del usuario
    public interface ISessionService
    {
        // Establece el nombre de usuario en la sesión
        void SetUsername(string username);

        // Obtiene el nombre de usuario de la sesión
        string GetUsername();

        // Establece el estado de administrador en la sesión
        void SetIsAdmin(bool isAdmin);

        // Obtiene el estado de administrador de la sesión
        bool GetIsAdmin();

        // Limpia los datos de la sesión
        void ClearSession();

        // Obtiene el ID de usuario de la sesión
        int GetUserId();

        // Establece el ID de usuario en la sesión
        void SetUserId(int userId);
    }
}
