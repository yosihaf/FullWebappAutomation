using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using static FullWebappAutomation.GlobalSettings;
using static FullWebappAutomation.HelperFunctions;
using static FullWebappAutomation.Consts;
using static FullWebappAutomation.BakeofficeTest;
using static FullWebappAutomation.WebappTest;
using System.Threading;
using System.Linq;
using System.Drawing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Interactions;
using System.Data.SqlClient;
using System.Data;

namespace FullWebappAutomation
{
    class Tests
    {
        public static dynamic connectDB(string sql)
        {
            string connectionString = GetUserPassword("1");
            SqlDataReader reader;
            DataTable dt = new DataTable();

           
            using (SqlConnection con = new SqlConnection(connectionString))
            {
             
                
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql,con);
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    con.Close();
               

            }
            return dt;
        }

        public static void All_Backoffice_Menus(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            BackofficeNavigation.GeneralActions.SandboxLogin(backofficeDriver, Username, Password);

            BackofficeNavigation.CompanyProfile.App_Home_Screen(backofficeDriver);
            BackofficeNavigation.CompanyProfile.Branding(backofficeDriver);
            BackofficeNavigation.CompanyProfile.Company_Profile(backofficeDriver);
            BackofficeNavigation.CompanyProfile.Email_Settings(backofficeDriver);
            BackofficeNavigation.CompanyProfile.Home_Screen_Shortcut(backofficeDriver);
            BackofficeNavigation.CompanyProfile.Security(backofficeDriver);
            BackofficeNavigation.CompanyProfile.Sync_Settings(backofficeDriver);

            BackofficeNavigation.Catalogs.Manage_Catalogs(backofficeDriver);
            BackofficeNavigation.Catalogs.Edit_Form(backofficeDriver);
            BackofficeNavigation.Catalogs.Catalog_Views(backofficeDriver);
            BackofficeNavigation.Catalogs.Fields(backofficeDriver);

            BackofficeNavigation.Items.Order_Center_Thumbnail_Views(backofficeDriver);
            BackofficeNavigation.Items.Order_Center_Grid_View(backofficeDriver);
            BackofficeNavigation.Items.Order_Center_Matrix_View(backofficeDriver);
            BackofficeNavigation.Items.Order_Center_Flat_Matrix_View(backofficeDriver);
            BackofficeNavigation.Items.Order_Center_Item_Details_View(backofficeDriver);
            BackofficeNavigation.Items.Catalog_Item_View(backofficeDriver);
            BackofficeNavigation.Items.Item_Share_Email_Info(backofficeDriver);
            BackofficeNavigation.Items.Smart_Search(backofficeDriver);
            BackofficeNavigation.Items.Filters(backofficeDriver);
            BackofficeNavigation.Items.Automated_Image_Uploader(backofficeDriver);
            BackofficeNavigation.Items.Fields(backofficeDriver);

            BackofficeNavigation.Accounts.Views_And_Forms(backofficeDriver);
            BackofficeNavigation.Accounts.Accounts_Lists(backofficeDriver);
            BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);
            BackofficeNavigation.Accounts.Map_View(backofficeDriver);
            BackofficeNavigation.Accounts.Card_Layout(backofficeDriver);
            BackofficeNavigation.Accounts.Account_Dashboard_Layout(backofficeDriver);
            BackofficeNavigation.Accounts.Search(backofficeDriver);
            BackofficeNavigation.Accounts.Smart_Search(backofficeDriver);
            BackofficeNavigation.Accounts.Fields(backofficeDriver);

            BackofficeNavigation.PricingPolicy.Pricing_Policy(backofficeDriver);
            BackofficeNavigation.PricingPolicy.Price_Level(backofficeDriver);
            BackofficeNavigation.PricingPolicy.Main_Category_Discount(backofficeDriver);
            BackofficeNavigation.PricingPolicy.Account_Special_Price_List(backofficeDriver);

            BackofficeNavigation.Users.Manage_Users(backofficeDriver);
            BackofficeNavigation.Users.Role_Heirarchy(backofficeDriver);
            BackofficeNavigation.Users.Profiles(backofficeDriver);
            BackofficeNavigation.Users.User_Lists(backofficeDriver);
            BackofficeNavigation.Users.Targets_Type(backofficeDriver);
            BackofficeNavigation.Users.Manage_Targets(backofficeDriver);
            BackofficeNavigation.Users.Rep_Dashboard_Add_Ons(backofficeDriver);

            BackofficeNavigation.Contacts.Contact_Lists(backofficeDriver);

            BackofficeNavigation.SalesActivities.Transaction_Types(backofficeDriver);
            BackofficeNavigation.SalesActivities.Activity_Types(backofficeDriver);
            BackofficeNavigation.SalesActivities.Sales_Activity_Lists(backofficeDriver);
            BackofficeNavigation.SalesActivities.Activity_List_Display_Options(backofficeDriver);
            BackofficeNavigation.SalesActivities.Activities_And_Menu_Setup(backofficeDriver);
            BackofficeNavigation.SalesActivities.Sales_Dashboard_Settings(backofficeDriver);

            BackofficeNavigation.ActivityPlanning.Account_Lists(backofficeDriver);
            BackofficeNavigation.ActivityPlanning.Activity_Planning_Display_Options(backofficeDriver);

            BackofficeNavigation.ERPIntegration.Plugin_Settings(backofficeDriver);
            BackofficeNavigation.ERPIntegration.Configuration(backofficeDriver);
            BackofficeNavigation.ERPIntegration.File_Uploads_And_Logs(backofficeDriver);

            BackofficeNavigation.ConfigurationFiles.Automated_Reports(backofficeDriver);
            BackofficeNavigation.ConfigurationFiles.Configuration_Files(backofficeDriver);
            BackofficeNavigation.ConfigurationFiles.Translation_Files(backofficeDriver);
            BackofficeNavigation.ConfigurationFiles.Online_Add_Ons(backofficeDriver);
            BackofficeNavigation.ConfigurationFiles.User_Defined_Tables(backofficeDriver);
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


        public static void webapp_Sandbox_Resync(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
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
            Small_view(webappDriver, "Small");

            // Item qty plus
            SafeClick(webappDriver, Xpath_item_qty_plus_or_minus(1, 2));

            Thread.Sleep(bufferTime);

            // Get units qty from qty selector
            orderInfo["unitsQty"] = SafeGetValue(webappDriver, item_qty_input(1), "title");

            // Cart
            SafeClick(webappDriver, cart());

            // Small view
            Small_view(webappDriver, "GridLine");

            // Transaction Menu
            SafeClick(webappDriver, Transaction_Menu());

            // Order details
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/ul/li/span");
            Thread.Sleep(bufferTime);

            // Create remark and store it
            orderInfo["orderRemark"] = "Automation " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Thread.Sleep(bufferTime);

            // Click remark field
            SafeClick(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[15]/div/app-custom-field-generator/app-custom-textbox/div/input");
            Thread.Sleep(bufferTime);

            // Add remark
            SafeSendKeys(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[15]/div/app-custom-field-generator/app-custom-textbox/div/input", orderInfo["orderRemark"]);
            Thread.Sleep(bufferTime);

            // Save button
            SafeClick(webappDriver, "//body/app-root/div/app-order-details/app-bread-crumbs/div/div/div/div[3]/div[2]");
            Thread.Sleep(bufferTime);

            // Submit
            SafeClick(webappDriver, "//button[@id='btnTransition']/span");
            Thread.Sleep(bufferTime);

            // Home button
            SafeClick(webappDriver, "//a[@id='btnMenuHome']/span");

            //try
            //{
            //    SafeClick(webappDriver, "//div[@class='btn allButtons btnOk grnbtn ng-star-inserted']");
            webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            //}
            //catch { }

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
            webapp_Sandbox_Home_Button(webappDriver, "Activities");
            Thread.Sleep(3000);


            //"ALL"
            select_list_general(webappDriver, "ALL");
            Thread.Sleep(3000);

            int index = 1;
            string value = "";

            while (value != "Order Remark")
            {
                try
                {
                    value = SafeGetValue(webappDriver, string.Format("//app-custom-list//div[{0}][@class='lc pull-left flip ng-star-inserted']/label", index), "innerHTML", maxRetry: 3);
                    if (value == "Order Remark")
                        break;
                    index++;
                }
                catch
                {
                    break;
                }
            }


            // Click order by Remark
            SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div/fieldset/div[{0}]/label", index));
            SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div/fieldset/div[{0}]/div/i[2]", index));
            Thread.Sleep(5000);


            // Get remark and assert correctly - first 10 activities
            bool remarkMatch = false;
            int orderIndexInList = 0;
            string actualRemark;

            for (int i = 1; i < 6; i++)
            {
                actualRemark = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/label[1]", i, index), "innerHTML").ToString();

                if (actualRemark == orderInfo["orderRemark"])
                {
                    remarkMatch = true;
                    orderIndexInList = i;
                    break;
                }
            }

            Assert(remarkMatch, "No sales order with matching remark in activity list");

            // Get actual activity ID from web page and compare with API data
            // string actualActivityID = SafeGetValue(webappDriver, string.Format("(//label[@id='WrntyID'])[{0}]", orderIndexInList), "innerHTML").ToString();

            // Get Activity ID from API
            var apiData = GetApiData(Username, Password, "transactions", "Remark=", orderInfo["orderRemark"]);
            string apiActivityRemark = apiData[0].Remark.ToString();

            Assert(orderInfo["orderRemark"] == apiActivityRemark, "Activity ID doesn't match API data");

            // Check correct Units Qty 
            // SafeClick(webappDriver, string.Format("(//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span)[{0}]", orderIndexInList));
            string actualUnitsQty = apiData[0].QuantitiesTotal.ToString();

            Assert(actualUnitsQty == orderInfo["unitsQty"], "Actual units Qty doesn't match ordered Qty");
        }


        /// <summary>
        /// Check if sales order arrives correctly to Backoffice
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="orderInfo"></param>
        public static void Backoffice_Sandbox_Check_Sales_Order(RemoteWebDriver backofficeDriver, Dictionary<string, string> orderInfo)
        {
            //backofficeDriver.Navigate().GoToUrl(backofficeSandboxHomePageUrl);
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
            var apiData = GetApiData(Username, Password, "transactions", "Remark=", orderInfo["orderRemark"]);
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

                webapp_Sandbox_Resync(webappDriver, backofficeDriver);
                Thread.Sleep(3000);
                string actualHomeButton = SafeGetValue(webappDriver, "//div[@id='mainButton']", "innerHTML").ToString();
                homeButtonMatch = actualHomeButton == "Visit";

            }
            catch { }

            finally
            {
                // Revert Home button to sales order
                Backoffice_Sandbox_Config_Home_Button(backofficeDriver, "(Sales Order)");
                webapp_Sandbox_Resync(webappDriver, backofficeDriver);


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

            BackofficeNavigation.CompanyProfile.Home_Screen_Shortcut(backofficeDriver);

            // Edit Admin
            backoffice_Edit_Admin(backofficeDriver, "Admin");

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

                webapp_Sandbox_Resync(webappDriver, backofficeDriver);

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

            BackofficeNavigation.CompanyProfile.App_Home_Screen(backofficeDriver);

            // Edit Admin
            backoffice_Edit_Admin(backofficeDriver, "Admin");

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
            BackofficeNavigation.CompanyProfile.App_Home_Screen(backofficeDriver);

            // Edit Admin
            backoffice_Edit_Admin(backofficeDriver, "Admin");

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
            Small_view(webappDriver, "Small");

            // Get item name from webpage
            string itemName = SafeGetValue(webappDriver, "//div/div[1]/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[6]/figure/app-custom-field-generator/app-custom-textbox/label/span", "innerHTML");

            // Get item ID from API
            var apiData = GetApiData(Username, Password, "items", "Name=", itemName);
            string apiItemCode = apiData[0].ExternalID.ToString();

            // Click search icon
            SafeClick(webappDriver, "//body/app-root/div/app-order-center/div/app-bread-crumbs/div/div/div/span/i");

            // Input search parameter
            SafeSendKeys(webappDriver, "//div/app-bread-crumbs//div[@class='input-group']//input", apiItemCode);

            // Click search button
            SafeClick(webappDriver, "//div/app-bread-crumbs//div[@class='input-group']//span[2]/i");

            Thread.Sleep(bufferTime);

            // Get item code from webpage
            string actualItemName = SafeGetValue(webappDriver, "//div/div[1]/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[6]/figure/app-custom-field-generator/app-custom-textbox/label/span", "innerHTML");

            Assert(itemName == actualItemName, "Actual item doesn't match expected");
        }


        // Checked with "Sales Order"
        public static void Webapp_Sandbox_Minimum_Quantity(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder(webappDriver);

            // Small view
            Small_view(webappDriver, "Small");

            // Get item ID from webpage
            string itemID = SafeGetValue(webappDriver, "//div/div[1]/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[7]/figure/app-custom-field-generator/app-custom-textbox/label/span", "innerHTML");

            // Get item data from api
            var apiData = GetApiData(Username, Password, "items", "ExternalID=", itemID);

            // Parse the data to integer and store it in variable - min qty
            int apiItemMinQty;
            Int32.TryParse(apiData[0].MinimumQuantity.ToString(), out apiItemMinQty);

            Assert(apiItemMinQty > 1, "Item Qty is 1, unable to perform test");

            // Item Qty selector field
            SafeClick(webappDriver, "//div/div[1]/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/input");

            // Insert less than min qty into qty selector
            SafeSendKeys(webappDriver, "//div/div[1]/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/input", (apiItemMinQty - 1).ToString());

            // Click outside the box
            SafeClick(webappDriver, "//div/div[1]/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[7]/figure/app-custom-field-generator/app-custom-textbox/label/span");

            Thread.Sleep(bufferTime);

            // Check qty selector style - supposed to be red (alerted)
            string qtySelectorStyle = SafeGetValue(webappDriver, "//div/div[1]/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile[8]/figure/app-custom-field-generator/app-custom-quantity-selector/div/input", "style");

            Assert(qtySelectorStyle.Contains("color: rgb(255, 0, 0);"), "Min qty doesn't mark in red");
        }


        // Checked with "Sales Order"
        public static void Webapp_Sandbox_Delete_Cart_Item(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder(webappDriver);

            // Small view
            Small_view(webappDriver, "Small");


            // First item qty plus
            SafeClick(webappDriver, Xpath_item_qty_plus_or_minus(1, 2));


            // Second item qty plus
            SafeClick(webappDriver, Xpath_item_qty_plus_or_minus(2, 2));


            // Cart
            SafeClick(webappDriver, cart());


            // Small view
            //Small_view(webappDriver, "GridLine");


            // Click minus button on first item
            SafeClick(webappDriver, Xpath_item_qty_plus_or_minus(1, 1));

            // Click "Delete" alert
            SafeClick(webappDriver, "//div[@type='button'][2]");

            // Click qty selector on remaining item
            SafeClick(webappDriver, item_qty_input(1));

            // Insert 0 into qty selector
            SafeClear(webappDriver, item_qty_input(1));
            SafeSendKeys(webappDriver, item_qty_input(1), "0");

            // Press enter
            SafeSendKeys(webappDriver, item_qty_input(1), Keys.Enter);

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
            Small_view(webappDriver, "Small");

            // First item qty plus
            SafeClick(webappDriver, Xpath_item_qty_plus_or_minus(1, 2));

            // Cart
            SafeClick(webappDriver, cart());

            // Small view
            Small_view(webappDriver, "GridLine");

            // Get unit price
            string unitPriceStr = SafeGetValue(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//label[@id='UnitPrice']", "innerHTML");
            unitPriceStr = unitPriceStr.Trim(new Char[] { ' ', '$' });
            double unitPrice;
            double.TryParse(unitPriceStr, out unitPrice);

            // Click on unit discount
            SafeClick(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitDiscountPercentage']");

            // Input 50% discount
            SafeClear(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitDiscountPercentage']");
            SafeSendKeys(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitDiscountPercentage']", "50");
            SafeSendKeys(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitDiscountPercentage']", Keys.Enter);
            Thread.Sleep(bufferTime);

            // Get price after discount
            string priceAfterDiscountStr = SafeGetValue(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitPriceAfterDiscount']", "title");
            priceAfterDiscountStr = priceAfterDiscountStr.Trim(new Char[] { '$' });
            double priceAfterDiscount;
            double.TryParse(priceAfterDiscountStr, out priceAfterDiscount);

            Assert(priceAfterDiscount == unitPrice / 2, "Price after discount miscalculated");

            double unitPriceAfterDiscount = 3;

            // Click on price after discount
            SafeClick(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitPriceAfterDiscount']");

            // Input price after discount
            SafeClear(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitPriceAfterDiscount']");
            SafeSendKeys(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitPriceAfterDiscount']", unitPriceAfterDiscount.ToString());
            SafeSendKeys(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitPriceAfterDiscount']", Keys.Enter);
            Thread.Sleep(bufferTime);

            // Click on unit discount
            SafeClick(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitDiscountPercentage']");

            // Get unit discount
            string discountStr = SafeGetValue(webappDriver, "//div[@class='grid-row ng-star-inserted'][1]//input[@id='UnitDiscountPercentage']", "title");
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
            Small_view(webappDriver, "Small");

            // Item qty plus
            SafeClick(webappDriver, Xpath_item_qty_plus_or_minus(1, 2));

            Thread.Sleep(bufferTime);

            // Get units qty from qty selector
            orderInfo["unitsQty"] = SafeGetValue(webappDriver, item_qty_input(1), "innerHTML");

            // Cart
            SafeClick(webappDriver, cart());

            // Small view
            Small_view(webappDriver, "GridLine");

            // Transaction Menu
            SafeClick(webappDriver, Transaction_Menu());

            // Order details
            SafeClick(webappDriver, selectAnyValueFromMnue("Order Details"));


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
            webapp_Sandbox_Home_Button(webappDriver, "Activities");
            Thread.Sleep(3000);

            //"All"
            select_list_general(webappDriver, "ALL");
            Thread.Sleep(3000);


            int index = 1;
            string value = "";

            while (value != "Order Remark")
            {
                try
                {
                    value = SafeGetValue(webappDriver, string.Format("//app-custom-list//div[{0}][@class='lc pull-left flip ng-star-inserted']/label", index), "innerHTML", maxRetry: 3);
                    if (value == "Order Remark")
                        break;
                    index++;
                }
                catch
                {
                    break;
                }
            }


            // Click order by Remark
            SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div/fieldset/div[{0}]/label", index));
            SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div/fieldset/div[{0}]/div/i[2]", index));
            Thread.Sleep(5000);


            //  Find the sales order in activity list
            string actualRemark;
            int orderIndexInList = 0;

            for (int i = 1; i < 12; i++)
            {
                actualRemark = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/label[1]", i, index), "innerHTML").ToString();

                if (actualRemark == orderInfo["orderRemark"])
                {
                    orderIndexInList = i;
                    break;
                }
            }

            // Drill down into the sales order 
            SafeClick(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[1]/app-custom-button[1]/a[1]/span", orderIndexInList));
            Thread.Sleep(3000);


            // Continue ordering
            SafeClick(webappDriver, "(//button[@type='button'])[2]");

            // Item qty plus
            SafeClick(webappDriver, Xpath_item_qty_plus_or_minus(1, 2));

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
            Small_view(webappDriver, "Small");

            // First item qty plus
            SafeClick(webappDriver, Xpath_item_qty_plus_or_minus(1, 2));

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

            string item1 = "", item2 = "";


            // item code
            item1 = SafeGetValue(webappDriver, string.Format("//div[contains(@class,'grid-body')]/div[1]/app-custom-form/fieldset/div[2]/app-custom-field-generator/app-custom-textbox/label"), "innerHTML");
            item2 = SafeGetValue(webappDriver, string.Format("//div[contains(@class,'grid-body')]/div[2]/app-custom-form/fieldset/div[2]/app-custom-field-generator/app-custom-textbox/label"), "innerHTML");
            Assert(item1 == item2, "Duplicate action failed for item code");


            // QTY
            item1 = SafeGetValue(webappDriver, string.Format("//div[contains(@class,'grid-body')]//div[1]//app-custom-form[1]//fieldset[1]//div[5]//app-custom-field-generator[1]//app-custom-quantity-selector[1]//div[1]//input[@id='UnitsQuantity']"), "title");
            item2 = SafeGetValue(webappDriver, string.Format("//div[contains(@class,'grid-body')]//div[2]//app-custom-form[1]//fieldset[1]//div[5]//app-custom-field-generator[1]//app-custom-quantity-selector[1]//div[1]//input[@id='UnitsQuantity']"), "title");
            Assert(item1 == item2, "Duplicate action failed for QTY");


            // Unit Price
            item1 = SafeGetValue(webappDriver, string.Format("//div[contains(@class,'grid-body')]/div[1]/app-custom-form/fieldset/div[6]/app-custom-field-generator/app-custom-textbox/label"), "innerHTML");
            item2 = SafeGetValue(webappDriver, string.Format("//div[contains(@class,'grid-body')]/div[2]/app-custom-form/fieldset/div[6]/app-custom-field-generator/app-custom-textbox/label"), "innerHTML");
            Assert(item1 == item2, "Duplicate action failed for Unit Price");


            // Unit Price After Discount
            item1 = SafeGetValue(webappDriver, string.Format("//div[contains(@class,'grid-body')]/div[1]/app-custom-form/fieldset/div[8]/app-custom-field-generator/app-custom-textbox//input"), "title");
            item2 = SafeGetValue(webappDriver, string.Format("//div[contains(@class,'grid-body')]/div[2]/app-custom-form/fieldset/div[8]/app-custom-field-generator/app-custom-textbox//input"), "title");
            Assert(item1 == item2, "Duplicate action failed for Unit Price After Discount");



        }


        // Checked with "Sales Order 2"
        public static void Webapp_Sandbox_Inventory_Alert(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            GetToOrderCenter_SalesOrder(webappDriver);

            // Small view
            Small_view(webappDriver, "Small");

            // Get item Name from webpage
            string itemID = SafeGetValue(webappDriver, "//div[1]/app-custom-form[1]/fieldset[1]/mat-grid-list[1]/div[1]/mat-grid-tile[3]/figure[1]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]//span", "innerHTML");

            // Get item api data
            var apiData = GetApiData(Username, Password, "items", "ExternalID=", itemID);

            // Parse out ExternalID
            string ExternalID = apiData[0].ExternalID.ToString();

            // Get item inverntory data from api
            apiData = GetApiData(Username, Password, "inventory", "Item.ExternalID=", ExternalID);

            // Parse out InStockQuantity
            int inStockQuantity;
            Int32.TryParse(apiData[0].InStockQuantity.ToString(), out inStockQuantity);

            // Item Qty selector field
            SafeClick(webappDriver, item_qty_input(1));

            // Insert less than min qty into qty selector
            SafeSendKeys(webappDriver, item_qty_input(1), (inStockQuantity + 20).ToString());

            // Click outside the box
            SafeClick(webappDriver, "//div[1]/app-order-center[1]/div[1]/app-bread-crumbs[1]/div[1]/div[1]/div[1]/div[1]/span[4]/span");

            Thread.Sleep(bufferTime);

            string quantityStr = SafeGetValue(webappDriver, item_qty_input(1), "title");

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
            WebappTest.webapp_Sandbox_Home_Button(webappDriver, "Activities");

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
            WebappTest.webapp_Sandbox_Home_Button(webappDriver, "Accounts");


            // First account
            SafeClick(webappDriver, first_account(webappDriver, 1));


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
            WebappTest.webapp_Sandbox_Home_Button(webappDriver, "Accounts");


            // Get first account's name and drill down into it
            string name = SafeGetValue(webappDriver, first_account(webappDriver, 1), "title");
            SafeClick(webappDriver, first_account(webappDriver,1));


            // Get account name from top of page and assert
            string nameInside = SafeGetValue(webappDriver,string.Format("//label[contains(text(),'{0}')]", name), "innerHTML");

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
            WebappTest.webapp_Sandbox_Home_Button(webappDriver, "Accounts");


            // Drill down into first account
            SafeClick(webappDriver, first_account(webappDriver,1));


            // ALL 
            select_list(webappDriver,"ALL");

            int indexOrder = 1;

            string value = "";

            while (value != "Order Remark")
            {
                try
                {
                    value = SafeGetValue(webappDriver, string.Format("//app-custom-list//div[{0}][@class='lc pull-left flip ng-star-inserted']/label", indexOrder), "innerHTML", maxRetry: 3);
                    if (value == "Order Remark")
                    {

                        break;
                    }

                    indexOrder++;
                }
                catch
                {
                    break;
                }
            }


            // Click order by Remark
            SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div/fieldset/div[{0}]/label", indexOrder));
            SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div/fieldset/div[{0}]/div/i[2]", indexOrder));
            Thread.Sleep(5000);


            string remark = "";
            string type = "";
            int i = 1;


            for ( i = 1; ; i++)
            {
                //actualRemark = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/label[1]", i, index), "innerHTML").ToString();

                remark = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/label[1]", i, indexOrder), "innerHTML").ToString();

                if (remark != "")
                {
                    SafeClick(webappDriver, first_account(webappDriver, 1));
                    break;
                }
            }

            // Transaction Menu
            SafeClick(webappDriver, Transaction_Menu());

            // Order details
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/ul/li/span");
            Thread.Sleep(bufferTime);



            // Click remark field
            SafeClick(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[15]/div/app-custom-field-generator/app-custom-textbox/div/input");
            Thread.Sleep(bufferTime);

            // Copy remark
            string continueOrderingButton = SafeGetValue(webappDriver, "//div[@id='orderDetailsContainer']/app-custom-form/fieldset/div[15]/div/app-custom-field-generator/app-custom-textbox/div/input", "title");
            Thread.Sleep(bufferTime);


            Assert(continueOrderingButton == remark, "Account activity drilldown failed (couldn't find continue ordering button)");


            //// Get activity IDs and remarks until you find one with a remark 
            //while (remark == "" || (type != "Sales Order" && type != "Sales Order  2"))
            //{
            //    try
            //    {
            //        i++;
            //        remark = SafeGetValue(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[{0}]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", i), "title").ToString();
            //        type = SafeGetValue(webappDriver, string.Format("(//label[@id='Type'])[{0}]", i), "title");
            //    }
            //    catch { break; }
            //}

            //// Drill down to the chosen activity
            //SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[{0}]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span", i));

            

           // Assert(continueOrderingButton == "Continue ordering", "Account activity drilldown failed (couldn't find continue ordering button)");
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
            Small_view(webappDriver, "Small");

            // First item qty plus
            SafeClick(webappDriver, Xpath_item_qty_plus_or_minus(1, 2));
            Thread.Sleep(bufferTime);

            // Get units qty from qty selector
            string originalQty = SafeGetValue(webappDriver, item_qty_input(1), "title");


            // Cart
            SafeClick(webappDriver, cart());

            // Transaction Menu
            SafeClick(webappDriver, Transaction_Menu());

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
            SafeClick(webappDriver, Transaction_Menu());

            // Duplicate transaction
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/ul/li[3]/span");
            Thread.Sleep(bufferTime);

            // Get units qty from qty selector
            string duplicatedQty = SafeGetValue(webappDriver, "//input[@id='UnitsQuantity']", "title");
            Console.WriteLine(duplicatedQty);

            // Transaction Menu
            SafeClick(webappDriver, Transaction_Menu());

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
            SafeClick(webappDriver, Transaction_Menu());

            // Order details
            SafeClick(webappDriver, "//div[@id='containerActions']/ul/li/ul/li/span");

            // Save button
            SafeClick(webappDriver, "//body/app-root/div/app-order-details/app-bread-crumbs/div/div/div/div[3]/div[2]");

        }


        public static void Backoffice_Sandbox_Load_File(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            FullWebappAutomation.BackofficeNavigation.ERPIntegration.Configuration(backofficeDriver);


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


            // UploadFile  API_PriceLevel_ to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_Price_List.csv", "API_PriceLevel_", false);

            // check if file succee loaded API_PriceLevel_
            checkFile(backofficeDriver, "API_PriceLevel_");


            // UploadFile  API_Special_Price_List to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_Special_Price_List.csv", "API_SpecialPriceLevel_", true);



            // check if file succee loaded API_SpecialPriceLevel_
            checkFile(backofficeDriver, "API_SpecialPriceLevel_");

            // UploadFile  API_Item to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_Item.csv", "API_Item_", true);


            // check if file succee loaded API_Item
            checkFile(backofficeDriver, "API_Item");


            // UploadFile  API_PriceLevelItem_ to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_price_List_Item.csv", "API_PriceLevelItem_", true);



            // check if file succee loaded API_PriceLevelItem_
            checkFile(backofficeDriver, "API_PriceLevelItem_");



            // UploadFile  API_Inventory_ to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_Inventory.csv", "API_Inventory_", true);


            // check if file succee loaded API_Inventory_
            checkFile(backofficeDriver, "API_Inventory");


            // UploadFile  API_Account to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_Account.csv", "API_Account_Overwrite_", true);



            // check if file succee loaded API_Account_Overwrite_
            checkFile(backofficeDriver, "API_Account_Overwrite_");

            // UploadFile  API_Transaction Sales Order to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_Transaction Sales Order.csv", "API_Transaction_Sales Order_", true);


            // check if file succee loaded API_Transaction_Sales Order_
            checkFile(backofficeDriver, "API_Transaction_Sales Order_");

            // UploadFile  API_Transactionline Sales Order to web
            UploadFile(backofficeDriver, @"C:\Users\yosef.h\Desktop\automation_documents\automation_files\API_TransactionLine Sales Order.csv", "API_TransactionLine_Sales Order_", true);


            // check if file succee loaded API_Item
            checkFile(backofficeDriver, "API_TransactionLine_Sales Order_");

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
            webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(1500);

            // get value Home button
            string value = SafeGetValue(webappDriver, "//div[1]/app-home-page[1]/footer[1]/div[@class='container']//div[@class='row ng-star-inserted']/div[1]", "innerHTML");

            // Assert if change
            Assert(value == "Sales Order 1", "The title no change");


            // reverse to Sales Order 
            Help_Sandbox_Change_Title_Home_Screen(backofficeDriver, "Sales Order");


            // Resync
            webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(1500);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Webapp_Sandbox_Change_All_Home(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            Dictionary<string, string> Fields = new Dictionary<string, string>();


            Backoffice_Change_All_Button_Home(backofficeDriver, Fields);


            string value = "";
            webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(4000);

            bool flag = false;

            // check if exist  button in home page 
            for (int i = 1; ; i++)
            {
                try
                {
                    value = SafeGetValue(webappDriver, string.Format("//app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i), "innerHTML", maxRetry: 2);
                    if (!Fields.ContainsKey(value))
                    {
                        break;
                    }
                }
                catch
                {
                    if (i == Fields.Count + 1)
                        flag = true;
                    break;
                }

            }


            // reverse all
            Backoffice_Delete_Layout(backofficeDriver, "Accounts", "Activity List", true);





            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");
            Thread.Sleep(4000);

            Assert(flag, "sume of button different between webapp to bakeoffice");

        }



        public static void Backoffice_Change_All_Button_Home(RemoteWebDriver backofficeDriver, Dictionary<string, string> Fields)
        {
            Fields.Add("Accounts", "Accounts");
            Fields.Add("Activity List", "ActivityList");
            Fields.Add("Contacts", "Contacts");
            Fields.Add("Users", "Users");
            Fields.Add("Photo", "Photo");
            Fields.Add("All Sales Transactions", "AL1");
            Fields.Add("Sales Order", "Sales Order");
            Fields.Add("Visit", "Visit");
            Fields.Add("All Activities", "AL2");
            Fields.Add("ALL", "AL7");

            Backoffice_Delete_Layout(backofficeDriver);

            foreach (var item in Fields)
            {
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).SendKeys(item.Key);
                SafeClick(backofficeDriver, string.Format("//span[@class='fl' and contains(.,'{0}')]//following-sibling::div", item.Key));
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).Clear();
                SafeClick(backofficeDriver, "//div[3]/div/div//span[@class='fa fa-search']");
                Thread.Sleep(1000);
            }
            



            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");


            Thread.Sleep(2000);
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

            FullWebappAutomation.BackofficeNavigation.CompanyProfile.App_Home_Screen(backofficeDriver);


            // Edit Admin
            backoffice_Edit_Admin(backofficeDriver, "Admin");

            string value;

            //delete all Layout
            for (int i = 1; ;)
            {
                try
                {

                    if (defaultiv)
                    {
                        value = SafeGetValue(backofficeDriver, string.Format("//div[6]/div[1]/div[4]/div[2]/div[1]/div[1]/div[3]/div[4]/div[1]/ul[1]/li[{0}]/div[1]/div[1]/div[1]", i), "innerHTML", maxRetry: 3, safeWait: 300);
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



            Backoffice_Delete_Layout(backofficeDriver);

            Dictionary<string, string> Fields = new Dictionary<string, string>();

            Backoffice_Change_All_Button_Home(backofficeDriver, Fields);
            // Add any 16 button


            Thread.Sleep(2000);
            webapp_Sandbox_Resync(webappDriver, backofficeDriver);
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
            bool flag = true;
            try
            {
                for (i = 1; ; i++)
                {
                    string valueF = SafeGetValue(webappDriver, string.Format("/html[1]/body[1]/app-root[1]/app-menu[1]/div[1]/nav[1]/div[3]/ul[1]/li[{0}]", i), "innerHTML", maxRetry: 5);
                    if (!Fields.Keys.Contains(valueF))
                    {
                        flag = false;
                    }
                }
            }
            catch
            {
                Assert(i == Fields.Count + 1 && flag, "no all menu exist");
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

            Bakeoffice_Sandbox_Online_action_Home_Page(webappDriver, backofficeDriver);

            webapp_Sandbox_Resync(webappDriver, backofficeDriver);
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

        public static void Bakeoffice_Sandbox_Online_action_Home_Page(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            FullWebappAutomation.BackofficeNavigation.ConfigurationFiles.Online_Add_Ons(backofficeDriver);

            // Add new
            SafeClick(backofficeDriver, "//div[6]/div[1]/div[4]/div[2]/div[1]/div[1]/div[1]/div[@id='btnAddNewOnlineAction']");

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


            FullWebappAutomation.BackofficeNavigation.CompanyProfile.App_Home_Screen(backofficeDriver);

            // Edit Admin
            backoffice_Edit_Admin(backofficeDriver, "Admin");

            int index = 0;
            while (true)
            {
                try
                {
                    backofficeDriver.FindElement(By.Id("txtSearchBankFields")).SendKeys("Online action automation");
                    index++;
                    SafeClick(backofficeDriver, string.Format("//div/div[@class='lb-bank ui-droppable']/ul/div[2]//li[{0}]//div[@class='fr plusIcon plusIconDisable']", index), 500, 6);
                    break;
                }
                catch {
                    backofficeDriver.FindElement(By.Id("txtSearchBankFields")).Clear();
                    if (index > 10)
                        break;
                    continue; }
            }

            Thread.Sleep(1000);


            // Save button
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div[4]/div/div");


        }

        #endregion


        #region Images

        /// <summary>
        /// Upload Image to Home screen webapp Admin
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="elementXPath"></param>
        /// <param name="filePath"></param>
        public static void Sandbox_Upload_Image(RemoteWebDriver backofficeDriver, string elementXPath, string filePath, bool isNew = true)
        {
            if (isNew)
                FullWebappAutomation.BackofficeNavigation.CompanyProfile.Branding(backofficeDriver);


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

            // Upload_Image 
            Sandbox_Upload_Image(backofficeDriver, "//div[@id='supeRepCont']//tr[1]/td[2]/table[1]/tbody[1]/tr[1]/td[2]/div[1][@id='btnSupeRepLandscape']", @"C:\Users\yosef.h\Desktop\automation_documents\automation_files_pictues\cat.jpeg");

            // Upload_Image 
            Sandbox_Upload_Image(backofficeDriver, "/html[1]/body[1]/form[1]/div[6]/div[1]/div[4]/div[2]/div[1]/div[2]/table[1]/tbody[1]/tr[1]/td[1]/table[1]/tbody[1]/tr[1]/td[2]/div[1]", @"C:\Users\yosef.h\Desktop\automation_documents\automation_files_pictues\shirt.jpeg", isNew: false);


            // wait 4 second
            Thread.Sleep(3000);


            webapp_Sandbox_Resync(webappDriver, backofficeDriver);
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

            FullWebappAutomation.BackofficeNavigation.CompanyProfile.Branding(backofficeDriver);

            // Edit Main Color
            SafeClick(backofficeDriver, "//div[6]/div[1]/div[4]/div[2]/div[1]/div[5]/div[1]/div[1]/div[2]");

            //yellow
            SafeClick(backofficeDriver, "//div[@class='sp-container sp-light']/div/div/div/span[@title='rgb(255, 255, 0)']/span[@style]");


            var domElement = backofficeDriver.FindElement(By.XPath("//form[1]/div[4]/div[1]"));

            Thread.Sleep(800);

            // Get Attribute of color  
            string attributeBO = domElement.GetAttribute("style");


            webapp_Sandbox_Resync(webappDriver, backofficeDriver);
            Thread.Sleep(4000);

            domElement = webappDriver.FindElement(By.XPath("//app-root[1]/app-menu[1]/div[1]/nav[1]"));
            string attributeWA = domElement.GetAttribute("style");

            bool isEqual = (attributeBO == attributeWA);

            Assert(isEqual, "The color Main  home page no change");
        }


        public static void Sandbox_Branding_Color_Secondary(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            FullWebappAutomation.BackofficeNavigation.CompanyProfile.Branding(backofficeDriver);

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
            FullWebappAutomation.BackofficeNavigation.CompanyProfile.Branding(backofficeDriver);

            // Upload_Image logo
            Sandbox_Upload_Image(backofficeDriver, "//div[@id='content']/div[@id='compLogoCont']/div[1]/table[1]/tbody[1]/tr[1]/td[2]/div[1]", @"C:\Users\yosef.h\Desktop\automation_documents\automation_files_pictues\cow.jpg");


            // element of logo image
            var domElement = webappDriver.FindElement(By.XPath("/html[1]/body[1]/app-root[1]/app-menu[1]/div[1]/nav[1]/div[2]/a[1]/img[1]"));


            // src of image from webapp
            string src = domElement.GetAttribute("src");


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
        /// checking if order by all type fields accounts list
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Sandbox_Order_By_Accounts(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Order By TSA
            Webapp_Sandbox_Order_By_TSA(webappDriver);

            // Order By basic
            Webapp_Sandbox_Order_By(webappDriver);
        }


        /// <summary>
        /// Search Accounts List by any fields
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Backoffice_Sandbox_Search_List_Accounts(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Dictionary<string, string> Fields = new Dictionary<string, string>();

            Fields.Add("Country", "Country");
            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Price list name", "PriceLevelName");
            Fields.Add("Dropdown", "TSADropdown");
            Fields.Add("Paragraph Text", "TSAParagraphText");
            Fields.Add("Number", "TSANumber");

            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);

            Sreach_Available_Fields(backofficeDriver, Fields, "Search");
        }



        public static void Backoffice_Sandbox_Smart_Search_List_Accounts(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Dictionary<string, string> Fields = new Dictionary<string, string>();

            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Name", "Name");
            Fields.Add("Street", "Street");
            Fields.Add("Country", "Country");
            Fields.Add("City", "City");
            Fields.Add("State", "State");
            Fields.Add("Phone", "Phone");
            Fields.Add("Zip code", "ZipCode");
            Fields.Add("Email", "Email");
            Fields.Add("Special price list name", "SpecialPriceListName");

            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);

            Sreach_Available_Fields_Per_List(backofficeDriver, Fields, "Smart Search", "Basic_List");


            Dictionary<string, string> TSA_Fields = new Dictionary<string, string>();


            TSA_Fields.Add("Account ID", "ExternalID");
            TSA_Fields.Add("Single Line Text", "TSASingleLineText");
            TSA_Fields.Add("Paragraph Text", "TSAParagraphText");
            TSA_Fields.Add("Date", "TSADate");
            TSA_Fields.Add("Number", "TSANumber");
            TSA_Fields.Add("Decimal Number", "TSADecimalNumber");
            TSA_Fields.Add("Currency", "TSACurrency");
            TSA_Fields.Add("Checkbox", "TSACheckbox");
            TSA_Fields.Add("Dropdown", "TSADropdown");

            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);

            Sreach_Available_Fields_Per_List(backofficeDriver, TSA_Fields, "Smart Search", "TSA_List");


            Thread.Sleep(3000);

        }



        public static void Backoffice_Sandbox_Create_Lists_Accounts(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            Dictionary<string, string> TSA_Fields = new Dictionary<string, string>();


            // Creat_New_List name TSA_Fields
            Creat_New_List_Lists_Accounts(backofficeDriver, "TSA_List");



            TSA_Fields.Add("Account ID", "ExternalID");
            TSA_Fields.Add("Single Line Text", "TSASingleLineText");
            TSA_Fields.Add("Limited Line Text", "TSALimitedLineText");
            TSA_Fields.Add("Paragraph Text", "TSAParagraphText");
            TSA_Fields.Add("Date", "TSADate");
            TSA_Fields.Add("Date + Time", "TSADateTime");
            TSA_Fields.Add("Number", "TSANumber");
            TSA_Fields.Add("Decimal Number", "TSADecimalNumber");
            TSA_Fields.Add("Currency", "TSACurrency");
            TSA_Fields.Add("Checkbox", "TSACheckbox");
            TSA_Fields.Add("Dropdown", "TSADropdown");
            TSA_Fields.Add("Multi Choice", "TSAMultiChoice");


            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);

            //  Add fields
            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, TSA_Fields, "TSA_List");

            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);


            Dictionary<string, string> Fields = new Dictionary<string, string>();

            Creat_New_List_Lists_Accounts(backofficeDriver, "Basic_List");

            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Name", "Name");
            Fields.Add("Street", "Street");
            Fields.Add("Country", "Country");
            Fields.Add("City", "City");
            Fields.Add("State", "State");
            Fields.Add("Phone", "Phone");
            Fields.Add("Zip code", "ZipCode");
            Fields.Add("Email", "Email");
            Fields.Add("Special price list name", "SpecialPriceListName");
            Fields.Add("Price list name", "PriceLevelName");
            Fields.Add("Creation Date", "CreationDate");



            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);


            //  Add fields
            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Fields, "Basic_List");


            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);


            Edit_Rep_Permission(backofficeDriver, "Basic_List", true);




        }


        /// <summary>
        /// test the TSA fields
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Sandbox_TSA_Fields_Header(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            Dictionary<string, string> TSA_Fields = new Dictionary<string, string>();

            TSA_Fields.Add("Account ID", "ExternalID");
            TSA_Fields.Add("Single Line Text", "TSASingleLineText");
            TSA_Fields.Add("Limited Line Text", "TSALimitedLineText");
            TSA_Fields.Add("Paragraph Text", "TSAParagraphText");
            TSA_Fields.Add("Date", "TSADate");
            TSA_Fields.Add("Date + Time", "TSADateTime");
            TSA_Fields.Add("Number", "TSANumber");
            TSA_Fields.Add("Decimal Number", "TSADecimalNumber");
            TSA_Fields.Add("Currency", "TSACurrency");
            TSA_Fields.Add("Checkbox", "TSACheckbox");
            TSA_Fields.Add("Dropdown", "TSADropdown");


            // backoffice_Sandbox_Smart_Search(backofficeDriver, TSA_Fields);

            //  webapp_Sandbox_Resync(webappDriver, backofficeDriver);

            Webapp_Sandbox_New_List_Table(webappDriver, "TSA_List",  TSA_Fields, "Accounts" );

            Webapp_Sandbox_New_List_Table(webappDriver, "TSA_List", TSA_Fields, "Accounts", isHeader: false);


        }


        /// <summary>
        /// test the Basic fields
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Sandbox_Add_Basic_List(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            Dictionary<string, string> Fields = new Dictionary<string, string>();


            //bakeoffice_Sandbox_Add_Basic_List(backofficeDriver,Fields);

            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Name", "Name");
            Fields.Add("Street", "Street");
            Fields.Add("Country", "Country");
            Fields.Add("City", "City");
            Fields.Add("State", "State");
            Fields.Add("Phone", "Phone");
            Fields.Add("Zip Code", "ZipCode");
            Fields.Add("Email", "Email");
            Fields.Add("Special price list name", "SpecialPriceListName");
            Fields.Add("Price list name", "PriceLevelName");
            Fields.Add("Creation Date", "CreationDate");

            //webapp_Sandbox_Resync(webappDriver, backofficeDriver);


            Webapp_Sandbox_New_List_Table(webappDriver, "Basic_List",  Fields,"Accounts");
        }


        /// <summary>
        /// Search Account
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Sandbox_Search_Account(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            //  backoffice_Sandbox_Search_Account(backofficeDriver);

            //   webapp_Sandbox_Resync(webappDriver, backofficeDriver);

            Webapp_Sandbox_Search_Account(webappDriver, "Basic_List");

        }


        public static void Sandbox_Smart_Search(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Dictionary<string, string> Fields = new Dictionary<string, string>();


            //  backoffice_Sandbox_Smart_Search(backofficeDriver, Fields);


            // webapp_Sandbox_Resync(webappDriver, backofficeDriver);


            webapp_Sandbox_Smart_Search(webappDriver, "Basic_List");
        }


        public static void Sandbox_TSA_Smart_Search(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            //   Dictionary<string, string> TSA_Fields = new Dictionary<string, string>();


            //   backoffice_Sandbox_Smart_Search(backofficeDriver, TSA_Fields);


            //   webapp_Sandbox_Resync(webappDriver, backofficeDriver);


            webapp_Sandbox_TSA_Smart_Search(webappDriver, "TSA_List");
        }


        public static void Sandbox_add_Account(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Dictionary<string, string> Fields = new Dictionary<string, string>();


            Fields.Add("Name", "Name");
            Fields.Add("Street", "Street");
            Fields.Add("Country", "Country");
            Fields.Add("City", "City");
            Fields.Add("State", "State");
            Fields.Add("Phone", "Phone");
            Fields.Add("Zip code", "ZipCode");
            Fields.Add("Email", "Email");
            Fields.Add("Special price list name", "SpecialPriceListName");
            Fields.Add("Price list name", "PriceLevelName");
            Fields.Add("Creation Date", "CreationDate");

            //creat list bakeoffice Name/ Street / City / Currency / ($)/ Zip Code / Country / State / Phone / Email / Account ID / Price list name /Special price list name

            // bakeoffice_Sandbox_Add_Basic_List(backofficeDriver, Fields);


            bakeoffice_Sandbox_Add_views(backofficeDriver, Fields);


            webapp_Sandbox_Resync(webappDriver, backofficeDriver);


            webapp_Sandbox_creat_Account(webappDriver, Fields);



        }


        public static void Sandbox_edit_Account(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Dictionary<string, string> Fields = new Dictionary<string, string>();


            Fields.Add("Name", "Name");
            Fields.Add("Street", "Street");
            Fields.Add("Country", "Country");
            Fields.Add("City", "City");
            Fields.Add("State", "State");
            Fields.Add("Phone", "Phone");
            Fields.Add("Zip code", "ZipCode");
            Fields.Add("Email", "Email");
            Fields.Add("Special price list name", "SpecialPriceListName");
            Fields.Add("Price list name", "PriceLevelName");
            Fields.Add("Creation Date", "CreationDate");

            //creat list bakeoffice Name/ Street / City / Currency / ($)/ Zip Code / Country / State / Phone / Email / Account ID / Price list name /Special price list name

            // bakeoffice_Sandbox_Add_Basic_List(backofficeDriver, Fields);


            bakeoffice_Sandbox_Add_views(backofficeDriver, Fields);


            webapp_Sandbox_Resync(webappDriver, backofficeDriver);


            webapp_Sandbox_edit_Account(webappDriver, Fields);
        }


        #endregion


        #region Activities 
        


        public static void backoffice_Custom_Fields_Transaction(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            FullWebappAutomation.BackofficeNavigation.SalesActivities.Transaction_Types(backofficeDriver);

            // Edit Sales Order Transaction
            SafeClick(backofficeDriver, "//div[contains(@class,'slick-cell')]//div[@title='Edit']");

            // Fields
            SafeClick(backofficeDriver, "//li[@title='Fields']//a[contains(text(),'Fields')]");


            Dictionary<string, string> TSA_Fields = new Dictionary<string, string>();



            TSA_Fields.Add("Single Line Text", "TSASingleLineText");
            TSA_Fields.Add("Limited Line Text", "TSALimitedLineText");
            TSA_Fields.Add("Paragraph Text", "TSAParagraphText");
            TSA_Fields.Add("Date", "TSADate");
            TSA_Fields.Add("Date + Time", "TSADateTime");
            TSA_Fields.Add("Limited Date", "TSADateTime");
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
            //TSA_Fields.Add("Button", "TSAButton");
            //TSA_Fields.Add("Reference Type", "TSAReferenceType");
            //TSA_Fields.Add("User defined tables Drop Down", "TSAUserdefinedtablesDropDown");
            //TSA_Fields.Add("Sum Transaction Lines", "TSASumTransactionLines");


            foreach (var item in TSA_Fields)
            {
                // Add Custom Field
                SafeClick(backofficeDriver, "//div[@name='0']//span[contains(text(),'Add Custom Field')]");


                // Select Type
                SafeClick(backofficeDriver, string.Format("//div[@id='mainCustomFieldLayout']/div/ul/li/h3[@title='{0}']", item.Key));


                // Name test
                SafeSendKeys(backofficeDriver, "//div[3]/div[@class='section']/input[1]", item.Key + " Transaction");


                // Description test
                SafeSendKeys(backofficeDriver, "//div[1]/div[3]/div[@name='descriptionSection']/input[1]", item.Key + " Transaction");


                // is no exist  Mapped Name Image/Attachment
                if (item.Key != "Image" && item.Key != "Attachment")
                {
                    // Mapped test
                    SafeSendKeys(backofficeDriver, "//div[1]/div[3]/div[@name='mappedNameSection']/input[1]", item.Key + " Transaction");
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
        }


        public static void backoffice_Custom_Fields_Activities(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            FullWebappAutomation.BackofficeNavigation.SalesActivities.Activity_Types(backofficeDriver);

            // Edit Photo
            SafeClick(backofficeDriver, "//div[@style='top:0px']/div[contains(@class,'slick-cell')]//div[@title='Edit']");


            // Fields
            SafeClick(backofficeDriver, "//li//a[contains(text(),'Fields')]");


            Dictionary<string, string> TSA_Fields = new Dictionary<string, string>();



            TSA_Fields.Add("Single Line Text", "TSASingleLineText");
            TSA_Fields.Add("Limited Line Text", "TSALimitedLineText");
            TSA_Fields.Add("Paragraph Text", "TSAParagraphText");
            TSA_Fields.Add("Date", "TSADate");
            TSA_Fields.Add("Date + Time", "TSADateTime");
            TSA_Fields.Add("Limited Date", "TSADateTime");
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
            //TSA_Fields.Add("Button", "TSAButton");
            //TSA_Fields.Add("Reference Type", "TSAReferenceType");
            //TSA_Fields.Add("User defined tables Drop Down", "TSAUserdefinedtablesDropDown");
            //TSA_Fields.Add("Sum Transaction Lines", "TSASumTransactionLines");


            foreach (var item in TSA_Fields)
            {
                // Add Custom Field
                SafeClick(backofficeDriver, "//div[@name='0']//span[contains(text(),'Add Custom Field')]");


                // Select Type
                SafeClick(backofficeDriver, string.Format("//div[@id='mainCustomFieldLayout']/div/ul/li/h3[@title='{0}']", item.Key));


                // Name test
                SafeSendKeys(backofficeDriver, "//div[3]/div[@class='section']/input[1]", item.Key + "Activities");


                // Description test
                SafeSendKeys(backofficeDriver, "//div[1]/div[3]/div[@name='descriptionSection']/input[1]", item.Key + "Activities");

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
        }

        public static void Sandbox_Create_Lists_Activities(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {

            Dictionary<string, Dictionary<string, string>> allList = new Dictionary<string, Dictionary<string, string>>();

            Backoffice_Sandbox_Create_Lists_Activities(backofficeDriver, allList);


        }


        public static void Sandbox_Order_By_Activities(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            // Order By Table_Basic_List
            Webapp_Sandbox_Order_By_Table_Basic_List(webappDriver);


            // Order By Details_Basic_List
            Webapp_Sandbox_Order_By_Details_Basic_List(webappDriver);
        }


        public static void Sandbox_Table_Fields_Header_Activities(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            
            #region Table_Basic_Fields
            Dictionary<string, string> Table_Basic_Fields = new Dictionary<string, string>();
            Table_Basic_Fields.Add("Action Time", "ActionDateTime");
            Table_Basic_Fields.Add("Activity Type Name", "ActivityTypeID");
            Table_Basic_Fields.Add("Creation Date", "CreationDateTime");
            Table_Basic_Fields.Add("Activity Creation Latitude", "CreationGeoCodeLAT");
            Table_Basic_Fields.Add("Activity Creation Longitude", "CreationGeoCodeLNG");
            Table_Basic_Fields.Add("CreatorID", "CreatorExternalID");
            Table_Basic_Fields.Add("Creator", "Creator");
            //Table_Basic_Fields.Add("Deleted", "Deleted");

            Webapp_Sandbox_New_List_Table(webappDriver, "Table_Basic_List", Table_Basic_Fields, "Activities");

            #endregion
           

            /*
            #region Card_Basic_List 

            Dictionary<string, string> Card_Basic_Fields = new Dictionary<string, string>();


            Card_Basic_Fields.Add("External ID", "ExternalID");
            Card_Basic_Fields.Add("Hidden", "Hidden");
            Card_Basic_Fields.Add("Modification Date", "ModificationDateTime");
            Card_Basic_Fields.Add("Planned Duration", "PlannedDuration");
            Card_Basic_Fields.Add("Planned End Time", "PlannedEndTime");
            Card_Basic_Fields.Add("Planned Start Time", "PlannedStartTime");
            Card_Basic_Fields.Add("Status", "Status");
            Card_Basic_Fields.Add("Submission Latitude", "SubmissionGeoCodeLAT");
            Card_Basic_Fields.Add("Submission Longitude", "SubmissionGeoCodeLNG");



            // Activities
            webapp_Sandbox_Home_Button(webappDriver, "Activities");

            bool isContains = true;
            try
            {
                SafeClick(webappDriver, string.Format("//div[@title='{0}']", "Card_Basic_List"), safeWait: 300, maxRetry: 20);
            }
            catch
            {
                try
                {
                    if (SafeGetValue(webappDriver, "//div[@class='ellipsis']", "innerHTML") != "Card_Basic_List")
                    {
                        SafeClick(webappDriver, "//div[@class='ellipsis']");
                        SafeClick(webappDriver, string.Format("//li[@title='{0}']", "Card_Basic_List"));
                    }
                }
                catch
                {
                    isContains = false;
                }
            }


            Thread.Sleep(5000);


            foreach (var item in Card_Basic_Fields)
            {
                try
                {
                    string value = SafeGetValue(backofficeDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div/div[1]/app-custom-form/fieldset/mat-grid-list/div/mat-grid-tile/figure/app-custom-field-generator//label[contains(@title,'{0}')]/span[1] ", item.Key), "innerHTML");
                    if (value.ToLower() != item.Key.ToLower())
                    {
                        isContains = false;
                    }
                }
                catch 
                {
                    isContains = false;
                }
              
            }
            Assert(isContains, "No all fields come to webapp");
            #endregion
            */
            
            #region Details_Basic_List end

            Dictionary<string, string> Details_Basic_Fields = new Dictionary<string, string>();

            Details_Basic_Fields.Add("Title", "Title");
            Details_Basic_Fields.Add("Activity Type", "Type");
            Details_Basic_Fields.Add("Sales Rep email", "Agent.Email");
            Details_Basic_Fields.Add("Sales Rep First Name", "Agent.FirstName");
            Details_Basic_Fields.Add("Order Remark", "Remark");
            Details_Basic_Fields.Add("Quantities Total", "QuantitiesTotal");
            Details_Basic_Fields.Add("Branch", "Branch");
            Details_Basic_Fields.Add("Ship to Name", "ShipToName");


            Webapp_Sandbox_New_List_Table(webappDriver, "Details_Basic_List", Details_Basic_Fields, "Activities");

            #endregion

            /*
            #region Line_Basic_List

            Dictionary<string, string> Line_Basic_List = new Dictionary<string, string>();

            Line_Basic_List.Add("Account Address", "Account.Address");
            Line_Basic_List.Add("Account sale", "Account.Agents");
            Line_Basic_List.Add("Account Buyers", "Account.Buyers");
            Line_Basic_List.Add("Account City", "Account.City");
            Line_Basic_List.Add("Account Contact Persons", "Account.ContactPersons");
            Line_Basic_List.Add("Account Country", "Account.Country");
            Line_Basic_List.Add("Account Creation Date", "Account.CreationDate");
            Line_Basic_List.Add("Account Deleted", "Account.Deleted");

            #endregion

            */
        }



        public static void Backoffice_Sandbox_Smart_Search_Activities(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Dictionary<string, string> Fields = new Dictionary<string, string>();

            Fields.Add("Action Time", "ActionDateTime");
            Fields.Add("Activity Type Name", "ActivityTypeID");
            Fields.Add("Creation Date", "CreationDateTime");
            Fields.Add("Activity Creation Latitude", "CreationGeoCodeLAT");
            Fields.Add("Activity Creation Longitude", "CreationGeoCodeLNG");
            Fields.Add("Creator", "Creator");
            Fields.Add("External ID", "ExternalID");
            

            FullWebappAutomation.BackofficeNavigation.SalesActivities.Activity_Lists_New(backofficeDriver);

            Sreach_Available_Fields_Per_List(backofficeDriver, Fields, "Smart Search", "Table_Basic_List");


            //Fields = new Dictionary<string, string>();

            //Fields.Add("Action Time", "ActionDateTime");
            //Fields.Add("Activity Type Name", "ActivityTypeID");
            //Fields.Add("Creation Date", "CreationDateTime");
            //Fields.Add("Activity Creation Latitude", "CreationGeoCodeLAT");
            //Fields.Add("Activity Creation Longitude", "CreationGeoCodeLNG");
            //Fields.Add("Creator", "Creator");
            //Fields.Add("External ID", "ExternalID");


            //FullWebappAutomation.Backoffice.SalesActivities.Activity_Lists_New(backofficeDriver);

            //Sreach_Available_Fields_Per_List(backofficeDriver, Fields, "Smart Search", "Card_Basic_List");


        }


        public static void Customize_Sandbox_Search_Activities(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Backoffice_Sandbox_Search_Activities(backofficeDriver);
        }


        public static void Sandbox_Search_Activities(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {


            string nameNewList = "ALL";

            // Menu
            FullWebappAutomation.BackofficeNavigation.SalesActivities.Activity_Lists_New(backofficeDriver);


            // Activities
            webapp_Sandbox_Home_Button(webappDriver, "Activities");


            try
            {
                SafeClick(webappDriver, string.Format("//div[@title='{0}']", nameNewList), safeWait: 300, maxRetry: 20);
            }
            catch
            {
                try
                {
                    if (SafeGetValue(webappDriver, "//div[@class='ellipsis']", "innerHTML") != nameNewList)
                    {
                        SafeClick(webappDriver, "//div[@class='ellipsis']");
                        SafeClick(webappDriver, string.Format("//li[@title='{0}']", nameNewList));
                    }
                }
                catch
                {
                    Assert(false, "no costome list");
                }
            }

            Thread.Sleep(3000);



            check_Field_Button_Search(webappDriver, "staples", 10, 1);

        }



        public static void Sandbox_Ceate_Account_Types(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            BackofficeNavigation.Accounts.Account_Types(backofficeDriver);
            Ceate_Account_Types(backofficeDriver, "New Type Account1");

            BackofficeNavigation.Accounts.Account_Types(backofficeDriver);
            Ceate_Account_Types(backofficeDriver, "New Type Account2");
        }

        public static void Ceate_Account_Types(RemoteWebDriver backofficeDriver, string nameNewList)
        {
            // + Create Account Types
            SafeClick(backofficeDriver, "//div[contains(@id,'btnAddNew')]");


            //  Input New List Fildes
            // input name
            SafeSendKeys(backofficeDriver, "//input[@name='Name']", nameNewList);


            // input description
            SafeSendKeys(backofficeDriver, "//input[@name='Description']", "Automation " + nameNewList);


            // Save
            SafeClick(backofficeDriver, "//div[contains(@id,'btnSave')]");
        }


      

        #endregion



        #region Transactions


        /// <summary>
        /// This function runs all tests related to Transaction_Accounts_Setting
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="backofficeDriver"></param>
        public static void Transaction_Accounts_Settings(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            //BO customizations
            CustomizeToForm(backofficeDriver, new string[] { "test1", "test2", "test3" }, BackofficeNavigation.CompanyProfile.App_Home_Screen);

            //test on webapp
            WebApp_Transaction_Accounts_Settings(webappDriver);
        }


        public static void Sandbox_Create_Sync(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Bakeoffice_Sandbox_Create_Sync(backofficeDriver);
        }


        public static void Transaction_Accounts_Settings_BO_Customization(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            Backoffice_Transaction_Accounts_Settings(backofficeDriver);
        }
        #endregion
    }
}
