using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using UI.Utilities;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace UI.Pages
{
    /// <summary>
    /// Represents the Drag & Drop functionality.
    /// </summary>
    public class DragAndDropPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public DragAndDropPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Performs a drag and drop action and captures screenshots of the dragged element before and after the action.
        /// </summary>
        public void DragAndDropItem(string itemId, string expectedText)
        {
            IWebElement menuItem = FindMenuItem(itemId);
            IWebElement orderTicket = FindOrderTicket();

            LoggerHelper.LogInfo($"Capturing screenshot of menu item before drag and drop: {expectedText}");
            TakeElementScreenshot(menuItem, "before.png");

            PerformDragAndDrop(menuItem, orderTicket);

            IWebElement droppedItem = FindDroppedItem(expectedText);

            LoggerHelper.LogInfo($"Capturing screenshot of dropped item after drag and drop: {expectedText}");
            TakeElementScreenshot(droppedItem, "after.png");
        }

        private IWebElement FindMenuItem(string itemId)
        {
            LoggerHelper.LogInfo($"Searching for menu item: {itemId}");
            return _wait.Until(d => d.FindElement(By.Id(itemId)));
        }

        private IWebElement FindOrderTicket()
        {
            LoggerHelper.LogInfo("Finding the Order Ticket area.");
            return _wait.Until(d => d.FindElement(By.Id("plate-items")));
        }

        private IWebElement FindDroppedItem(string expectedText)
        {
            LoggerHelper.LogInfo($"Searching for dropped item: {expectedText}");
            return _wait.Until(d => d.FindElement(By.XPath($"//ul[@id='plate-items']/li[text()='{expectedText}']")));
        }

        private void PerformDragAndDrop(IWebElement source, IWebElement target)
        {
            LoggerHelper.LogInfo("Performing Drag & Drop action.");
            Actions actions = new Actions(_driver);
            actions.ClickAndHold(source)
                   .MoveToElement(target)
                   .Release()
                   .Perform();
            LoggerHelper.LogInfo("Drag & Drop action completed.");
        }

        private void TakeElementScreenshot(IWebElement element, string fileName)
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                Bitmap fullImg = new Bitmap(new MemoryStream(screenshot.AsByteArray));

                Point elementLocation = element.Location;
                Size elementSize = element.Size;

                Rectangle cropArea = new Rectangle(elementLocation.X, elementLocation.Y, elementSize.Width, elementSize.Height);
                Bitmap elementScreenshot = fullImg.Clone(cropArea, fullImg.PixelFormat);

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                elementScreenshot.Save(filePath, ImageFormat.Png);

                LoggerHelper.LogInfo($"Screenshot saved: {filePath}");
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"Error capturing screenshot: {ex.Message}");
            }
        }
    }
}
