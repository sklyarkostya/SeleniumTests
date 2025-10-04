using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.LocatorsBank;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.BankTests;

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
    /*
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
    */
    public void Login(IWebDriver driver, WebDriverWait wait, string login, string password, string confirmCode)
    {
        // Ввести номер клиента, пароль и нажать кнопку Войти
        driver.FindElement(Locators.Login.loginInputLocator).SendKeys(login);
        driver.FindElement(Locators.Login.passwordInputLocator).SendKeys(password);
        wait.Until(ExpectedConditions.ElementExists(Locators.Login.loginButtonLocator));
        driver.FindElement(Locators.Login.loginButtonLocator).Click();
        
        // Ввести проверочный код
        driver.FindElement(Locators.Login.confirmInputLocator).SendKeys(confirmCode);
    }
    
    [Test]
    public void OperationConvertPointInRubles()
    {
        using var driver = CreateDriver();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        driver.Navigate().GoToUrl("https://retail-tst.payment.ru/login");
        
        // Авторизация
        Login(driver, wait, "131349", "1111111", "111111");
       
        // Раскрыть раздел "Бонусы", нажать на бонуcный счет
        driver.FindElement(Locators.ConvertRubles.sectionBonusesLocators).Click();
        driver.FindElement(Locators.ConvertRubles.bonusAccountLocators).Click();
        string balancePoints = driver.FindElement(Locators.ConvertRubles.balancePointsLocators).Text.Replace(" ", "").Replace(",", ".");
        balancePoints = StringFormatter.CleanInvisible(balancePoints);
        
        // Нажать на кнопку "Потратить баллы"; на витрине нажать кнопку "Перевести в рубли"
        driver.FindElement(Locators.ConvertRubles.spendPointsButtonLocator).Click();
        driver.FindElement(Locators.ConvertRubles.convertInRublesButtonLocator).Click();
        
        // Дождаться появления заголовка операции; проверить заголовок, код операции; сверить баланс 
        wait.Until(ExpectedConditions.ElementExists(Locators.ConvertRubles.operationConvertBonusPointsTitleLocator));
        Assert.That(driver.FindElement(Locators.ConvertRubles.operationConvertBonusPointsTitleLocator).Text, Is.EqualTo("Перевод Бонусных баллов в рубли на счёт карты"));
        Assert.That(driver.FindElement(Locators.ConvertRubles.operationCodeConvertBonusPointsTitleLocator).Text, Is.EqualTo("код 612"));
        Assert.That(StringFormatter.CleanInvisible(driver.FindElement(Locators.ConvertRubles.balancePouintsInOperationLocators).Text), Is.EqualTo(balancePoints));
        
        // Нажать на поле выбора карты, выбрать карту
        Thread.Sleep(TimeSpan.FromSeconds(1));
        driver.FindElement(Locators.ConvertRubles.paymentCardSelectionFieldLocator).Click();
        
        string numberCard = driver.FindElement(Locators.ConvertRubles.numberCardLocator).Text;
        
        Thread.Sleep(TimeSpan.FromSeconds(1));
        driver.FindElement(Locators.ConvertRubles.choiceCardButtonLocator).Click();
        
        balancePoints = balancePoints.Remove(balancePoints.LastIndexOf("."));
        balancePoints = StringFormatter.FormatWithSpaces(balancePoints);
       
        string balanceInString = driver.FindElement(Locators.ConvertRubles.infoSumTextLocator).Text;
        balanceInString = balanceInString.Remove(balanceInString.LastIndexOf("."));
        
        var minSum = StringFormatter.GetFirstIntegerPart(balanceInString);
        
        // Проверить подсказку под полем суммы; очистить поле суммы
        Assert.That(balanceInString, Is.EqualTo($"сумма в диапазоне от {minSum}.00 до {balancePoints}"));
        driver.FindElement(Locators.ConvertRubles.sumPointsInputLocator).Clear();
        
        // Ввести в поле суммы минимальную сумму
        driver.FindElement(Locators.ConvertRubles.sumPointsInputLocator).SendKeys(minSum);
        
        // Нажать кнопку "Продолжить"
        driver.FindElement(Locators.ConvertRubles.continueButtonLocator).Click();
        
        // Проверить номер карты на корректность и проверить сумму
        Assert.That(driver.FindElement(Locators.ConvertRubles.selectedNumberCardLocator).Text.Substring(8), Is.EqualTo(numberCard.Substring(2)));
        Assert.That(driver.FindElement(Locators.ConvertRubles.confirmSumPointsLocators).Text, Is.EqualTo(minSum));
        
        // Подтвердить операцию
        driver.FindElement(Locators.ConvertRubles.confirmFinalButtonLocators).Click();
        
        // Проверить заголовок, номер карты на корректность и проверить сумму
        Assert.That(driver.FindElement(Locators.ConvertRubles.completedOperationTitleLocator).Text, Is.EqualTo("Операция выполнена"));
        Assert.That(driver.FindElement(Locators.ConvertRubles.selectedNumberCardLocator).Text.Substring(8), Is.EqualTo(numberCard.Substring(2)));
        Assert.That(driver.FindElement(Locators.ConvertRubles.confirmSumPointsLocators).Text, Is.EqualTo(minSum));
    }
}