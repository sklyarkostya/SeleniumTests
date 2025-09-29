using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests;

public class LocatorsHomework
{
    private static IWebDriver CreateDriver()
    {
      var driver = new ChromeDriver();
      driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
      return driver;
    }

    [Test]
    public void Locators()
    {
        using var driver = CreateDriver();

        driver.Navigate().GoToUrl("https://kontur.ru/lp/qa-auto");

        // Локатор кнопки “Попробовать”
        var tryButton = driver.FindElement(By.CssSelector("#try-button"));

        // Локатор кнопки “Отправить заявку” внизу страницы
        var sendOrderButton = driver.FindElement(By.CssSelector("[data-long-text='Отправить заявку']"));

        driver.Navigate().GoToUrl("https://kontur.ru/lp/qa-auto-integration");

        // Локатор Виджета Заявки в конце страницы
        var widgetForm = driver.FindElement(By.CssSelector(".skb-form-widget"));

        // Поле ввода Фамилии
        var surnameInput = driver.FindElement(By.CssSelector("[name='b162ffe1-cac4-4bb5-af3b-be7c09dc3de2.surname']"));

        // Поле ввода Имени
        var nameInput = driver.FindElement(By.CssSelector("[name='b162ffe1-cac4-4bb5-af3b-be7c09dc3de2.name']"));

        // Селект Региона
        var regionSelect = driver.FindElement(By.CssSelector("[name='46dd3759-b5bf-4fbc-b637-1893c18d6935']"));

        // Поле ввода Email
        var emailInput = driver.FindElement(By.CssSelector("[type='email']"));

        // Локатор ошибки, если ввели неверный Email
        var emailInputValidationError = driver.FindElement(By.CssSelector("#fw_field_1e2aa95c0c404ed89ee5a6f66636f362_12bd0e8a3af146fb8cac695ffa93b40a-error"));

        // Поле ввода Телефона
        var phoneInput = driver.FindElement(By.CssSelector("[type='tel']"));

        // Поле ввода Организации
        var companyNameInput = driver.FindElement(By.CssSelector("[data-field-role='company-name']"));

        // Саджест Организации
        var organizationSuggest = driver.FindElement(By.CssSelector(".organization-suggest-container"));

        // Первый элемент в саджесте Организации
        var organizationSuggestFirstItem = driver.FindElement(By.CssSelector(".autocomplete-suggestion.autocomplete-selected[data-index='0']"));

        // Поле загрузки файла Список Контрагентов
        var contragentsFileUploaderInput = driver.FindElement(By.CssSelector("/* локатор */"));

        // Лейбл с названием загруженного файла
        var fileNameLabel = driver.FindElement(By.CssSelector("[type='file']"));

        // Чекбокс Связаться в определённый день
        var customDayCheckbox = driver.FindElement(By.CssSelector("[for='fw_field_66640d792e75494789d6c4da4089519e_12bd0e8a3af146fb8cac695ffa93b40a']"));

        // Поле ввода Даты
        var datePicker = driver.FindElement(By.CssSelector("[data-role='local-date']"));

        // Кнопка “Отправить заявку”
        var submitButton = driver.FindElement(By.CssSelector("[data-tid='FWSubmitButton']"));

        // Заголовок об успешной отправке “Заявка отправлена”
        var successMessageTitle = driver.FindElement(By.CssSelector("[data-role='success-message-title']"));
    }
}