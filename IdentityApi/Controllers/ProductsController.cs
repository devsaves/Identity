using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Services;
using IdentityApi.Respository;
using IdentityApi.Respository.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _PRODUCT;
        public ProductsController(IProductRepository product)
        {
            _PRODUCT = product;
        }

        [HttpGet]
        [Authorize(Roles = "Técnico")]
        public async Task<IActionResult> Get()
        {
            var fromDb = await _PRODUCT.GetAllAsync();

            if (fromDb == null) return NoContent();

            return Ok(fromDb);
        }

        [HttpGet("seed")]
        public async Task<IActionResult> seed()
        {
            var ToDb = new List<Product>();

            ToDb.Add(new Product() { Id = 0, Name = "Refrigerante", Description = "Coca-cola 350Ml Lata" });
            ToDb.Add(new Product() { Id = 0, Name = "Leite", Description = "Caixinha itambe 1 Litro" });
            ToDb.Add(new Product() { Id = 0, Name = "Biscoito", Description = "Polvilho saco 500 G" });
            ToDb.Add(new Product() { Id = 0, Name = "Isqueiro", Description = "Isqueiro bic pequeno" });
            ToDb.Add(new Product() { Id = 0, Name = "Sabão", Description = "Sabão de barra coco embalagem com 4 150G cada. peso total 600G" });
            ToDb.Add(new Product() { Id = 0, Name = "Chips (salgadinho)", Description = "Fandangos elma chips 500 g" });

            //_PRODUCT.AddList(ToDb.ToArray());

            ToDb.ForEach(product =>
            {
                _PRODUCT.Add(product);
            });


            if (await _PRODUCT.save())
            {
                var getFromDb = await _PRODUCT.GetAllAsync();
                return Ok(getFromDb);
            }
            return BadRequest();
        }
    }
}