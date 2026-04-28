namespace App.Application.Features.Products.Create
{
    public record CreateProductRequest(string Name, decimal Price, int Stock, int CategoryId);
}

//public class CreateProductRequest
//{
//    public string Name { get; init; }
//    public decimal Price { get; init; }
//    public int Stock { get; init; }
//    public int CategoryId { get; init; }

//    public CreateProductRequest(string name, decimal price, int stock, int categoryId)
//    {
//        Name = name;
//        Price = price;
//        Stock = stock;
//        CategoryId = categoryId;
//    }
//}