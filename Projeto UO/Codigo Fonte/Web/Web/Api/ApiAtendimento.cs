using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Api {
    public class ApiAtendimento : Api {
        public async Task<bool> CadastrarAtendimento(Atendimento atendimento) {
            using (HttpClient _atendimentoClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", atendimento.Id.ToString()),
                    new KeyValuePair<string,string>("Detalhes", atendimento.Detalhes),
                    new KeyValuePair<string,string>("DataAtendimento", atendimento.DataAtendimento.ToString("yyyy-MM-dd HH:mm:ss")), 
                    new KeyValuePair<string,string>("Status", atendimento.Status),
                    new KeyValuePair<string,string>("CpfTecnico", atendimento.CpfTecnico),
                    new KeyValuePair<string,string>("IdChamado", atendimento.IdChamado.ToString())
                });

                var response = await _atendimentoClient.PostAsync($"http://{Endereco}:{Porta}/CadastroAtendimento/Cadastrar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<ObservableCollection<Atendimento>> ListarAtendimentos() {
            using (HttpClient _atendimentoClient = new HttpClient()) {
                var response = await _atendimentoClient.GetAsync($"http://{Endereco}:{Porta}/CadastroAtendimento/Listar/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Atendimento>>(content);

                return resultado;
            }
        }

        public async Task<List<Atendimento>> ListarAtendimentosDeChamado(int id) {
            using (HttpClient _atendimentoClient = new HttpClient()) {
                var response = await _atendimentoClient.GetAsync($"http://{Endereco}:{Porta}/CadastroAtendimento/BuscarAtendimentosDeChamado/{id}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                List<Atendimento> atendimentos = JsonConvert.DeserializeObject<List<Atendimento>>(content);

                return atendimentos;
            }
        }

        public async Task<Atendimento> BuscarAtendimento(int id) {
            using (HttpClient _atendimentoClient = new HttpClient()) {
                var response = await _atendimentoClient.GetAsync($"http://{Endereco}:{Porta}/CadastroAtendimento/BuscarPorId/{id}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                Atendimento atendimento = JsonConvert.DeserializeObject<Atendimento>(content);

                return atendimento;
            }
        }

        public async Task<bool> AlterarAtendimento(Atendimento atendimento) {
            using (HttpClient _atendimentoClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", atendimento.Id.ToString()),
                    new KeyValuePair<string,string>("Detalhes", atendimento.Detalhes),
                    new KeyValuePair<string,string>("DataAtendimento", atendimento.DataAtendimento.ToString("yyyy-MM-dd HH:mm:ss")),
                    new KeyValuePair<string,string>("Status", atendimento.Status),
                    new KeyValuePair<string,string>("CpfTecnico", atendimento.CpfTecnico),
                    new KeyValuePair<string,string>("IdChamado", atendimento.IdChamado.ToString())
                });

                var response = await _atendimentoClient.PutAsync($"http://{Endereco}:{Porta}/CadastroAtendimento/Alterar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

        public async Task<bool> ExcluirAtendimento(int id) {
            using (HttpClient _atendimentoClient = new HttpClient()) {
                var response = await _atendimentoClient.DeleteAsync($"http://{Endereco}:{Porta}/CadastroAtendimento/Excluir/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
    }
}