using App.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities
{
    public class Category : BaseEntity<int>, IAuditEntity
    {
        public string Name { get; set; } = default!;
        public List<Product>? Products { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
