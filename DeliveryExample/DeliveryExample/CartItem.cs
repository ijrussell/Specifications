using System;

namespace DeliveryExample {
  public class CartItem {
    public CartItem(Product product, int quantity ) {
      Product = product;
      Quantity = quantity;
    }

    public Product Product { get; private set; }
    public int Quantity { get; private set; }

    public void Add(int quantity) {
      if (quantity <= 0) {
        throw new ApplicationException("Quantity must be greater than 0");
      }
      
      Quantity += quantity;
    }

    public void Remove( int quantity ) {
      if ( quantity <= 0 ) {
        throw new ApplicationException( "Quantity must be greater than 0" );
      }
      if ( quantity > Quantity ) {
        throw new ApplicationException( "You cannot remove more items than you have in the cart" );
      }

      Quantity -= quantity;
    }
  }
}