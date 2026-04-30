using App.Domain.Entities;
using App.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Caching
{
    public class ProRepo : GenRepo<Product>, IProRepo
    {
        public ProRepo(AppDbContext context) : base(context)
        {
        }
    }
}
