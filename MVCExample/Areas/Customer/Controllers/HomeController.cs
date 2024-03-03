using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using MVCExample.Models;
using System.Diagnostics;

namespace MVCExample.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable< Bulky.Models.Models.Product > productlist=_unitOfWork.Product.GetAll(includes:"Category").ToList();
            return View(productlist);
        }
        public IActionResult Details(int id)
        {
            Bulky.Models.Models.Product product = _unitOfWork.Product.Get(u=>u.CategoryId==id,includes: "Category");
            return View(product);
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
