using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGVakBooks.Models;

namespace CGVakBooks.DataAccess.Repository.IRepository
{
    public interface IShoppingCart : IRepository<ShoppingCart>
    {
       int IncrementCount(ShoppingCart cart,int count);

        int DecrementCount(ShoppingCart cart, int count);

    }

}
