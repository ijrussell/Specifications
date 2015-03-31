using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using TechTalk.SpecFlow;

namespace DeliveryExample.Tests {
  [Binding]
  public class CalculateDeliveryTypeSteps {
    private ShoppingCart _shoppingCart;
    private Product _bookProduct = new Product {
      Id = 1,
      Name = "Product1",
      ProductType = ProductType.Book
    };
    private Product _electricalProduct = new Product {
      Id = 2,
      Name = "Product2",
      ProductType = ProductType.Electrical
    };
    private List<DeliveryType> _deliveryTypes;

    [Given( @"I create an order for a VIP customer" )]
    public void GivenICreateAnOrderForAVIPCustomer() {
      var vipCustomer = new Customer {
        CustomerType = CustomerType.VIP
      };

      _shoppingCart = new ShoppingCart( vipCustomer );
    }

    [Given( @"I create an order for a non-VIP customer" )]
    public void GivenICreateAnOrderForANon_VIPCustomer() {
      var vipCustomer = new Customer {
        CustomerType = CustomerType.Regular
      };

      _shoppingCart = new ShoppingCart( vipCustomer );
    }

    [Given( @"I add (.*) non-books" )]
    public void GivenIAddNon_Books( int p0 ) {
      _shoppingCart.Add( _electricalProduct, p0 );
    }

    [Given( @"I add (.*) books" )]
    public void GivenIAddBooks( int p0 ) {
      _shoppingCart.Add( _bookProduct, p0 );
    }

    [When( @"I ask for delivery types" )]
    public void WhenIAskForDeliveryTypes() {
      _deliveryTypes = _shoppingCart.GetAvailableDeliveryTypes();
    }

    [Then( @"I see free delivery as an option" )]
    public void ThenISeeFreeDeliveryAsAnOption() {
      var freeDelivery = _deliveryTypes.Contains( DeliveryType.Free );

      Assert.That( freeDelivery, Is.True, "Expected Free delivery" );
    }

    [Then( @"I do not see free delivery as an option" )]
    public void ThenIDoNotSeeFreeDeliveryAsAnOption() {
      var freeDelivery = _deliveryTypes.Contains( DeliveryType.Free );

      Assert.That( freeDelivery, Is.False, "Did not expect Free delivery" );
    }
  }
}
