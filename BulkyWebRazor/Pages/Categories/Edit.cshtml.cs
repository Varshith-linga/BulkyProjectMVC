using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazor.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Category? CategoryList { get; set; }
        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(int? id)
        {
            if (id != 0 || id != null)
            {
                CategoryList = _context.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(CategoryList);
                _context.SaveChanges();
                TempData["success"] = "Category Updated successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
