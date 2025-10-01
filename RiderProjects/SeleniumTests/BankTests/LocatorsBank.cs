using OpenQA.Selenium;

namespace SeleniumTests.LocatorsBank;
public static class Locators
{
    public class Login
    {
        public static By loginInputLocator => By.CssSelector("[name='username']");
        public static By passwordInputLocator => By.CssSelector("[name='password']");
        public static By loginButtonLocator => By.CssSelector("[type='submit']");
        public static By confirmInputLocator => By.CssSelector("#confirmStepSms");
    }
    public class Account
    {
        public static By opeanAccountButtonLocator => By.XPath("//span[text(), 'Открыть счёт']");
        public static By paymentsAndTranferButtonLocator => By.CssSelector(".desktop-menu__list li:nth-child(2)");
        public static By paymentsAndTranfersTitleLocator => By.CssSelector(".psb-title_type-title_3");
        public static By transferByPhoneNumberButtonLocator => By.XPath("//psb-text[text()='По номеру телефона']");
        public static By phoneNumberInputLocator => By.CssSelector("[type='text']");
        public static By submitPhoneNumberButtonLocator => By.CssSelector(".operations-item__title");
        public static By systemTransferTitleLocator => By.CssSelector(".psb-title_type-title_2");
        public static By choiceBankPSB => By.XPath("//psb-text[text()='ПСБ']");
        public static By byPhoneNumberInPSBTitleLocator => By.CssSelector(".operation-header__operation-name");
    }
    public class ConvertRubles
    {
        public static By sectionBonusesLocators => By.CssSelector("rtl-sidebar-product-block:nth-child(8)");
        public static By bonusAccountLocators => By.XPath("//span[text()='Бонусный счёт']");
        public static By spendPointsButtonLocator => By.CssSelector("[appearance='primary']");
        public static By cardConvertTitleLocator => By.CssSelector("rtl-account-showcase-card:last-child .psb-title");
        public static By conversionRateTitleLocator => By.CssSelector("rtl-account-showcase-card:last-child .psb-status");
        public static By infoConvertTextLocator => By.CssSelector("rtl-account-showcase-card:last-child .psb-text");
        public static By convertInRublesButtonLocator => By.CssSelector("rtl-account-showcase-card:last-child .button");
        public static By operationConvertBonusPointsTitleLocator => By.CssSelector(".operation-header__operation-name");
        public static By operationCodeConvertBonusPointsTitleLocator => By.CssSelector(".operation-header__operation-code");
        public static By paymentCardSelectionFieldLocator => By.CssSelector(".money-source-form");
        public static By choiceCardButtonLocator => By.CssSelector("mat-option:first-child");
        public static By numberCardLocator => By.CssSelector("mat-option:first-child .money-source-select-option__info-account");
        public static By sumPointsInputLocator => By.CssSelector(".form-input-element");
        public static By infoSumTextLocator => By.CssSelector(".invalid-feedback-message__text");
        public static By continueButtonLocator => By.XPath("//span[contains(text(),'Продолжить')]");
        public static By selectedNumberCardLocator => By.XPath("//div[contains(@class,'operation-confirm-param')][div[contains(text(),'Номер карты')]]/div[@class='operation-confirm-param__value']");
    }
}