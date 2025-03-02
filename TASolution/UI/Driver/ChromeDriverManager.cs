using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UI.Driver
{
    /// <summary>
    /// ChromeDriver implementation of IDriverManager.
    /// </summary>
    public class ChromeDriverManager : IDriverManager
    {
        /// <summary>
        /// Returns a new instance of ChromeDriver.
        /// </summary>
        /// <returns>Chrome WebDriver instance.</returns>
        public IWebDriver GetDriver()
        {
            return new ChromeDriver();
        }
    }
}
