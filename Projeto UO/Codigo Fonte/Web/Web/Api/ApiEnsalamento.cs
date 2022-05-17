using Backend.Componente;
using Backend.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Web.Api {
    public class ApiEnsalamento : Api {
        public async Task<bool> CadastrarEnsalamento(List<Ensalamento> ensalamentos) {
            using(HttpClient _ensalamentoClient = new HttpClient()) {
                var lista = JsonConvert.SerializeObject(ensalamentos, Formatting.Indented);
                var _Content = new StringContent(lista, Encoding.UTF8, "application/json");

                var response = await _ensalamentoClient.PostAsync($"http://{Endereco}:{Porta}/Ensalamento/Cadastrar/", _Content).ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
        }

        public async Task<object> ListarEnsalamentos(bool periodicidade = false) {
            using(HttpClient _ensalamentoClient = new HttpClient()) {
                var response = await _ensalamentoClient.GetStringAsync($"http://{Endereco}:{Porta}/Ensalamento/Listar/{periodicidade}").ConfigureAwait(false);
                List<Ensalamento> ensalamentos;
                List<List<bool>> periodicidades;
                if(periodicidade) {
                    //Listar Periodicidades
                    periodicidades = new List<List<bool>>();
                    periodicidades = JsonConvert.DeserializeObject<List<List<bool>>>(response);
                    return periodicidades;
                }
                else {
                    //Listar Ensalamentos
                    ensalamentos = new List<Ensalamento>();
                    ensalamentos = JsonConvert.DeserializeObject<List<Ensalamento>>(response);
                    return ensalamentos;
                }
            }
        }

		/// <summary>
		/// Busca todas as datas (índices) de um ensalamento.
		/// <para>Serve também para buscar Ensalamentos por Id. Basta enviar um Ensalamento com a Id que quiser buscar.</para>
		/// </summary>
		/// <param name="ensalamento"></param>
		/// <returns>Retorna uma Lista de Ensalamentos, de acordo com o parâmetro (GET)</returns>
		public async Task<List<Ensalamento>> BuscarEnsalamentoDesmembrado(Ensalamento ensalamento) {
            using(HttpClient _ensalamentoClient = new HttpClient()) {
                var lista = JsonConvert.SerializeObject(ensalamento, Formatting.Indented);
                var _Content = new StringContent(lista, Encoding.UTF8, "application/json");

                var response = await _ensalamentoClient.PostAsync($"http://{Endereco}:{Porta}/Ensalamento/BuscarEnsalamentoDesmembrado/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Ensalamento>>(content);
                return resultado;
            }
        }
		
        public async Task<List<Ensalamento>> AtribuirIncludes(List<Ensalamento> ensalamentos, Ensalamento ensalamento = null) {
            using(HttpClient _ensalamentoClient = new HttpClient()) {
                StringContent _Content;
                string objeto;
                if(ensalamento != null) {
                    ensalamentos = new List<Ensalamento>();
                    ensalamentos.Add(ensalamento);
                }
                objeto = JsonConvert.SerializeObject(ensalamentos, Formatting.Indented);
                _Content = new StringContent(objeto, Encoding.UTF8, "application/json");
                var response = await _ensalamentoClient.PostAsync($"http://{Endereco}:{Porta}/Ensalamento/AtribuirIncludes/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Ensalamento>>(content);
                return resultado;
            }
        }

        public async Task<List<Ensalamento>> VerificarEnsalamento(List<Ensalamento> ensalamentos) {
            using(HttpClient _ensalamentoClient = new HttpClient()) {
                var lista = JsonConvert.SerializeObject(ensalamentos, Formatting.Indented);
                var _Content = new StringContent(lista, Encoding.UTF8, "application/json");

                var response = await _ensalamentoClient.PostAsync($"http://{Endereco}:{Porta}/Ensalamento/Verificar/", _Content).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Ensalamento>>(content);
                return resultado;
            }
        }

        public async Task<bool> ExcluirEnsalamento(int id) {
            using(HttpClient _ensalamentoClient = new HttpClient()) {
                var response = await _ensalamentoClient.DeleteAsync($"http://{Endereco}:{Porta}/Ensalamento/Excluir/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<bool>(content);

                return resultado;
            }
        }

		/// <summary>
		/// Caso queira o mapa de hoje, passar DateTime.Today ou DateTime.MinValue como parâmetro
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
        public async Task<List<EnsalamentoPeriodo>> GetAllEnsalementoDia(DateTime data) {
            using(HttpClient _ChamadoClient = new HttpClient()) {
				if(data.Equals(DateTime.MinValue))
					data = DateTime.Today;

				string ensalamento = JsonConvert.SerializeObject(new Ensalamento { Data = data }, Formatting.Indented);
				StringContent _Content = new StringContent(ensalamento, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _ChamadoClient.PostAsync($"http://{Endereco}:{Porta}/Ensalamento/GetAllEnsalamentoDia", _Content).ConfigureAwait(false);
				String content = await response.Content.ReadAsStringAsync();
				List<EnsalamentoPeriodo> resultado = JsonConvert.DeserializeObject<List<EnsalamentoPeriodo>>(content);

                return resultado;
            }
        }

    }
}