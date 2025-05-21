using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BuscaCep 
{

    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("digite o CEP do usuario: ");
            string Cep = Console.ReadLine() ?? "";

            using HttpClient client = new HttpClient();
            try
            {
                string url = $"https://viacep.com.br/ws/{Cep}/json/";

                var responsta = await client.GetStringAsync(url);

                var endereco = JsonConvert.DeserializeObject<Endereco>(responsta);

                if (endereco != null && endereco.Cep != null)
                {
                    Console.WriteLine($"\n📍 Resultado:");
                    Console.WriteLine($"Logradouro: {endereco.Logradouro}");
                    Console.WriteLine($"Bairro: {endereco.Bairro}");
                    Console.WriteLine($"Cidade: {endereco.Localidade}");
                    Console.WriteLine($"Estado: {endereco.Uf}");
                    Console.WriteLine($"DDD: {endereco.Ddd}");
                }
                else
                {
                    Console.WriteLine("❌ CEP inválido ou não encontrado.");
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro ao buscar o CEP: " + ex.Message);
            }

        }
    }

}
