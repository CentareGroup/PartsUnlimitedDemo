namespace PartsUnlimited.Website.IntegrationTests.Pages
{
    using OpenQA.Selenium;

    using SpecBind.Pages;

    [PageNavigation("Checkout/AddressAndPayment")]
    public class CheckoutPage
    {
        [ElementLocator(Name = "Name")]
        public IWebElement Name { get; set; }

        [ElementLocator(Name = "Phone")]
        public IWebElement Phone { get; set; }

        [ElementLocator(Name = "Address")]
        public IWebElement Address { get; set; }

        [ElementLocator(Name = "City")]
        public IWebElement City { get; set; }

        [ElementLocator(Name = "State")]
        public IWebElement State { get; set; }

        [ElementLocator(Name = "PostalCode")]
        public IWebElement PostalCode { get; set; }

        [ElementLocator(Name = "PromoCode")]
        public IWebElement PromoCode { get; set; }

        [ElementLocator(Name = "Country")]
        public IWebElement Country { get; set; }

        [ElementLocator(TagName = "input", Type = "submit", Value = "Submit Order")]
        public IWebElement SubmitOrder { get; set; }
    }
}