using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using MongoDB.Bson;

namespace MongoDb_POC.Dominio
{
    public abstract class LogBase
    {                       
          
        private string detalhe;
        private DateTime data;
        private EventoEnum evento;

        public LogBase()
        {
            this.data       = DateTime.Now;
            this.detalhe    = string.Empty;
            this.evento     = EventoEnum.Informacao;
        }

        public ObjectId _id { get; set; }
                 
        public string Detalhe
        {
            get { return detalhe; }
            set { detalhe = value; }
        }

        public DateTime Data
        {
            get { return data; } 
            set { data = value; }
        }

        public EventoEnum Evento
        {
            get { return evento; }
            set { evento = value; }
        }
        
    }
}
