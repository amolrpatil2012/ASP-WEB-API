namespace ProductAPI.Models;

public class ProductData
{
            public static List<Product>  Products =     
        
            new List<Product>()
            {
              new Product { Id = 1, Name = "Laptop",  Price = 30000 },
              new Product { Id = 2, Name = "Desktop", Price = 25000 },
              new Product { Id = 3, Name = "Mobile",  Price = 20000 },
              new Product { Id = 4, Name = "Monitor", Price = 10000 },                
              new Product { Id = 5, Name = "Keyboard",Price = 5000 }
            };
      
}
