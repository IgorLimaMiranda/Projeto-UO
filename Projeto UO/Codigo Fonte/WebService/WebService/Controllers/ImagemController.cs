using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace WebService.Controllers
{
    [RoutePrefix("CadastroImagem")]
    public class ImageController : ApiController {

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public async Task<HttpResponseMessage> CadastrarImagem() {
            string Imagem = string.Empty;
            //string Imagem = Guid.NewGuid().ToString("N") + ".jpg";


            var result = new HttpResponseMessage(HttpStatusCode.OK);
            if(Request.Content.IsMimeMultipartContent()) {
                await Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider()).ContinueWith((task) => {
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach(HttpContent content in provider.Contents) {
                        var fileName = content.Headers.ContentDisposition.FileName;
                        if(string.IsNullOrWhiteSpace(fileName)) {
                            continue;
                        }

                        Stream stream = content.ReadAsStreamAsync().Result;
                        Image image = Image.FromStream(stream);
                        var testName = content.Headers.ContentDisposition.Name;
                        string filePath = HostingEnvironment.MapPath("~/Imagens/Profile/");						
						string nomeArquivo = $"{((content.Headers.ContentDisposition.FileName).Substring(0, 14)).Replace(".", "").Replace("-", "")}{(content.Headers.ContentDisposition.FileName).Substring(14)}";
						string fullPath = Path.Combine(filePath, nomeArquivo);
                        image.Save(fullPath);
                    }
                });

                result.Content = new StringContent(Imagem);

                return result;
            }
            else {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }

        [AcceptVerbs("POST")]
        [Route("CadastrarOcorrencia")]
        public async Task<HttpResponseMessage> CadastrarImagemOcorrencia() {
            string Imagem = string.Empty;
            //string Imagem = Guid.NewGuid().ToString("N") + ".jpg";


            var result = new HttpResponseMessage(HttpStatusCode.OK);
            if (Request.Content.IsMimeMultipartContent()) {
                await Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider()).ContinueWith((task) => {
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach (HttpContent content in provider.Contents) {
                        var fileName = content.Headers.ContentDisposition.FileName;
                        if (string.IsNullOrWhiteSpace(fileName)) {
                            continue;
                        }

                        Stream stream = content.ReadAsStreamAsync().Result;
                        Image image = Image.FromStream(stream);
                        var testName = content.Headers.ContentDisposition.Name;
                        string filePath = HostingEnvironment.MapPath("~/Imagens/Ocorrencia/");
                        string nomeArquivo = $"{(content.Headers.ContentDisposition.FileName)}";
                        string fullPath = Path.Combine(filePath, nomeArquivo);
                        image.Save(fullPath);
                    }
                });

                result.Content = new StringContent(Imagem);

                return result;
            }
            else {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }


		[AcceptVerbs("POST")]
		[Route("CadastrarPerfil")]
		public async Task<HttpResponseMessage> CadastrarImagemPerfil() {
			string Imagem = string.Empty;
			//string Imagem = Guid.NewGuid().ToString("N") + ".jpg";


			var result = new HttpResponseMessage(HttpStatusCode.OK);
			if(Request.Content.IsMimeMultipartContent()) {
				await Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider()).ContinueWith((task) => {
					MultipartMemoryStreamProvider provider = task.Result;
					foreach(HttpContent content in provider.Contents) {
						var fileName = content.Headers.ContentDisposition.FileName;
						if(string.IsNullOrWhiteSpace(fileName)) {
							continue;
						}

						Stream stream = content.ReadAsStreamAsync().Result;
						Image image = Image.FromStream(stream);
						var testName = content.Headers.ContentDisposition.Name;
						string filePath = HostingEnvironment.MapPath("~/Imagens/Profile/");
						string nomeArquivo = $"{(content.Headers.ContentDisposition.FileName)}";
						string fullPath = Path.Combine(filePath, nomeArquivo);
						image.Save(fullPath);
					}
				});

				result.Content = new StringContent(Imagem);

				return result;
			}
			else {
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
			}
		}

	}
}
