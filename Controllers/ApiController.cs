using Ajax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding;
using System.Text;

namespace Ajax.Controllers
{
    public class ApiController : Controller
    {
        MyDBContext _context = new MyDBContext();
        public IActionResult Index()
        {
            return Content("<h1>Hello,World!</h1>","text/html",Encoding.UTF8);
        }
        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(p => p.City).Distinct();
            return Json(cities);
        }
        public IActionResult Avatar(int id = 1)
        {
            Member? member = _context.Members.Find(id);
            if (member != null)
            {
                byte[] img = member.FileData;
                if (img != null)
                {
                    return File(img, "image/jpeg");
                }
            }
            return NotFound();
        }
        public IActionResult Fetch() 
        {
            return View();
        }
        public IActionResult Register(string name,int age=25)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "guest";
            }
            return Content($"Hello {name}. {age}歲","text/explain",Encoding.UTF8);
        }
        public IActionResult CheckAccount(string name)
        {
            if (_context.Members.Count(n=>n.Name ==name) !=0)
            {
                return Content("名稱已存在");
            }
            else return Content("OK");
        }
    }
}
