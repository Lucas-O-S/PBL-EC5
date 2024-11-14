using Microsoft.VisualBasic;
using RestSharp;
using SitePBL.Models;
using System.Text.Json;

namespace SitePBL.DAO
{
    // A palavra-chave `async` permite definir métodos assíncronos que podem ser executados de forma não bloqueante.
    // Um método marcado com `async` pode usar a palavra-chave `await`, que libera o thread atual enquanto aguarda uma operação assíncrona.
    // Dessa forma, o programa pode continuar executando outras tarefas enquanto espera, melhorando a eficiência, especialmente em operações que dependem de E/S (como chamadas de rede ou acesso a arquivos).
    // Quando o tipo de retorno de um método `async` é `Task`, ele indica que o método retorna uma tarefa assíncrona (Task), que pode ser aguardada pelo chamador.
    // Se o método não precisar retornar nenhum valor, o tipo de retorno é `Task`; se precisar retornar um valor, o tipo de retorno será `Task<T>`, onde `T` é o tipo do valor retornado.


    /// <summary>
    /// Classe estatica Relacionada ao Fiware
    /// </summary>
    public static class HelperFiwareDAO
    {
        public static string host = "4.228.64.5";


        /// <summary>
        /// Metodo que faz o helth check do servidor
        /// </summary>
        /// <param name="host">ip do servidor</param>
        /// <returns>Valor booleano referente ao estado do server</returns>
        public static async Task<bool> VerificarServer(string host)
        {
            try
            {
                //Prepara a chamada http
                var options = new RestClientOptions($"http://{host}:4041")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
               
                //Faz a requisição
                var request = new RestRequest("/iot/about", Method.Get);

                //Resposta da requisição
                RestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /// <summary>
        /// Faz a leitura das N leituras anteriores
        /// </summary>
        /// <param name="host">ip do servidor</param>
        /// <param name="lamp">id da lampada, é uma strings </param>
        /// <param name="n">numeros de leituras requisitadas</param>
        /// <returns>lista das N ultimas leituras do sensor</returns>
        public static async Task<List<LeituraViewModel>> VerificarDados(string host, string lamp, int n)
        {
            ///Cria uma lista de leituras
            List<LeituraViewModel> leituras = new List<LeituraViewModel>();
            
            ///Prepara a chamada http
            var options = new RestClientOptions($"http://{host}:8666")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest($"/STH/v1/contextEntities/type/Lamp/id/urn:ngsi-ld:Lamp:{lamp}/attributes/temperature?lastN={n}", Method.Get);
            request.AddHeader("fiware-service", "smart");
            request.AddHeader("fiware-servicepath", "/");

            ///Recebe a resposta
            RestResponse response = await client.ExecuteAsync(request);
            
            //Verifica se foi bem sucedida
            if (response.IsSuccessStatusCode)
            {

                //Converte a resposta em uma variavel json
                JsonDocument doc = JsonDocument.Parse(response.Content);

                //Pega a lista dentro da propriedade contextResponses
                var contexto = doc.RootElement.GetProperty("contextResponses");

                //Verifica valores dentro da lista, só vai funcionar uma vez devido a ser uma lista de 1 elemento
                foreach (var contextResponse in contexto.EnumerateArray())
                {
                    // Acessando o objeto 'contextElement' dentro de cada 'contextResponse'
                    var contextElement = contextResponse.GetProperty("contextElement");

                    // Acessando o array 'attributes' dentro de 'contextElement'
                    if (contextElement.TryGetProperty("attributes", out var attributes))
                    {
                        // Iterando sobre o array 'attributes' para encontrar o atributo 'temperature'
                        foreach (var attribute in attributes.EnumerateArray())
                        {
                            // Verificando se o nome do atributo é 'temperature'
                            if (attribute.GetProperty("name").GetString() == "temperature")
                            {
                                // Acessando o array 'values' dentro do atributo 'temperature'
                                var values = attribute.GetProperty("values");

                                // Iterando sobre o array 'values' para extrair os dados de temperatura
                                foreach (var value in values.EnumerateArray())
                                {
                                    // Extraindo os dados do valor 
                                    DateTime recvTime = DateTime.Parse(value.GetProperty("recvTime").GetString());
                                    float attrValue = value.GetProperty("attrValue").GetSingle();

                                    //adiciona a lista as leitruas
                                    leituras.Add(new LeituraViewModel(attrValue, recvTime));
                                }
                            }
                        }
                    }
                }

            }
            return leituras;

        }

        /// <summary>
        /// Metodo que le o ultimo valor do sensor
        /// </summary>
        /// <param name="host"> ip do server </param>
        /// <param name="lamp"> id da lamp </param>
        /// <returns>Ultima leitura do sensor</returns>
        public static async Task<LeituraViewModel> Ler(string host, string lamp)
        {
            ///Cria uma struct da leitura
            LeituraViewModel leitura;

            ///Prepara a requisição
            var options = new RestClientOptions($"http://{host}:1026")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest($"/v2/entities/urn:ngsi-ld:Lamp:{lamp}/attrs/temperature", Method.Get);
            request.AddHeader("fiware-service", "smart");
            request.AddHeader("fiware-servicepath", "/");
            request.AddHeader("accept", "application/json");
            
            ///Recebe a resposta
            RestResponse response = await client.ExecuteAsync(request);
            
            ///Verifica se foi bem sucedida
            if (response.IsSuccessStatusCode)
            {

                //Trasforma em json a resposta
                JsonDocument doc = JsonDocument.Parse(response.Content);
                
                ///salva os valores do json
                float temp = doc.RootElement.GetProperty("value").GetSingle();
                
                string dataString = doc.RootElement.GetProperty("metadata").GetProperty("TimeInstant").GetProperty("value").ToString();
                DateTime data = DateTime.Parse(dataString);
                
                ///Salva dentro da variavel os valores
                leitura = new LeituraViewModel(temp, data);
                return leitura;
            }
            return leitura = new LeituraViewModel();
        }


        /// <summary>
        /// Cria uma lamp nova
        /// </summary>
        /// <param name="host">Ip do servidor</param>
        /// <param name="lamp">Id da lamp</param>
        /// <returns></returns>
        public static async Task CriarLamp(string host, string lamp)
        {

            ///requisições nescessarias para criar e preparar a lamp
            await Provisioning(host, lamp);
            await Registering(host, lamp);
            await Subscribe(host, lamp);

        }


        /// <summary>
        /// cria a lamp escolhida
        /// </summary>
        /// <param name="host">ip do servidor</param>
        /// <param name="lamp">id da lamp</param>
        /// <returns></returns>
        private static async Task Provisioning(string host, string lamp)
        {

            //Prepara a requisição http
            var options = new RestClientOptions($"http://{host}:4041")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/iot/devices", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("fiware-service", "smart");
            request.AddHeader("fiware-servicepath", "/");

            //cria o body do json
            var body = new
            {
                devices = new[]
                {
                    new
                    {
                        device_id = $"lamp{lamp}",
                        entity_name = $"urn:ngsi-ld:Lamp:{lamp}",
                        entity_type = "Lamp",
                        protocol = "PDI-IoTA-UltraLight",
                        transport = "MQTT",
                        commands = new[]
                        {
                            new { name = "on", type = "command" },
                            new { name = "off", type = "command" }
                        },
                        attributes = new[]
                        {
                            new { object_id = "s", name = "state", type = "Text" },
                            new { object_id = "t", name = "temperature", type = "float" }
                        }
                    }
                }
            };

            // Convertendo o objeto para JSON
            string jsonBody = JsonSerializer.Serialize(body);

            // Adicionando o corpo JSON na requisição
            request.AddStringBody(jsonBody, DataFormat.Json);

            // Enviando a requisição
            await client.ExecuteAsync(request);


        }


        /// <summary>
        /// Registra a lamp
        /// </summary>
        /// <param name="host">ip do servidor</param>
        /// <param name="lamp">id da lamp</param>
        /// <returns></returns>
        private static async Task Registering(string host, string lamp)
        {
            ///Prepara a requisição http
            var options = new RestClientOptions($"http://{host}:1026")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/v2/registrations", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("fiware-service", "smart");
            request.AddHeader("fiware-servicepath", "/");

            // Construindo o corpo JSON com objetos anônimos
            var body = new
            {
                description = "Lamp Commands",
                dataProvided = new
                {
                    entities = new[]
                    {
                new
                {
                    id = $"urn:ngsi-ld:Lamp:{lamp}",
                    type = "Lamp"
                }
            },
                    attrs = new[] { "on", "off" }
                },
                provider = new
                {
                    http = new
                    {
                        url = $"http://{host}:4041"
                    },
                    legacyForwarding = true
                }
            };

            // Convertendo o objeto para JSON
            string jsonBody = JsonSerializer.Serialize(body);

            // Adicionando o corpo JSON na requisição
            request.AddStringBody(jsonBody, DataFormat.Json);

            // Enviando a requisição
            await client.ExecuteAsync(request);


        }


        /// <summary>
        /// Inscreve a lamp ao sth comet
        /// </summary>
        /// <param name="host">ip do server</param>
        /// <param name="lamp">id da lamp</param>
        /// <returns></returns>
        private static async Task Subscribe(string host, string lamp)
        {
            ///Prepara a requisição http
            var options = new RestClientOptions($"http://{host}:1026")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/v2/subscriptions", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("fiware-service", "smart");
            request.AddHeader("fiware-servicepath", "/");

            // Criando o corpo JSON com objetos anônimos
            var body = new
            {
                description = "Notify STH-Comet of all Motion Sensor count changes",
                subject = new
                {
                    entities = new[]
                    {
                new
                {
                    id = $"urn:ngsi-ld:Lamp:{lamp}",
                    type = "Lamp"
                }
            },
                    condition = new
                    {
                        attrs = new[] { "temperature" }
                    }
                },
                notification = new
                {
                    http = new
                    {
                        url = $"http://{host}:8666/notify"
                    },
                    attrs = new[] { "temperature" },
                    attrsFormat = "legacy"
                }
            };

            // Convertendo o objeto para JSON
            string jsonBody = JsonSerializer.Serialize(body);

            // Adicionando o corpo JSON na requisição
            request.AddStringBody(jsonBody, DataFormat.Json);

            // Enviando a requisição
            await client.ExecuteAsync(request);

        }


    }
}
