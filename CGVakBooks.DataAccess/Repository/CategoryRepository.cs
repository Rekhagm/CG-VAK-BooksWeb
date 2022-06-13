using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGVakBooks.DataAccess.Data;
using CGVakBooks.DataAccess.Repository.IRepository;
using CGVakBooks.Models;

namespace CGVakBooks.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category> ,ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();

        }

        public void Update(Category category)
        {
             _db.Update(category);

        }
    }
}
