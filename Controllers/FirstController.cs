using System;
using System.IO;
using System.Linq;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{

    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly ProductService _productService;
        public FirstController(ILogger<FirstController> logger, ProductService productService){
            _logger = logger;
            _productService = productService;
        }
        public string Index() {
            // _logger.LogInformation("Thảo meo meo meo");
            Console.WriteLine("meomeo");
            _logger.LogDebug("fsfsfsdf");
            _logger.LogWarning("warning");
            _logger.LogError("error");

            // serilog
            return "Tôi là Index của First";
        }

        public void Nothing(){
            _logger.LogInformation("Nothing Action");
            Response.Headers.Add("hi", "xin chao cac ban");
        }

        public object AnyThing() => new int[] { 1, 2, 3, 4};
        
        public IActionResult Readme(){
            var content = @"Xin chào 
                cin chao cac ban, xdfs
                fsfsfsdfs
                fsdf
            ";
            return Content(content, "text/html");
        }

        public IActionResult Bird()
        {
            //Startup.contentrootpath
            string filePath = Path.Combine(Startup.ContentRootPath, "Files", "bird.jpg");
            var bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes, "image/jpg");
        }

        public IActionResult IphonePrice(){
            return Json(
                new {
                    productName = "Điện thoại Iphone 15",
                    price = 1000
                }
            );
        }

        public IActionResult Privacy(){
            var url = Url.Action("Privacy", "Home");
            _logger.LogInformation("Chuyen huong den /home/privacy");
            return LocalRedirect(url);// local ~ host
        }

        public IActionResult Google(){
            var url = "https://laptrinhvb.net";
            _logger.LogInformation("Chuyen huong den webstite laptrinhvb.net");
            return Redirect(url);// local ~ host
        }

        public IActionResult HelloView(string username)
        {
            if(string.IsNullOrEmpty(username))
                username = "Khách";
            // view() -> Razor engize, doc .cshtml (template)
            //----------------------------------------------------------------
            // View (template) - template đường dẫn tuyệt đối đến file .cshtml
            // View(template, model)
            //return View("/MyView/xinchao1.cshtml", username);
            
            //xinchao2.cshtml => View/First/xinchao2.cshtml
            //return View("xinchao2", username);

            // HellowView.cshtml -> /View/First/HelloView.cshtml
            // /view/controller/action.cshtml
            // return View((object)username);

            return View("xinchao3", username);
            // sử dụng phổ biến
            // View();
            // View(model);

        }

        [TempData]
        public string StatusMessage {set; get;}
        
        [AcceptVerbs("POST", "GET")]
        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();

            if(product == null){
                // TempData["StatusMessage"] = "San pham ban yeu cau khong co";
                StatusMessage = "Khong tim thay san pham nao ban oi.";
                return Redirect(Url.Action("Index", "Home"));
            }

            // if(product == null)
            //     return NotFound();
            // return Content($"San pham ID = {id}");

            // /View/First/ViewProduct.cshtml
            // /MyView/First/ViewProduct.cshtml
           // return View(product);

            // ViewData => object
            // this.ViewData["product"] = product;
            // this.ViewData["title"] = "Hello Web MVC - love it";
            // return View("ViewProduct2");
            ViewBag.product = product; // dạng đối tượng
            TempData["thongbao"] = "fdsfsdfsfsdfsd"; // => session redirect to trang khác
            return View("ViewProduct3");
        }
    }
}