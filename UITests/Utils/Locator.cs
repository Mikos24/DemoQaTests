using OpenQA.Selenium;

namespace DemoQATests.UITests.Utils
{
    /// <summary>
    /// Custom locator class to abstract away Selenium's By type from Page Objects.
    /// This enforces the architectural principle of keeping Selenium usage confined to Helper classes.
    /// </summary>
    internal class Locator
    {
        private readonly By seleniumBy;
        public string Description { get; }

        private Locator(By seleniumBy, string description)
        {
            this.seleniumBy = seleniumBy;
            Description = description;
        }

        /// <summary>
        /// Converts the custom locator to Selenium's By type.
        /// This method should only be used within TestHelpers class.
        /// </summary>
        internal By ToSeleniumBy() => seleniumBy;

        // Factory methods for creating locators
        public static Locator ById(string id) => new Locator(By.Id(id), $"ID: {id}");
        public static Locator ByClassName(string className) => new Locator(By.ClassName(className), $"ClassName: {className}");
        public static Locator ByName(string name) => new Locator(By.Name(name), $"Name: {name}");
        public static Locator ByTagName(string tagName) => new Locator(By.TagName(tagName), $"TagName: {tagName}");
        public static Locator ByCssSelector(string cssSelector) => new Locator(By.CssSelector(cssSelector), $"CSS: {cssSelector}");
        public static Locator ByXPath(string xpath) => new Locator(By.XPath(xpath), $"XPath: {xpath}");
        public static Locator ByLinkText(string linkText) => new Locator(By.LinkText(linkText), $"LinkText: {linkText}");
        public static Locator ByPartialLinkText(string partialLinkText) => new Locator(By.PartialLinkText(partialLinkText), $"PartialLinkText: {partialLinkText}");

        public override string ToString() => Description;
    }
}
