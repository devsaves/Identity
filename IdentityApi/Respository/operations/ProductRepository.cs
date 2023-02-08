using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityApi.Respository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Respository.operations
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IdDbContext _CONTEXT;
        public ProductRepository(IdDbContext CONTEXT) : base(CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }

    }
}