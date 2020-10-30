using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HaveIBeenPwnedAPI.Services
{
    public class HaveIBeenPwnedService
    {
        static readonly HttpClient client = new HttpClient();
        private IConfiguration config;

        public HaveIBeenPwnedService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<string> GetBreachedHashes(string hashedPassword)
        {
            string firstFiveChars = hashedPassword.Substring(0, 5);

            HttpResponseMessage response = await client.GetAsync($"{config.GetSection("HaveIBeenPwnedAPIURL").Value}/{firstFiveChars}");
            string responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
