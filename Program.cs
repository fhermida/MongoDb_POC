using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using MongoDb_POC.Dominio;
using MongoDB.Driver;
using MongoDB.Driver.Builders;


namespace MongoDb_POC
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                
                Console.WriteLine("\nDigite a Qtd de linhas a serem inseridas no MongoDb");
                var qtdLinhas = int.Parse(Console.ReadLine());

                Console.WriteLine("\nDigite o Id do canal");
                var idCanal = int.Parse(Console.ReadLine());

                var ListaDeLogs = CriarLogAssinaturas(qtdLinhas, idCanal);

                Console.WriteLine("\nDigite o tipo de execução: a=Async/s=sync/sl=sync lote/al=async lote");
                var tipoExecucao = Console.ReadLine();

                var dataInicio = DateTime.Now;
                Console.WriteLine("Inicio do processamento. {0}", dataInicio);

                switch (tipoExecucao.ToLower())
                {
                    case "s":
                        CriarLogMongoDb(ListaDeLogs);
                        break;

                    case "a":
                        CriarLogMongoDbOneAsync(ListaDeLogs);
                        break;

                    case "sl":
                        CriarLogMongoDbLote(ListaDeLogs);
                        break;

                    case "al":
                        CriarLogMongoDbOneAsyncLote(ListaDeLogs);
                        break;

                    default:
                    {
                        Console.WriteLine("\nOpção Inválida! ");
                        continue;
                    }
                }

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


        public static IEnumerable<LogAssinatura> CriarLogAssinaturas(int qtdLinhas, int idCanal)
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
                    IdCanal = idCanal,
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


        public static void CriarLogMongoDbLote(IEnumerable<LogAssinatura> LogAssinatura)
        {
            var dataBase = CriarConexaoMongo();

            var colecao = dataBase.GetCollection<LogAssinatura>("LogAssinatura");

             colecao.InsertMany(LogAssinatura);
            
        }

        public static async void CriarLogMongoDbOneAsync(IEnumerable<LogAssinatura> LogAssinatura)
        {
            var dataBase = CriarConexaoMongo();

            var colecao = dataBase.GetCollection<LogAssinatura>("LogAssinatura");

            foreach (var item in LogAssinatura)
            {
              await colecao.InsertOneAsync(item);
            }
        }

        public static async void CriarLogMongoDbOneAsyncLote(IEnumerable<LogAssinatura> LogAssinatura)
        {
            var dataBase = CriarConexaoMongo();

            var colecao = dataBase.GetCollection<LogAssinatura>("LogAssinatura");

            await colecao.InsertManyAsync(LogAssinatura);
        }

        public static void ListarColecao()
        {
            var dataBase = CriarConexaoMongo();
            var query = Builders<LogAssinatura>.Filter.Exists(p => p.IdCanal);
            var colecao = dataBase.GetCollection<LogAssinatura>("LogAssinatura").Find(query);

            int i = 0;
            foreach (var item in colecao.ToList())
            {
                Console.Write(string.Concat(item._id, "\n"));
                i++;
            }

            Console.WriteLine("Total de Linhas Exibidas: {0}", i);
        }
        
    }
}
