using OpenQA.Selenium;

namespace SeleniumTests.LocatorsBank;
public static class Locators
{
    public static By loginInputLocator => By.CssSelector("[name='username']");
    public static By passwordInputLocator => By.CssSelector("[name='password']");
    public static By loginButtonLocator => By.CssSelector("[type='submit']");
    public static By confirmInputLocator => By.CssSelector("#confirmStepSms");
    
    /*public static By sectionBonusesLocators => By.CssSelector("#expansion-panel-19");
    public static By bonusAccountLocators => By.XPath("//span[text()='Бонусный счёт']");
    public static By spendPointsButtonLocator => By.CssSelector("[appearance='primary']");
    public static By opeanAccountButtonLocator => By.XPath("//span[text(), 'Открыть счёт']");
    */
    
    public static By paymentsAndTranferButtonLocator => By.CssSelector(".desktop-menu__list li:nth-child(2)");
    public static By paymentsAndTranfersTitleLocator => By.CssSelector(".psb-title_type-title_3");
    public static By transferByPhoneNumberButtonLocator => By.XPath("//psb-text[text()='По номеру телефона']");
    public static By phoneNumberInputLocator => By.CssSelector("[type='text']");
    public static By submitPhoneNumberButtonLocator => By.CssSelector(".operations-item__title");
    public static By systemTransferTitleLocator => By.CssSelector(".psb-title_type-title_2");
    public static By choiceBankPSB => By.XPath("//psb-text[text()='ПСБ']");
    public static By byPhoneNumberInPSBTitleLocator => By.CssSelector(".operation-header__operation-name");

}