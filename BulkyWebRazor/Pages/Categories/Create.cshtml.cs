using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
    [BindProperties]//it is used to bind the data while using post method(for class)
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        //[BindProperty]it is used to bind the data while using post method(for action)
        public Category CategoryList { get; set; }
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            _context.Categories.Add(CategoryList);
            _context.SaveChanges();
            TempData["success"] = "Category Created Successfully";
            return RedirectToPage("Index");
        } 
    }
}
