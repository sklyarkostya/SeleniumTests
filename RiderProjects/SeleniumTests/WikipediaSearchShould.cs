using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumTests;

public class WikipediaSearchShould
{
    private static IWebDriver CreateDriver()
    {
        var driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        return driver;
    }
    
    [Test]
    public void BeReturnSeleniumPage_WhenFindSelenium()
    {
        // --- Запустить браузер
        using var driver = CreateDriver();

        // --- Выполнить тест

        // 1. Открыть сайт https://en.wikipedia.org/
        driver.Navigate().GoToUrl("https://en.wikipedia.org/");

        // 2. Ввести в поле поиска текст: Selenium (software)
        driver.FindElement(By.Name("search")).SendKeys("Selenium (software)");

        // 3. Нажать кнопку поиска (значок лупы)
        driver.FindElement(By.CssSelector("button.cdx-button.cdx-button--action-default.cdx-button--weight-normal.cdx-button--size-medium.cdx-button--framed.cdx-search-input__end-button")).Click();

        // --- Проверить результат

        // Ожидаемый результат: заголовок страницы — "Selenium (software)"
        var heading = driver.FindElement(By.Id("firstHeading")).Text;
        Assert.That(heading, Is.EqualTo("Selenium (software)"), "Заголовок страницы не совпадает с ожидаемым.");

        // --- Закрыть браузер
        driver.Quit();
    }
}