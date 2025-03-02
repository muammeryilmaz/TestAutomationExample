using OpenQA.Selenium;

namespace UI.Driver
{
    /// <summary>
    /// Interface for managing WebDriver instances.
    /// This ensures that different driver implementations can be used interchangeably.
    /// </summary>
    public interface IDriverManager
    {
        /// <summary>
        /// Retrieves an instance of IWebDriver.
        /// </summary>
        /// <returns>An instance of IWebDriver.</returns>
        IWebDriver GetDriver();
    }
}
