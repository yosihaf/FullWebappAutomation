using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using static FullWebappAutomation.GlobalSettings;
using static FullWebappAutomation.HelperFunctions;
using static FullWebappAutomation.Tests;

namespace FullWebappAutomation
{
    class TestOnChrome
    {
        public static RemoteWebDriver webappDriver, backofficeDriver;
        //public static RemoteWebDriver backofficeDriver2;
        //public static bool isCreatLocal;
        public static void SetUp()
        {
            DesiredCapabilities capability = DesiredCapabilities.Chrome();


            //isCreatLocal = FullWebappAutomation.GlobalSettings.isCreat;
            webappDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability, TimeSpan.FromSeconds(600));
            backofficeDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability, TimeSpan.FromSeconds(600));

            //backofficeDriver2 = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability, TimeSpan.FromSeconds(600));


            GlobalSettings.InitLogFiles();
        }

        public static void TearDown()
        {
            webappDriver.Quit();
            backofficeDriver.Quit();
            WriteToFinalizedPerformanceLog();
            System.Diagnostics.Process.Start(successLogFilePath);
        }

        /// <summary>
        /// Runs test cases
        /// </summary>
        public static void TestSuite(string chosenUsername, Dictionary<string, bool> testsToRun)
        {
            DanUsername = chosenUsername;
            DanPassword = GetUserPassword(DanUsername);


            ////if (isCreatLocal == false)
            ////{
          //  Backoffice.GeneralActions.SandboxCreateLog(backofficeDriver2);
          //  backofficeDriver2.Quit();
            //isCreatLocal = true;
            ////}


            // Login
            Webapp_Sandbox_Login(webappDriver, DanUsername, DanPassword);
            Backoffice.GeneralActions.SandboxLogin(backofficeDriver, DanUsername, DanPassword);

            //if (true)
            //{
            //    // Load file items / inventory / accounts
            //    Delegator delegatedFunction = Backoffice_Sandbox_Load_File;
            //    BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            //}

            if (testsToRun["Resync"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Resync;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Config Home Button"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Config_Home_Button;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Config App Buttons"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Config_App_Buttons;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Sales Order"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Sales_Order;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Item Search"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Item_Search;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Minimum Quantity"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Minimum_Quantity;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Delete Cart Item"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Delete_Cart_Item;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Unit Price Discount"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Unit_Price_Discount;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Continue Ordering"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Continue_Ordering;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Duplicate Line Item"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Duplicate_Line_Item;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Inventory Alert"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Inventory_Alert;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Search Activity"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Search_Activity;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Delete Activity"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Delete_Activity;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Account Search Activity"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Account_Search_Activity;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Account Drill Down"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Account_Drill_Down;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Enter To Activity"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Enter_To_Activity;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Account Activity Drilldown"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Account_Activity_Drilldown;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }
            
            if (testsToRun["Breadcrumbs Navigation"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Breadcrumbs_Navigation;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Duplicate Transaction"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Duplicate_Transaction;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Search Account"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Search_Account;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if(testsToRun["Order By"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Order_By;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            if (testsToRun["Image Home"])
            {
                Delegator delegatedFunction = Sandbox_Minimize_Home_As_Tablet;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }

            

            if (testsToRun["Creat Bayer"])
            {
                Delegator delegatedFunction = Backoffice_Sandbox_Creat_Byer;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }
            #region  Home page test 
            //1
            if (testsToRun["Change title home screen"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Change_Title_Home_Screen;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }
            //2
            if (testsToRun["Change Title All Home"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Change_All_Home;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }
            //3
            if (testsToRun["Menu of Home screen"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Menu_Home_Screen;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }
            //4
            if (testsToRun["Online action"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Online_action_Home_Page;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }
            //5
            if (testsToRun["Branding color Main"])
            {
                Delegator delegatedFunction = Sandbox_Branding_Color_Main;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }
            //6
            if (testsToRun["Branding color Secondary"])
            {
                Delegator delegatedFunction = Sandbox_Branding_Color_Secondary;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }
            //7
            if (testsToRun["Branding image logo"])
            {
                Delegator delegatedFunction = Sandbox_Branding_Logo;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }


            #endregion


            if (testsToRun["New_Basic_List_Account_Table"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Add_Basic_Fields;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }


            if (testsToRun["New_TSA_List_Account_Table"])
            {
                Delegator delegatedFunction = Webapp_Sandbox_Creat_TSA_Fields_And_Added;
                BasicTestWrapper(delegatedFunction, webappDriver, backofficeDriver);
            }
            
        }

        public static void RunTests(string chosenUsername, Dictionary<string, bool> testsToRun)
        {
            SetUp();
            TestSuite(chosenUsername, testsToRun);
            TearDown();
        }

    }
}
