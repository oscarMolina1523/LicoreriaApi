using LicoreriaBackend.Data;
using LicoreriaBackend.Interfaces;
using LicoreriaBackend.Models;

namespace LicoreriaBackend.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext context;

        public UsuarioRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CreateUsuario(Usuario usuario)
        {
            context.Add(usuario);
            return Save();
        }

        public bool DeleteUsuario(Usuario usuario)
        {
            context.Remove(usuario);
            return Save();
        }

        public Usuario GetUsuario(int id)
        {
            return context.Usuarios.Where(e => e.Idusuario == id).FirstOrDefault();
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return context.Usuarios.ToList();
        }


        public bool UpdateUsuario(Usuario usuario)
        {
            context.Update(usuario);
            return Save();
        }

        public bool UsuarioExists(int id)
        {
            return context.Usuarios.Any(c => c.Idusuario == id);
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
