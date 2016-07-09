using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb_POC.Dominio
{
 
    public class LogAssinatura : LogBase
    {
        private long? idUsuario;
        private int idCanal;
        private long? idAssinatura;
        private DateTime? dataAssinatura;

        public LogAssinatura() : base()
        {
            this.idUsuario      = null;
            this.idCanal        = 0;
            this.idAssinatura   = null;
            this.dataAssinatura = null;
        }

        public long? IdUsuario 
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        public int IdCanal
        {
            get { return idCanal; }
            set { idCanal = value; }
        }

        public long? IdAssinatura
        {
            get { return idAssinatura; }
            set { idAssinatura = value; }
        }

        public DateTime? DataAssinatura
        {
            get { return dataAssinatura; }
            set { dataAssinatura = value; }
        }
    }
}
