using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGVakBooks.Models;

namespace CGVakBooks.DataAccess.Repository.IRepository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        public void Update(OrderDetails orderDetails);
    }
}
