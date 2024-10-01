using Microsoft.AspNetCore.Mvc;
using MouseTracking.Models;

namespace MouseTracking.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("save")]
        public IActionResult SaveMouseData([FromBody] MouseDataRequest mouseMovementData)
        {
            return Ok("Данные сохранены получены");
        }
    }
}
