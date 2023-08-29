using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace VideoDownloader.SeleniumWebDriver
{
    public class SeleniumVideoDownloader
    {
        private IWebDriver _webDriver;
        
        private readonly By _safeFromInput = By.XPath("//input[@name='sf_url' and @id='sf_url']");
        private readonly By _downloadButton = By.XPath("//button[@name='sf_submit' and @id='sf_submit']");

        private readonly By _submitAndDownloadButton = By.XPath("//a[contains(@title, 'video format')]");

        public SeleniumVideoDownloader(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            
            _webDriver.Navigate().GoToUrl(SettingsData.SafeFromURL);
        }

        public void PasteVideoLinkAndDownload(string videoLink)
        {
            IWebElement inputElement = _webDriver.FindElement(_safeFromInput);
            inputElement.Clear();
            inputElement.SendKeys(videoLink);
            Thread.Sleep(3000);
            _webDriver.FindElement(_downloadButton).Click();
            Thread.Sleep(5000);
            _webDriver.FindElement(_submitAndDownloadButton).Click();
        }

    }
}