using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SearchKeyboard
{
    public class SearchProductTests
    {
        private WebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]

        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void SearchProduct_Should_AddToCart()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("keyboard");
            driver.FindElement(By.XPath("//input[@type='image']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();

                Assert.IsTrue(driver.PageSource.Contains("keyboard"),
                    "The product keyboard was not added in the cart page");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }

        }

        [Test, Order(2)]
        public void SearchProduct_Junk_ShouldThrowNoSuchElementException()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("junk");
            driver.FindElement(By.XPath("//input[@type='image']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();
            }
            catch (NoSuchElementException ex)
            {

                Assert.Pass("Expected NoSuchElementException was thrown");
                Console.WriteLine("Timeout - " + ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }

        }
    }
}