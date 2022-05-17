using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Api {
    public class ApiEquipamento : Api {
        public async Task<bool> CadastrarEquipamento(Equipamento equipamento) {
            using (HttpClient _equipamentoClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", equipamento.Id.ToString()),
                    new KeyValuePair<string,string>("Descricao", equipamento.Descricao)
                });

                var response = await _equipamentoClient.PostAsync($"http://{Endereco}:{Porta}/CadastroEquipamento/Cadastrar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<Equipamento>> ListarEquipamentos() {
            using (HttpClient _equipamentoClient = new HttpClient()) {
                var response = await _equipamentoClient.GetAsync($"http://{Endereco}:{Porta}/CadastroEquipamento/Listar/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Equipamento>>(content);

                return resultado;
            }
        }

        public async Task<Equipamento> BuscarEquipamento(int id) {
            using (HttpClient _equipamentoClient = new HttpClient()) {
                var response = await _equipamentoClient.GetAsync($"http://{Endereco}:{Porta}/CadastroEquipamento/BuscarPorId/{id}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                Equipamento equipamento = JsonConvert.DeserializeObject<Equipamento>(content);

                return equipamento;
            }
        }

        public async Task<bool> AlterarEquipamento(Equipamento equipamento) {
            using (HttpClient _equipamentoClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", equipamento.Id.ToString()),
                    new KeyValuePair<string,string>("Descricao", equipamento.Descricao)
                });

                var response = await _equipamentoClient.PutAsync($"http://{Endereco}:{Porta}/CadastroEquipamento/Alterar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<bool> ExcluirEquipamento(int id) {
            using (HttpClient _equipamentoClient = new HttpClient()) {
                var response = await _equipamentoClient.DeleteAsync($"http://{Endereco}:{Porta}/CadastroEquipamento/Excluir/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
    }
}