using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    [Route("he-mat-troi/[action]")]
    public class PlanetController : Controller
    {
        private readonly PlanetService _planetService;
        private readonly ILogger<PlanetController> _logger;

        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger)
        {
            _planetService = planetService;
            _logger = logger;
        }

        [Route("/danh-sach-cac-hanh-tinh.html")] // có dấu / đầu khi không connect route controller
        public IActionResult Index() // he-mat-troi/danh-sach-cac-hanh-tinh.html
        {
            return View();
        }

        [BindProperty(SupportsGet = true, Name = "action")]
        public string Name {get; set;} // Action ~ PlanetModel

        // [Route("abc")]
        public IActionResult Mercury() // he-mat-troi/mercury
        {
            var planet = _planetService.Where(x => x.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

         public IActionResult Earth()
        {
            var planet = _planetService.Where(x => x.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        [HttpGet("/saomoc.html")]
         public IActionResult Jupiter()
        {
            var planet = _planetService.Where(x => x.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

         public IActionResult Mars()
        {
            var planet = _planetService.Where(x => x.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

         public IActionResult Neptune()
        {
            var planet = _planetService.Where(x => x.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

         public IActionResult Saturn()
        {
            var planet = _planetService.Where(x => x.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

         public IActionResult Uranus()
        {
            var planet = _planetService.Where(x => x.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        [Route("sao/[action]", Order = 3, Name = "Venus1")] // sao/venus
        [Route("sao/[controller]/[action]", Order = 2, Name = "Venus2")] // * sao/planet/venus
        [Route("[controller]-[action].html", Order = 1, Name = "Venus3")] // planet-venus.html
         public IActionResult Venus()
        {
            var planet = _planetService.Where(x => x.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }


        // controller, action, area => [controller] [action] [area]
        [Route("hanhtinh/{id:int}")]
        public IActionResult PlanetInfo(int id){
            var planet = _planetService.Where(x => x.Id == id).FirstOrDefault();
            return View("Detail", planet);
        }
    }
}