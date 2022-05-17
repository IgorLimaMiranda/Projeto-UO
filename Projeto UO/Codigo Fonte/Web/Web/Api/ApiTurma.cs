using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Api {
    public class ApiTurma : Api {
        public async Task<bool> CadastrarTurma(Turma turma) {
            using (HttpClient _turmaClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", turma.Id.ToString()),
                    new KeyValuePair<string,string>("Ano", turma.Ano.ToString()),
                    new KeyValuePair<string,string>("IdCurso", turma.IdCurso.ToString())
                });

                var response = await _turmaClient.PostAsync($"http://{Endereco}:{Porta}/CadastroTurma/Cadastrar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
        
        public async Task<ObservableCollection<Turma>> ListarTurmas() {
            using (HttpClient _turmaClient = new HttpClient()) {
                var response = await _turmaClient.GetAsync($"http://{Endereco}:{Porta}/CadastroTurma/Listar/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Turma>>(content);

                return resultado;
            }
        }

        public async Task<Turma> BuscarTurma(Turma turma) {
            using (HttpClient _turmaClient = new HttpClient()) {
                var response = await _turmaClient.GetAsync($"http://{Endereco}:{Porta}/CadastroTurma/BuscarPorId/{turma.Id}/{turma.Ano}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                Turma turmaLocal = JsonConvert.DeserializeObject<Turma>(content);

                return turmaLocal;
            }
        }

        public async Task<bool> AlterarTurma(Turma turma) {
            using (HttpClient _turmaClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", turma.Id.ToString()),
                    new KeyValuePair<string,string>("Ano", turma.Ano.ToString()),
                    new KeyValuePair<string,string>("IdCurso", turma.IdCurso.ToString())
                });

                var response = await _turmaClient.PutAsync($"http://{Endereco}:{Porta}/CadastroTurma/Alterar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
        
        public async Task<bool> ExcluirTurma(Turma turma) {
            using (HttpClient _turmaClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", turma.Id.ToString()),
                    new KeyValuePair<string,string>("Ano", turma.Ano.ToString())
                });

                var response = await _turmaClient.PostAsync($"http://{Endereco}:{Porta}/CadastroTurma/Excluir/", _Content);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
    }
}