namespace PartsUnlimited.Website.IntegrationTests.Pages
{
    using OpenQA.Selenium;

    using SpecBind.Pages;

    [PageNavigation("/ShoppingCart")]
    public class ShoppingCart
    {
        [ElementLocator(Id = "checkout-btn")]
        public IWebElement Checkout { get; set; }
    }
}
