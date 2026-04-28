using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.Products.UpdateStock
{
    public record UpdateProductStockRequest(int ProductId, int Quantity);
}
 