using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriverTask.Models
{
    public class ComposePage
    {
        private IWebDriver webDriver;

        private LoginPage loginPage;

        private readonly WebDriverWait wait;

        public ComposePage(IWebDriver driver)
        {
            webDriver = driver;
            loginPage = new LoginPage(webDriver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void SendEmail(string loginUrl, string sender, string senderPassword, string recipient, string subject, string body)
        {
            // loginPage.NavigateToLoginPage(loginUrl);

            // loginPage.Login(sender, senderPassword);

            Login(sender, senderPassword);

            // var composeButton = webDriver.FindElement(By.Id(""))
            // var recipientInput = webDriver.FindElement(By.Id(":te"));
            // var subjectInput = webDriver.FindElement(By.Id(":ps"));
            // var bodyInput = webDriver.FindElement(By.Id(":r2"));
            // var sendButton = webDriver.FindElement(By.Id(":pi"));

            // recipientInput.SendKeys(recipient);
            // subjectInput.SendKeys(subject);
            // bodyInput.SendKeys(body);
            // sendButton.Click();

            webDriver.FindElement(By.XPath("//div[text()='Compose']")).Click();

            webDriver.FindElement(By.Name("to")).SendKeys(recipient);
            webDriver.FindElement(By.Name("subjectbox")).SendKeys(subject);
            webDriver.FindElement(By.CssSelector("div[role='textbox']")).SendKeys(body);

            webDriver.FindElement(By.CssSelector("div[data-tooltip*='Send']")).Click();
        }

        public void Login(string username, string password)
        {
            webDriver.Navigate().GoToUrl("https://accounts.google.com/ServiceLogin");

            var emailInput = webDriver.FindElement(By.Id("identifierId"));
            emailInput.SendKeys(username);

            webDriver.FindElement(By.Id("identifierNext")).Click();

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            // wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("password")));
            // wait.Until(ExpectedConditions.ElementIsVisible(By.Name("password")));

            var passwordInput = webDriver.FindElement(By.Name("password"));

            // Прокрутите страницу к элементу PasswordInput
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView();", passwordInput);

            // Дождитесь, когда поле ввода пароля станет видимым и доступным
            try
            {
                wait.Until(driver => passwordInput.Displayed && passwordInput.Enabled);
            }
            catch (WebDriverTimeoutException ex)
            {
                // Добавим отладочную информацию
                Console.WriteLine($"Timed out waiting for PasswordInput to be clickable. Exception: {ex}");
                throw; // Повторно вызываем исключение
            }

            passwordInput.SendKeys(password);

            webDriver.FindElement(By.Id("passwordNext")).Click();

        }
    }
}