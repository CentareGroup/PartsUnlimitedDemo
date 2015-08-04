Feature: Jerry orders a product through the website

Scenario: Jerry orders a "free" promo code product
	Given I navigated to the home page
	  And I chose Blue Performance Alloy Rim
	  And I waited for the Product Details page
	  And I chose Add To Cart
	  And I waited for the shopping cart page
	  And I chose Checkout
	  And I waited for the account login page
	  And I entered data
			| Field    | Value                         |
			| email    | jerry@takemyorder.com         |
			| password | YouShouldChangeThisPassword1! |
	  And I chose Login
	  And I waited for the checkout page
	 When I enter data
			| Field       | Value           |
			| Name        | Jerry Freloader |
			| Phone       | 414-293-1010    |
			| Address     | 123 Elm Street  |
			| City        | Brookfield      |
			| State       | WI              |
			| Postal Code | 53005           |
			| Country     | USA             |
		And I enter data
			| Field      | Value |
			| Promo Code | FREE  |
		And I choose submit order
		Then I wait for the order complete page