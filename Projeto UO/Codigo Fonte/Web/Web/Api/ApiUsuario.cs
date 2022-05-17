using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Api {
    public class ApiUsuario : Api {
        public async Task<bool> CadastrarUsuario(Usuario usuario) {
            using (HttpClient _usuarioClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Cpf", usuario.Cpf),
                    new KeyValuePair<string,string>("Nome", usuario.Nome),
                    new KeyValuePair<string,string>("Email", usuario.Email),
                    new KeyValuePair<string,string>("Senha", usuario.Senha),
                    new KeyValuePair<string,string>("Pergunta", usuario.Pergunta),
                    new KeyValuePair<string,string>("Resposta", usuario.Resposta),
                    new KeyValuePair<string,string>("PrimeiroAcesso", usuario.PrimeiroAcesso.ToString()),
                    new KeyValuePair<string,string>("ImagemUsuario", usuario.ImagemUsuario),
                    new KeyValuePair<string,string>("TipoUsuario", usuario.TipoUsuario),
                    new KeyValuePair<string,string>("EixoEducacional", usuario.EixoEducacional)
                });

                var response = await _usuarioClient.PostAsync($"http://{Endereco}:{Porta}/CadastroUsuario/Cadastrar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<Usuario>> ListarUsuarios() {
            using (HttpClient _usuarioClient = new HttpClient()) {
                var response = await _usuarioClient.GetAsync($"http://{Endereco}:{Porta}/CadastroUsuario/Listar/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Usuario>>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<Usuario>> ListarUsuariosAdministrativos() {
            using (HttpClient _usuarioClient = new HttpClient()) {
                var response = await _usuarioClient.GetAsync($"http://{Endereco}:{Porta}/CadastroUsuario/ListarUsuariosAdministrativos/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Usuario>>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<Usuario>> ListarUsuariosAdministrativosCoordenadores() {
            using (HttpClient _usuarioClient = new HttpClient()) {
                var response = await _usuarioClient.GetAsync($"http://{Endereco}:{Porta}/CadastroUsuario/ListarUsuariosAdministrativosCoordenadores/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Usuario>>(content);

                return resultado;
            }
        }

        public async Task<Usuario> BuscarUsuario(string cpf) {
            using (HttpClient _usuarioClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Cpf", cpf)
                 });

                var response = await _usuarioClient.PostAsync($"http://{Endereco}:{Porta}/CadastroUsuario/BuscarPorCpf/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(content);

                return usuario;
            }
        }

        public async Task<bool> AlterarUsuario(Usuario usuario) {
            using (HttpClient _usuarioClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Cpf", usuario.Cpf),
                    new KeyValuePair<string,string>("Nome", usuario.Nome),
                    new KeyValuePair<string,string>("Email", usuario.Email),
                    new KeyValuePair<string,string>("Senha", usuario.Senha),
                    new KeyValuePair<string,string>("Pergunta", usuario.Pergunta),
                    new KeyValuePair<string,string>("Resposta", usuario.Resposta),
                    new KeyValuePair<string,string>("PrimeiroAcesso", usuario.PrimeiroAcesso.ToString()),
                    new KeyValuePair<string,string>("ImagemUsuario", usuario.ImagemUsuario),
                    new KeyValuePair<string,string>("TipoUsuario", usuario.TipoUsuario),
                    new KeyValuePair<string,string>("EixoEducacional", usuario.EixoEducacional)
                });

                var response = await _usuarioClient.PutAsync($"http://{Endereco}:{Porta}/CadastroUsuario/Alterar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
        
        public async Task<bool> ExcluirUsuario(string cpf) {
            using (HttpClient _usuarioClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Cpf", cpf)
                });

                var response = await _usuarioClient.PostAsync($"http://{Endereco}:{Porta}/CadastroUsuario/Excluir", _Content);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<Usuario> Login(Usuario usuario) {
            using (HttpClient _usuarioClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Cpf",usuario.Cpf),
                    new KeyValuePair<string,string>("Senha",usuario.Senha)
                });

                var response = await _usuarioClient.PostAsync($"http://{Endereco}:{Porta}/CadastroUsuario/Logar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                Usuario usuarioLocal = JsonConvert.DeserializeObject<Usuario>(content);

                return usuarioLocal;
            }
        }

        public async Task<Usuario> RecuperarSenhaUsuario(Usuario usuario) {
            using (HttpClient _usuarioClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Cpf", usuario.Cpf),
                    new KeyValuePair<string,string>("Resposta", usuario.Resposta)
                });

                var response = await _usuarioClient.PostAsync($"http://{Endereco}:{Porta}/CadastroUsuario/RecuperarSenha/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                Usuario usuarioLocal = JsonConvert.DeserializeObject<Usuario>(content);

                return usuarioLocal;
            }
        }

        //public async Task<Pessoa> Login(Pessoa pessoa)
        //{
        //    using (HttpClient _usuarioClient = new HttpClient())
        //    {
        //        var httpRequest = new HttpRequestMessage
        //        {
        //            RequestUri = new Uri($"http://{Endereco}:{Porta}/api/usuario/Login"),
        //            Method = HttpMethod.Get
        //        };

        //        httpRequest.Content = new StringContent("\"" + pessoa, Encoding.UTF8, "aplication/json");
        //        var response = await _usuarioClient.SendAsync(httpRequest).ConfigureAwait(false);

        //        var content = await response.Content.ReadAsStringAsync();
        //        var resultado = JsonConvert.DeserializeObject<Pessoa>(content);
        //        return resultado;
        //    }
        //}

        //public async Task<bool> AlterarSenha(Pessoa pessoa)
        //{
        //    using(HttpClient _usuarioClient = new HttpClient())
        //    {
        //        var _Content = new FormUrlEncodedContent(new[]{
        //            new KeyValuePair<string,string>("Cpf",pessoa.Cpf),
        //            new KeyValuePair<string,string>("Senha",pessoa.Senha)
        //        });

        //        var response = await _usuarioClient.PutAsync($"http://{Endereco}:{Porta}/api/usuario/AlterarUsuario", _Content).ConfigureAwait(false);
        //        var content = await response.Content.ReadAsStringAsync();
        //        var resultado = JsonConvert.DeserializeObject<bool>(content);

        //        return resultado;
        //    }
        //}

        ////LOGIN
        //public async Task<Pessoa> Login(Login login)
        //{
        //    using (HttpClient _usuarioClient = new HttpClient())
        //    {
        //        var httpRequest = new HttpRequestMessage
        //        {
        //            RequestUri = new Uri($"http://{Endereco}:{Porta}/api/usuario/Login"),
        //            Method = HttpMethod.Post
        //        };

        //        httpRequest.Content = new StringContent("\"" + login, Encoding.UTF8, "aplication/json");
        //        var response = await _usuarioClient.SendAsync(httpRequest).ConfigureAwait(false);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            var resultado = JsonConvert.DeserializeObject<Pessoa>(content);
        //            return resultado;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}