using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb_POC.Dominio
{
    public abstract class LogBase
    {
        #region Atributos

        
        private long id;
        private int idEntidade;
        private TipoEnum tipo;
        private DateTime data;
        private string detalhe;
        private EventoEnum evento;
        private string telefone;
        private long? tempoProcessamento;

        #endregion

        #region Construtores

        protected LogBase()
        {
            this.id                 = 0L;
            this.idEntidade         = 0;
            this.tipo               = TipoEnum.Indefinido;
            this.data               = DateTime.Now;
            this.detalhe            = string.Empty;
            this.evento             = EventoEnum.Informacao;
            this.telefone           = string.Empty;
            this.tempoProcessamento = null;
        }

        #endregion

        #region Propriedades

        [BsonId]
        public ObjectId _id { get; set; }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdEntidade
        {
            get { return idEntidade; }
            set { idEntidade = value; }
        }

        public TipoEnum Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public string Detalhe
        {
            get { return detalhe; }
            set { detalhe = value; }
        }

        public EventoEnum Evento
        {
            get { return evento; }
            set { evento = value; }
        }

        public string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        public long? TempoProcessamento
        {
            get { return tempoProcessamento; }
            set { tempoProcessamento = value; }
        }

        #endregion
    }
}
