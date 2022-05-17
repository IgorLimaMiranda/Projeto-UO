using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Api {
    public class ApiIncidenteRecorrente : Api {
        public async Task<bool> CadastrarIncidenteRecorrente(IncidenteRecorrente incidenteRecorrente) {
            using (HttpClient _incidenteRecorrenteClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", incidenteRecorrente.Id.ToString()),
                    new KeyValuePair<string,string>("Descricao", incidenteRecorrente.Descricao)
                });

                var response = await _incidenteRecorrenteClient.PostAsync($"http://{Endereco}:{Porta}/CadastroIncidente/Cadastrar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<IncidenteRecorrente>> ListarIncidenteRecorrente() {
            using (HttpClient _incidenteClient = new HttpClient()) {
                var response = await _incidenteClient.GetAsync($"http://{Endereco}:{Porta}/CadastroIncidente/Listar/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<IncidenteRecorrente>>(content);

                return resultado;
            }
        }

        public async Task<IncidenteRecorrente> BuscarIncidenteRecorrente(int id) {
            using (HttpClient _incidenteClient = new HttpClient()) {
                var response = await _incidenteClient.GetAsync($"http://{Endereco}:{Porta}/CadastroIncidente/BuscarPorId/{id}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                IncidenteRecorrente incidenteRecorrente = JsonConvert.DeserializeObject<IncidenteRecorrente>(content);

                return incidenteRecorrente;
            }
        }

        public async Task<bool> AlterarIncidenteRecorrente(IncidenteRecorrente incidenteRecorrente) {
            using (HttpClient _incidenteClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", incidenteRecorrente.Id.ToString()),
                    new KeyValuePair<string,string>("Descricao", incidenteRecorrente.Descricao)
                });

                var response = await _incidenteClient.PutAsync($"http://{Endereco}:{Porta}/CadastroIncidente/Alterar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<bool> ExcluirIncidenteRecorrente(int id) {
            using (HttpClient _incidenteClient = new HttpClient()) {
                var response = await _incidenteClient.DeleteAsync($"http://{Endereco}:{Porta}/CadastroIncidente/Excluir/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
    }
}