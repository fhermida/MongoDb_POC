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

                Console.WriteLine("\nDigite a Qtd de linhas a serem inseridas no MongoDb");
                qtdLinhas = int.Parse(Console.ReadLine());

                var ListaDeLogs = CriarLogAssinaturas(qtdLinhas);

                var dataInicio = DateTime.Now;
                Console.WriteLine("Inicio do processamento. {0}", dataInicio);

                //CriarLogMongoDb(ListaDeLogs);
                CriarLogMongoDbOneAsync(ListaDeLogs);

                var dataFim = DateTime.Now;
                Console.WriteLine("Final do processamento. {0}", dataFim);

                Console.WriteLine("Tempo de processamento:{0}", (dataFim - dataInicio));

                Console.ReadKey();

                Console.WriteLine("Deseja Exibir os registros da coleção? s/n");

                string exibeColecao = Console.ReadLine();

                if (exibeColecao == "s")
                {
                    var dataIni = DateTime.Now;
                    Console.WriteLine("Inicio do processamento. {0}", dataIni);

                    ListarColecao();

                    var dataFi = DateTime.Now;
                    Console.WriteLine("Final do processamento. {0}", dataFi);

                    Console.WriteLine("Tempo de processamento:{0}", (dataFi - dataIni));
                }

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
                    IdCanal = 75,
                    IdUsuario = 1212,
                };

                listaLogAssinaturas.Add(logAssinatura);
            }

            return listaLogAssinaturas;
        }
       
       public static IMongoDatabase CriarConexaoMongo()
        {
            var cliente = new MongoClient("mongodb://localhost:27017");

            return cliente.GetDatabase("local");
        }

        public static void CriarLogMongoDb(IEnumerable<LogAssinatura> LogAssinatura)
        {  
            var dataBase = CriarConexaoMongo();

            var colecao = dataBase.GetCollection<LogAssinatura>("LogAssinatura");

            LogAssinatura.AsParallel().ForAll(itemassinatura =>
            {
                colecao.InsertOne(itemassinatura);
            });
                                                           
                  
        }

        public static void CriarLogMongoDbOneAsync(IEnumerable<LogAssinatura> LogAssinatura)
        {
            var dataBase = CriarConexaoMongo();

            var colecao = dataBase.GetCollection<LogAssinatura>("LogAssinatura");

            LogAssinatura.AsParallel().ForAll( async itemassinatura =>
            {
              await colecao.InsertOneAsync(itemassinatura);
            });
        }

        public static void ListarColecao()
        {
            var dataBase = CriarConexaoMongo();
            var filter = Builders<LogAssinatura>.Filter.Exists(p => p.IdCanal);
            var colecao = dataBase.GetCollection<LogAssinatura>("LogAssinatura").Find(filter).ToList();

            int i = 0;
            foreach (var item in colecao)
            {
                Console.Write(string.Concat(item._id, "\n"));
                i++;
            }

            Console.WriteLine("Total de Linhas Exibidas: {0}", i);
        }
        
    }
}
