using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Common
{
    public class BaseEntity<T>
    {
        public T Id { get; set; } = default!;
    }
}
