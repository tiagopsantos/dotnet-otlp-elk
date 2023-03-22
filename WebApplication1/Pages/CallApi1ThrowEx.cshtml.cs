using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Pages
{
    public class CallApi1ThrowEx : PageModel
    {
        private readonly ILogger<CallApi1ThrowEx> _logger;
        //public IConfiguration _config { get; set; }
        public string APIUrl { get; set; }
        public CallApi1ThrowEx(ILogger<CallApi1ThrowEx> logger, IConfiguration config)
        {
            //_config = config;
            APIUrl = config["Api1URL"];
            _logger = logger;
        }
        public async Task OnGet()
        {
            _logger.LogInformation("Get CallApi1ThrowEx ");
            //for (int i = 0; i < 10; i++)
            {
                var httpClient = new HttpClient();
                var httpResponseMessage = await httpClient.GetAsync($"{APIUrl}/ThrowEx");
            }
        }
    }
}
