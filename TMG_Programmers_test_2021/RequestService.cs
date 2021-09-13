using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TMG_Programmers_test_2021.Model;

namespace TMG_Programmers_test_2021
{
    public class RequestService
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string serverApiRoot = "http://tmgwebtest.azurewebsites.net";
        private static readonly string address = "/api/textstrings/";
        private static readonly string authorizationKey = "0J/RgNC40LLQtdGC0LjQutC4IQ==";

        public static async Task<Response> HttpGetRequestAsync(string id)
        {
            if (id is null)
            {
                return null;
            }
            var response = new Response();
            try
            {
                //adding headers for authorization / добавление заголовков для авторизации 
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("TMG-Api-Key", authorizationKey);

                //send a request to the server / посылаем запрос на сервер 
                HttpResponseMessage responseMessage = await client.GetAsync($"{serverApiRoot}{address}{id}");
                responseMessage.EnsureSuccessStatusCode();
                var message = await responseMessage.Content.ReadAsStringAsync();

                //Deserialize the Json file received from the server / Десериализуем полученый от сервера Json фаил
                response = JsonConvert.DeserializeObject<Response>(message);
                response.IsError = false;
            }
            catch (Exception e)
            {

                response.MessageError = e.Message;
                response.IsError = true;
            }
            return response;
        }
    }
}
