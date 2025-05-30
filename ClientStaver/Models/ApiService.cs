using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace ClientStaver.Models
{
    public class ApiService
    {

        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetLoginTokenAsync(LoginRequest login)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", login);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadFromJsonAsync<LoginResponse>();
                Token.Value = token?.Token;
            }
            return response;
        }

        public async Task<List<ComputerStat>> GetComputerStatsAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.Value);

            var response = await _httpClient.GetAsync("api/ComputerStat");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ComputerStat>>();
            }
            return new List<ComputerStat>();
        }

        public async Task<List<BitcoinStat>> GetBitcoinStatsAsync()
        {
            var response = await _httpClient.GetAsync("api/BitcoinStat");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<BitcoinStat>>();
            }
            return new List<BitcoinStat>();
        }

        public async Task<List<TempHumSensor>> GetTempHumSensorsAsync()
        {
            var response = await _httpClient.GetAsync("api/TempHumSensor");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TempHumSensor>>();
            }
            return new List<TempHumSensor>();
        }

        public async Task<List<TempHumSensor>> GetTempHumSensorsAsync(string location)
        {
            var response = await _httpClient.GetAsync($"api/TempHumSensor/location/{location}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TempHumSensor>>();
            }
            return new List<TempHumSensor>();
        }

        public async Task<TempHumSensor> GetTempHumSensorAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/TempHumSensor/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TempHumSensor>();
            }
            return null;
        }

        public async Task<HttpResponseMessage> PostTempHumSensorAsync(TempHumSensor data)
        {
            var response = await _httpClient.PostAsJsonAsync("api/TempHumSensor", data);

            return response;
        }

        public async Task<HttpResponseMessage> PutTempHumSensorAsync(TempHumSensor data)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/TempHumSensor/{data.Id}", data);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteTempHumSensorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/TempHumSensor/{id}");

            return response;
        }
    }
}
