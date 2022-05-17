using Backend.Enum;
using Backend.Model;
using Backend.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository
{
    public class UsuarioRepository : Repository<Usuario>
    {
        public Usuario RealizarLogin(Usuario usuario)
        {
            Usuario usuarioLocal = RetornaUsuario(usuario.Cpf);
            if (usuarioLocal != null)
            {
                Usuario usuarioCompleto = (from u in ctx.Usuario
                                           where u.Cpf.Equals(usuario.Cpf)
                                           select u).ToList()[0];

                if (usuarioCompleto != null)
                {
                    return usuarioCompleto;
                }

                return usuarioLocal;
            }

            return null;
        }

        public Usuario RetornaUsuario(string cpf)
        {
            var usuario = (from u in ctx.Usuario
                           where u.Cpf.Equals(cpf)
                           select u);
            if (usuario.Count() > 0)
                return usuario.ToList()[0];

            return null;
        }

        public Usuario RecuperarSenha(Usuario usuario)
        {
            var usuarioLocal = (from u in ctx.Usuario
                                where u.Cpf.Equals(usuario.Cpf) &&
                                u.Resposta.Equals(usuario.Resposta)
                                select u);
            if (usuarioLocal.Count() > 0)
                return usuarioLocal.ToList()[0];

            return null;
        }

        public List<Usuario> RetornarUsuariosAdministrativosCoordenadores()
        {
            var usuarios = (from u in ctx.Usuario
                            where u.TipoUsuario.Equals(EnumTipoUsuario.ADMINISTRATIVO.GetDescription()) ||
                            u.TipoUsuario.Equals(EnumTipoUsuario.COORDENADOR.GetDescription())
                            orderby u.Nome
                            select u);

            if (usuarios.Count() > 0)
                return usuarios.ToList();

            return null;
        }
    }
}

