using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MongoDb_POC.Dominio;
using MongoDB.Driver;


namespace MongoDb_POC
{
    class Program
    {
        static void Main(string[] args)
        {
            int qtdLinhas = 0;

            Console.WriteLine("Digite a Qtd de linhas a serem inserida no MongoDb");
            qtdLinhas = int.Parse(Console.ReadLine());

            var ListaDeLogs = CriarLogAssinaturas(qtdLinhas);

            Console.WriteLine("Inicio do processamento...");

            CriarLogMongoDb(ListaDeLogs);

            //ListaDeLogs.AsParallel().ForAll(CriarLogMongoDb);

            Console.WriteLine("Final do processamento.");

            Console.ReadKey();

        }


        public static IEnumerable<LogAssinatura> CriarLogAssinaturas(int qtdLinhas)
        {
            IList<LogAssinatura> listaLogAssinaturas = new List<LogAssinatura>();

            for (int i = 0; i < qtdLinhas; i++)
            {
                LogAssinatura logAssinatura = new LogAssinatura
                {
                    Detalhe = "fasfjahfakshfkalshflaçkshflaçkshfçlakshflaksfhlaçkshflçakshfçlaksflakshflkashflçkashflçkashflçkashflçkahsf" + Guid.NewGuid(),
                    Data = DateTime.Now,
                    DataAssinatura = DateTime.Now,
                    Evento = EventoEnum.Informacao,
                    IdAssinatura = 12345678912,
                    IdCanal = 67,
                    IdUsuario = 1212,
                };

                listaLogAssinaturas.Add(logAssinatura);
            }

            return listaLogAssinaturas;
        }


        public static void CriarLogMongoDb(IEnumerable<LogAssinatura> LogAssinatura)
        {

            var cliente = new MongoClient("mongodb://localhost:27017");

            var dataBase = cliente.GetDatabase("local");

            var colecao = dataBase.GetCollection<LogAssinatura>("LogAssinatura");
            
            foreach (var item in LogAssinatura)
            {
                colecao.InsertOneAsync(item);
            }



        }
        
    }
}
