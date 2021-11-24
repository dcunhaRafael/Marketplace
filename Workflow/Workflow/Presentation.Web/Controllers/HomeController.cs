using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Web.Models;
using Presentation.Web.Services.Proxy;
using System.Diagnostics;

namespace Presentation.Web.Controllers {
    public class HomeController : BaseController {
        public HomeController(IAppCache memoryCache, ILogger<HomeController> logger, ICommonService commonService) : base(memoryCache, logger, commonService) {

        }

        public IActionResult Index() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
