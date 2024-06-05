using ETicaretAPI.Application.Repositories.ProductRepositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _writeRepository;
        private readonly IProductReadRepository _readRepository;

        public ProductsController(IProductWriteRepository writeRepository, IProductReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }
        [HttpPost("Post")]
        public async Task<IActionResult> Post()
        {
           await _writeRepository.AddAsync(new() { Name = "yeni Ürün", Price = 245, Stock = 20 });
           await _writeRepository.SaveAsync();
            return Ok(" kayıt eklendi.");
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(string id)
        {
            Product p = await _readRepository.GetByIdAsync(id);
            return Ok(p);
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var entity = await _readRepository.GetByIdAsync("05d18c90-3d13-4d80-1c7b-08dc85385dbd");
            entity.Name = "Yeni Güncelleme";

            await _writeRepository.SaveAsync();
            return Ok("Güncelleme İşlemi Başarılı");
        }
    }
}
