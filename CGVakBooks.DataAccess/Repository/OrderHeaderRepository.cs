using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGVakBooks.Models;
using CGVakBooks.DataAccess.Repository.IRepository;
using CGVakBooks.DataAccess.Data;

namespace CGVakBooks.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
            _db.Update(orderHeader);
        }

        public void UpdateStatus(int id,string Orderstatus, string? paymentstatus= null)
        {
            var orderfromdb = _db.OrderHeader.FirstOrDefault(u => u.Id == id);

            if(orderfromdb !=null)
            {
                orderfromdb.OrderStatus = Orderstatus;
                if(paymentstatus!=null)
                {
                    orderfromdb.PaymentStatus= paymentstatus;
                }
            }
        }

        public void UpdateStripePaymentId(int id, string sessionId, string paymentItemId)
        {
            var orderfromdb = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            orderfromdb.PaymentDate= DateTime.Now;
            orderfromdb.SessionId = sessionId;
            orderfromdb.paymentItemId = paymentItemId;
        }
    }
}
