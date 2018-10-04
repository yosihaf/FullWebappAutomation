﻿using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using static FullWebappAutomation.GlobalSettings;
using static FullWebappAutomation.HelperFunctions;
using static FullWebappAutomation.Consts;
using System.Threading;
using System.Linq;
using System.Drawing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FullWebappAutomation
{
    class Tests
    {
        public static void All_Backoffice_Menus(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Backoffice.GeneralActions.SandboxLogin(backofficeDriver, DanUsername, DanPassword);

            Backoffice.CompanyProfile.App_Home_Screen(backofficeDriver);
            Backoffice.CompanyProfile.Branding(backofficeDriver);
            Backoffice.CompanyProfile.Company_Profile(backofficeDriver);
            Backoffice.CompanyProfile.Email_Settings(backofficeDriver);
            Backoffice.CompanyProfile.Home_Screen_Shortcut(backofficeDriver);
            Backoffice.CompanyProfile.Security(backofficeDriver);
            Backoffice.CompanyProfile.Sync_Settings(backofficeDriver);

            Backoffice.Catalogs.Manage_Catalogs(backofficeDriver);
            Backoffice.Catalogs.Edit_Form(backofficeDriver);
            Backoffice.Catalogs.Catalog_Views(backofficeDriver);
            Backoffice.Catalogs.Fields(backofficeDriver);

            Backoffice.Items.Order_Center_Thumbnail_Views(backofficeDriver);
            Backoffice.Items.Order_Center_Grid_View(backofficeDriver);
            Backoffice.Items.Order_Center_Matrix_View(backofficeDriver);
            Backoffice.Items.Order_Center_Flat_Matrix_View(backofficeDriver);
            Backoffice.Items.Order_Center_Item_Details_View(backofficeDriver);
            Backoffice.Items.Catalog_Item_View(backofficeDriver);
            Backoffice.Items.Item_Share_Email_Info(backofficeDriver);
            Backoffice.Items.Smart_Search(backofficeDriver);
            Backoffice.Items.Filters(backofficeDriver);
            Backoffice.Items.Automated_Image_Uploader(backofficeDriver);
            Backoffice.Items.Fields(backofficeDriver);

            Backoffice.Accounts.Views_And_Forms(backofficeDriver);
            Backoffice.Accounts.Accounts_Lists(backofficeDriver);
            Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);
            Backoffice.Accounts.Map_View(backofficeDriver);
            Backoffice.Accounts.Card_Layout(backofficeDriver);
            Backoffice.Accounts.Account_Dashboard_Layout(backofficeDriver);
            Backoffice.Accounts.Search(backofficeDriver);
            Backoffice.Accounts.Smart_Search(backofficeDriver);
            Backoffice.Accounts.Fields(backofficeDriver);

            Backoffice.PricingPolicy.Pricing_Policy(backofficeDriver);
            Backoffice.PricingPolicy.Price_Level(backofficeDriver);
            Backoffice.PricingPolicy.Main_Category_Discount(backofficeDriver);
            Backoffice.PricingPolicy.Account_Special_Price_List(backofficeDriver);

            Backoffice.Users.Manage_Users(backofficeDriver);
            Backoffice.Users.Role_Heirarchy(backofficeDriver);
            Backoffice.Users.Profiles(backofficeDriver);
            Backoffice.Users.User_Lists(backofficeDriver);
            Backoffice.Users.Targets_Type(backofficeDriver);
            Backoffice.Users.Manage_Targets(backofficeDriver);
            Backoffice.Users.Rep_Dashboard_Add_Ons(backofficeDriver);

            Backoffice.Contacts.Contact_Lists(backofficeDriver);

            Backoffice.SalesActivities.Transaction_Types(backofficeDriver);
            Backoffice.SalesActivities.Activity_Types(backofficeDriver);
            Backoffice.SalesActivities.Sales_Activity_Lists(backofficeDriver);
            Backoffice.SalesActivities.Activity_List_Display_Options(backofficeDriver);
            Backoffice.SalesActivities.Activities_And_Menu_Setup(backofficeDriver);
            Backoffice.SalesActivities.Sales_Dashboard_Settings(backofficeDriver);

            Backoffice.ActivityPlanning.Account_Lists(backofficeDriver);
            Backoffice.ActivityPlanning.Activity_Planning_Display_Options(backofficeDriver);

            Backoffice.ERPIntegration.Plugin_Settings(backofficeDriver);
            Backoffice.ERPIntegration.Configuration(backofficeDriver);
            Backoffice.ERPIntegration.File_Uploads_And_Logs(backofficeDriver);

            Backoffice.ConfigurationFiles.Automated_Reports(backofficeDriver);
            Backoffice.ConfigurationFiles.Configuration_Files(backofficeDriver);
            Backoffice.ConfigurationFiles.Translation_Files(backofficeDriver);
            Backoffice.ConfigurationFiles.Online_Add_Ons(backofficeDriver);
            Backoffice.ConfigurationFiles.User_Defined_Tables(backofficeDriver);
        }

        public static void Webapp_Sandbox_Login(RemoteWebDriver webappDriver, string username, string password)
        {
            Exception error = null;
            bool testSuccess = true;

            try
            {
                // Login page
                webappDriver.Navigate().GoToUrl(webappSandboxLoginPageUrl);
                webappDriver.Manage().Window.Maximize();

                // Input credentials
                SafeSendKeys(webappDriver, "//input[@type='email']", username);
                SafeSendKeys(webappDriver, "//input[@type='password']", password);

                // Login button
                SafeClick(webappDriver, "//button[@type='submit']");


            }
            catch (Exception e)
            {
                error = e;
                testSuccess = false;
            }
            finally
            {
                WriteToSuccessLog("Webapp_Sandbox_Login", testSuccess, error);
            }
        }

        public static void Webapp_Sandbox_Resync(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            // Add "/rec" to url
            if (!(webappDriver.Url.Contains("/supportmenu")))
                webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl + "/supportmenu");

            // Click "Resync"
            SafeClick(webappDriver, "/html/body/app-root/div/app-home-page/app-user-helper/div/nav/div/div/ul/li[2]/a");

            // Start Resyncing
            SafeClick(webappDriver, "/html/body/app-root/div/app-home-page/app-user-helper/div/nav/div/div/ul/li[2]/ul/li/a");
            Thread.Sleep(3000);
        }

        // Checked with "Sales Order"
        public static void Webapp_Sandbox_Sales_Order(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Dictionary to store order info to assert later
            Dictionary<string, string> orderInfo = new Dictionary<string, string>();

            GetToOrderCenter_SalesOrder(webappDriver);

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // Item qty plus
            SafeClick(webappDriver,
               "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/span[2]/i");

            Thread.Sleep(bufferTime);

            // Get units qty from qty selector
            orderInfo["unitsQty"] = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/input", "innerHTML");

            // Cart
            SafeClick(webappDriver, "//button[@id='goToCartBtn']/span");

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li[2]");
            Thread.Sleep(bufferTime);

            // Transaction Menu
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/a/i");

            // Order details
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/ul/li/span");

            // Create remark and store it
            orderInfo["orderRemark"] = "Automation " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            // Click remark field
            SafeClick(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[15]/div/app-custom-field-generator/app-custom-textbox/div/input");

            // Add remark
            SafeSendKeys(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[15]/div/app-custom-field-generator/app-custom-textbox/div/input", orderInfo["orderRemark"]);

            // Save button
            SafeClick(webappDriver, "//body/app-root/div/app-order-details/app-bread-crumbs/div/div/div/div[3]/div[2]");

            // Submit
            SafeClick(webappDriver, "//button[@id='btnTransition']/span");

            // Home button
            SafeClick(webappDriver, "//a[@id='btnMenuHome']/span");

            try
            {
                SafeClick(webappDriver, "//div[@class='btn allButtons btnOk grnbtn ng-star-inserted']");
                Webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            }
            catch { }

            Webapp_Sandbox_Check_Sales_Order(webappDriver, orderInfo);
            Backoffice_Sandbox_Check_Sales_Order(backofficeDriver, orderInfo);
        }

        /// <summary>
        /// Check if sales order appears correctly in activities list
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="orderInfo"></param>
        public static void Webapp_Sandbox_Check_Sales_Order(RemoteWebDriver webappDriver, Dictionary<string, string> orderInfo)
        {
            // Activities
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div[2]/div");
            Thread.Sleep(3000);


            // Click on order Remark
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/div/fieldset/div/label");


            // Click order by Remark
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/div/fieldset/div/div/i[2]");
            Thread.Sleep(3000);


            // Get remark and assert correctly - first 10 activities
            bool remarkMatch = false;
            int orderIndexInList = 0;
            string actualRemark;

            for (int i = 1; i < 6; i++)
            {
                actualRemark = SafeGetValue(webappDriver, string.Format("(//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span)[{0}]", i), "innerHTML").ToString();

                if (actualRemark == orderInfo["orderRemark"])
                {
                    remarkMatch = true;
                    orderIndexInList = i;
                    break;
                }
            }

            Assert(remarkMatch, "No sales order with matching remark in activity list");

            // Get actual activity ID from web page and compare with API data
            string actualActivityID = SafeGetValue(webappDriver, string.Format("(//label[@id='WrntyID'])[{0}]", orderIndexInList), "innerHTML").ToString();

            // Get Activity ID from API
            var apiData = GetApiData(DanUsername, DanPassword, "transactions", "Remark=", orderInfo["orderRemark"]);
            string apiActivityID = apiData[0].InternalID.ToString();

            Assert(actualActivityID == apiActivityID, "Activity ID doesn't match API data");

            // Check correct Units Qty 
            SafeClick(webappDriver, string.Format("(//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span)[{0}]", orderIndexInList));
            string actualUnitsQty = SafeGetValue(webappDriver, "//input[@id='UnitsQuantity']", "innerHTML");

            Assert(actualUnitsQty == orderInfo["unitsQty"], "Actual units Qty doesn't match ordered Qty");
        }

        /// <summary>
        /// Check if sales order arrives correctly to Backoffice
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="orderInfo"></param>
        public static void Backoffice_Sandbox_Check_Sales_Order(RemoteWebDriver backofficeDriver, Dictionary<string, string> orderInfo)
        {
            backofficeDriver.Navigate().GoToUrl(backofficeSandboxHomePageUrl);
            SafeClick(backofficeDriver, "(//a[contains(text(),'Activities')])[2]");

            bool remarkMatch = false;
            int orderIndexInList = 0;
            string actualRemark;

            for (int i = 0; i < 30; i++)
            {
                try
                {
                    actualRemark = SafeGetValue(backofficeDriver, string.Format("//table[@class='ll_tbl']/tbody/tr[{0}]/td[2]/a", i), "title");
                    if (actualRemark == orderInfo["orderRemark"])
                    {
                        remarkMatch = true;
                        orderIndexInList = i;
                        break;
                    }
                }

                catch { }
            }

            Assert(remarkMatch, "No sales order with matching remark in activity list");

            // Get activity ID from BO
            string actualActivityID = SafeGetValue(backofficeDriver, string.Format("//table[@class='ll_tbl']/tbody/tr[{0}]/td[3]/a", orderIndexInList), "title");

            // Get activity ID from API
            var apiData = GetApiData(DanUsername, DanPassword, "transactions", "Remark=", orderInfo["orderRemark"]);
            string apiActivityID = apiData[0].InternalID.ToString();

            Assert(apiActivityID == actualActivityID, "BO activity id doesn't match sql data");
        }

        public static void Webapp_Sandbox_Config_Home_Button(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            bool homeButtonMatch = false;
            try
            {
                // Change home button in Backoffice
                Backoffice_Sandbox_Config_Home_Button(backofficeDriver, "(Visit)");

                Webapp_Sandbox_Resync(webappDriver, backofficeDriver);
                Thread.Sleep(3000);
                string actualHomeButton = SafeGetValue(webappDriver, "//div[@id='mainButton']", "innerHTML").ToString();
                homeButtonMatch = actualHomeButton == "Visit";

            }
            catch { }

            finally
            {
                // Revert Home button to sales order
                Backoffice_Sandbox_Config_Home_Button(backofficeDriver, "(Sales Order)");
                Webapp_Sandbox_Resync(webappDriver, backofficeDriver);


                Assert(homeButtonMatch, "Home button not matching Backoffice configuration");
            }
        }

        /// <summary>
        /// Change home button to visit
        /// </summary>
        /// <param name="backofficeDriver"></param>
        public static void Backoffice_Sandbox_Config_Home_Button(RemoteWebDriver backofficeDriver, string Config_Home_Button)
        {
            //backofficeDriver.Navigate().GoToUrl(backofficeSandboxHomePageUrl);

            Backoffice.CompanyProfile.Home_Screen_Shortcut(backofficeDriver);

            // Edit Rep
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div/div[2]/div/span[2]");

            // Delete "Config_Home_Button" button
            SafeClick(backofficeDriver, "//div[1]/div[1]/div[3]/div[4]/div[1]/ul[1]/li[1]/div[1]/div[1]/span[4]");


            // Find the "Activities" button and click it
            int i = 0;
            for (i = 1; i < 10; i++)
            {
                try
                {
                    // Find the "Activities" button and click it
                    if (SafeGetValue(backofficeDriver, string.Format("//div[3]/div[2]/ul[1]/div[{0}]/div[1]/div[1]", i), "innerHTML") == "Activities")
                    {
                        SafeClick(backofficeDriver, string.Format("//div[1]/div[3]/div[2]/ul[1]/div[{0}]/div[1]", i));
                        break;
                    }
                }
                catch { }
            }



            for (int j = 1; j < 10; j++)
            {
                try
                {
                    // Find the "Config_Home_Button" button and click it 
                    if (SafeGetValue(backofficeDriver, string.Format("//div[{0}]/ul[1]/li[{1}]/div[1]/span[2]/span[1]", i, j), "innerHTML").ToString().Trim() == Config_Home_Button)
                    {
                        SafeClick(backofficeDriver, string.Format("//div[2]/ul[1]/div[{0}]/ul[1]/li[{1}]/div[1]/div[1]", i, j));
                        break;
                    }
                }

                catch { }
            }

            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");
        }


        public static void Webapp_Sandbox_Config_App_Buttons(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            bool visitButtonFlag = false, photoButtonFlag = false;
            string currentButton;
            try
            {
                // Config app buttons in Backoffice
                Backoffice_Sandbox_Config_App_Buttons(backofficeDriver);

                Webapp_Sandbox_Resync(webappDriver, backofficeDriver);

                // Search for visit and photo buttons in home screen
                for (int i = 1; i < 10; i++)
                {
                    if (photoButtonFlag && visitButtonFlag)
                        break;

                    currentButton = SafeGetValue(webappDriver, string.Format("//div[@id='mainCont']/app-home-page/footer/div/div[2]/div[{0}]/div", i), "title");
                    if (currentButton == "Visit")
                        visitButtonFlag = true;

                    if (currentButton == "Photo")
                        photoButtonFlag = true;
                }
            }
            catch { }

            finally
            {
                Backoffice_Sandbox_Revert_Config_App_Buttons(backofficeDriver);
                Assert(visitButtonFlag && photoButtonFlag, "App button configuration not working properly");
            }
        }

        /// <summary>
        /// Configure app buttons
        /// </summary>
        /// <param name="backofficeDriver"></param>
        public static void Backoffice_Sandbox_Config_App_Buttons(RemoteWebDriver backofficeDriver)
        {
            //backofficeDriver.Navigate().GoToUrl(backofficeSandboxHomePageUrl);

            Backoffice.CompanyProfile.App_Home_Screen(backofficeDriver);

            // Edit Rep
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div/div[2]/div/span[2]");

            // Find the "Activities" button and click it
            int j = 0;
            for (j = 1; j < 10; j++)
            {
                try
                {
                    // Find the "Activities" button and click it
                    if (SafeGetValue(backofficeDriver, string.Format("//div[3]/div[2]/ul[1]/div[{0}]/div[1]/div[1]", j), "innerHTML") == "Activities")
                    {
                        SafeClick(backofficeDriver, string.Format("//div[1]/div[3]/div[2]/ul[1]/div[{0}]/div[1]", j));
                        break;
                    }
                }
                catch { }
            }

            bool visitAdded = false, photoAdded = false;
            string SafeGetValueOfFild;
            for (int i = 1; i < 10; i++)
            {
                if (visitAdded && photoAdded)
                    break;
                try
                {

                    SafeGetValueOfFild = SafeGetValue(backofficeDriver, string.Format("//div[{0}]/ul[1]/li[{1}]/div[1]/span[2]/span[1]", j, i), "innerHTML").ToString().Trim();

                    // Find the "Visit" button 
                    if (SafeGetValueOfFild == "(Visit)")
                    {
                        visitAdded = true;
                        SafeClick(backofficeDriver, string.Format("//ul[1]/div[{0}]/ul[1]/li[{1}]/div[1]/div[1]", j, i));
                    }

                    // Find the "Photo" button
                    if (SafeGetValueOfFild == "(Photo)")
                    {
                        photoAdded = true;
                        SafeClick(backofficeDriver, string.Format("//ul[1]/div[{0}]/ul[1]/li[{1}]/div[1]/div[1]", j, i));
                    }
                }
                catch { }
            }

            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");
        }

        /// <summary>
        /// Revert app buttons back to original
        /// </summary>
        /// <param name="backofficeDriver"></param>
        public static void Backoffice_Sandbox_Revert_Config_App_Buttons(RemoteWebDriver backofficeDriver)
        {
            bool visitDeleted = false, photoDeleted = false;
            string Value;

            //backofficeDriver.Navigate().GoToUrl(backofficeSandboxHomePageUrl);
            Backoffice.CompanyProfile.App_Home_Screen(backofficeDriver);

            // Edit Rep
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div/div[2]/div/span[2]");

            for (int i = 1; i < 10; i++)
            {
                if (visitDeleted && photoDeleted)
                    break;
                try
                {
                    Value = SafeGetValue(backofficeDriver, string.Format("//ul[1]/li[{0}]/div[1]/div[1]/div[1]", i), "title").ToString().Trim();

                    // Find the "Visit" button and delete it
                    if (Value == "Visit")
                    {
                        SafeClick(backofficeDriver, string.Format("//ul[1]/li[{0}]/div[1]/div[1]/span[4]", i));
                        i--;
                        visitDeleted = true;
                    }

                    // Find the "Photo" button and delete it
                    if (Value == "Photo")
                    {
                        SafeClick(backofficeDriver, string.Format("//ul[1]/li[{0}]/div[1]/div[1]/span[4]", i));
                        i--;
                        photoDeleted = true;
                    }
                }
                catch { }
            }

            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");
        }

        /// <summary>
        /// to do
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Webapp_Sandbox_Home_Picture(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Setting 
            SafeClick(backofficeDriver, "//div[1]/div[1]/div[3]/div[1]");


            // Company Profile
            SafeClick(backofficeDriver, "//div[2]/div[3]/h3[1]/span[1]");


            // Branding
            SafeClick(backofficeDriver, "//div[1]/div[2]/div[3]/div[1]/p[1]");


            // Upload Landscape Image Change
            SafeClick(backofficeDriver, "//div[2]/div[1]/div[2]/table[1]/tbody[1]/tr[1]/td[2]/table[1]/tbody[1]/tr[1]/td[2]/div[1]");


            // Select pdf



            // Extension is not allowed. pop up press ok


            // Select jpeg


            // Resync app


            // Check if exsist popo


            // Assert the test
        }
        // Checked with "Sales Order"
        public static void Webapp_Sandbox_Item_Search(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder(webappDriver);

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // Get item name from webpage
            string itemName = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[6]/figure/app-custom-field-generator/app-custom-textbox/label/span", "innerHTML");

            // Get item ID from API
            var apiData = GetApiData(DanUsername, DanPassword, "items", "Name=", itemName);
            string apiItemCode = apiData[0].ExternalID.ToString();

            // Click search icon
            SafeClick(webappDriver, "//body/app-root/div/app-order-center/div/app-bread-crumbs/div/div/div/span/i");

            // Input search parameter
            SafeSendKeys(webappDriver, "//body/app-root/div/app-order-center/div/app-bread-crumbs/div/div/div/div[5]/div/div/input", apiItemCode);

            // Click search button
            SafeClick(webappDriver, "//body/app-root/div/app-order-center/div/app-bread-crumbs/div/div/div/div[5]/div/div/span[2]");

            Thread.Sleep(bufferTime);

            // Get item code from webpage
            string actualItemName = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[6]/figure/app-custom-field-generator/app-custom-textbox/label/span", "innerHTML");

            Assert(itemName == actualItemName, "Actual item doesn't match expected");
        }

        // Checked with "Sales Order"
        public static void Webapp_Sandbox_Minimum_Quantity(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder(webappDriver);

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // Get item ID from webpage
            string itemID = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[7]/figure/app-custom-field-generator/app-custom-textbox/label/span", "innerHTML");

            // Get item data from api
            var apiData = GetApiData(DanUsername, DanPassword, "items", "ExternalID=", itemID);

            // Parse the data to integer and store it in variable - min qty
            int apiItemMinQty;
            Int32.TryParse(apiData[0].MinimumQuantity.ToString(), out apiItemMinQty);

            Assert(apiItemMinQty > 1, "Item Qty is 1, unable to perform test");

            // Item Qty selector field
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/input");

            // Insert less than min qty into qty selector
            SafeSendKeys(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/input", (apiItemMinQty - 1).ToString());

            // Click outside the box
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]");

            Thread.Sleep(bufferTime);

            // Check qty selector style - supposed to be red (alerted)
            string qtySelectorStyle = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/input", "style");

            Assert(qtySelectorStyle.Contains("color: rgb(255, 0, 0);"), "Min qty doesn't mark in red");
        }

        // Checked with "Sales Order"
        public static void Webapp_Sandbox_Delete_Cart_Item(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder(webappDriver);

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // First item qty plus
            SafeClick(webappDriver,
                "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/span[2]/i");

            // Second item qty plus
            SafeClick(webappDriver,
                "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[2]/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/span[2]/i");

            // Cart
            SafeClick(webappDriver, "//button[@id='goToCartBtn']/span");

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // Click minus button on first item
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[5]/figure/app-custom-field-generator/app-custom-quantity-selector/div/span/i");

            // Click "Delete" alert
            SafeClick(webappDriver, "//div[@type='button'][2]");

            // Click qty selector on remaining item
            SafeClick(webappDriver, "//input[@id='UnitsQuantity']");

            // Insert 0 into qty selector
            SafeSendKeys(webappDriver, "//input[@id='UnitsQuantity']", Keys.Backspace);
            SafeSendKeys(webappDriver, "//input[@id='UnitsQuantity']", "0");

            // Press enter
            SafeSendKeys(webappDriver, "//input[@id='UnitsQuantity']", Keys.Enter);

            // Click "Delete" alert
            SafeClick(webappDriver, "//div[@type='button'][2]");

            // Find the "Items not found" message
            string notFoundMessage = SafeGetValue(webappDriver, "//div[@class='no-data ng-star-inserted']", "innerHTML");

            Assert(notFoundMessage == "Items not found", "Delete action didn't work properly");
        }

        // Checked with "Sales Order 2"
        public static void Webapp_Sandbox_Unit_Price_Discount(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder2(webappDriver);

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // First item qty plus
            SafeClick(webappDriver,
                "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[5]/figure/app-custom-field-generator/app-custom-quantity-selector/div/span[2]/i");

            // Cart
            SafeClick(webappDriver, "//button[@id='goToCartBtn']/span");

            // Small view
            //SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            //SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // Get unit price
            string unitPriceStr = SafeGetValue(webappDriver, "//label[@id='UnitPrice']", "innerHTML");
            unitPriceStr = unitPriceStr.Trim(new Char[] { ' ', '$' });
            double unitPrice;
            double.TryParse(unitPriceStr, out unitPrice);

            // Click on unit discount
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div[7]/app-custom-field-generator/app-custom-textbox/input");

            // Input 50% discount
            SafeClear(webappDriver, "//input[@id='UnitDiscountPercentage']");
            SafeSendKeys(webappDriver, "//input[@id='UnitDiscountPercentage']", "50");
            SafeSendKeys(webappDriver, "//input[@id='UnitDiscountPercentage']", Keys.Enter);
            Thread.Sleep(bufferTime);

            // Get price after discount
            string priceAfterDiscountStr = SafeGetValue(webappDriver, "//input[@id='UnitPriceAfterDiscount']", "title");
            priceAfterDiscountStr = priceAfterDiscountStr.Trim(new Char[] { '$' });
            double priceAfterDiscount;
            double.TryParse(priceAfterDiscountStr, out priceAfterDiscount);

            Assert(priceAfterDiscount == unitPrice / 2, "Price after discount miscalculated");

            double unitPriceAfterDiscount = 3;

            // Click on price after discount
            SafeClick(webappDriver, "//input[@id='UnitPriceAfterDiscount']");

            // Input price after discount
            SafeClear(webappDriver, "//input[@id='UnitPriceAfterDiscount']");
            SafeSendKeys(webappDriver, "//input[@id='UnitPriceAfterDiscount']", unitPriceAfterDiscount.ToString());
            SafeSendKeys(webappDriver, "//input[@id='UnitPriceAfterDiscount']", Keys.Enter);
            Thread.Sleep(bufferTime);

            // Click on unit discount
            SafeClick(webappDriver, "//input[@id='UnitDiscountPercentage']");

            // Get unit discount
            string discountStr = SafeGetValue(webappDriver, "//input[@id='UnitDiscountPercentage']", "title");
            discountStr = discountStr.Trim(new char[] { '%' });
            double discount;
            double.TryParse(discountStr, out discount);

            Assert(discount == (((unitPrice - unitPriceAfterDiscount) / unitPrice) * 100), "Discount miscalculated");
        }

        // Checked with "Sales Order" 
        public static void Webapp_Sandbox_Continue_Ordering(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Dictionary to store order info to assert later
            Dictionary<string, string> orderInfo = new Dictionary<string, string>();

            GetToOrderCenter_SalesOrder(webappDriver);

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // Item qty plus
            SafeClick(webappDriver,
               "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/span[2]/i");

            Thread.Sleep(bufferTime);

            // Get units qty from qty selector
            orderInfo["unitsQty"] = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/input", "innerHTML");

            // Cart
            SafeClick(webappDriver, "//button[@id='goToCartBtn']/span");

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // Transaction Menu
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/a/i");

            // Order details
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/ul/li/span");

            // Create remark and store it
            orderInfo["orderRemark"] = "Automation " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            // Click remark field
            SafeClick(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[15]/div/app-custom-field-generator/app-custom-textbox/div/input");

            // Add remark
            SafeSendKeys(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[15]/div/app-custom-field-generator/app-custom-textbox/div/input", orderInfo["orderRemark"]);

            // Save button
            SafeClick(webappDriver, "//body/app-root/div/app-order-details/app-bread-crumbs/div/div/div/div[3]/div[2]");

            // Home button
            SafeClick(webappDriver, "//a[@id='btnMenuHome']/span");

            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            // Activities
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div[2]/div");


            // Click on order Remark //div[@id='viewsContainer']/app-custom-list/div/fieldset/div/label
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/div/fieldset/div/label");

            // Click order by Remark //div[@id='viewsContainer']/app-custom-list/div/fieldset/div/label
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/div/fieldset/div/div/i[2]");
            Thread.Sleep(3000);



            //  Find the sales order in activity list
            string actualRemark;
            int orderIndexInList = 0;

            for (int i = 1; i < 12; i++)
            {
                actualRemark = SafeGetValue(webappDriver, string.Format("(//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span)[{0}]", i), "innerHTML").ToString();

                if (actualRemark == orderInfo["orderRemark"])
                {
                    orderIndexInList = i;
                    break;
                }
            }

            // Drill down into the sales order 
            SafeClick(webappDriver, string.Format("(//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span)[{0}]", orderIndexInList));
            Thread.Sleep(3000);


            // Continue ordering
            SafeClick(webappDriver, "(//button[@type='button'])[2]");

            // Item qty plus
            SafeClick(webappDriver,
               "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/span[2]/i");

            // Cart
            SafeClick(webappDriver, "//button[@id='goToCartBtn']/span");

            // Submit
            SafeClick(webappDriver, "//button[@id='btnTransition']/span");
        }

        // Checked with "Sales Order"
        public static void Webapp_Sandbox_Duplicate_Line_Item(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder(webappDriver);

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // First item qty plus
            SafeClick(webappDriver,
                "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/span[2]/i");

            // Cart
            SafeClick(webappDriver, "//button[@id='goToCartBtn']/span");

            // GridLine view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li[2]");
            Thread.Sleep(bufferTime);

            // Select item
            SafeClick(webappDriver, "(//input[@type='checkbox'])[2]");

            // Edit
            SafeClick(webappDriver, "//li[@id='editButton']/a/i");

            // Duplicate
            SafeClick(webappDriver, "//li[@id='dropdownActionsDuplicate']/span");

            // Duplicate popup button
            SafeClick(webappDriver, "(//div[@type='button'])[2]");

            string item1 = SafeGetValue(webappDriver, "//body/app-root/div/app-cart/div/div/div/div/app-custom-list/virtual-scroll/div/div/app-custom-form/fieldset/div[4]/app-custom-field-generator/app-custom-textbox/label", "title");
            string item2 = SafeGetValue(webappDriver, "//body/app-root/div/app-cart/div/div/div/div/app-custom-list/virtual-scroll/div/div[2]/app-custom-form/fieldset/div[4]/app-custom-field-generator/app-custom-textbox/label", "title");

            Assert(item1 == item2, "Duplicate action failed");
        }

        // Checked with "Sales Order 2"
        public static void Webapp_Sandbox_Inventory_Alert(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder2(webappDriver);

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // Get item Name from webpage
            string itemID = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[3]/figure/app-custom-field-generator/app-custom-textbox/label/span", "innerHTML");

            // Get item api data
            var apiData = GetApiData(DanUsername, DanPassword, "items", "Name=", itemID);

            // Parse out InternalID
            string InternalID = apiData[0].InternalID.ToString();

            // Get item inverntory data from api
            apiData = GetApiData(DanUsername, DanPassword, "inventory", "ItemInternalID=", InternalID);

            // Parse out InStockQuantity
            int inStockQuantity;
            Int32.TryParse(apiData[0].InStockQuantity.ToString(), out inStockQuantity);

            // Item Qty selector field
            SafeClick(webappDriver, "//input[@id='UnitsQuantity']");

            // Insert less than min qty into qty selector
            SafeSendKeys(webappDriver, "//input[@id='UnitsQuantity']", (inStockQuantity + 20).ToString());

            // Click outside the box
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]");

            Thread.Sleep(bufferTime);

            string quantityStr = SafeGetValue(webappDriver, "//input[@id='UnitsQuantity']", "title");

            double quantity = Double.Parse(quantityStr);

            Assert(quantity == (double)inStockQuantity, "Inverntory auto-set failed");
        }

        public static void Webapp_Sandbox_Search_Activity(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Activities
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div[2]/div");

            // Get first activity ID and remark
            string id = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div[2]/app-custom-field-generator/app-custom-textbox/label", "title").ToString();
            string remark = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", "title").ToString();

            // Click search icon, then search box 
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/a/span");
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/input");

            // Insert ID into search bar and click search
            SafeSendKeys(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/input", id);
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/a/span");

            Thread.Sleep(3000);

            // Get found item's ID and remark
            string foundID = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div[2]/app-custom-field-generator/app-custom-textbox/label", "title").ToString();
            string foundRemark = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", "title").ToString();

            Assert((foundID == id) && (foundRemark == remark), "Activity search failed");
        }

        public static void Webapp_Sandbox_Delete_Activity(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Activities
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div[2]/div");

            // Get first activity ID and remark
            string id = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div[2]/app-custom-field-generator/app-custom-textbox/label", "title").ToString();

            // Select first activity
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/input");

            // Click edit pencil 
            SafeClick(webappDriver, "//div[@id='topBarContainer']/div/list-actions/div/a/span");

            // Click "delete"
            SafeClick(webappDriver, "//div[@id='topBarContainer']/div/list-actions/div/ul/li[4]");

            // "Continue" on popup
            SafeClick(webappDriver, "(//div[@type='button'])[2]");
            Thread.Sleep(7000);

            string foundID = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div[2]/app-custom-field-generator/app-custom-textbox/label", "title").ToString();

            Assert(id != foundID, "Delete activity action failed");
        }

        public static void Webapp_Sandbox_Account_Search_Activity(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Accounts
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div/div");

            // First account
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[1]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span");

            // Get first activity id
            string id = SafeGetValue(webappDriver, " //body/app-root/div/app-accounts-home-page/div/div[2]/div/div/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div[2]/app-custom-field-generator/app-custom-textbox/label", "title");

            // Get remark
            string remark = SafeGetValue(webappDriver, " //body/app-root/div/app-accounts-home-page/div/div[2]/div/div/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", "title");

            // Click search button
            SafeClick(webappDriver, "//li[@id='btnSearch']/a/span");

            // Input id
            SafeClick(webappDriver, "//li[@id='btnSearch']/div/input");
            SafeSendKeys(webappDriver, "//li[@id='btnSearch']/div/input", id);

            // Click search button
            SafeClick(webappDriver, "//li[@id='btnSearch']/a/span");
            Thread.Sleep(bufferTime);

            // Assert activity found (same remark and id)
            string foundId = SafeGetValue(webappDriver, "//body/app-root/div/app-accounts-home-page/div/div[2]/div/div/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div[2]/app-custom-field-generator/app-custom-textbox/label", "title");
            string foundRemark = SafeGetValue(webappDriver, "//body/app-root/div/app-accounts-home-page/div/div[2]/div/div/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", "title");

            Assert(id == foundId && remark == foundRemark, "Account activity search failed");
        }

        public static void Webapp_Sandbox_Account_Drill_Down(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Accounts
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div/div");

            // Get first account's name and drill down into it
            string name = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[1]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", "title");
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[1]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span");

            // Get account name from top of page and assert
            string nameInside = SafeGetValue(webappDriver, "//div[@id='accountsHomePageCont']/acc-details/div/div/div/label", "innerHTML");

            Assert(name == nameInside, "Account drill down failed");
        }

        public static void Webapp_Sandbox_Enter_To_Activity(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Activities
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div[2]/div");

            string remark = "";
            string type = "";
            string id = "";
            int i = 0;

            // Get activity IDs and remarks until you find one with a remark 
            while (remark == "" || (type != "Sales Order" && type != "Sales Order  2"))
            {
                try
                {
                    i++;
                    remark = SafeGetValue(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[{0}]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", i), "title", safeWait: 100).ToString();
                    id = SafeGetValue(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[{0}]/app-custom-form/fieldset/div[2]/app-custom-field-generator/app-custom-textbox/label", i), "title", safeWait: 100).ToString();
                    type = SafeGetValue(webappDriver, string.Format("(//label[@id='Type'])[{0}]", i), "title", safeWait: 50);
                }
                catch { break; }
            }

            // Drill down to the chosen activity
            SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[{0}]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", i));

            Thread.Sleep(bufferTime + 1500);
            string continueOrderingButton = SafeGetValue(webappDriver, "(//button[@type='button'])[2]", "innerHTML");

            Assert(continueOrderingButton == "Continue ordering", "Enter to activity failed (couldn't find continue ordering button)");
        }

        public static void Webapp_Sandbox_Account_Activity_Drilldown(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Accounts
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div/div");

            // Drill down into first account
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[1]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span");

            string remark = "";
            string type = "";
            string id = "";
            int i = 0;

            // Get activity IDs and remarks until you find one with a remark 
            while (remark == "" || (type != "Sales Order" && type != "Sales Order  2"))
            {
                try
                {
                    i++;
                    remark = SafeGetValue(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[{0}]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", i), "title").ToString();
                    id = SafeGetValue(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[{0}]/app-custom-form/fieldset/div[2]/app-custom-field-generator/app-custom-textbox/label", i), "title").ToString();
                    type = SafeGetValue(webappDriver, string.Format("(//label[@id='Type'])[{0}]", i), "title");
                }
                catch { break; }
            }

            // Drill down to the chosen activity
            SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[{0}]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", i));

            Thread.Sleep(bufferTime);
            string continueOrderingButton = SafeGetValue(webappDriver, "(//button[@type='button'])[2]", "innerHTML");

            Assert(continueOrderingButton == "Continue ordering", "Account activity drilldown failed (couldn't find continue ordering button)");
        }

        public static void Webapp_Sandbox_Breadcrumbs_Navigation(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder(webappDriver);

            // Item info
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile/figure/app-custom-field-generator/app-custom-image/span/i");

            // App back button
            SafeClick(webappDriver, "//div[@id='header']/div/div");

            // Get item name from webpage
            string itemName = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[6]/figure/app-custom-field-generator/app-custom-textbox/label/span", "innerHTML");
        }

        // Checked with "Sales Order 2"
        public static void Webapp_Sandbox_Duplicate_Transaction(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder2(webappDriver);

            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/span");
            SafeClick(webappDriver, "//div[@id='header']/div/div[4]/ul/li/ul/li");
            Thread.Sleep(bufferTime);

            // First item qty plus
            SafeClick(webappDriver,
                "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[5]/figure/app-custom-field-generator/app-custom-quantity-selector/div/span[2]/i");
            Thread.Sleep(bufferTime);

            // Get units qty from qty selector
            string originalQty = SafeGetValue(webappDriver, "//input[@id='UnitsQuantity']", "title");
            Console.WriteLine(originalQty);

            // Cart
            SafeClick(webappDriver, "//button[@id='goToCartBtn']/span");

            // Transaction Menu
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/a/i");

            // Order details
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/ul/li/span");

            // Create remark and store it
            string originalRemark = "Automation " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            // Click remark field
            SafeClick(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[14]/div/app-custom-field-generator/app-custom-textarea/div/textarea");

            // Add remark
            SafeSendKeys(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[14]/div/app-custom-field-generator/app-custom-textarea/div/textarea", originalRemark);

            // Save button
            SafeClick(webappDriver, "//body/app-root/div/app-order-details/app-bread-crumbs/div/div/div/div[3]/div[2]");

            // Transaction Menu
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/a/i");

            // Duplicate transaction
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/ul/li[3]/span");
            Thread.Sleep(bufferTime);

            // Get units qty from qty selector
            string duplicatedQty = SafeGetValue(webappDriver, "//input[@id='UnitsQuantity']", "title");
            Console.WriteLine(duplicatedQty);
            // Transaction Menu
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/a/i");

            // Order details
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/ul/li/span");

            // Get remark from form
            string duplicatedRemark = SafeGetValue(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[15]/div/app-custom-field-generator/app-custom-textbox/div/input", "title");
            double result;
            double result2;
            Assert(Double.TryParse(duplicatedQty, out result) == Double.TryParse(originalQty, out result2) && duplicatedRemark == originalRemark, "Duplicated data doesn't match original");
        }

       

        public static void Webapp_Sandbox_Mandatory_Fields(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder2(webappDriver);

            // Cart
            SafeClick(webappDriver, "//button[@id='goToCartBtn']/span");

            // Transaction Menu
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/a/i");

            // Order details
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/ul/li/span");

            // Save button
            SafeClick(webappDriver, "//body/app-root/div/app-order-details/app-bread-crumbs/div/div/div/div[3]/div[2]");


        }


        public static void Webapp_Sandbox_Order_By(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            bool flag = false;

            // Clicke in button Accounts
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div/div");


            // Clicke in sort down for all column
            int i = 1;
            string itemTemp = null;
            string itemNew = null;
            int countI = 1, fla = -1;
            while (true)
            {

                try
                {
                    SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div[1]/fieldset/div[{0}]/label", i));
                    SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div/fieldset/div[{0}]/div/i[2]", i));
                    Thread.Sleep(2000);
                }
                catch (Exception)
                {
                    break;
                }


                // Checked if list order down by Descending 
                countI = 1;
                try
                {
                    itemNew = SafeGetValue(webappDriver, string.Format("/html[1]/body[1]/app-root[1]/div[1]/app-generic-list[1]/div[1]/div[2]/div[2]/div[1]/app-custom-list[1]/virtual-scroll[1]/div[2]/div[1]/app-custom-form[1]/fieldset[1]/div[{0}]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]", i), "innerHTML", maxRetry: 5);
                    fla = 1;
                }
                catch (Exception)
                {
                    itemNew = SafeGetValue(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div[{0}]/app-custom-field-generator/app-custom-button/a/span", i), "innerHTML", maxRetry: 5);
                    fla = 2;
                }
                while (true)
                {
                    itemTemp = itemNew;
                    countI++;
                    if (fla == 2)
                        itemNew = SafeGetValue(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[{0}]/app-custom-form/fieldset/div[{1}]/app-custom-field-generator/app-custom-button/a/span", countI, i), "innerHTML", maxRetry: 5);
                    if (fla == 1)
                        itemNew = SafeGetValue(webappDriver, string.Format("/html[1]/body[1]/app-root[1]/div[1]/app-generic-list[1]/div[1]/div[2]/div[2]/div[1]/app-custom-list[1]/virtual-scroll[1]/div[2]/div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]", countI, i), "innerHTML", maxRetry: 5);

                    if (itemTemp.CompareTo(itemNew) < 0)
                    {
                        flag = true;
                        break;
                    }
                }
            }

            i++;

            //Assert of checking 
            Assert(!flag, "The list not Descending");
        }


        public static void Backoffice_Sandbox_Load_File(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);
            FullWebappAutomation.Backoffice.ERPIntegration.Configuration(backofficeDriver);


            // CSV Delimiter
            SafeClick(backofficeDriver, "//input[@id='txtCSVDelimiterToShow']");


            // Change Othrer
            SafeClear(backofficeDriver, "//input[@id='txtCSVDelimiter']");
            SafeSendKeys(backofficeDriver, "//input[@id='txtCSVDelimiter']", ",");


            // Internal Delimitier Fields
            SafeClear(backofficeDriver, "//input[@id='InterDelFields']");
            SafeSendKeys(backofficeDriver, "//input[@id='InterDelFields']", ";");


            //  Save button
            SafeClick(backofficeDriver, "//div[@id='saveBtn']");


            // OK
            SafeClick(backofficeDriver, "//div[@id='msgModalRightBtn']");


            //OK
            SafeClick(backofficeDriver, "//div[@id='msgModalLeftBtn']");


            // UploadFile  API_Item to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_Item.csv", "API_Item_",false);


            // check if file succee loaded API_Item
            checkFile(backofficeDriver, "API_Item");


            // UploadFile  API_Item to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_Inventory.csv", "API_Inventory_",true);


            // check if file succee loaded API_Item
            checkFile(backofficeDriver, "API_Inventory");


            // UploadFile  API_Account to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_Account.csv", "API_Account_",true);
           
        

            // check if file succee loaded API_Item
            checkFile(backofficeDriver, "API_Account");
        }


        #region home page test


        /// <summary>
        /// Creat Byer
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Backoffice_Sandbox_Creat_Byer(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Dictionary<string, string> Buyer = new Dictionary<string, string>();

            
            // setting button
            SafeClick(backofficeDriver, "//div[@id='settingCont']//div[@class='header']");

            try
            {
                //  Close  popup next
                SafeClick(backofficeDriver, "//div[@id='walkme-balloon-3983183']/div/div/div[@class='walkme-custom-balloon-bottom-div']//span[@class='walkme-custom-balloon-button-text']", maxRetry: 4);
            }
            catch { }

            // Account
            SafeClick(backofficeDriver, "//div[1]/div[1]/ul[1]/li[3]/a[1]");


            // Select first Account
            SafeClick(backofficeDriver, "//table[1]/tbody[1]/tr[1]/td[1]/input[1]");


            //  Action button
            SafeClick(backofficeDriver, "//div[6]/div[1]/div[1]/div[1]/div[1]/div[2]");


            //  Manage as Buyer
            SafeClick(backofficeDriver, "//div[1]/div[1]/div[1]/div[3]/ul[1]/li[5]");


            //  + Add new contact
            SafeClick(backofficeDriver, "//div[1]/div[10]/div[1]/div[1]/div[4]");


            //  first name
            SafeSendKeys(backofficeDriver, "//div[8]/div[2]/div[1]/p[1]/input[1]", "auto12");


            // last name
            SafeSendKeys(backofficeDriver, "//div[1]/div[8]/div[2]/div[1]/p[2]/input[1]", "auto12");


            // Role
            SafeSendKeys(backofficeDriver, "//div[8]/div[2]/div[1]/p[3]/input[1]", "auto12");


            // Date of birth
            SafeSendKeys(backofficeDriver, "//div[8]/div[2]/div[1]/p[4]/input[1]", DateTime.Now.ToString("MM/dd/yyyy"));


            // External ID
            SafeSendKeys(backofficeDriver, "//div[8]/div[2]/div[1]/p[5]/input[1]", "auto12");


            // Phone number
            SafeSendKeys(backofficeDriver, "//div[8]/div[2]/div[1]/p[5]/input[1]", "0000-2222-222");


            // Mobile number
            SafeSendKeys(backofficeDriver, "//div[8]/div[2]/div[1]/p[6]/input[1]", "000-5555-4444");


            // Email
            SafeSendKeys(backofficeDriver, "//div[6]/div[1]/div[8]/div[2]/div[1]/p[9]/input[1]", "aoto12@pepperitest.com");


            // save
            SafeClick(backofficeDriver, "//div[6]/div[1]/div[8]/div[3]/div[1]/div[1]");


            // check box 
            SafeClick(backofficeDriver, "//div[6]//div[1]/div[1]/table[1]/tbody[1]/tr[1]/td[@class='ll_td ll_td0 ll_multiSelect']/input[1]");


            // Action
            SafeClick(backofficeDriver, "//form[1]/div[6]/div[1]/div[10]/div[1]/div[1]/div[2]");


            //  Connect as Buyer
            SafeClick(backofficeDriver, "//div[6]/div[1]/div[10]/div[1]/div[1]/div[3]/ul[1]/li[1]");


            // send
            SafeClick(backofficeDriver, "//div[6]/div[1]/div[11]/div[3]/div[1]/div[2]");


            // copy password
            Buyer.Add("Contact Person Name", SafeGetValue(backofficeDriver, "//div[2]/div[1]/div[1]/div[1]/span[1]/div[1]/table[1]/tbody[1]/tr[1]/td[1]", "innerHTML"));
            Buyer.Add("Email", SafeGetValue(backofficeDriver, "//div[2]/div[1]/div[1]/div[1]/span[1]/div[1]/table[1]/tbody[1]/tr[1]/td[2]", "innerHTML"));
            Buyer.Add("password", SafeGetValue(backofficeDriver, "//div[2]/div[1]/div[1]/div[1]/span[1]/div[1]/table[1]/tbody[1]/tr[1]/td[3]", "innerHTML"));


            Thread.Sleep(2000);


            //press ok at pop up
            SafeClick(backofficeDriver, "//form[1]/div[11]/div[2]/div[1]/div[5]/div[2]");


            RemoteWebDriver BuyerWebappDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), DesiredCapabilities.Chrome(), TimeSpan.FromSeconds(600));


            //login to buyer
            Webapp_Sandbox_Login(BuyerWebappDriver, Buyer["Email"], Buyer["password"]);


            Sandbox_Change_Size_All_Case_Home_Screen(webappDriver, "width: 300", "width: 214");

            // Upload Image to Landscape
            Sandbox_Upload_Image(backofficeDriver, "//div[@id='btnB2BLandscape']", @"C:\Users\yosef.h\Desktop\automation_documents\automation_files_pictues\pinkS.jpg");


            // Upload Image to Portrait
            Sandbox_Upload_Image(backofficeDriver, "//div[@id='btnB2BPortrait']", @"C:\Users\yosef.h\Desktop\automation_documents\automation_files_pictues\cow.jpg");


            // Check if image became to buyer
            Sandbox_Change_Size_All_Case_Home_Screen(webappDriver, "width: 1064", "width: 1170");


            BuyerWebappDriver.Quit();
        }


        /// <summary>
        /// change sale order to sales order 1
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Webapp_Sandbox_Change_Title_Home_Screen(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Change to Sales Order 1
            Help_Sandbox_Change_Title_Home_Screen(backofficeDriver, "Sales Order 1");

            // Resync
            Webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(1500);

            // get value Home button
            string value = SafeGetValue(webappDriver, "//div[1]/app-home-page[1]/footer[1]/div[@class='container']//div[@class='row ng-star-inserted']/div[1]", "innerHTML");

            // Assert if change
            Assert(value == "Sales Order 1", "The title no change");


            // reverse to Sales Order 
            Help_Sandbox_Change_Title_Home_Screen(backofficeDriver, "Sales Order");


            // Resync
            Webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(1500);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Webapp_Sandbox_Change_All_Home(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            int total = 0;
            string value;
            Backoffice.CompanyProfile.App_Home_Screen(backofficeDriver);


            // Edit Rep
            SafeClick(backofficeDriver, "//div[@id='centerContainer']/div[@id='content']/div[@id='formContTemplate']/div[1]/div[2]/div[1]/span[2]");


            Backoffice_Delete_Layout(backofficeDriver);


            // Add any 16 button

            // Available Filds
            for (int i = 1; ; i++)
            {
                try
                {
                    SafeClick(backofficeDriver, string.Format("//body/form[@id='aspnetForm']/div[@class='page']/div[@class='main']/div[@id='appHomePageCustCont']/div[@id='centerContainer']/div[@id='content']/div[@id='formContTemplate']/div[@class='template-forms-edit lb']/div[@class='lb-bank ui-droppable']/ul/div[{0}]/div[1]/span[1]", i), maxRetry: 2);

                }
                catch
                {
                    break;
                }
                // sub Available Filds
                for (int j = 1; ; j++)
                {
                    try
                    {
                        value = SafeGetValue(backofficeDriver, string.Format("//div[{0}]/ul[1]/li[{1}]/div[1]/span[2]/span[1]", i, j), "innerHTML", maxRetry: 2).ToString().Trim();
                        if (value.Contains("Catalog") || value.Contains("Dashboard"))
                        {
                            continue;
                        }
                        SafeClick(backofficeDriver, string.Format("//ul//div[{0}]//ul[1]//li[{1}]//div[1]//div[1]", i, j), maxRetry: 2);
                        total++;
                        if (total >= 16)
                            break;
                    }
                    catch
                    {
                        break;
                    }
                }
                if (total >= 16)
                    break;
            }

            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");


            Thread.Sleep(2000);
            Webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(4000);

            bool flag = false;
            // check if exist 16 button in home page 
            for (int i = 1; ; i++)
            {


                try
                {
                    SafeGetValue(webappDriver, string.Format("//app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i), "innerHTML", maxRetry: 2);
                }
                catch
                {
                    if (i == 17)
                        flag = true;
                    break;

                }
            }

            // Edit Rep
            SafeClick(backofficeDriver, "//div[@id='centerContainer']/div[@id='content']/div[@id='formContTemplate']/div[1]/div[2]/div[1]/span[2]");


            // reverse all
            Backoffice_Delete_Layout(backofficeDriver, "Accounts", "Activity List", true);





            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");
            Thread.Sleep(4000);

            Assert(flag, "sume of button different between webapp to bakeoffice");
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="Accounts"></param>
        /// <param name="Activity"></param>
        /// <param name="defaultiv"></param>
        public static void Backoffice_Delete_Layout(RemoteWebDriver backofficeDriver, string Accounts = null, string Activity = null, bool defaultiv = false)
        {
            string value;
            //delete all Layout
            for (int i = 1; ;)
            {
                try
                {

                    if (defaultiv)
                    {
                        value = SafeGetValue(backofficeDriver, string.Format("//div[6]/div[1]/div[4]/div[2]/div[1]/div[1]/div[3]/div[4]/div[1]/ul[1]/li[{0}]/div[1]/div[1]/div[1]", i), "innerHTML", maxRetry: 3);
                        if (value != Accounts && !value.Contains(Activity))
                            SafeClick(backofficeDriver, string.Format("//ul[@class='clearfix ui-sortable']//li[{0}]//div[1]//div[1]//span[4]", i), maxRetry: 2);
                        else
                            i++;
                    }
                    else
                        SafeClick(backofficeDriver, string.Format("//ul[@class='clearfix ui-sortable']//li[{0}]//div[1]//div[1]//span[4]", i), maxRetry: 2);


                }
                catch
                {
                    break;
                }

            }
        }

        /// <summary>
        /// when small tab the all button go to menu 
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Webapp_Sandbox_Menu_Home_Screen(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            int total = 0;
            string value;
            int i = 0;
            Backoffice.CompanyProfile.App_Home_Screen(backofficeDriver);


            // Edit Rep
            SafeClick(backofficeDriver, "//div[@id='centerContainer']/div[@id='content']/div[@id='formContTemplate']/div[1]/div[2]/div[1]/span[2]");


            Backoffice_Delete_Layout(backofficeDriver);


            // Add any 16 button

            // Available Filds
            for (i = 1; ; i++)
            {
                try
                {
                    SafeClick(backofficeDriver, string.Format("//body/form[@id='aspnetForm']/div[@class='page']/div[@class='main']/div[@id='appHomePageCustCont']/div[@id='centerContainer']/div[@id='content']/div[@id='formContTemplate']/div[@class='template-forms-edit lb']/div[@class='lb-bank ui-droppable']/ul/div[{0}]/div[1]/span[1]", i), maxRetry: 2);

                }
                catch
                {
                    break;
                }
                // sub Available Filds
                for (int j = 1; ; j++)
                {
                    try
                    {
                        value = SafeGetValue(backofficeDriver, string.Format("//div[{0}]/ul[1]/li[{1}]/div[1]/span[2]/span[1]", i, j), "innerHTML", maxRetry: 2).ToString().Trim();
                        if (value.Contains("Catalog") || value.Contains("Dashboard"))
                        {
                            continue;
                        }
                        SafeClick(backofficeDriver, string.Format("//ul//div[{0}]//ul[1]//li[{1}]//div[1]//div[1]", i, j), maxRetry: 2);
                        total++;
                        if (total >= 16)
                            break;
                    }
                    catch
                    {
                        break;
                    }
                }
                if (total >= 16)
                    break;
            }

            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");


            Thread.Sleep(2000);
            Webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(4000);



            webappDriver.Manage().Window.Size = new Size(700, 350);
            // wait the Minimize case
            Thread.Sleep(2000);
            try
            {
                SafeClick(webappDriver, "/html[1]/body[1]/app-root[1]/app-menu[1]/div[1]/nav[1]/button[1]", maxRetry: 3);

            }
            catch
            {
                Assert(false, "Although the page is small there is no menu");
            }

            try
            {
                for (i = 1; ; i++)
                {
                    SafeGetValue(webappDriver, string.Format("/html[1]/body[1]/app-root[1]/app-menu[1]/div[1]/nav[1]/div[3]/ul[1]/li[{0}]", i), "innerHTML", maxRetry: 5);
                }
            }
            catch
            {
                Assert(i == 17, "no all menu exist");
            }


            webappDriver.Manage().Window.Maximize();


            // reverse all

            // Edit Rep
            SafeClick(backofficeDriver, "//div[@id='centerContainer']/div[@id='content']/div[@id='formContTemplate']/div[1]/div[2]/div[1]/span[2]");

            Backoffice_Delete_Layout(backofficeDriver, "Accounts", "Activity", true);

            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");
            Thread.Sleep(5000);

        }


        public static void Webapp_Sandbox_Online_action_Home_Page(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            FullWebappAutomation.Backoffice.ConfigurationFiles.Online_Add_Ons(backofficeDriver);

            // Add new
            SafeClick(backofficeDriver, "//div[2]/div/div/div/div[2]");

            // input name
            SafeSendKeys(backofficeDriver, "//div[@id='GeneralInfo']/table/tbody/tr/td[2]/input[@id='actName']", "Online action automation");

            // input Description
            SafeSendKeys(backofficeDriver, "//div[@id='GeneralInfo']/table/tbody/tr/td[2]/input[@id='actDescription']", "go to new url");

            // input URL
            SafeSendKeys(backofficeDriver, "//div[@id='GeneralInfo']/table/tbody/tr[4]/td[2]/input[@id='actURL']", @"https://www.hidabroot.org/");

            // select icon
            SafeClick(backofficeDriver, "//div[@id='activitiesIconsCont']/div[@id='icon2']");

            // save
            SafeClick(backofficeDriver, "/html[1]/body[1]/form[1]/div[6]/div[1]/div[5]/div[3]/div[1]/div[1]");


            FullWebappAutomation.Backoffice.CompanyProfile.App_Home_Screen(backofficeDriver);

            // Edit Admin
            int k = 2;
            while (true)
            {
                try
                {
                    if (SafeGetValue(backofficeDriver, string.Format("//div[@id='formContTemplate']/div/div[{0}]/div/span[1]", k), "innerHTML", maxRetry: 3) == "Admin")
                    {
                        SafeClick(backofficeDriver, string.Format("//div[@id='formContTemplate']/div/div[{0}]/div/span[2]", k));
                        break;
                    }
                }
                catch { }
                k++;
            }

            int j = 0;
            for (j = 1; j < 10; j++)
            {
                try
                {
                    // Find the "Online Add-ons" button and click it
                    if (SafeGetValue(backofficeDriver, string.Format("//div[3]/div[2]/ul[1]/div[{0}]/div[1]/div[1]", j), "innerHTML") == "Online Add-ons")
                    {
                        SafeClick(backofficeDriver, string.Format("//div[1]/div[3]/div[2]/ul[1]/div[{0}]/div[1]", j));
                        break;
                    }
                }
                catch { }
            }


            // + Online action automation
            SafeClick(backofficeDriver, string.Format("//div[@class='lb-bank ui-droppable']//div[{0}]//ul[1]//li[1]//div[1]//div[1]", j));


            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");


            Webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(2000);


            // check if exist button in "Online action automation" and click
            for (int i = 1; ; i++)
            {
                if (SafeGetValue(webappDriver, string.Format("//app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i), "innerHTML", maxRetry: 2) == "Online action automation")
                {
                    SafeClick(webappDriver, string.Format("//div[@id='mainCont']/app-home-page/footer/div/div[2]/div[{0}]/div", i));
                    break;
                }
            }

            var browserTabs = webappDriver.WindowHandles;
            webappDriver.SwitchTo().Window(browserTabs[1]);

            bool isOpen = webappDriver.Url == @"https://www.hidabroot.org/";


            webappDriver.Close();
            webappDriver.SwitchTo().Window(browserTabs[0]);

        }
        #endregion


        #region Images

        /// <summary>
        /// Upload Image to Home screen webapp Admin
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="elementXPath"></param>
        /// <param name="filePath"></param>
        public static void Sandbox_Upload_Image(RemoteWebDriver backofficeDriver, string elementXPath, string filePath)
        {
            FullWebappAutomation.Backoffice.CompanyProfile.Branding(backofficeDriver);


            //  Change Button 
            SafeClick(backofficeDriver, elementXPath);


            // Upload  Image
            // Upload to web
            backofficeDriver.SwitchTo().ActiveElement().SendKeys(filePath);
            backofficeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Thread.Sleep(4000);


        }


        /// <summary>
        /// test to image all case home page
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Sandbox_Minimize_Home_As_Tablet(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {


            Sandbox_Upload_Image(backofficeDriver, "//div[@id='supeRepCont']//tr[1]/td[2]/table[1]/tbody[1]/tr[1]/td[2]/div[1][@id='btnSupeRepLandscape']", @"C:\Users\yosef.h\Desktop\automation_documents\automation_files_pictues\cat.jpeg");

            Sandbox_Upload_Image(backofficeDriver, "/html[1]/body[1]/form[1]/div[6]/div[1]/div[4]/div[2]/div[1]/div[2]/table[1]/tbody[1]/tr[1]/td[1]/table[1]/tbody[1]/tr[1]/td[2]/div[1]", @"C:\Users\yosef.h\Desktop\automation_documents\automation_files_pictues\shirt.jpeg");


            // wait 4 second
            Thread.Sleep(3000);


            Webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(2000);


            Sandbox_Change_Size_All_Case_Home_Screen(webappDriver, "width: 300", "width: 214");
        }


        /// <summary>
        /// Change Size All Case Home_Screen
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="sizeImageLandscape"></param>
        /// <param name="sizeImagePortrait"></param>
        public static void Sandbox_Change_Size_All_Case_Home_Screen(RemoteWebDriver webappDriver, string sizeImageLandscape, string sizeImagePortrait)
        {

            Thread.Sleep(3000);

            webappDriver.Manage().Window.Maximize();


            // Check if Home page Contains cat image Maximize case
            var domElement = webappDriver.FindElement(By.XPath("/html[1]/body[1]/app-root[1]/div[1]/app-home-page[1]/div[1]/img[1]"));


            // Get Attribute of image  
            string attributeMaximize = domElement.GetAttribute("style");


            // width: 300 is cat image
            bool imgStor = attributeMaximize.Contains(sizeImageLandscape);


            Assert(imgStor, "the cat img Maximize case is  not stor");


            // Check if Home page Contains shirt img Middle case
            webappDriver.Manage().Window.Size = new Size(500, 700);
            // wait the Middle case
            Thread.Sleep(2000);

            domElement = webappDriver.FindElement(By.XPath("/html[1]/body[1]/app-root[1]/div[1]/app-home-page[1]/div[1]/img[1]"));

            string attributeMiddle = domElement.GetAttribute("style");

            imgStor = attributeMiddle.Contains(sizeImagePortrait);

            Assert(imgStor, "the shirt img Middle case is not stor");

            webappDriver.Manage().Window.Size = new Size(700, 350);
            // wait the Minimize case
            Thread.Sleep(2000);

            domElement = webappDriver.FindElement(By.XPath("/html[1]/body[1]/app-root[1]/div[1]/app-home-page[1]/div[1]/img[1]"));
            Thread.Sleep(2000);
            string attributeMinimize = domElement.GetAttribute("style");
            imgStor = attributeMinimize.Contains(sizeImageLandscape);
            Assert(imgStor, "the cat img Minimize case is not view");

            webappDriver.Manage().Window.Maximize();
        }


        public static void Sandbox_Branding_Color_Main(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            FullWebappAutomation.Backoffice.CompanyProfile.Branding(backofficeDriver);

            // Edit Main Color
            SafeClick(backofficeDriver, "//div[6]/div[1]/div[4]/div[2]/div[1]/div[5]/div[1]/div[1]/div[2]");

            //yellow
            SafeClick(backofficeDriver, "//div[@class='sp-container sp-light']/div/div/div/span[@title='rgb(255, 255, 0)']/span[@style]");


            var domElement = backofficeDriver.FindElement(By.XPath("//form[1]/div[4]/div[1]"));

            Thread.Sleep(800);

            // Get Attribute of color  
            string attributeBO = domElement.GetAttribute("style");




            Webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(4000);

            domElement = webappDriver.FindElement(By.XPath("//app-root[1]/app-menu[1]/div[1]/nav[1]"));
            string attributeWA = domElement.GetAttribute("style");

            bool isSame = (attributeBO == attributeWA);

            Assert(isSame, "The color Main  home page no change");
        }


        public static void Sandbox_Branding_Color_Secondary(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            FullWebappAutomation.Backoffice.CompanyProfile.Branding(backofficeDriver);

            // Edit Secondary Color
            SafeClick(backofficeDriver, "/html[1]/body[1]/form[1]/div[6]/div[1]/div[4]/div[2]/div[1]/div[5]/div[2]/div[2]");

            //yellow
            SafeClick(backofficeDriver, "//div[@class='sp-container sp-light']/div[1]/div[1]/div[1]/span[5]/span[1]");

            var domElement = backofficeDriver.FindElement(By.XPath("//form[1]/div[4]/div[2]"));

            Thread.Sleep(800);

            // Get Attribute of color  
            string attributeBO = domElement.GetAttribute("style");


            bool isSame = attributeBO.Contains("rgb(255, 255, 0)");

            Assert(isSame, "The color Secondary home page no change");
        }


        public static void Sandbox_Branding_Logo(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            FullWebappAutomation.Backoffice.CompanyProfile.Branding(backofficeDriver);

            // Upload_Image logo
            Sandbox_Upload_Image(backofficeDriver, "//div[@id='content']/div[@id='compLogoCont']/div[1]/table[1]/tbody[1]/tr[1]/td[2]/div[1]", @"C:\Users\yosef.h\Desktop\automation_documents\automation_files_pictues\cow.jpg");


            // element of logo image
            var domElement = webappDriver.FindElement(By.XPath("/html[1]/body[1]/app-root[1]/app-menu[1]/div[1]/nav[1]/div[2]/a[1]/img[1]"));


            // src of image from webapp
            string src= domElement.GetAttribute("src");


            // open a new tab and set the context
            webappDriver.ExecuteScript("window.open('_blank', 'tab2');");
            var browserTabs = webappDriver.WindowHandles;
            webappDriver.SwitchTo().Window(browserTabs[1]);
            webappDriver.Navigate().GoToUrl(src);

            string cow = webappDriver.Title;
           

            webappDriver.Close();
            webappDriver.SwitchTo().Window(browserTabs[0]);


            Assert(cow.Contains("(1024×577)"), "logo no fine webapp");
        }





        #endregion


        #region Account

        /// <summary>
        /// test the TSA fields
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Webapp_Sandbox_Creat_TSA_Fields_And_Added(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Dictionary<string, string> TSA_Fields = new Dictionary<string, string>();


            TSA_Fields.Add("Single Line Text", "TSASingleLineText");
            TSA_Fields.Add("Limited Line Text", "TSALimitedLineText");
            TSA_Fields.Add("Paragraph Text", "TSAParagraphText");
            TSA_Fields.Add("Date", "TSADate");
            TSA_Fields.Add("Date + Time", "TSADateTime");
            TSA_Fields.Add("Duration", "TSADuration");
            TSA_Fields.Add("Number", "TSANumber");
            TSA_Fields.Add("Decimal Number", "TSADecimalNumber");
            TSA_Fields.Add("Currency", "TSACurrency");
            TSA_Fields.Add("Checkbox", "TSACheckbox");
            TSA_Fields.Add("Dropdown", "TSADropdown");
            TSA_Fields.Add("Multi Choice", "TSAMultiChoice");
            TSA_Fields.Add("Image", "TSAImage");
            TSA_Fields.Add("Signature Drawing", "TSASignatureDrawing");
            TSA_Fields.Add("Phone number", "TSAPhonenumber");
            TSA_Fields.Add("Link", "TSALink");


            FullWebappAutomation.Backoffice.Accounts.Fields(backofficeDriver);

            foreach (var item in TSA_Fields)
            {
                // Add Custom Fields
                SafeClick(backofficeDriver, "//span[@class='allButtons grnbtn roundCorner dc-add']");


                // Select Type
                SafeClick(backofficeDriver, string.Format("//div[@id='mainCustomFieldLayout']/div/ul/li/h3[@title='{0}']", item.Key));


                // Name test
                SafeSendKeys(backofficeDriver, "//div[3]/div[@class='section']/input[1]", item.Key);


                // Description test
                SafeSendKeys(backofficeDriver, "//div[1]/div[3]/div[@name='descriptionSection']/input[1]", item.Key);

                // is no exist  Mapped Name Image/Attachment
                if (item.Key != "Image" && item.Key != "Attachment")
                {
                    // Mapped test
                    SafeSendKeys(backofficeDriver, "//div[1]/div[3]/div[@name='mappedNameSection']/input[1]", item.Key);
                }

                if (item.Key == "Dropdown")
                {
                    SafeSendKeys(backofficeDriver, "//div[@class='ComboBox specialSection']/div/div/textarea[@name='textArea']", "first line \n second line");
                }

                if (item.Key == "Multi Choice")
                {
                    SafeSendKeys(backofficeDriver, "//div[@class='MultiTickBox specialSection']/div/div/textarea[@name='textArea']", "first line \n second line");
                }

                //   save 
                SafeClick(backofficeDriver, "//div[1]/div[1]/div[2]/div[2]/div[1]/div[4]/div[@name='save']");

                Thread.Sleep(2000);
            }

            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\Account_TSA.csv", "API_Account_Overwrite_", false);

            Webapp_Sandbox_New_List_Table(webappDriver, backofficeDriver, "TSA_List", TSA_Fields);



            TSA_Fields_Line_(webappDriver, backofficeDriver);

         

        }


        public static void TSA_Fields_Line_(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Dictionary<string, string> TSA_Fields_Line = new Dictionary<string, string>();

            TSA_Fields_Line.Add("Single Line Text", "TSASingleLineText");
            TSA_Fields_Line.Add("Limited Line Text", "TSALimitedLineText");
            TSA_Fields_Line.Add("Duration", "TSADuration");
            TSA_Fields_Line.Add("Number", "TSANumber");
            TSA_Fields_Line.Add("Decimal Number", "TSADecimalNumber");
            TSA_Fields_Line.Add("Currency", "TSACurrency");
            TSA_Fields_Line.Add("Dropdown", "TSADropdown");
            TSA_Fields_Line.Add("Multi Choice", "TSAMultiChoice");
            TSA_Fields_Line.Add("Phone number", "TSAPhonenumber");


            Webapp_Sandbox_New_List_Table(webappDriver, backofficeDriver, "TSA_List_Line", TSA_Fields_Line);


            SafeClick(webappDriver, "//div[@class='ellipsis']");
        }



        /// <summary>
        /// test the Basic fields
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Webapp_Sandbox_Add_Basic_Fields(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Dictionary<string, string> Fields = new Dictionary<string, string>();

            Fields.Add("Name", "Name");
            Fields.Add("City", "City");
            Fields.Add("Street", "Street");
            Fields.Add("Sales Reps", "Agents");
            Fields.Add("Price List ExternalID", "PriceListExternalID");
            Fields.Add("Special price list external ID", "SpecialPriceListExternalID");
            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Email", "Email");
            Fields.Add("Country", "Country");

            Webapp_Sandbox_New_List_Table(webappDriver, backofficeDriver, "Basic_List", Fields);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="backofficeDriver"></param>
        private static void Edit_Rep_Permission(RemoteWebDriver backofficeDriver)
        {
            // Permission
            SafeClick(backofficeDriver, "//md-tab-item[3][@class='md-tab ng-scope ng-isolate-scope ng-binding']");


            // Edit Rep
            SafeClick(backofficeDriver, "//div[4]/div[1]/div[1]/div[1]/div[1]/md-tabs[1]/md-tabs-content-wrapper[1]/md-tab-content[3]/div[1]/div[1]/div[1]/div[2]/div[1]/span[2]");


            // Account Lists
            SafeClick(backofficeDriver, "//div[1]/div[1]/md-tabs[1]/md-tabs-content-wrapper[1]/md-tab-content[3]/div[1]/div[1]/div[3]/div[2]/ul[1]");
            SafeClick(backofficeDriver, "//div[1]/md-tabs[1]/md-tabs-content-wrapper[1]/md-tab-content[3]/div[1]/div[1]/div[3]/div[2]/ul[1]/div[1]/ul[1]/li[1]/div[1]/div[1]");


            // Save
            SafeClick(backofficeDriver, "//div[1]/md-tabs[1]/md-tabs-content-wrapper[1]/md-tab-content[3]/div[1]/div[1]/div[4]/div[1]/div[1]");
        }


        /// <summary>
        /// Creat_New_List
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="nameNewList">new the new list</param>
        private static void Creat_New_List(RemoteWebDriver backofficeDriver,string nameNewList)
        {
            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);

            // + Create New List
            SafeClick(backofficeDriver, "//div[@id='btnAddNewAcc']");


            //  Input New List Fildes
            // input name
            SafeSendKeys(backofficeDriver, "//div[@class='ListName parCont clearfix']/input[@name='name']", nameNewList);


            // input description
            SafeSendKeys(backofficeDriver, "//div[@class='ListDescription parCont clearfix']/input[@name='description']", "Automation "+nameNewList);


            //// Date Range
            //SafeClick(backofficeDriver, "//div[@id='s2id_autogen17']//a[@class='select2-choice']");


            //// Creation Date 
            //SafeClick(backofficeDriver, "//select[@class='dateApiName ng-untouched ng-valid ng-not-empty ng-dirty ng-valid-parse']//option[@value='CreationDate']");


            // Save
            SafeClick(backofficeDriver, "//div[@class='general-info ng-scope']/input[1]");

            
        }
      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        /// <param name="nameNewList"></param>
        /// <param name="Fields"></param>
        public static void Webapp_Sandbox_New_List_Table(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver, string nameNewList, Dictionary<string, string> Fields,bool isHeader=true)
        {
            int index = 1;
            string valueH = "";
            string valueL;
            bool isContains = true;


            Creat_New_List(backofficeDriver, nameNewList);


            //  Add fields
            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Fields, nameNewList);


            Thread.Sleep(2000);


            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);


            Edit_Rep_Permission(backofficeDriver);


            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);


            Webapp_Sandbox_Resync(webappDriver, backofficeDriver);


            Thread.Sleep(5000);


            // Account
            for (int i = 1; i < 10; i++)
            {
                try
                {
                    if (SafeGetValue(webappDriver, string.Format("//app-root[1]/div[1]/app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i.ToString()), "innerHTML") == "Accounts")
                        SafeClick(webappDriver, string.Format("//app-root[1]/div[1]/app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i.ToString()));
                    break;
                }
                catch { }
            }

            try
            {
                SafeClick(webappDriver, string.Format("//div[@title='{0}']", nameNewList),safeWait:300,maxRetry:20);
            }
            catch
            {
                try
                {
                    if (SafeGetValue(webappDriver, "//div[@class='ellipsis']", "innerHTML") != nameNewList)
                    {
                        SafeClick(webappDriver, "//div[@class='ellipsis']");
                        SafeClick(webappDriver, string.Format("//li[@title='{0}']",nameNewList));
                    }
                }
                catch 
                {
                    isContains = false;
                }
            }


            Thread.Sleep(5000);

            if (isHeader)
            {
                // Check if all fields come to webapp
                while (true)
                {
                    try
                    {
                        valueH = SafeGetValue(webappDriver, string.Format("//app-custom-list//div[{0}][@class='lc pull-left flip ng-star-inserted']/label", index), "innerHTML", maxRetry: 3);
                        if (!Fields.Keys.Contains(valueH))
                        {
                            isContains = false;
                            break;
                        }

                        index++;
                    }
                    catch
                    {
                        break;
                    }
                }

                Assert(index == Fields.Count + 1 && isContains, "No all fields come to webapp");
            }
            else
            {

            }
        }


        public static void Webapp_Sandbox_Search_Account(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            Creat_New_List( backofficeDriver,  "basic");

            // key=name, value=API name
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Street", "Street");


            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Fields, "basic");

            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);

            Edit_Rep_Permission(backofficeDriver);


            Sreach(backofficeDriver,Fields);

            Thread.Sleep(3000);

            // Accounts
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div/div");

            // Get name of last account
            String name = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[4]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", "title").ToString();

            // Click search button
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/a/span");

            // Input name
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/input");
            SafeSendKeys(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/input", name);

            // Click search button
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/a/span");
            Thread.Sleep(bufferTime);


            String foundName = SafeGetValue(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[1]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", "title").ToString();

            Assert(serach(name, foundName), "Account search failed (found name doesn't match expected)");
            /* bool Compar = false;
             StringComparison comp = StringComparison.OrdinalIgnoreCase;
             Compar= foundName.Contains(name,comp);*/

            //public static bool serach();

            string temp = null;
            int index = 0;
            while (index != -1)
            {
                index = name.IndexOf(" ");
                if (index > -1)
                    temp = name.Substring(0, index);
                else
                    temp = name;
                //foundName.Contains(temp);
                Assert(foundName.Contains(temp), "Account search failed (found name doesn't match expected)");
                name = name.Remove(0, index + 1);
            }



            // Assert(name == foundName, "Account search failed (found name doesn't match expected)");
        }

        private static void Sreach(RemoteWebDriver backofficeDriver, Dictionary<string, string> fields)
        {
            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);

            // Configuration


            // edit search
            //md-tab-content[2]//div[@style='top:0px']/div[@class='slick-cell l0 r0']

          //  --------------------------------------------------------------------------------------To do
            // edit rep


        }

        #endregion

        #region Activities 




        #endregion
    }
}
