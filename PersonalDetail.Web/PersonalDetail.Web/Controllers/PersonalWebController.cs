using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalDetail.Web.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PersonalDetail.Web.Controllers
{
    public class PersonalWebController : Controller
    {
        private readonly ILogger<PersonalWebController> _logger;

        private readonly HttpClient _client;


        public PersonalWebController(ILogger<PersonalWebController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new Models.PersonalDetail();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(Models.PersonalDetail model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("http://localhost:38787/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP POST
                var postTask = client.PostAsJsonAsync("api/personaldetail/save", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return View(Constant.ThankYouView);
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(model);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
