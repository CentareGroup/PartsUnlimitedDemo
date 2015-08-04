namespace PartsUnlimited.Website.IntegrationTests.Pages
{
    using OpenQA.Selenium;

    using SpecBind.Pages;

    [PageNavigation("/Account/Login")]
    [PageAlias("Login")]
    public class AccountLogin
    {
        [ElementLocator(Id = "Email")]
        public IWebElement Email { get; set; }

        [ElementLocator(TagName = "input", Type = "submit", Value = "Login")]
        public IWebElement Login { get; set; }

        [ElementLocator(Id = "Password")]
        public IWebElement Password { get; set; }
    }
}