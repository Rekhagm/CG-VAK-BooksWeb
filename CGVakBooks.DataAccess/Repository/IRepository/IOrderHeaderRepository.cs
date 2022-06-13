using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGVakBooks.Models;

namespace CGVakBooks.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);

        void UpdateStatus(int id, string Orderstatus, string? paymentstatus = null);

        void UpdateStripePaymentId(int id, string sessionId, string paymentItemId);
    }
}
