using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace WorkingWithAlerts
{
    public class Tests
    {
        private WebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]

        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void HandleBasicAlert()
        {
            driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[1]/button")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert text is not as expected");

            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You successfully clicked an alert"), "Result message is not as expected");

        }
        [Test, Order(2)]    
        public void HandleConfirmAlert()
        {
            driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[2]/button")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected");

            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Ok"),
                "Result message is not as expected after accepting the alert");

            driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[2]/button")).Click();

            alert = driver.SwitchTo().Alert();
            alert.Dismiss();

            resultElement = driver.FindElement (By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Cancel"),
                "Result message is not as expected after dismissing the alert");

        }

        [Test, Order(3)]
        public void HandlePromptAlert()
        {
            driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[3]/button")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text is not as expected");

            string inputText = "Hello there!";
            alert.SendKeys(inputText);
            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You entered: " + inputText),
                "Result message is not as expected after entering text in the prompt.");

        }
    }
}