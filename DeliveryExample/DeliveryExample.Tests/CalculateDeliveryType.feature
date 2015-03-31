Feature: Calculate Delivery Type
  Free delivery is offered to VIP customers once they purchase a certain number
    of books. Free delivery is not offered to regular customers or VIP customers
    buying anything else than books.
  Given that the minimum number of books to get free delivery is five, then we
    expect the following:

    CustomerType  Cart Contents         Delivery
    VIP           5 books               Free + Standard
    VIP           4 books               Standard
    VIP           5 books + 1 non-book  Standard
    VIP           5 non-books           Standard
    Regular       5 books               Standard

Scenario: Free delivery available for VIP customer who just orders five or more books
	Given I create an order for a VIP customer
	And I add 5 books 
	When I ask for delivery types
	Then I see free delivery as an option

Scenario: Free delivery not available for VIP customer who orders five or more books and another non-book item
	Given I create an order for a VIP customer
	And I add 5 books 
	And I add 1 non-books 
	When I ask for delivery types
	Then I do not see free delivery as an option

Scenario: Free delivery not available for VIP customer who just orders less than 5 books
	Given I create an order for a VIP customer
	And I add 4 books 
	When I ask for delivery types
	Then I do not see free delivery as an option

Scenario: Free delivery not available for VIP customer who orders more than 5 non-book items
	Given I create an order for a VIP customer
	And I add 5 non-books 
	When I ask for delivery types
	Then I do not see free delivery as an option

Scenario: Free delivery not available for non-VIP customer
	Given I create an order for a non-VIP customer
	And I add 5 books 
	When I ask for delivery types
	Then I do not see free delivery as an option
