using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UI.Utilities;
using System;

namespace UI.Pages
{
    /// <summary>
    /// Represents the main page of the application.
    /// </summary>
    public class MainPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement DragAndDropLink => _wait.Until(d => d.FindElement(By.CssSelector("#__next > div > div > section > div > div > a:nth-child(5)")));

        /// <summary>
        /// Navigates to the Drag & Drop page by clicking on the relevant link.
        /// </summary>
        public void GoToDragAndDropPage()
        {
            LoggerHelper.LogInfo("Clicking on the Drag & Drop link.");
            DragAndDropLink.Click();
            LoggerHelper.LogInfo("Navigated to the Drag & Drop page.");
        }
    }
}
