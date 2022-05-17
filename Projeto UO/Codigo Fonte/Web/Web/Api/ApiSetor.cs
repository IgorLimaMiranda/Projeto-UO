using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Api {
    public class ApiSetor : Api {
        public async Task<bool> CadastrarSetor(Setor setor) {
            using (HttpClient _setorClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", setor.Id.ToString()),
                    new KeyValuePair<string,string>("Nome", setor.Nome),
                    new KeyValuePair<string,string>("Andar", setor.Andar)
                });

                var response = await _setorClient.PostAsync($"http://{Endereco}:{Porta}/CadastroSetor/Cadastrar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<Setor>> ListarSetores() {
            using (HttpClient _setorClient = new HttpClient()) {
                var response = await _setorClient.GetAsync($"http://{Endereco}:{Porta}/CadastroSetor/Listar/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Setor>>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<Setor>> ListarSetoresSalas() {
            using (HttpClient _setorClient = new HttpClient()) {
                var response = await _setorClient.GetAsync($"http://{Endereco}:{Porta}/CadastroSetor/ListarSetorSala/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Setor>>(content);

                return resultado;
            }
        }

        public async Task<Setor> BuscarSetor(int id) {
            using (HttpClient _setorClient = new HttpClient()) {
                var response = await _setorClient.GetAsync($"http://{Endereco}:{Porta}/CadastroSetor/BuscarPorId/{id}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                Setor setor = JsonConvert.DeserializeObject<Setor>(content);

                return setor;
            }
        }

        public async Task<bool> AlterarSetor(Setor setor) {
            using (HttpClient _setorClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", setor.Id.ToString()),
                    new KeyValuePair<string,string>("Nome", setor.Nome),
                    new KeyValuePair<string,string>("Andar", setor.Andar)
                });

                var response = await _setorClient.PutAsync($"http://{Endereco}:{Porta}/CadastroSetor/Alterar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<bool> ExcluirSetor(int id) {
            using (HttpClient _setorClient = new HttpClient()) {
                var response = await _setorClient.DeleteAsync($"http://{Endereco}:{Porta}/CadastroSetor/Excluir/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
    }
}