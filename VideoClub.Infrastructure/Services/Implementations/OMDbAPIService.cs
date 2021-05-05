using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using VideoClub.Infrastructure.Services.Interfaces;

namespace VideoClub.Infrastructure.Services.Implementations
{
    public class OMDbAPIService : IOMDbAPIService
    {
        private readonly ILoggingService _logger;
        public OMDbAPIService(ILoggingService logger)
        {
            _logger = logger;
        }

        public async Task<string> GetMovieArtworkByTitle(string q)
        {
            string BASE_URL = "http://www.omdbapi.com/?";
            string API_KEY = GetAPIKey();
            try
            {
                string URL = BASE_URL + "apikey=" + API_KEY + "&t=" + q;
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(URL),
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    JObject ResultsInJSON = (JObject)JsonConvert.DeserializeObject(body);

                    return (string)ResultsInJSON["Poster"];
                }
            }
            catch (Exception e)
            {
                _logger.Writer.Error(e, "Could not fetch API.");
            }
            return null;
        }

        public string GetAPIKey()
        {
            try
            {
                using (StreamReader file = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "OMDbAPIKEY.json"))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject JSONObject = (JObject)JToken.ReadFrom(reader);
                    return (string)JSONObject["API_KEY"];
                }
            }
            catch (Exception e)
            {
                _logger.Writer.Error(e, "Could not fetch API KEY from file.");
                return null;
            }
        }
    }
}
