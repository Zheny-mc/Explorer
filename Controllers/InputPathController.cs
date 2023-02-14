using ASP_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ASP_project.Controllers
{
    public class InputPathController : Controller
    {
        public IActionResult Index() { return View(new InputPath()); }

        
        public IActionResult Check(InputPath inputPath)
        {
            if (ModelState.IsValid && FileSystemInfoExtensions.checkPath(inputPath.path))
            {
                string path = inputPath.path;
                path = string.Join(@"\", path.Split(new char[] { '\\', '/' }).Where(l => l.Length > 0) );
                if (path[path.Length - 1] == ':')
                {
                    path += @"\";
                }
                inputPath.path = path;
                GetTree.tree = new Tree(inputPath.path);
                GetTree.tree.getNext();
                return Redirect("/Home/");
            }

            inputPath.status = "Ошибка, пути!";
            return View("Index", inputPath);
        }
    }


}
