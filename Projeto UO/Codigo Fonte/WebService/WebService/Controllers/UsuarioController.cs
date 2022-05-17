using Backend.ApiMira;
using Backend.Enum;
using Backend.Model;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebService.Controllers {
    [RoutePrefix("CadastroUsuario")]
    public class UsuarioController : ApiController {

        private UsuarioRepository usuarioRepository;
        public UsuarioController() {
            usuarioRepository = new UsuarioRepository();
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public bool CadastrarUsuario(Usuario usuario) {
            try {
                if (usuario.ImagemUsuario == null)
                    usuario.ImagemUsuario = "user.png";
                usuario.ImagemUsuario = $"{(usuario.ImagemUsuario.Substring(0, 14)).Replace(".", "").Replace("-", "")}{(usuario.ImagemUsuario).Substring(14)}";
                usuarioRepository.Add(usuario);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public IEnumerable<Usuario> ListarUsuarios() {
            try {
                return usuarioRepository.GetAll();
            } catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("GET")]
        [Route("ListarUsuariosAdministrativos")]
        public IEnumerable<Usuario> ListarUsuariosAdministradores() {
            try {
                return usuarioRepository.Get(usuario => usuario.TipoUsuario.Equals(EnumTipoUsuario.ADMINISTRATIVO.GetDescription()));
            }
            catch (Exception e) {
                return null;
            }
        }


        [AcceptVerbs("POST")]
        [Route("BuscarPorCpf")]
        public Usuario BuscarUsuario(Usuario usuario) {
            try {
                return usuarioRepository.Get(u => u.Cpf.Equals(usuario.Cpf)).First();
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar")]
        public bool EditarUsuario(Usuario usuario) {
            try {
                if(!(usuario.ImagemUsuario.Equals("user.png") || usuario.ImagemUsuario.Count()==15))
                    usuario.ImagemUsuario = $"{(usuario.ImagemUsuario.Substring(0, 14)).Replace(".", "").Replace("-", "")}{(usuario.ImagemUsuario).Substring(14)}";
                usuarioRepository.Update(u => u.Cpf.Equals(usuario.Cpf), usuario);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("Excluir")]
        public bool ExcluirUsuario(Usuario usuario) {
            try {
                usuarioRepository.Delete(u => u.Cpf.Equals(usuario.Cpf));
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        [AcceptVerbs("POST")]
        [Route("Logar")]
        public async Task<Usuario> RealizarLogin(Usuario usuario) {
            try {
                Usuario usuarioLocal = usuarioRepository.RealizarLogin(usuario);
                if (usuarioLocal != null)
                    return usuarioLocal;
            
                usuarioLocal = await new ApiMiraService().ConfirmarInformacao(usuario.Cpf, usuario.Senha);
                if(usuarioLocal != null) {
                    usuarioRepository.Add(usuarioLocal);
                    return usuarioLocal;
                }

                return usuarioLocal;
            }
            catch (Exception e) {
                return null;
            }
        }

        [AcceptVerbs("POST")]
        [Route("RecuperarSenha")]
        public async Task<Usuario> RecuperarSenha(Usuario usuario) {
            try {
                return usuarioRepository.RecuperarSenha(usuario);
            } catch (Exception e) {
                return null;
            }
        }
    }
}