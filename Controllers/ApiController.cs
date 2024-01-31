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
        public IActionResult Sites(string city)
        {
            var sites = _context.Addresses.Where(p=>p.City==city).Select(p => p.SiteId).Distinct();
            return Json(sites);
        }
        public IActionResult Roads(string site)
        {
            var roads = _context.Addresses.Where(p => p.SiteId == site).Select(p => p.Road).Distinct();
            return Json(roads);
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
        public IActionResult Register(string name,string email, int age=25)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "guest";
            }
            return Content($"Hello {name}. {age}歲. email:{email}","text/explain",Encoding.UTF8);
        }
        public IActionResult CheckAccount(string name)
        {
            if (_context.Members.Count(n=>n.Name ==name) !=0)
            {
                return Content("名稱已存在");
            }
            else return Content("OK");
        }
        public IActionResult imgId()
        {
            var memID = _context.Members.Select(p => p.MemberId);
            return Json(memID);
        }
    }
}
