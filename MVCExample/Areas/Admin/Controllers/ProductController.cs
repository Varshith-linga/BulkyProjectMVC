using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Bulky.Models.ViewModel;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCExample.Data;
using MVCExample.Models;
using System.Collections.Generic;


namespace MVCExample.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            List<Product> objeCategoryList = _unitOfWork.Product.GetAll(includes:"Category").ToList();
            
            return View(objeCategoryList);
        }
        //for update and create
        public IActionResult Upsert(int? id)
        {
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            ProductVM productVM = new()
            {
                //using projection we convert the class columns into Ienumerable<selectListitem>
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product =new Product()
            };
            //if id=0 r null then the item is used for create
            if(id==null || id == 0){
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u=>u.Id==id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productvm,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHost.WebRootPath;
                if(file != null)
                {
                    //randem Name for our file
                    string filename=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string ProductPath=Path.Combine(wwwRootPath,@"Images\Product");
                    if (!string.IsNullOrEmpty(productvm.Product.ImageUrl))
                    {
                        //delete the old image
                        var old = Path.Combine(wwwRootPath,productvm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(old))
                        {
                            System.IO.File.Delete(old);
                        }   
                    }
                    using (var fileStream = new FileStream(Path.Combine(ProductPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productvm.Product.ImageUrl = @"\Images\Product\" + filename;
                }
                if (productvm.Product.Id == 0)
                {    
                    _unitOfWork.Product.Add(productvm.Product);
                }
                else{
                    _unitOfWork.Product.Update(productvm.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product Add successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productvm.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productvm);
            }
            
        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == 0 || id == null)
        //    {
        //        return NotFound();
        //    }
        //    Product? cat = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (cat == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(cat);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
        //    _unitOfWork.Product.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Product Deleted successfully";
        //    return RedirectToAction("Index");
        //}


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objeCategoryList = _unitOfWork.Product.GetAll(includes: "Category").ToList();
            return Json(new {data=objeCategoryList});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Product not found " });
            }

            // Check if ImageUrl is not null or empty
            //if (string.IsNullOrEmpty(productToBeDeleted.ImageUrl))
            //{
            //    return Json(new { success = false, message = "Product image URL is null or empty" });
            //}

            var old = Path.Combine(_webHost.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(old))
            {
                System.IO.File.Delete(old);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful" });
        }


        #endregion
    }
}
