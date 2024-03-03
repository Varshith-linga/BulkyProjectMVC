using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Category? CategoryList { get; set; }
        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(int? id)
        {
            if(id!=0 || id!=null)
            {
                CategoryList = _context.Categories.Find(id);
            }
        }
        public IActionResult OnPost(int? id)
        {
            Category? obj = _context.Categories.Find(id);
            if(obj==null)
            {
                return NotFound();
            }
            _context.Categories.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToPage("Index");
        }
    }
}
