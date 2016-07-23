
namespace MongoDb_POC.Dominio
{
    public class LogComunicacao : LogBase
    {
        #region Atributos

        private string url;
        private string verbo;
        private string corpoRequisicao;
        private int respostaHttp;
        private string corpoResposta;
        private int? idOperadora;

        #endregion

        #region Construtores

        public LogComunicacao() : base()
        {
            this.url             = string.Empty;
            this.verbo           = string.Empty;
            this.corpoRequisicao = string.Empty;
            this.respostaHttp    = 0;
            this.corpoResposta   = string.Empty;
            this.idOperadora     = null;
        }

        #endregion

        #region Propriedades

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public string Verbo
        {
            get { return verbo; }
            set { verbo = value; }
        }

        public string CorpoRequisicao
        {
            get { return corpoRequisicao; }
            set { corpoRequisicao = value; }
        }

        public int RespostaHttp
        {
            get { return respostaHttp; }
            set { respostaHttp = value; }
        }

        public string CorpoResposta
        {
            get { return corpoResposta; }
            set { corpoResposta = value; }
        }

        public int? IdOperadora
        {
            get { return idOperadora; }
            set { idOperadora = value; }
        }

        #endregion
    }
}
