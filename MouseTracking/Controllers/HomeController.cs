using Microsoft.AspNetCore.Mvc;
using MouseTracking.Models;
using MouseTracking.Services;

namespace MouseTracking.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataBaseServices _dataBase;
        public HomeController(ILogger<HomeController> logger, DataBaseServices dataBase)
        {
            _logger = logger;
            _dataBase = dataBase;
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
            _dataBase.SaveData(mouseMovementData.MouseMovementData);
            return Ok("Данные сохранены получены");
        }
    }
}
