namespace Origin.Seed.Data
{
    internal class BaseLetterTemplateContent
    {
        public const string LOGO_CONTENT = "logo.png";
        public const string SIGNATURE_CONTENT = "signature.png";
        public const string COMPANY_NAME_CONTENT = $"[{CustomerProductLetterSubstitutes.COMPANY_NAME}]";
        public const string COMPANY_ADDRESS_1_CONTENT = $"[{CustomerProductLetterSubstitutes.COMPANY_ADDRESS_1}]";
        public const string COMPANY_ADDRESS_2_CONTENT = $"[{CustomerProductLetterSubstitutes.COMPANY_ADDRESS_2}]";
        public const string COMPANY_ADDRESS_3_CONTENT = $"[{CustomerProductLetterSubstitutes.COMPANY_ADDRESS_3}]";
        public const string COMPANY_PHONE_NUMBER_CONTENT = $"[{CustomerProductLetterSubstitutes.COMPANY_PHONE_NUMBER}]";
        public const string COMPANY_EMAIL_CONTENT = $"[{CustomerProductLetterSubstitutes.COMPANY_EMAIL}]";
        public const string SIGNEE_CONTENT = $"[{CustomerProductLetterSubstitutes.SIGNEE}]";
        public const string SIGNEE_TITLE_CONTENT = $"[{CustomerProductLetterSubstitutes.SIGNEE_TITLE}]";

        public const string CUSTOMER_NAME_CONTENT = $"[{CustomerProductLetterSubstitutes.CUSTOMER_TITLE}] [{CustomerProductLetterSubstitutes.CUSTOMER_FIRST_NAME}] [{CustomerProductLetterSubstitutes.CUSTOMER_SURNAME}]";
        public const string CUSTOMER_ADDRESS_1_CONTENT = $"[{CustomerProductLetterSubstitutes.CUSTOMER_ADDRESS_1}]";
        public const string CUSTOMER_ADDRESS_2_CONTENT = $"[{CustomerProductLetterSubstitutes.CUSTOMER_ADDRESS_2}]";
        public const string CUSTOMER_ADDRESS_3_CONTENT = $"[{CustomerProductLetterSubstitutes.CUSTOMER_ADDRESS_3}]";
        public const string CUSTOMER_ADDRESS_4_CONTENT = $"[{CustomerProductLetterSubstitutes.CUSTOMER_ADDRESS_4}]";
        public const string CUSTOMER_ADDRESS_5_CONTENT = $"[{CustomerProductLetterSubstitutes.CUSTOMER_ADDRESS_5}]";
        public const string CUSTOMER_ADDRESS_6_CONTENT = $"[{CustomerProductLetterSubstitutes.CUSTOMER_ADDRESS_6}]";
        public const string CUSTOMER_ADDRESS_7_CONTENT = $"[{CustomerProductLetterSubstitutes.CUSTOMER_ADDRESS_7}]";
        public const string LETTER_TITLE_CONTENT = $"Customer Account [{CustomerProductLetterSubstitutes.CUSTOMER_SORT_CODE}]/[{CustomerProductLetterSubstitutes.CUSTOMER_ACCOUNT_NUMBER}]";

        public const string FOOTER_CONTENT = "Footers are a no-nonsense part of a letter that presents practical information that would be out of place anywhere else. Footers provide a place to put all the necessary contact information like the physical address, phone number, email, and other details someone may need to know. They may also contain legal information and a copyright symbol. Footers communicate relevant information, and can be a final call to action, and leave you with one last impression.";
        public const string LOREM_IPSUM_IS_NONSENSE_MICROSOFT = "The phrase \"Lorem ipsum dolor sit amet consectetuer\" has the appearance of an intelligent Latin idiom. Actually, it is nonsense. Although the phrase is nonsense, it does have a long history. The phrase has been used for several centuries by typographers to show the most distinctive features of their fonts. It is used because the letters involved and the letter spacing in those combinations reveal, at their best, the weight, design, and other important features of the typeface. During the 1500s, a printer adapted Cicero's text to develop a page of type samples. Since then, the Latin-like text has been the printing industry's standard for fake, or dummy, text. Before electronic publishing, graphic designers had to mock up layouts by drawing in squiggled lines to indicate text. The advent of self-adhesive sheets preprinted with \"Lorem ipsum\" gave a more realistic way to indicate where text would go on a page.";
    }
}
