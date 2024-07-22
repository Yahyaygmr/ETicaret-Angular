﻿using ETicaretAPI.Application.Repositories.ProductRepositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.ViewModels.Products;
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

        [HttpGet("id")]
        public async Task<IActionResult> Get(string id)
        {
            Product p = await _readRepository.GetByIdAsync(id, false);
            return Ok(p);
        }
        [HttpGet]
        public IActionResult Get([FromQuery] Pagination pagination)
        {
            var totalCount = _readRepository.GetAll(false).Count();
            var entities = _readRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToList();
            return Ok(new
            {
                totalCount,
                entities
            });
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductViewModel model)
        {
            await _writeRepository.AddAsync(new() { Name = model.Name, Price = model.Price, Stock = model.Stock });
            await _writeRepository.SaveAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductViewModel model)
        {
            var product = await _readRepository.GetByIdAsync(model.Id);
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.Name = model.Name;
            await _writeRepository.SaveAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _writeRepository.RemoveAsync(id);
            await _writeRepository.SaveAsync();
            return Ok("Silme işlemi başarılı");
        }
    }
}
