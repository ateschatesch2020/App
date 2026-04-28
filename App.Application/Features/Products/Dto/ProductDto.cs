using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.Products.Dto
{
   public record ProductDto(int Id, string Name, decimal Price, int Stock, int CategoryId);
}
