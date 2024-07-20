using Ocelot.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WorkingWithiFrames
{
    public class WorkingWithiFrames
    {
        private WebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void TestFrameByIndex()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected");

            }

            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(2)]
        public void TestFrameById()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));

            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(3)]
        public void TestFrameByWebElement()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));

            driver.SwitchTo().Frame(frameElement);
            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected");

            }

            driver.SwitchTo().DefaultContent();
        }
    }
}