using NUnit.Framework;
using OpenQA.Selenium;
using UI.Pages;
using UI.Driver;
using UI.Utilities;
using System.Collections.Generic;

namespace UI.Tests
{
    /// <summary>
    /// Test class for verifying Drag & Drop functionality.
    /// </summary>
    public class DragAndDropFunctionalityTests
    {
        private IWebDriver driver;
        private IDriverManager driverManager;
        private MainPage mainPage;
        private DragAndDropPage dragAndDropPage;
        private Dictionary<string, string> testItems;

        [SetUp]
        public void Setup()
        {
            LoggerHelper.LogInfo("Test setup is starting.");

            driverManager = new ChromeDriverManager();
            driver = driverManager.GetDriver();
            driver.Manage().Window.Maximize();

            NavigateToHomePage();
            NavigateToDragAndDropPage();

            testItems = ConfigReader.GetDragMenuItems();
            LoggerHelper.LogInfo($"Number of test items retrieved from configuration: {testItems.Count}");
        }

        private void NavigateToHomePage()
        {
            string baseUrl = "https://kitchen.applitools.com/";
            LoggerHelper.LogInfo($"Navigating to the home page: {baseUrl}");
            driver.Navigate().GoToUrl(baseUrl);
            mainPage = new MainPage(driver);
        }

        private void NavigateToDragAndDropPage()
        {
            LoggerHelper.LogInfo("Navigating to the Drag & Drop page.");
            mainPage.GoToDragAndDropPage();
            dragAndDropPage = new DragAndDropPage(driver);
        }

        [Test]
        public void Test_DragAndDrop()
        {
            foreach (var item in testItems)
            {
                LoggerHelper.LogInfo($"Starting test for: {item.Key}");

                dragAndDropPage.DragAndDropItem(item.Key, item.Value);

                LoggerHelper.LogInfo("Performing image comparison.");
                bool imagesAreSame = ValidationHelper.AreImagesEqual("before.png", "after.png");

                if (imagesAreSame)
                {
                    LoggerHelper.LogInfo("Image validation passed. The dragged item remained unchanged.");
                }
                else
                {
                    LoggerHelper.LogError("Image validation failed. The dragged item has changed.");
                }

                Assert.That(imagesAreSame, Is.True, $"Image validation failed for: {item.Value}");
            }

            LoggerHelper.LogInfo("All Drag & Drop tests completed.");
        }

        [TearDown]
        public void Cleanup()
        {
            LoggerHelper.LogInfo("Closing the browser.");
            driver.Quit();
        }
    }
}
