using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.LocatorsBank;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests;

public class BankUITests
{
    private static IWebDriver CreateDriver()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument(@"--incognito");
        options.AddArgument("--start-maximized");
        options.AddArgument("--ignore-certificate-errors");
        var driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        return driver;
    }
    
    [Test]
    public void OperationTransferByPhoneNumber()
    {
        using var driver = CreateDriver();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        driver.Navigate().GoToUrl("https://retail-tst.payment.ru/login");
        
        // Ввести логин и пароль клиента, нажать кнопку войти
        driver.FindElement(Locators.loginInputLocator).SendKeys("131349");
        driver.FindElement(Locators.passwordInputLocator).SendKeys("1111111");
        wait.Until(ExpectedConditions.ElementExists(Locators.loginButtonLocator));
        driver.FindElement(Locators.loginButtonLocator).Click();
        
        // Ввести проверочный код
        driver.FindElement(Locators.confirmInputLocator).SendKeys("111111");
        /*
        // Раскрыть раздел "Бонусы", нажать на бонуcный счет
        //driver.FindElement(Locators.sectionBonusesLocators).Click();
        driver.FindElement(Locators.bonusAccountLocators).Click();
        
        //Нажать на кнопку "Потратить баллы"
        driver.FindElement(Locators.spendPointsButtonLocator).Click();
        */
        
        //Нажать на кнопку "Платежи и переводы" и проверить заголовок "Платежи и переводы"
        driver.FindElement(Locators.paymentsAndTranferButtonLocator).Click();
        Assert.That(driver.FindElement(Locators.paymentsAndTranfersTitleLocator).Text, Is.EqualTo("Платежи и переводы"));
        
        //Нажать на операцию перевода по номеру телефона, ввести номер телефона, нажать на кнопку с введенным телефоном
        driver.FindElement(Locators.transferByPhoneNumberButtonLocator).Click();
        driver.FindElement(Locators.phoneNumberInputLocator).SendKeys("9154561123");
        driver.FindElement(Locators.submitPhoneNumberButtonLocator).Click();
        Assert.That(driver.FindElement(Locators.systemTransferTitleLocator).Text, Is.EqualTo("Выбор системы перевода"));
        
        //
        driver.FindElement(Locators.choiceBankPSB).Click();
        Assert.That(driver.FindElement(Locators.byPhoneNumberInPSBTitleLocator).Text, Is.EqualTo("По номеру телефона в ПСБ"));
        
        Thread.Sleep(TimeSpan.FromSeconds(5));
    }
}