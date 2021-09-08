using Newtonsoft.Json;

namespace TMG_Programmers_test_2021.Model
{
    public class Response
    {
        [JsonProperty("text")]
        public string Message { get; set; }
        public string MessageError { get; set; }
        public bool IsError { get; set; }
    }
}
