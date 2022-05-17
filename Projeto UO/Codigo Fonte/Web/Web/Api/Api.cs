namespace Web.Api {
    public abstract class Api {
        public string Endereco { get { return Configuration.EnderecoWebServer; } }
        public string Porta { get { return Configuration.PortaWebServer; } }
        public string CaminhoImagem { get { return Configuration.CaminhoImagem; } }
    }
}