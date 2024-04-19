﻿using ETicaretAPI.Application.Repositories.ProductRepositories;
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
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _writeRepository.AddRangeAsync(new()
            {
                new(){Id=Guid.NewGuid(),Name="Product1",Price=100,Stock=10,CreatedDate=DateTime.Now },
                new(){Id=Guid.NewGuid(),Name="Product2",Price=200,Stock=20,CreatedDate=DateTime.Now },
                new(){Id=Guid.NewGuid(),Name="Product3",Price=300,Stock=30,CreatedDate=DateTime.Now },
                new(){Id=Guid.NewGuid(),Name="Product4",Price=400,Stock=40,CreatedDate=DateTime.Now },
                new(){Id=Guid.NewGuid(),Name="Product5",Price=500,Stock=50,CreatedDate=DateTime.Now },
                new(){Id=Guid.NewGuid(),Name="Product6",Price=600,Stock=60,CreatedDate=DateTime.Now },
            });
            await _writeRepository.SaveAsync();
            return Ok("Products created");
        }
    }
}
