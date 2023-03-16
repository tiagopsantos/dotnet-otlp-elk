using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class CallApi1AndThenApi2 : PageModel
    {
        //public IConfiguration _config { get; set; }
        public string APIUrl { get; set; }
        public CallApi1AndThenApi2(IConfiguration config)
        {
            //_config = config;
            APIUrl = config["Api1URL"];
        }
        public async Task OnGet()
        {
            for (int i = 0; i < 10; i++)
            {
                var httpClient = new HttpClient();
                var httpResponseMessage = await httpClient.GetAsync($"{APIUrl}/WeatherForecastIn2");
            }
        }
    }
}
