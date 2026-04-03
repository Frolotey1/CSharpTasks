namespace Shop;
using System;

public struct RequestItem {
      public Article product;
      public uint quantity;
        
      private Article Product {
          get { return product; }
          set { product = value; }
      }
        
      private uint Quantity {
          get { return quantity; }
          set { quantity = value; }
      }
        
      public void Run() {
          Console.WriteLine("Ввод товара для позиции заказа");
            
          Article article = new Article();
          article.Run();
          Product = article;
            
          Console.Write("Введите количество единиц товара: ");
          Quantity = uint.Parse(Console.ReadLine());
            
          Console.WriteLine($"\nПозиция заказа:");
          Console.WriteLine($"Товар: {Product.name}");
          Console.WriteLine($"Количество: {Quantity}");
          Console.WriteLine($"Стоимость позиции: {Product.price * Quantity:C}");
      }
        
      public decimal GetTotalPrice() {
          return (decimal)Product.price * Quantity;
      }
}
