using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Web.Api;

namespace Web.Api {
    public class ApiImagem : Api {
        public async void CadastrarImagem(Stream _photoStream, String nomeArquivo) {
            using (var client = new HttpClient()) {

                using (var content = new MultipartFormDataContent()) {


                    if (_photoStream != null) {
                        var fileContent = new StreamContent(_photoStream);
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                            FileName = nomeArquivo
                        };
                        var requestUri = $"http://{Endereco}:{Porta}/CadastroImagem/Cadastrar/";
                        content.Add(fileContent, requestUri);

                        var result = client.PostAsync(requestUri, content).Result;
                        _photoStream.Dispose();
                    }
                }

            }
        }
    }
}
