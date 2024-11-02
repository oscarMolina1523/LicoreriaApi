using LicoreriaBackend.Models;

namespace LicoreriaBackend.Interfaces
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int id);
        bool UsuarioExists(int id);
        bool CreateUsuario(Usuario usuario);
        bool UpdateUsuario(Usuario usuario);
        bool DeleteUsuario(Usuario usuario);
        bool Save();
    }
}
