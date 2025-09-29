using System;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace SeleniumTests;

public class ActionsHomework
{
    private static IWebDriver CreateDriver()
    {
      var driver = new ChromeDriver();
      driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
      return driver;
    }
    
    [Test]
    public void Diadoc_OrderFromWidget_Success()
    {
        using var driver = CreateDriver();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        
        // Локатор кнопки “Попробовать”
        var tryButtonLocator = By.CssSelector("#try-button");

        // Локатор кнопки “Отправить заявку” внизу страницы
        var sendOrderButtonLocator = By.CssSelector("[data-long-text='Отправить заявку']");

        // Локатор Виджета Заявки в конце страницы
        var widgetFormLocator = By.CssSelector(".skb-form-widget");

        // Поле ввода Фамилии
        var surnameInputLocator = By.CssSelector("[name='b162ffe1-cac4-4bb5-af3b-be7c09dc3de2.surname']");

        // Поле ввода Имени
        var nameInputLocator = By.CssSelector("[name='b162ffe1-cac4-4bb5-af3b-be7c09dc3de2.name']");

        // Селект Региона
        var regionSelectLocator = By.CssSelector("[name='46dd3759-b5bf-4fbc-b637-1893c18d6935']");

        var regionUral = By.CssSelector("option:nth-child(66)");

        // Поле ввода Email
        var emailInputLocator = By.CssSelector("[type='email']");

        // Локатор ошибки, если ввели неверный Email
        var emailInputValidationErrorLocator = By.CssSelector("span[id$='-error']");

        // Поле ввода Телефона
        var phoneInputLocator = By.CssSelector("[type='tel']");

        // Поле ввода Организации
        var companyNameInputLocator = By.CssSelector("[data-field-role='company-name']");

        // Саджест Организации
        var organizationSuggestLocator = By.CssSelector(".organization-suggest-container");

        // Первый элемент в саджесте Организации
        var organizationSuggestFirstItemLocator = By.CssSelector(".autocomplete-suggestion.autocomplete-selected[data-index='0']");

        // Поле загрузки файла Список Контрагентов
        var contragentsFileUploaderInputLocator = By.CssSelector("input[type='file']");

        // Лейбл с названием загруженного файла
        var fileNameLabelLocator = By.CssSelector("[type='file']");

        // Чекбокс Связаться в определённый день
        var customDayCheckboxLocator = By.XPath("//label[contains(text(),'Связаться в определенный день')]");

        // Поле ввода Даты
        var datePickerLocator = By.CssSelector("[data-role='local-date']");

        // Кнопка “Отправить заявку”
        var submitButtonLocator = By.CssSelector("[data-tid='FWSubmitButton']");

        // Заголовок об успешной отправке “Заявка отправлена”
        var successMessageTitleLocator = By.CssSelector("[data-role='success-message-title']");
        
        // 1. Перейти на страницу https://kontur.ru/lp/qa-auto
        driver.Navigate().GoToUrl("https://kontur.ru/lp/qa-auto");
        
        // 2. Кликнуть по кнопке "Попробовать" в обложке
        driver.FindElement(tryButtonLocator).Click();
        
        // 3. Дождаться скролла вниз и нажать кнопку "Отправить заявку"
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(sendOrderButtonLocator));
        driver.FindElement(sendOrderButtonLocator).Click();
        
        // 4. Прокрутить страницу до "Виджета заявки"
        new Actions(driver)
            .MoveToElement(driver.FindElement(widgetFormLocator))
            .Build()
            .Perform();
        
        // 5. Дождаться появления "Виджета заявки"
        wait.Until(ExpectedConditions.ElementExists(widgetFormLocator));
        
        // 6. Заполнить поля "Фамилия" и "Имя"
        driver.FindElement(surnameInputLocator).SendKeys("Иванов");
        driver.FindElement(nameInputLocator).SendKeys("Иван");
        
        // 7. В селекте "Регион" выбрать "Свердловская область"
        driver.FindElement(regionSelectLocator).Click();
        driver.FindElement(regionSelectLocator).SendKeys("Свердловская область");
        new Actions(driver)
            .SendKeys(Keys.Tab)
            .Build()
            .Perform();
        
        // 8. В поле Электронная почта для связи ввести - невалидный email
        driver.FindElement(emailInputLocator).SendKeys("Ivan");

        // 9. Снять фокус (например, нажать Tab)
        new Actions(driver)
            .SendKeys(Keys.Tab)
            .Build()
            .Perform();
        
        // 10. Проверить, что появилась ошибка "Некорректный адрес электронной почты"
        Assert.AreEqual("Некорректный адрес электронной почты", driver.FindElement(emailInputValidationErrorLocator).Text);
        
        // 11. Очистить поле, ввести - валидный email
        driver.FindElement(emailInputLocator).SendKeys(Keys.Clear);
        driver.FindElement(emailInputLocator).SendKeys("ivan1251@gmail.com");
        
        // 12. Заполнить поле "Телефон для связи"
        driver.FindElement(phoneInputLocator).SendKeys("9128763455");
        
        // 13. В поле Ваша организация ввести Контур
        driver.FindElement(companyNameInputLocator).SendKeys("Контур");
        
        // 14. Дождаться появления Саджеста организаций
        wait.Until(ExpectedConditions.ElementExists(organizationSuggestLocator));
        
        // 15. Выбрать первый элемент из выпавшего списка
        driver.FindElement(organizationSuggestFirstItemLocator).Click();
        
        // 16. В поле Список контрагентов загрузить .docx файл
        driver.FindElement(contragentsFileUploaderInputLocator).SendKeys(@"C:\\Users\\Константин\\OneDrive\\Desktop\\Тест.docx");
        
        // 17. Дождаться появления лейбла с названием файла
        wait.Until(ExpectedConditions.ElementExists(fileNameLabelLocator));
        
        // 18. Поставить галочку в поле "Связаться в определённый день"
        driver.FindElement(customDayCheckboxLocator).Click();
        
        // 19. Дождаться появления поля Дата
        wait.Until(ExpectedConditions.ElementExists(datePickerLocator));
        
        // 20. Установить дату доставки = сегодня + 8 дней
        var currentDate = DateTime.Today.AddDays(8).ToString();
        driver.FindElement(datePickerLocator).SendKeys(currentDate);
        new Actions(driver)
            .SendKeys(Keys.Tab)
            .Build()
            .Perform();
        Thread.Sleep(500);
        
        // 21. Нажать кнопку Отправить
        driver.FindElement(submitButtonLocator).Click();
        
        // 22. Дождаться появления сообщение об успешно отправленной заявке
        wait.Until(ExpectedConditions.ElementExists(successMessageTitleLocator));
        Assert.AreEqual("Заявка отправлена!", driver.FindElement(successMessageTitleLocator).Text);
    }
}
