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
            while (true)
            {
                         
                int qtdLinhas = 0;

                Console.WriteLine("\nDigite a Qtd de linhas a serem inserida no MongoDb");
                qtdLinhas = int.Parse(Console.ReadLine());

                var ListaDeLogs = CriarLogAssinaturas(qtdLinhas);

                var dataInicio = DateTime.Now;
                Console.WriteLine(string.Format("Inicio do processamento. {0}", dataInicio ));
                                               
                CriarLogMongoDb(ListaDeLogs);

                var dataFim = DateTime.Now;
                Console.WriteLine(string.Format("Final do processamento. {0}", dataFim));

                Console.Write("Tempo de processamento:{0}", (dataFim - dataInicio));

                Console.ReadKey();
            }

        }


        public static IEnumerable<LogAssinatura> CriarLogAssinaturas(int qtdLinhas)
        {
            IList<LogAssinatura> listaLogAssinaturas = new List<LogAssinatura>();

            for (int i = 0; i < qtdLinhas; i++)
            {
                LogAssinatura logAssinatura = new LogAssinatura
                {
                    Detalhe = "fasfjahfakshfkalshflaçkshflaçkshfçlakshflaksfhlaçkshflçakshfçlaksflakshflkashflçkashflçkashflçkashflçkahsf",
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

            LogAssinatura.AsParallel().ForAll(itemassinatura => {
                colecao.InsertOne(itemassinatura);
            });
                  
        }
        
    }
}
