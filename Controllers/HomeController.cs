using ASP_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace WebUI_Explorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //-----------------------------------------------
        public IActionResult Index(string path)
        {
            if (GetTree.tree.init_dir == null)
            {
                return Redirect("/InputPath/");
            }

            if (path != null)
            {
                GetTree.tree.getNext(path);
            }
            return View(GetTree.tree);
        }
        //-----------------------------------------------

        public IActionResult GotoBack()
        {
            if (GetTree.tree.init_dir == null)
            {
                return Redirect("/InputPath/");
            }

            GetTree.tree.getBack();
            return View("Index", GetTree.tree);
        }

        public IActionResult SortFiles()
        {
            if (GetTree.tree.init_dir == null)
            {
                return Redirect("/InputPath/");
            }

            GetTree.tree.IsSort = !GetTree.tree.IsSort;
            GetTree.tree.getNext();
            return View("Index", GetTree.tree);
        }

        //-----------------------------------------------
        public IActionResult About()
        {
            return View();
        }
        //-----------------------------------------------




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
