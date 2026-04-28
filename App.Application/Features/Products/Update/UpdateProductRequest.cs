using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.Products.Update
{
    public record UpdateProductRequest(string Name, decimal Price, int Stock, int CategoryId);
}
