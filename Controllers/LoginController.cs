using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HaveIBeenPwnedAPI.Models;
using HaveIBeenPwnedAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HaveIBeenPwnedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration config;

        public LoginController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost]
        [EnableCors("LoginPolicy")]
        public async Task<ActionResult> Login([FromBody] User user)
        {
            string hashed = new HashingService().SHA1UTF8Hash(user.password);
            string suffix = hashed.Substring(5);

            HaveIBeenPwnedService service = new HaveIBeenPwnedService(config);
            string breachedHashes = await service.GetBreachedHashes(hashed);

            Regex reg = new Regex($@"{suffix}:([0-9]*)", RegexOptions.Singleline);

            bool existing = reg.IsMatch(breachedHashes);
            int occurances = Int32.Parse(reg.Matches(breachedHashes)[0].Groups[1].Value);

            return Ok(new { breachedPassword = existing, numberOfBreaches =  occurances });
        }
    }
}
