using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGVakBooks.DataAccess.Data;
using CGVakBooks.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CGVakBooks.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category {get; private set;}

        public ICoverTypeRepository CoverType { get; private set; }

        public IProductrepository Product { get; private set; }

        public IShoppingCart ShoppingCart { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }




        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
