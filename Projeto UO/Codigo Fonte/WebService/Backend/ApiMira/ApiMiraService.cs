using Backend.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Backend.Enum;

namespace Backend.ApiMira {
    public class ApiMiraService {
        public async Task<Usuario> ConfirmarInformacao(String cpf, String senha) {
            try {
                cpf = GerenciarMascaraDoCpf(cpf);
                AutenticarUsuario x = Autorizar(cpf, senha).Result;
                String codigoPessoaFisica = Convert.ToString(x.CodigoPessoaFisica);
                using (HttpClient _dadosAlunoClient = new HttpClient()) {
                    var requisicao = new HttpRequestMessage {
                        RequestUri = new Uri("http://mira.ms.senac.br/senacintegracao/ConsultarInformacao"),
                        Method = HttpMethod.Post
                    };

                    requisicao.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requisicao.Headers.Add("DRHash", "FF505D6D8ED34BD3A3B6F9D90C58DF58");
                    requisicao.Headers.Add("AppHash", "96604AB6645E43FBAE4AFAB392DBF5EE");
                    requisicao.Headers.Add("CodigoPessoaFisica", codigoPessoaFisica);

                    var resposta = await _dadosAlunoClient.SendAsync(requisicao).ConfigureAwait(false);
                    var conteudo = await resposta.Content.ReadAsStringAsync();

                    UsuarioMira dadosAluno = JsonConvert.DeserializeObject<UsuarioMira>(conteudo);

                    Usuario usuario = null;
                    if (dadosAluno.CPF != null) {
                        usuario = new Usuario {
                            Cpf = GerenciarMascaraDoCpf(dadosAluno.CPF),
                            Nome = dadosAluno.NomeAluno,
                            Email = dadosAluno.Email,
                            Senha = senha,
                            PrimeiroAcesso = true,
                            TipoUsuario = Enum.EnumTipoUsuario.ALUNO.GetDescription()
                        };
                    }

                    return usuario;
                }
            }
            catch (Exception e) {
                return null;
            }

        }

        public async Task<AutenticarUsuario> Autorizar(string cpf, string senha) {
            using (HttpClient _usuarioClient = new HttpClient()) {

                var httpRequest = new HttpRequestMessage {
                    RequestUri = new Uri("http://mira.ms.senac.br/senacintegracao/autorizar"),
                    Method = HttpMethod.Post
                };

                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(cpf + "|" + senha);
                var hash64 = Convert.ToBase64String(plainTextBytes);


                httpRequest.Headers.Add("AppHash", "96604AB6645E43FBAE4AFAB392DBF5EE");
                httpRequest.Headers.Add("DRHash", "FF505D6D8ED34BD3A3B6F9D90C58DF58");
                httpRequest.Content = new StringContent("\"" + hash64 + "\"", Encoding.UTF8, "application/json");

                var response = await _usuarioClient.SendAsync(httpRequest).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<AutenticarUsuario>(content);
                return resultado;
            }
        }

        private string GerenciarMascaraDoCpf(string cpf) {
            string cpfLocal = string.Empty;
            if (cpf.Length == 14) {
                cpfLocal = cpf.Replace(".", "");
                cpfLocal = cpfLocal.Replace("-", "");
                return cpfLocal;
            }
            else if (cpf.Length == 11) {
                cpfLocal = cpf.Insert(3, ".");
                cpfLocal = cpfLocal.Insert(7, ".");
                cpfLocal = cpfLocal.Insert(11, "-");
                return cpfLocal;
            }

            return cpfLocal;
        }
    }
}