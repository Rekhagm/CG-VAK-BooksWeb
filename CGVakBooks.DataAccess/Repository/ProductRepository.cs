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
    public class ProductRepository : Repository<products>, IProductrepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();

        }

        public void Update(products products)
        {
            _db.Update(products);

        }
    }
}
