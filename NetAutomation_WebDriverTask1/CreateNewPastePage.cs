using OpenQA.Selenium;

namespace NetAutomation_WebDriverTask1
{
    public class PastebinHomePage(IWebDriver driver, string url = "https://pastebin.com/")
    {
        private readonly IWebDriver driver = driver;
        private readonly string url = url;

        private readonly By errorSummary = By.CssSelector(".error-summary");
        private readonly By newPasteTextBox = By.Id("postform-text");
        private readonly By pasteExpirationDropdown = By.Id("select2-postform-expiration-container");
        private readonly string pasteExpirationOption = "select2-postform-expiration-results";//"//li[text()='{0}']";
        private readonly string cookieDialogueAgreeBtn = ".css-47sehv";
        private readonly By pasteNameTextBox = By.Id("postform-name");
        private readonly By createNewPasteButton = By.XPath("//button[text()='Create New Paste']");

        public void NavigateToPage()
        {
            driver.Navigate().GoToUrl(url);
        }

        public void EnterPasteText(string pasteText)
        {
            driver.FindElement(newPasteTextBox).SendKeys(pasteText);
        }

        /// <summary>
        /// Dealing with potential ad banners :( that cover up the elements
        /// </summary>
        public void ScrollDown()
        {
            var windowSize = driver.Manage().Window.Size;
            (driver as IJavaScriptExecutor)?.ExecuteScript($"window.scrollBy(0, {windowSize.Height});");
        }

        /// <summary>
        /// Cookie agreement dialogue is shown for some countries :(
        /// </summary>
        public void AgreeToCookies()
        {
            try 
            {
                driver.FindElement(By.CssSelector(cookieDialogueAgreeBtn)).Click();
            }
            catch {}
        }

        public void SelectPasteExpiration(string expiration)
        {
            driver.FindElement(pasteExpirationDropdown).Click();
            //driver.FindElement(By.XPath(String.Format(pasteExpirationOption, expiration))).Click();
            var elements = driver.FindElement(By.Id(pasteExpirationOption));

            foreach(var child in elements.FindElements(By.TagName("li")))
            {
                //TestContext.Out.WriteLine($"Child Text: {child.Text}");
                if(child.Text.Contains(expiration, StringComparison.CurrentCultureIgnoreCase))
                {
                    child.Click();
                    break;
                }
            }
        }

        /// <summary>
        /// Running out of X free paste per 24 hours for example
        /// </summary>
        public string CheckForErrors()
        {
            return driver.FindElement(errorSummary).Text;
        }

        public void EnterPasteName(string pasteName)
        {
            driver.FindElement(pasteNameTextBox).SendKeys(pasteName);
        }

        public void ClickCreateNewPaste()
        {
            driver.FindElement(createNewPasteButton).Click();
        }
    }
}