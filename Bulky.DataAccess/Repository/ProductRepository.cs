using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using MVCExample.Data;
using MVCExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context):base(context)
        {
            _context=context;
        }

        public void Update(Product product)
        {
            var obj=_context.Products.FirstOrDefault(u=>u.Id==product.Id);
            if(obj!=null)
            {
                obj.Title = product.Title;
                obj.ISBN = product.ISBN;
                obj.Price50 = product.Price50;
                obj.Price100 = product.Price100;
                obj.ListPrice = product.ListPrice;
                obj.Description = product.Description;
                obj.Price = product.Price;
                obj.Author = product.Author;
                obj.CategoryId = product.CategoryId;
                if (product.ImageUrl != null)
                {
                    obj.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
