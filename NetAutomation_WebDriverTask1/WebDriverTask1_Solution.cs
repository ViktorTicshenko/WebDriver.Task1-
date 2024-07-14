using OpenQA.Selenium;

namespace NetAutomation_WebDriverTask1;

using BrowserType = BrowserFactory.BrowserType;

[Parallelizable(scope: ParallelScope.Fixtures)]
[TestFixture("Chrome")]
[TestFixture("Firefox")]
[TestFixture("Edge")]
public class CreatePasteTest(string browserTypeString)
{
    private IWebDriver? driver;
    private PastebinHomePage? pastebinHomePage;

    private readonly string browserTypeString = browserTypeString;

    [SetUp]
    public void SetUp()
    {
        var browserFactory = BrowserFactory.Instance;

        if (Enum.TryParse<BrowserType>(browserTypeString, true, out BrowserType browserType))
        {
            driver = browserFactory.GetDriver(browserType);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(browserTypeString), browserTypeString, null);
        }

        pastebinHomePage = new PastebinHomePage(driver);

        pastebinHomePage.NavigateToPage();
    }

    [Test]
    [TestCase("Hello from WebDriver", "10 Minutes", "helloweb")]
    public void CreateNewPaste(string pasteText, string pasteExpiration, string pasteName)
    {
        pastebinHomePage!.AgreeToCookies();
        pastebinHomePage.ScrollDown();
        pastebinHomePage.EnterPasteText(pasteText);
        pastebinHomePage.SelectPasteExpiration(pasteExpiration);
        pastebinHomePage.EnterPasteName(pasteName);
        pastebinHomePage.ClickCreateNewPaste();

        var errorMsg = pastebinHomePage.CheckForErrors();

        //Assert.That(errorMsg, Is.Empty, $"Error Message: {errorMsg}");
        
        if(errorMsg.Length == 0) 
            Assert.That(driver!.PageSource, Does.Contain(pasteText));
    }

    [TearDown]
    public void TearDown()
    {
        driver?.Dispose();
    }
}