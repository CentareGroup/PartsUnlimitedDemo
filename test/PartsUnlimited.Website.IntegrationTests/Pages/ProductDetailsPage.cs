namespace PartsUnlimited.Website.IntegrationTests.Pages
{
    using OpenQA.Selenium;

    using SpecBind.Pages;

    [PageNavigation("Store/Details/[0-9]+")]
    public class ProductDetailsPage
    {
        [ElementLocator(Id = "addtocart")]
        public IWebElement AddToCart { get; set; }
    }
}