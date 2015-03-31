using System.Collections.Generic;
using System.Linq;

namespace DeliveryExample {
  public class ShoppingCart {
    private List<CartItem> _items;

    public ShoppingCart( Customer customer ) {
      Customer = customer;
      DeliveryType = DeliveryType.Standard;
      _items = new List<CartItem>();
    }

    public Customer Customer { get; private set; }
    public DeliveryType DeliveryType { get; private set; }

    public void Add( Product product, int quantity = 1 ) {
      var existing = _items.FirstOrDefault( x => x.Product.Id == product.Id );

      if ( existing != null ) {
        existing.Add( quantity );
      }
      else {
        _items.Add( new CartItem( product, quantity ) );
      }
    }

    public List<DeliveryType> GetAvailableDeliveryTypes() {
      var list = new List<DeliveryType> { DeliveryType.Standard };

      var itemCount = _items.Sum( x => x.Quantity );
      var bookCount = _items.Where( x => x.Product.ProductType == ProductType.Book ).Sum( x => x.Quantity );

      if ( Customer.CustomerType == CustomerType.VIP && itemCount == bookCount && bookCount >= 5 ) {
        list.Add( DeliveryType.Free );
      }

      return list;
    }
  }
}