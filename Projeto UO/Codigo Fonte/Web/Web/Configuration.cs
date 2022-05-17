namespace Web {
    public static class Configuration {

        public static string Dashboard => $"{SistemaWeb}/Dashboard";
        public static string Login => $"{SistemaWeb}/Login/Index";
        public static string Ocorrencia => $"{SistemaWeb}/Ocorrencia";
    
        public static string ProtocoloHTTP => "http://";
        public static string ProtocoloHTTPS => "https://";

		//Início das propriedades de uso em Laboratório
		public static string EnderecoWeb => "localhost";
        public static string PortaWeb => "6936";

        public static string EnderecoWebServer => "localhost";
       // public static string EnderecoWebServer => "192.168.22.182";
		public static string PortaWebServer => "54565";
		//Fim das propriedades de uso em Laboratório

		////Inicio das propriedades de uso no Servidor de Desenvolvimento
		//public static string EnderecoWeb => "192.168.22.4";
		//public static string PortaWeb => "82";

		//public static string EnderecoWebServer => "192.168.22.4";
		//public static string PortaWebServer => "8094";
		////Fim das propriedades de uso no Servidor de Desenvolvimento

		public static string CaminhoImagem => "Imagens/Profile"; //Nunca comentar este, pois é o mesmo caminho em qualquer WS

        public static string SistemaWeb => $"{ProtocoloHTTP}{EnderecoWeb}:{PortaWeb}";
        public static string SistemaWebServer => $"{ProtocoloHTTP}{EnderecoWebServer}:{PortaWebServer}";

        public static string VersaoWeb => "1.0.6";

    }
}