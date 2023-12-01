using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebDriverTask.Models
{
    public class LoginPage
    {
        private IWebDriver webDriver;

        public LoginPage(IWebDriver driver)
        {
            webDriver = driver;
        }

        public void NavigateToLoginPage(string url)
        {
            webDriver.Navigate().GoToUrl(url);
        }

        public void Login(string username, string password)
        {
            var usernameInput = webDriver.FindElement(By.Id("username"));
            var passwordInput = webDriver.FindElement(By.Id("password"));
            var loginButton = webDriver.FindElement(By.CssSelector("button[type='submit']"));

            usernameInput.SendKeys(username);
            passwordInput.SendKeys(password);
            loginButton.Click();
        }

        public bool IsLoginSuccessful()
        {
            try
            {
                var successElement = webDriver.FindElement(By.CssSelector(".success-message"));
                return successElement != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}