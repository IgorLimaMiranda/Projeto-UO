using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Web.Api {
    public class ApiEquipamentoSetor : Api {
        public async Task<bool> CadastrarEquipamentoSetor(EquipamentoSetor equipamentoSetor) {
            using (HttpClient _equipamentoClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("IdEquipamento", equipamentoSetor.IdEquipamento.ToString()),
                    new KeyValuePair<string,string>("IdSetor", equipamentoSetor.IdSetor.ToString()),
                    new KeyValuePair<string,string>("Quantidade", equipamentoSetor.Quantidade.ToString())
                });

                var response = await _equipamentoClient.PostAsync($"http://{Endereco}:{Porta}/CadastroEquipamentoSetor/Cadastrar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<bool> CadastrarEquipamentosSetor(List<EquipamentoSetor> equipamentosSetor) {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(equipamentosSetor));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (HttpClient _equipamentoClient = new HttpClient()) {
                //List<FormUrlEncodedContent> lista_Content = new List<FormUrlEncodedContent>();
                //foreach (var equipamentoSetor in equipamentosSetor) {
                //    var _Content = new FormUrlEncodedContent(new[]{
                //    new KeyValuePair<string,string>("IdEquipamento", equipamentoSetor.IdEquipamento.ToString()),
                //    new KeyValuePair<string,string>("IdSetor", equipamentoSetor.IdSetor.ToString()),
                //    new KeyValuePair<string,string>("Quantidade", equipamentoSetor.Quantidade.ToString())
                //});
                    //lista_Content.Add(_Content);
                //}
                var response = await _equipamentoClient.PostAsync($"http://{Endereco}:{Porta}/CadastroEquipamentoSetor/CadastrarEquipamentosSetor/", httpContent).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }


        public async Task<ObservableCollection<EquipamentoSetor>> ListarEquipamentoSetor() {
            using (HttpClient _equipamentoClient = new HttpClient()) {
                var response = await _equipamentoClient.GetAsync($"http://{Endereco}:{Porta}/CadastroEquipamentoSetor/Listar/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<EquipamentoSetor>>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<EquipamentoSetor>> ListarEquipamentosSala(Sala sala) {
            using (HttpClient _equipamentoClient = new HttpClient()) {
                var response = await _equipamentoClient.GetAsync($"http://{Endereco}:{Porta}/CadastroEquipamentoSetor/ListarEquipamentosSala/{sala.Id}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<EquipamentoSetor>>(content);

                return resultado;
            }
        }

        public async Task<bool> AlterarEquipamentoSetor(EquipamentoSetor equipamentoSetor) {
            using (HttpClient _equipamentoClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("IdEquipamento", equipamentoSetor.IdEquipamento.ToString()),
                    new KeyValuePair<string,string>("IdSetor", equipamentoSetor.IdSetor.ToString()),
                    new KeyValuePair<string,string>("Quantidade", equipamentoSetor.Quantidade.ToString())
                });

                var response = await _equipamentoClient.PutAsync($"http://{Endereco}:{Porta}/CadastroEquipamentoSetor/Alterar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<bool> ExcluirEquipamentosSetor(int idSetor) {
            using (HttpClient _equipamentoClient = new HttpClient()) {
                var response = await _equipamentoClient.DeleteAsync($"http://{Endereco}:{Porta}/CadastroEquipamentoSetor/ExcluirEquipamentosSetor/{idSetor}");
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }


    }
}