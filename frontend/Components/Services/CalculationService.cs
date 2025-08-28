using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace frontend.Services
{
    public class CalculationService : ICalculationService
    {
        static HttpClient client = new HttpClient();

        public void Initialize()
        {
            // Update port # in the following line.
            var baseUri = Environment.GetEnvironmentVariable("CALC_API_BASE_URI");
            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<int> Calculate(CalculationModel calculation)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "calculate", calculation);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<int>();
        }

        private class Calculation
        {
            public string FirstNumber { get; set; }
            public string SecondNumber { get; set; }
            public string Operation { get; set; }
        }
    }
}