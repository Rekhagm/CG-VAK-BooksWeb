using CGVakBooks.DataAccess.Repository.IRepository;
using CGVakBooks.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CGVAKBooks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<products> Get()
        {
            var products = _unitOfWork.Product.GetAll();
            return products;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public products Get(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            return product;
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] products products)
        {
            if(products != null)
            {
                _unitOfWork.Product.Add(products);
                _unitOfWork.Save();
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public products Put(int id, [FromBody] products products)
        {
            
            if(products != null)
            {
                _unitOfWork.Product.Update(products);
                _unitOfWork.Save();
            }
            return products;
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           var product= _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
        }
    }
}
