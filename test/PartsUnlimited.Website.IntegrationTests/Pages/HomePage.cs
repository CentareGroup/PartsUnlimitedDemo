namespace PartsUnlimited.Website.IntegrationTests.Pages
{
    using OpenQA.Selenium;

    using SpecBind.Pages;

    [PageNavigation("/")]
    public class HomePage
    {
        [ElementLocator(Title = "Blue Performance Alloy Rim", TagName = "a")]
        public IWebElement BluePerformanceAlloyRim { get; set; }
    }
}
