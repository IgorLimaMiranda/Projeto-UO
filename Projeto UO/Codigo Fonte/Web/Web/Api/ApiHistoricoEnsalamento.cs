using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Web.Api {
    public class ApiHistoricoEnsalamento : Api {
        public async Task<bool> CadastrarHistoricoEnsalamento(HistoricoEnsalamento historico) {
            using (HttpClient _ensalamentoClient = new HttpClient()) {
                var objeto = JsonConvert.SerializeObject(historico, Formatting.Indented);
                var _Content = new StringContent(objeto, Encoding.UTF8, "application/json");
                var response = await _ensalamentoClient.PostAsync($"http://{Endereco}:{Porta}/HistoricoEnsalamento/Cadastrar/", _Content).ConfigureAwait(false);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<List<HistoricoEnsalamento>> ListarHistoricos() {
            using (HttpClient _historicoClient = new HttpClient()) {
                var response = await _historicoClient.GetStringAsync($"http://{Endereco}:{Porta}/HistoricoEnsalamento/Listar/").ConfigureAwait(false);
                List<HistoricoEnsalamento> historicos = JsonConvert.DeserializeObject<List<HistoricoEnsalamento>>(response);
                return historicos;
            }
        }

        public async Task<ObservableCollection<string>> ListarOcupantes() {
            using (HttpClient _historicoClient = new HttpClient()) {
                var response = await _historicoClient.GetAsync($"http://{Endereco}:{Porta}/HistoricoEnsalamento/ListarOcupantes/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<string>>(content);
                return resultado;
            }
        }

        public async Task<ObservableCollection<string>> ListarAcao() {
            using(HttpClient _historicoClient = new HttpClient()) {
                var response = await _historicoClient.GetAsync($"http://{Endereco}:{Porta}/HistoricoEnsalamento/ListarAcoes/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<string>>(content);
                return resultado;
            }
        }

        public async Task<ObservableCollection<string>> ListarUsuarios() {
            using (HttpClient _historicoClient = new HttpClient()) {
                var response = await _historicoClient.GetAsync($"http://{Endereco}:{Porta}/HistoricoEnsalamento/ListarUsuarios/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<string>>(content);
                return resultado;
            }
        }

        public async Task<ObservableCollection<string>> ListarTurmas() {
            using(HttpClient _turmaClient = new HttpClient()) {
                var response = await _turmaClient.GetAsync($"http://{Endereco}:{Porta}/HistoricoEnsalamento/ListarTurmas/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<string>>(content);
                return resultado;
            }
        }

        public async Task<ObservableCollection<string>> ListarSalas() {
            using(HttpClient _salaClient = new HttpClient()) {
                var response = await _salaClient.GetAsync($"http://{Endereco}:{Porta}/HistoricoEnsalamento/ListarSalas/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<string>>(content);
                return resultado;
            }
        }
    }
}