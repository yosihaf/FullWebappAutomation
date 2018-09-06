﻿using OpenQA.Selenium.Remote;
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
            public static void SandboxLogin(RemoteWebDriver backofficeDriver, string username, string password)
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
                        SafeClick(backofficeDriver, "//div[@id='walkme-balloon-951840']/div/div/div[2]/div/div", maxRetry: 5);
                        Thread.Sleep(3000);

                        // Close first popup next
                        SafeClick(backofficeDriver, "//div/button/span[@class='walkme-custom-balloon-button-text']", maxRetry: 5);


                        //  Close last popup next
                        SafeClick(backofficeDriver, "//button[2]/span[@class='walkme-custom-balloon-button-text']", maxRetry: 5);
                    }
                    catch (Exception) { }

                    try
                    {
                        // Close  popup next
                        SafeClick(backofficeDriver, "//div[@class='walkme-custom-balloon-top-div-bottom']/div/button/span",maxRetry:4);
                        SafeClick(backofficeDriver, "//div[@class='walkme-custom-balloon-top-div-bottom']/div/button[2]/span",maxRetry: 4);
                    }
                    catch { }

                   
                    
                  

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
                    FullWebappAutomation.GlobalSettings.DanUsername = string.Format("automation{0}@pepperitest.com", indexForCreate);
                    FullWebappAutomation.GlobalSettings.DanPassword = "Aa123456";


                    // SING UP button
                    SafeClick(backofficeDriver, "/html[1]/body[1]/div[6]/div[2]/div[1]/div[1]");
                   

                    // Close first popup next
                    SafeClick(backofficeDriver, "//div/button/span[@class='walkme-custom-balloon-button-text']");


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
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Pricing']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='4']/label");
                SafeClick(backofficeDriver, "//div[@id='Pricing']/p[1]");
            }

            public static void Price_Level(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Pricing']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='4']/label");
                SafeClick(backofficeDriver, "//div[@id='Pricing']/p[2]");
            }

            public static void Main_Category_Discount(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Pricing']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='4']/label");
                SafeClick(backofficeDriver, "//div[@id='Pricing']/p[3]");
            }

            public static void Account_Special_Price_List(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Pricing']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='4']/label");
                SafeClick(backofficeDriver, "//div[@id='Pricing']/p[4]");
            }
        }

        internal class Users
        {
            public static void Manage_Users(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Users']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='5']/label");
                SafeClick(backofficeDriver, "//div[@id='Users']/p[1]");
            }

            public static void Role_Heirarchy(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Users']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='5']/label");
                SafeClick(backofficeDriver, "//div[@id='Users']/p[2]");
            }

            public static void Profiles(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Users']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='5']/label");
                SafeClick(backofficeDriver, "//div[@id='Users']/p[3]");
            }

            public static void User_Lists(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Users']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='5']/label");
                SafeClick(backofficeDriver, "//div[@id='Users']/p[4]");
            }

            public static void Targets_Type(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Users']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='5']/label");
                SafeClick(backofficeDriver, "//div[@id='Users']/p[5]");
            }

            public static void Manage_Targets(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Users']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='5']/label");
                SafeClick(backofficeDriver, "//div[@id='Users']/p[6]");
            }

            public static void Rep_Dashboard_Add_Ons(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Users']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='5']/label");
                SafeClick(backofficeDriver, "//div[@id='Users']/p[7]");
            }
        }

        internal class Contacts
        {
            public static void Contact_Lists(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Contacts']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@title='Contacts']/label");
                SafeClick(backofficeDriver, "//div[@id='Contacts']/p[1]");
            }
        }

        internal class SalesActivities
        {
            public static void Transaction_Types(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Orders']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='7']/label");
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[1]");
            }

            public static void Activity_Types(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Orders']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='7']/label");
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[2]");
            }

            public static void Sales_Activity_Lists(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Orders']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='7']/label");
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[3]");
            }

            public static void Activity_List_Display_Options(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Orders']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='7']/label");
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[4]");
            }

            public static void Activities_And_Menu_Setup(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Orders']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='7']/label");
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[5]");
            }

            public static void Sales_Dashboard_Settings(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='Orders']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='7']/label");
                SafeClick(backofficeDriver, "//div[@id='Orders']/p[6]");
            }
        }

        internal class ActivityPlanning
        {
            public static void Account_Lists(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='PlanningActivities']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='8']/label");
                SafeClick(backofficeDriver, "//div[@id='PlanningActivities']/p[1]");
            }

            public static void Activity_Planning_Display_Options(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='PlanningActivities']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='8']/label");
                SafeClick(backofficeDriver, "//div[@id='PlanningActivities']/p[2]");
            }
        }

        internal class ERPIntegration
        {
            public static void Plugin_Settings(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='ERPIntegration']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='9']/label");
                SafeClick(backofficeDriver, "//div[@id='ERPIntegration']/p[1]");
            }

            public static void Configuration(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='ERPIntegration']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='9']/label");
                SafeClick(backofficeDriver, "//div[@id='ERPIntegration']/p[2]");
            }

            public static void File_Uploads_And_Logs(RemoteWebDriver backofficeDriver)
            {
                SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
                if (SafeGetValue(backofficeDriver, "//div[@id='ERPIntegration']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='9']/label");
                SafeClick(backofficeDriver, "//div[@id='ERPIntegration']/p[3]");
            }
        }

        internal class ConfigurationFiles
        {
            public static void Automated_Reports(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='ConfigurationFiles']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='10']/label");
                SafeClick(backofficeDriver, "//div[@id='ConfigurationFiles']/p[1]");
            }

            public static void Configuration_Files(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='ConfigurationFiles']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='10']/label");
                SafeClick(backofficeDriver, "//div[@id='ConfigurationFiles']/p[2]");
            }

            public static void Translation_Files(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='ConfigurationFiles']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='10']/label");
                SafeClick(backofficeDriver, "//div[@id='ConfigurationFiles']/p[3]");
            }

            public static void Online_Add_Ons(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='ConfigurationFiles']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='10']/label");
                SafeClick(backofficeDriver, "//div[@id='ConfigurationFiles']/p[4]");
            }

            public static void User_Defined_Tables(RemoteWebDriver backofficeDriver)
            {
                Setting(backofficeDriver);
                if (SafeGetValue(backofficeDriver, "//div[@id='ConfigurationFiles']", "style").ToString().Contains("display: none;"))
                    SafeClick(backofficeDriver, "//h3[@id='10']/label");
                SafeClick(backofficeDriver, "//div[@id='ConfigurationFiles']/p[5]");
            }
        }
    }
}



