namespace ClientStaver.Models
{
    public class ApiService
    {

        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ComputerStat>> GetComputerStatsAsync()
        {
            var response = await _httpClient.GetAsync("http://apistaver:8070/api/ComputerStat");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ComputerStat>>();
            }
            return new List<ComputerStat>();
        }

        public async Task<List<BitcoinStat>> GetBitcoinStatsAsync()
        {
            var response = await _httpClient.GetAsync("http://apistaver:8070/api/BitcoinStat");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<BitcoinStat>>();
            }
            return new List<BitcoinStat>();
        }

    }
}
