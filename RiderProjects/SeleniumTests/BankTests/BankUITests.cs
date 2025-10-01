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
        driver.FindElement(Locators.Login.loginInputLocator).SendKeys("131349");
        driver.FindElement(Locators.Login.passwordInputLocator).SendKeys("1111111");
        wait.Until(ExpectedConditions.ElementExists(Locators.Login.loginButtonLocator));
        driver.FindElement(Locators.Login.loginButtonLocator).Click();
        
        // Ввести проверочный код
        driver.FindElement(Locators.Login.confirmInputLocator).SendKeys("111111");
        
        //Нажать на кнопку "Платежи и переводы" и проверить заголовок "Платежи и переводы"
        driver.FindElement(Locators.Account.paymentsAndTranferButtonLocator).Click();
        Assert.That(driver.FindElement(Locators.Account.paymentsAndTranfersTitleLocator).Text, Is.EqualTo("Платежи и переводы"));
        
        //Нажать на операцию перевода по номеру телефона, ввести номер телефона, нажать на кнопку с введенным телефоном
        driver.FindElement(Locators.Account.transferByPhoneNumberButtonLocator).Click();
        driver.FindElement(Locators.Account.phoneNumberInputLocator).SendKeys("9154561123");
        driver.FindElement(Locators.Account.submitPhoneNumberButtonLocator).Click();
        Assert.That(driver.FindElement(Locators.Account.systemTransferTitleLocator).Text, Is.EqualTo("Выбор системы перевода"));
        
        //
        driver.FindElement(Locators.Account.choiceBankPSB).Click();
        Assert.That(driver.FindElement(Locators.Account.byPhoneNumberInPSBTitleLocator).Text, Is.EqualTo("По номеру телефона в ПСБ"));
        
        Thread.Sleep(TimeSpan.FromSeconds(5));
    }
    
    [Test]
    public void OperationConvertPointInRubles()
    {
        using var driver = CreateDriver();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        driver.Navigate().GoToUrl("https://retail-tst.payment.ru/login");
        
        // Ввести логин и пароль клиента, нажать кнопку войти
        driver.FindElement(Locators.Login.loginInputLocator).SendKeys("131349");
        driver.FindElement(Locators.Login.passwordInputLocator).SendKeys("1111111");
        wait.Until(ExpectedConditions.ElementExists(Locators.Login.loginButtonLocator));
        driver.FindElement(Locators.Login.loginButtonLocator).Click();
        
        // Ввести проверочный код
        driver.FindElement(Locators.Login.confirmInputLocator).SendKeys("111111");
       
        // Раскрыть раздел "Бонусы", нажать на бонуcный счет
        driver.FindElement(Locators.ConvertRubles.sectionBonusesLocators).Click();
        driver.FindElement(Locators.ConvertRubles.bonusAccountLocators).Click();
        
        // Нажать на кнопку "Потратить баллы"; проверить, что карточка называется "Конвертация"; на витрине нажать кнопку "Перевести в рубли"
        driver.FindElement(Locators.ConvertRubles.spendPointsButtonLocator).Click();
        
        // Проверить, что карточка называется "Конвертация"; курс конвертации 1 балл = 1 Р; текст карточки корректный
        Assert.That(driver.FindElement(Locators.ConvertRubles.cardConvertTitleLocator).Text, Is.EqualTo("Конвертация"));
        Assert.That(driver.FindElement(Locators.ConvertRubles.conversionRateTitleLocator).Text, Is.EqualTo("1 балл = 1 ₽"));
        Assert.That(driver.FindElement(Locators.ConvertRubles.infoConvertTextLocator).Text, Is.EqualTo("Можно перевести баллы в рубли на карту или на счёт"));
        Assert.That(driver.FindElement(Locators.ConvertRubles.convertInRublesButtonLocator).Text, Is.EqualTo("Перевести в рубли"));
        
        // Нажать на кнопку "Перевести в рубли"
        driver.FindElement(Locators.ConvertRubles.convertInRublesButtonLocator).Click();
        
        // Дождаться появления заголовка операции; проверить заголовок, код операции
        wait.Until(ExpectedConditions.ElementExists(Locators.ConvertRubles.operationConvertBonusPointsTitleLocator));
        Assert.That(driver.FindElement(Locators.ConvertRubles.operationConvertBonusPointsTitleLocator).Text, Is.EqualTo("Перевод Бонусных баллов в рубли на счёт карты"));
        Assert.That(driver.FindElement(Locators.ConvertRubles.operationCodeConvertBonusPointsTitleLocator).Text, Is.EqualTo("код 612"));
        
        // Нажать на поле выбора карты, выбрать карту
        Thread.Sleep(TimeSpan.FromSeconds(2));
        driver.FindElement(Locators.ConvertRubles.paymentCardSelectionFieldLocator).Click();
        string numberCard = driver.FindElement(Locators.ConvertRubles.numberCardLocator).Text;
        Thread.Sleep(TimeSpan.FromSeconds(2));
        driver.FindElement(Locators.ConvertRubles.choiceCardButtonLocator).Click();
        
        // Стереть сумму из второго поля, ввести 1
        Assert.That(driver.FindElement(Locators.ConvertRubles.infoSumTextLocator).Text, Is.EqualTo("сумма в диапазоне от 500.00 до"));
        driver.FindElement(Locators.ConvertRubles.sumPointsInputLocator).Clear();
        driver.FindElement(Locators.ConvertRubles.sumPointsInputLocator).SendKeys("500");
        
        //Нажать кнопку "Продолжить"
        driver.FindElement(Locators.ConvertRubles.continueButtonLocator).Click();
        
        Assert.That(driver.FindElement(Locators.ConvertRubles.selectedNumberCardLocator).Text, Is.EqualTo(numberCard));
        
        Thread.Sleep(TimeSpan.FromSeconds(5));
    }
}