using NUnit.Framework;

namespace DeliveryExample.Tests {
  [TestFixture]
  public class CalculateDeliveryTypeTests {
    private Customer vipCustomer = new Customer { CustomerType = CustomerType.VIP };
    private Customer regularCustomer = new Customer { CustomerType = CustomerType.Regular };
    private Product bookProduct = new Product { ProductType = ProductType.Book, Id = 1 };
    private Product nonBookProduct = new Product { ProductType = ProductType.Electrical, Id = 2 };

    [Test]
    public void Free_delivery_available_for_VIP_customer_who_just_orders_five_or_more_books() {
      var shoppingCart = new ShoppingCart( vipCustomer );

      shoppingCart.Add( bookProduct, 5 );

      var deliveryTypes = shoppingCart.GetAvailableDeliveryTypes();

      Assert.That( deliveryTypes.Contains( DeliveryType.Standard ), "Standard delivery was expected" );
      Assert.That( deliveryTypes.Contains( DeliveryType.Free ), "Free delivery was expected" );
    }

    [Test]
    public void Free_delivery_not_available_for_VIP_customer_who_orders_five_or_more_books_and_another_nonbook_item() {
      var shoppingCart = new ShoppingCart( vipCustomer );

      shoppingCart.Add( bookProduct, 5 );
      shoppingCart.Add( nonBookProduct, 1 );

      var deliveryTypes = shoppingCart.GetAvailableDeliveryTypes();

      Assert.That( deliveryTypes.Contains( DeliveryType.Standard ), "Standard delivery was expected" );
      Assert.That( !deliveryTypes.Contains( DeliveryType.Free ), "Free delivery was not expected" );
    }

    [Test]
    public void Free_delivery_not_available_for_VIP_customer_who_just_orders_less_than_5_books() {
      var shoppingCart = new ShoppingCart( vipCustomer );

      shoppingCart.Add( bookProduct, 4 );

      var deliveryTypes = shoppingCart.GetAvailableDeliveryTypes();

      Assert.That( deliveryTypes.Contains( DeliveryType.Standard ), "Standard delivery was expected" );
      Assert.That( !deliveryTypes.Contains( DeliveryType.Free ), "Free delivery was not expected" );
    }

    [Test]
    public void Free_delivery_not_available_for_VIP_customer_who_orders_more_than_5_nonbook_items() {
      var shoppingCart = new ShoppingCart( vipCustomer );

      shoppingCart.Add( nonBookProduct, 5 );

      var deliveryTypes = shoppingCart.GetAvailableDeliveryTypes();

      Assert.That( deliveryTypes.Contains( DeliveryType.Standard ), "Standard delivery was expected" );
      Assert.That( !deliveryTypes.Contains( DeliveryType.Free ), "Free delivery was not expected" );
    }

    [Test]
    public void Free_delivery_not_available_for_nonVIP_customer() {
      var shoppingCart = new ShoppingCart( regularCustomer );

      shoppingCart.Add( bookProduct, 5 );

      var deliveryTypes = shoppingCart.GetAvailableDeliveryTypes();

      Assert.That( deliveryTypes.Contains( DeliveryType.Standard ), "Standard delivery was expected" );
      Assert.That( !deliveryTypes.Contains( DeliveryType.Free ), "Free delivery was not expected" );
    }
  }
}