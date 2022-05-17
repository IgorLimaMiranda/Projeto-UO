using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Api {
    public class ApiChamado : Api {
        public async Task<bool> CadastrarChamado(Chamado chamado) {
            using (HttpClient _CadastroClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", chamado.IdChamado.ToString()),
                    new KeyValuePair<string,string>("Patrimonio", chamado.Patrimonio),
                    new KeyValuePair<string,string>("Descricao", chamado.Descricao),
                    new KeyValuePair<string,string>("HoraData", chamado.HoraData.ToString("yyyy-MM-dd HH:mm:ss")),
                    new KeyValuePair<string,string>("Imagem", chamado.Imagem),
                    new KeyValuePair<string,string>("IdSetor", chamado.IdSetor.ToString()),
                    new KeyValuePair<string,string>("IdIncidenteRecorrente", chamado.IdIncidenteRecorrente.ToString()),
                    new KeyValuePair<string,string>("CpfUsuario", chamado.CpfUsuario)
                });

                var response = await _CadastroClient.PostAsync($"http://{Endereco}:{Porta}/CadastroChamado/Cadastrar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<Chamado>> ListarChamado(Usuario usuario, bool apenasMeusChamados = false) {
            using (HttpClient _CadastroClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Cpf", usuario.Cpf),
                    new KeyValuePair<string,string>("TipoUsuario", usuario.TipoUsuario)
                });

                var response = await _CadastroClient.PostAsync($"http://{Endereco}:{Porta}/CadastroChamado/Listar/{apenasMeusChamados}", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Chamado>>(content);

                return resultado;
            }
        }

        public async Task<Chamado> BuscarChamado(int id) {
            using (HttpClient _CadastroClient = new HttpClient()) {
                var response = await _CadastroClient.GetAsync($"http://{Endereco}:{Porta}/CadastroChamado/BuscarPorId/{id}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                Chamado chamado = JsonConvert.DeserializeObject<Chamado>(content);

                return chamado;
            }
        }

        public async Task<bool> AlterarChamado(Chamado chamado) {
            using (HttpClient _ChamadoClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", chamado.IdChamado.ToString()),
                    new KeyValuePair<string,string>("Patrimonio", chamado.Patrimonio),
                    new KeyValuePair<string,string>("Descricao", chamado.Descricao),
                    new KeyValuePair<string,string>("HoraData", chamado.HoraData.ToString("yyyy-MM-dd HH:mm:ss")),
                    new KeyValuePair<string,string>("Imagem", chamado.Imagem),
                    new KeyValuePair<string,string>("IdSetor", chamado.IdSetor.ToString()),
                    new KeyValuePair<string,string>("IdIncidenteRecorrente", chamado.IdIncidenteRecorrente.ToString()),
                    new KeyValuePair<string,string>("CpfUsuario", chamado.CpfUsuario)
                });

                var response = await _ChamadoClient.PutAsync($"http://{Endereco}:{Porta}/CadastroChamado/Alterar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<List<Chamado>> ListarChamadosDashboard(Usuario usuario) {
            using (HttpClient _ChamadoClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Cpf", usuario.Cpf.ToString())
                });

                var response = await _ChamadoClient.PostAsync($"http://{Endereco}:{Porta}/CadastroChamado/ListarChamadosDashboard", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Chamado>>(content);

                return resultado;
            }
        }
    }
}