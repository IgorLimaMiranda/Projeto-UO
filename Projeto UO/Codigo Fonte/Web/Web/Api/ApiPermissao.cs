using Backend.Model.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Api {
    public class ApiPermissao : Api {
        public async Task<List<Permissao>> ListarPermissao(String tipoUsuario) {
            using (HttpClient _menuClient = new HttpClient()) {
                var response = await _menuClient.GetStringAsync($"http://{Endereco}:{Porta}/Permissao/Listar/{tipoUsuario}").ConfigureAwait(false);
                var lista = JsonConvert.DeserializeObject<List<Permissao>>(response);
                return lista;
            }
        }
    }
}