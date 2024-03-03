﻿using Bulky.DataAccess.Repository.IRepository;
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
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context):base(context)
        {
            _context=context;
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
