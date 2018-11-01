using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using static FullWebappAutomation.HelperFunctions;
using static FullWebappAutomation.Consts;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using  System.Windows.Forms;
using static FullWebappAutomation.GlobalSettings;

namespace FullWebappAutomation
{
    namespace Backoffice
    {
        

        internal class GeneralActions
        {
            static Random rnd = new Random();
            public static void SandboxLogin(RemoteWebDriver backofficeDriver, string username, string password,bool isF=false)
            {
                Exception error = null;
                bool testSuccess = true;

                try
                {
                    // Login page
                    backofficeDriver.Navigate().GoToUrl(backofficeSandboxLoginPageUrl);
                    backofficeDriver.Manage().Window.Maximize();

                    // Input credentials
                    SafeSendKeys(backofficeDriver, "//input[@type='email']", username);
                    SafeSendKeys(backofficeDriver, "//input[@type='password']", password);

                    // Login button
                    SafeClick(backofficeDriver, "//div[@id='loginBtn']");

                    // Close popup
                    try
                    {
                        SafeClick(backofficeDriver, "//div[@id='walkme-balloon-951840']/div/div/div[2]/div/div", safeWait: 500, maxRetry: 5);
                        Thread.Sleep(3000);
                    }
                    catch { }

                    try
                    {
                        // Close first popup next 
                        SafeClick(backofficeDriver, "//div/button/span[@class='walkme-custom-balloon-button-text']", safeWait: 500, maxRetry: 5);


                        //  Close last popup next
                        SafeClick(backofficeDriver, "//button[2]/span[@class='walkme-custom-balloon-button-text']", safeWait: 500, maxRetry: 5);
                    }
                    catch (Exception) { }

                    try
                    {
                        // Close  popup next
                        SafeClick(backofficeDriver, "//div[@class='walkme-custom-balloon-top-div-bottom']/div/button/span", safeWait: 500, maxRetry: 2);
                        SafeClick(backofficeDriver, "//div[@class='walkme-custom-balloon-top-div-bottom']/div/button[2]/span", safeWait: 500, maxRetry: 2);
                    }
                    catch { }

                   if(isF)
                    Setting(backofficeDriver,isF);


                }

                catch (Exception e)
                {
                    error = e;
                    testSuccess = false;
                }

                finally
                {
                    WriteToSuccessLog("Backoffice_Sandbox_Login", testSuccess, error);
                }

            }
            public static void SandboxCreateLog(RemoteWebDriver backofficeDriver)
            {
                Exception error = null;
                bool testSuccess = true;
                try
                {


                    // Login page
                    backofficeDriver.Navigate().GoToUrl(backofficeSandboxHomePageUrlFreeTrial);
                    backofficeDriver.Manage().Window.Maximize();


                    //   Switch To Frame
                    IWebElement detailFrame = backofficeDriver.FindElement(By.XPath("//iframe[@class='registration-frame']"));
                    backofficeDriver.SwitchTo().Frame(detailFrame);

                    // get number from json file
                    int indexForCreate = LoadFromNewLog();

                    // Input credentials
                    SafeSendKeys(backofficeDriver, "//html[1]/body[1]/div[6]/div[1]/input[1]", string.Format("automation{0} yosef{0}", indexForCreate));
                    SafeSendKeys(backofficeDriver, "/html[1]/body[1]/div[6]/div[1]/input[2]", string.Format("+972{0} yosef{0}", rnd.Next(1000000, 10000000)));
                    SafeSendKeys(backofficeDriver, "/html[1]/body[1]/div[6]/div[1]/input[3]", string.Format("automation{0}@pepperitest.com", indexForCreate));
                    SafeSendKeys(backofficeDriver, "/html[1]/body[1]/div[6]/div[1]/input[4]", string.Format("automation{0}test", indexForCreate));
                    SafeSendKeys(backofficeDriver, "/html[1]/body[1]/div[6]/div[1]/input[6]", "Aa123456");


                    // 
                    FullWebappAutomation.GlobalSettings.Username = string.Format("automation{0}@pepperitest.com", indexForCreate);
                    FullWebappAutomation.GlobalSettings.Password = "Aa123456";


                    // SING UP button
                    SafeClick(backofficeDriver, "/html[1]/body[1]/div[6]/div[2]/div[1]/div[1]");
                   

                    // Close first popup next
                    SafeClick(backofficeDriver, "//div/button/span[@class='walkme-custom-balloon-button-text']",safeWait: 2000,maxRetry: 100);


                    //  Close last popup next
                    SafeClick(backofficeDriver, "//button[2]/span[@class='walkme-custom-balloon-button-text']");


                    // Select Country
                    SafeClick(backofficeDriver, "//select[@id='country']");


                    // Choose israel 
                    SafeClick(backofficeDriver, "//option[@value='IL']");

                    // Save and Next button
                    SafeClick(backofficeDriver, "//div[@id='btnSaveAndNext']");

                    // Choose imeg
                    SafeClick(backofficeDriver, "//div[@class='imageCont indust_Home']");
                     
                    // Save and Next button
                    SafeClick(backofficeDriver, "//div[@id='btnSaveAndNext']");

                    try
                    {
                        SafeClick(backofficeDriver, "//div[@id='walkme-menu']/div[@title='Close']");
                    }
                    catch { }

                    // Click next in pop up
                    SafeClick(backofficeDriver, "//div/div/div/div/div/button/span",safeWait: 2000, maxRetry: 40);

                    Var_Sandbox_Enable_New_List_Account(Username);
                }
                catch (Exception e)
                {
                    error = e;
                    testSuccess = false;
                }
                finally
                {
                    WriteToSuccessLog("Backoffice_Sandbox_Creat_Log", testSuccess, error);
                }
            }
        }

        internal class CompanyProfile
        {
            public static void Branding(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='CompanyProfile']", "style").ToString().Contains("display: none;"))
                {
                    SafeClick(backofficeDriver, "//h3[@title='Company Profile']/label");
                }

                SafeClick(backofficeDriver, "//div[@id='CompanyProfile']/p[@id='Branding']");
            }

            public static void Company_Profile(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='CompanyProfile']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Company Profile']/label");
                SafeClick(backofficeDriver, "//div[@id='CompanyProfile']/p[@id='Settings_CompanyProfile']");
            }

            public static void Sync_Settings(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='CompanyProfile']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Company Profile']/label");
                SafeClick(backofficeDriver, "//div[@id='CompanyProfile']/p[@id='Settings_ScheduledSync']");
            }

            public static void Email_Settings(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='CompanyProfile']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Company Profile']/label");
                SafeClick(backofficeDriver, "//div[@id='CompanyProfile']/p[@id='Settings_SMTP']");
            }

            public static void Security(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='CompanyProfile']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Company Profile']/label");
                SafeClick(backofficeDriver, "//div[@id='CompanyProfile']/p[@id='Settings_PasswordPolicy']");
            }

            public static void App_Home_Screen(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='CompanyProfile']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Company Profile']/label");
                SafeClick(backofficeDriver, "//div[@id='CompanyProfile']/p[@id='Settings_AppHomePage']");
            }

            public static void Home_Screen_Shortcut(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='CompanyProfile']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Company Profile']/label");
                SafeClick(backofficeDriver, "//div[@id='CompanyProfile']/p[@id='Settings_FastButton']");
            }
        }

        internal class Catalogs
        {
            public static void Manage_Catalogs(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='Catalogs']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Catalogs']/label");
                SafeClick(backofficeDriver, "//div[@id='Catalogs']/p[@id='AddCatalog']");
            }

            public static void Edit_Form(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='Catalogs']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Catalogs']/label");
                SafeClick(backofficeDriver, "//div[@id='Catalogs']/p[@id='CatalogViewsAndForms']");
            }

            public static void Catalog_Views(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='Catalogs']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Catalogs']/label");
                SafeClick(backofficeDriver, "//div[@id='Catalogs']/p[@id='SelectCatalogView']");
            }

            public static void Fields(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='Catalogs']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Catalogs']/label");
                SafeClick(backofficeDriver, "//div[@id='Catalogs']/p[@id='CatalogCustomization']");
            }
        }

        internal class Items
        {
            public static void Item(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='Items']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Items']/label");
            }
            public static void Order_Center_Thumbnail_Views(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='OrderCenterItemThumbnailView']");
            }

            public static void Order_Center_Grid_View(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='OrderCenterListView']");
            }

            public static void Order_Center_Matrix_View(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='OrderCenterMatrixView']");
            }

            public static void Order_Center_Flat_Matrix_View(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='OrderCenterFlatMatrixView']");
            }

            public static void Order_Center_Item_Details_View(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='OrderCenterItemDetailsView']");
            }

            public static void Catalog_Item_View(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='CatalogView']");
            }

            public static void Item_Share_Email_Info(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='ItemShareInformation']");
            }

            public static void Smart_Search(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='SuperepSmartSearch3']");
            }

            public static void Filters(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='ItemsFilters']");
            }

            public static void Automated_Image_Uploader(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='ImageUploaderSetup']");
            }


            public static void VariantDimensions(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='Settings_VariantDimensions']");
            }

            public static void Fields(RemoteWebDriver backofficeDriver)
            {
                Item(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Items']/p[@id='ItemsDataCustomization']");
            }
        }

        internal class Accounts
        {
            public static void Account(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='Accounts']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Accounts']/label");
            }

            public static void Views_And_Forms(RemoteWebDriver backofficeDriver)
            {
                Account(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Accounts']/p[@id='Settings_AccountTypes']");
            }

            public static void Accounts_Lists(RemoteWebDriver backofficeDriver)
            {
                Account(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Accounts']/p[@id='AccountsList']");
            }

            public static void Accounts_Lists_New(RemoteWebDriver backofficeDriver)
            {
                Account(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Accounts']/p[@id='AccountGenericList']");
            }

            public static void Map_View(RemoteWebDriver backofficeDriver)
            {
                Account(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Accounts']/p[@id='MapView']");
            }

            public static void Card_Layout(RemoteWebDriver backofficeDriver)
            {
                Account(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Accounts']/p[@id='AccountCard']");
            }

            public static void Account_Dashboard_Layout(RemoteWebDriver backofficeDriver)
            {
                Account(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Accounts']/p[@id='AccountDashboardLayout']");
            }

            public static void Search(RemoteWebDriver backofficeDriver)
            {
                Account(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Accounts']/p[@id='SuperepSmartSearch20']");
            }

            public static void Smart_Search(RemoteWebDriver backofficeDriver)
            {
                Account(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Accounts']/p[@id='SuperepSmartSearch4']");
            }

            public static void Fields(RemoteWebDriver backofficeDriver)
            {
                Account(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Accounts']/p[@id='DataCustomization']");
            }
        }

        internal class PricingPolicy
        {

            public static void PricingPolicy_(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='Pricing']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='4']/label");
            }

            public static void Pricing_Policy(RemoteWebDriver backofficeDriver)
            {
                PricingPolicy_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Pricing']/p[@id='PricingPolicy']");
            }

            public static void Price_Level(RemoteWebDriver backofficeDriver)
            {
                PricingPolicy_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Pricing']/p[@id='PriceLevel']");
            }

            public static void Main_Category_Discount(RemoteWebDriver backofficeDriver)
            {
                PricingPolicy_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Pricing']/p[@id='MainCatDiscount']");
            }

            public static void Account_Special_Price_List(RemoteWebDriver backofficeDriver)
            {
                PricingPolicy_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Pricing']/p[4]");
            }
        }

        internal class Users
        {
            public static void User(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='Users']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='5']/label");
            }
                
            public static void Manage_Users(RemoteWebDriver backofficeDriver)
            {
                User(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Users']/p[@id='allUsers']");
            }

            public static void Role_Heirarchy(RemoteWebDriver backofficeDriver)
            {
                User(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Users']/p[@id='roleHierarchy']");
            }

            public static void Profiles(RemoteWebDriver backofficeDriver)
            {
                User(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Users']/p[@id='userProfile']");
            }

            public static void User_Lists(RemoteWebDriver backofficeDriver)
            {
                User(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Users']/p[@id='UserGenericList']");
            }

            public static void Targets_Type(RemoteWebDriver backofficeDriver)
            {
                User(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Users']/p[@id='TargetsType']");
            }

            public static void Manage_Targets(RemoteWebDriver backofficeDriver)
            {
                User(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Users']/p[@id='ManageTargets']");
            }

            public static void Rep_Dashboard_Add_Ons(RemoteWebDriver backofficeDriver)
            {
                User(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Users']/p[@id='SupeRepAgentDashboardMenu']");
            }
        }

        internal class Contacts
        {
            public static void Contact(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='Contacts']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Contacts']/label");
            }

            public static void Contact_Lists(RemoteWebDriver backofficeDriver)
            {
                Contact(backofficeDriver); 
                SafeClick(backofficeDriver, "//div[@id='Contacts']/p[@id='ContactPersonGenericList']");
            }

            public static void Contact_Form(RemoteWebDriver backofficeDriver)
            {
                Contact(backofficeDriver); 
                            SafeClick(backofficeDriver, "//div[@id='Contacts']/p[@id='ContactPersonForm']");
            }

            public static void Contacts_Fields(RemoteWebDriver backofficeDriver)
            {
                Contact(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Contacts']/p[@id='ContactsFields']");
            }

        }

        internal class SalesActivities
        {
            public static void SalesActivity(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='Orders']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='7']/label");
            }

            public static void Transaction_Types(RemoteWebDriver backofficeDriver)
            {
                SalesActivity(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[@id='OrderTypesMenu']");
            }

            public static void Activity_Types(RemoteWebDriver backofficeDriver)
            {
                SalesActivity(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[@id='ActivityTypes']");
            }

            public static void Sales_Activity_Lists(RemoteWebDriver backofficeDriver)
            {
                SalesActivity(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[@id='ActivityList']");
            }

            public static void Activity_List_Display_Options(RemoteWebDriver backofficeDriver)
            {
                SalesActivity(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[@id='ActivityListBar']");
            }

            public static void Activities_And_Menu_Setup(RemoteWebDriver backofficeDriver)
            {
                SalesActivity(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[@id='SupeRepActivityMenu']");
            }

            public static void Sales_Dashboard_Settings(RemoteWebDriver backofficeDriver)
            {
                SalesActivity(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[@id='SalesDashboardCust']");
            }
        }

        internal class ActivityPlanning
        {
            public static void ActivityPlanning_(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='PlanningActivities']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='8']/label");
            }

            public static void Account_Lists(RemoteWebDriver backofficeDriver)
            {
                ActivityPlanning_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='PlanningActivities']/p[@id='AccountsGridView']");
            }

            public static void Activity_Planning_Display_Options(RemoteWebDriver backofficeDriver)
            {
                ActivityPlanning_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='PlanningActivities']/p[@id='DailyPlanView']");
            }
        }

        internal class ERPIntegration
        {
            public static void ERP_Integration(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                try
                {
                    SafeClick(backofficeDriver, "//div/button/span[@class='walkme-custom-balloon-button-text']", safeWait: 100, maxRetry: 3);
                }
                catch { }
                if (SafeGetValue(backofficeDriver, "//div[@id='ERPIntegration']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='9']/label");
            }

            public static void Plugin_Settings(RemoteWebDriver backofficeDriver)
            {
                ERP_Integration(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='ERPIntegration']/p[@id='Settings_PluginSettings']");
            }

            public static void Configuration(RemoteWebDriver backofficeDriver)
            {
                ERP_Integration(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='ERPIntegration']/p[@id='ErpSetup']");
            }

            public static void File_Uploads_And_Logs(RemoteWebDriver backofficeDriver)
            {
                ERP_Integration(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='ERPIntegration']/p[@id='ErpFilesDetails']");
            }
        }

        internal class ConfigurationFiles
        {
            public static void Configuration_Files_(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='ConfigurationFiles']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='10']/label");
            }

            public static void Automated_Reports(RemoteWebDriver backofficeDriver)
            {
                Configuration_Files_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='ConfigurationFiles']/p[@id='AutomatedMail']");
            }

            public static void Configuration_Files(RemoteWebDriver backofficeDriver)
            {
                Configuration_Files_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='ConfigurationFiles']/p[@id='ConfigurationFiles']");
            }

            public static void Translation_Files(RemoteWebDriver backofficeDriver)
            {
                Configuration_Files_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='ConfigurationFiles']/p[@id='TranslationFiles']");
            }

            public static void Online_Add_Ons(RemoteWebDriver backofficeDriver)
            {
                Configuration_Files_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='ConfigurationFiles']/p[@id='OnlineActions']");
            }

            public static void User_Defined_Tables(RemoteWebDriver backofficeDriver)
            {
                Configuration_Files_(backofficeDriver);
                SafeClick(backofficeDriver, "//div[@id='ConfigurationFiles']/p[@id='MapData']");
            }
        }
    }
}



