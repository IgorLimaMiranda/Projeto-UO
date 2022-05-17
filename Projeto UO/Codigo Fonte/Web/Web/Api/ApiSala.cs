using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Api {
    public class ApiSala : Api {
        public async Task<bool> CadastrarSala(Sala sala) {
            using (HttpClient _salaClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", sala.Id.ToString()),
                    new KeyValuePair<string,string>("Numero", sala.Numero.ToString()),
                    new KeyValuePair<string,string>("Andar", sala.Andar.ToString()),
                    new KeyValuePair<string,string>("Nome", sala.Nome.ToString()),
                    new KeyValuePair<string,string>("Tipo", sala.Tipo.ToString()),
                    new KeyValuePair<string,string>("Descricao", sala.Descricao),
                    new KeyValuePair<string,string>("Capacidade", sala.Capacidade.ToString()),
                    new KeyValuePair<string,string>("DataAlteracao", sala.DataAlteracao. ToString())
                });

                var response = await _salaClient.PostAsync($"http://{Endereco}:{Porta}/CadastroSala/Cadastrar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<Sala>> ListarSalas() {
            using (HttpClient _salaClient = new HttpClient()) {
                var response = await _salaClient.GetAsync($"http://{Endereco}:{Porta}/CadastroSala/Listar/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Sala>>(content);

                return resultado;
            }
        }

        public async Task<Sala> BuscarSala(int id = 0, int numero = 0) {
            using (HttpClient _salaClient = new HttpClient()) {
                var response = await _salaClient.GetAsync($"http://{Endereco}:{Porta}/CadastroSala/BuscarPorId/{id}/{numero}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                Sala sala = JsonConvert.DeserializeObject<Sala>(content);

                return sala;
            }
        }

        public async Task<bool> AlterarSala(Sala sala) {
            using (HttpClient _salaClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", sala.Id.ToString()),
                    new KeyValuePair<string,string>("Numero", sala.Numero.ToString()),
                    new KeyValuePair<string,string>("Andar", sala.Andar.ToString()),
                    new KeyValuePair<string,string>("Nome", sala.Nome.ToString()),
                    new KeyValuePair<string,string>("Tipo", sala.Tipo.ToString()),
                    new KeyValuePair<string,string>("Descricao", sala.Descricao),
                    new KeyValuePair<string,string>("Capacidade", sala.Capacidade.ToString()),
                    new KeyValuePair<string,string>("DataAlteracao", sala.DataAlteracao. ToString())
                });

                var response = await _salaClient.PutAsync($"http://{Endereco}:{Porta}/CadastroSala/Alterar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
        
        public async Task<bool> ExcluirSala(int id) {
            using (HttpClient _salaClient = new HttpClient()) {
                var response = await _salaClient.DeleteAsync($"http://{Endereco}:{Porta}/CadastroSala/Excluir/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
    }
}