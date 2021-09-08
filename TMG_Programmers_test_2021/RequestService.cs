using Newtonsoft.Json;
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
        private static readonly string authorizationKey = "0J/RgNC40LLQtdGC0LjQutC4IQ==";/*0J/RgNC40LLQtdGC0LjQutC4IQ==*/

        public static async Task<Response> HttpGetRequest(string id)
        {
            var response = new Response();
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("TMG-Api-Key", authorizationKey);
                HttpResponseMessage responseMessage = await client.GetAsync($"{serverApiRoot}{address}{id}");
                responseMessage.EnsureSuccessStatusCode();
                var message = await responseMessage.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Response>(message);
                response.IsError = false;
            }
            catch (HttpRequestException e)
            {
                response.MessageError = e.Message;
                response.IsError = true;
            }
            return response;
        }
    }
}
