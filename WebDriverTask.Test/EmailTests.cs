using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverTask.Models;

namespace WebDriverTask.Test
{
    [TestFixture]
    public class EmailTests
    {
        private IWebDriver webDriver;
        
        private LoginPage loginPage;

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
            loginPage = new LoginPage(webDriver);
        }

        [Test]
        public void TestLoginAndPassword()
        {
            loginPage.NavigateToLoginPage("https://the-internet.herokuapp.com/login");

            loginPage.Login("tomsmith", "SuperSecretPassword!");
            Assert.IsTrue(loginPage.IsLoginSuccessful(), "Login with correct credentials failed");
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }
    }
}