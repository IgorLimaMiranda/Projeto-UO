using Backend.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Api {
    public class ApiCurso : Api {
        public async Task<bool> CadastrarCurso(Curso curso) {
            using (HttpClient _cursoClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", curso.Id.ToString()),
                    new KeyValuePair<string,string>("Descricao", curso.Descricao),
                    new KeyValuePair<string,string>("Sigla", curso.Sigla)
                });

                var response = await _cursoClient.PostAsync($"http://{Endereco}:{Porta}/CadastroCurso/Cadastrar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
        
        public async Task<ObservableCollection<Curso>> ListarCursos() {
            using (HttpClient _cursoClient = new HttpClient()) {
                var response = await _cursoClient.GetAsync($"http://{Endereco}:{Porta}/CadastroCurso/Listar/").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ObservableCollection<Curso>>(content);

                return resultado;
            }
        }
        
        public async Task<Curso> BuscarCurso(int id) {
            using (HttpClient _cursoClient = new HttpClient()) {
                var response = await _cursoClient.GetAsync($"http://{Endereco}:{Porta}/CadastroCurso/BuscarPorId/{id}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                Curso curso = JsonConvert.DeserializeObject<Curso>(content);

                return curso;
            }
        }
        
        public async Task<bool> AlterarCurso(Curso curso) {
            using (HttpClient _cursoClient = new HttpClient()) {
                var _Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id", curso.Id.ToString()),
                    new KeyValuePair<string,string>("Descricao", curso.Descricao),
                    new KeyValuePair<string,string>("Sigla", curso.Sigla)
                });

                var response = await _cursoClient.PutAsync($"http://{Endereco}:{Porta}/CadastroCurso/Alterar", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
        
        public async Task<bool> ExcluirCurso(int id) {
            using (HttpClient _cursoClient = new HttpClient()) {
                var response = await _cursoClient.DeleteAsync($"http://{Endereco}:{Porta}/CadastroCurso/Excluir/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }
    }
}