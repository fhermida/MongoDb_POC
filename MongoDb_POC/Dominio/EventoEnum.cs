using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb_POC.Dominio
{
    public enum EventoEnum
    {
        Informacao = 0,
        Alerta = 1,
        Erro = 2,
        Excecao = 3,
        Depuracao = 4
    }
}
