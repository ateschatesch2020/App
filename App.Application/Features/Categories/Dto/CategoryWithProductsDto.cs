using App.Application.Features.Products.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.Categories.Dto
{
    public record CategoryWithProductsDto(int Id, string Name, List<ProductDto> Products)   ;
}
