using OpenQA.Selenium.Remote;
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
    class WebappTest
    {

        public static void Webapp_Sandbox_New_List_Table(RemoteWebDriver webappDriver, string nameNewList, Dictionary<string, string> Fields, bool isHeader = true)
        {

            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);




            int index = 1;
            string valueH = "";
            bool isContains = true;

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
            SafeClick(webappDriver, "//div[@class='ellipsis']");
        }



        public static void webapp_Sandbox_Smart_Search(RemoteWebDriver webappDriver,string nameNewList)
        {

            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);


            // Accounts
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
                    Assert(false,"no costome list");
                }
            }


            Thread.Sleep(5000);

            Dictionary<string, string> values = new Dictionary<string, string>();
            Dictionary<int, string> columns = new Dictionary<int, string>();

            // sum fo line  11
            HashSet<int> numbers = new HashSet<int>() { 2, 4 };
            columns.Add(11, "select");
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "PriceLevelDbId", numbers, columns, values, 11, true,typeCurrently: "TaxtBox");

            // sum fo line 10
            numbers = new HashSet<int>() { 3};
            columns.Add(9, "TaxtBox");
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "Email", numbers, columns, values, 10, true,typeCurrently: "TaxtBox");

            //// sum fo line 3
            //numbers = new HashSet<int>() { 2, 4, 6 };
            //columns.Add(1, "Button");
            //Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "ExternalID", numbers, columns, values, 3, true, typeCurrently: "Button");

            // sum fo line 3
            numbers = new HashSet<int>() { 2, 3 ,4};
            columns.Add(3, "TaxtBox");
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "Street", numbers, columns, values, 3, true,typeCurrently: "TaxtBox");




            columns.Remove(3);
            foreach (var item in values.Where(i => i.Value == "Street").ToList())
            {
                values.Remove(item.Key.ToString());
            }

            // sum fo line 10
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "Street", numbers, columns, values, 10, false, numToRemuve:2);



            //columns.Remove(1);
            //foreach (var item in values.Where(i => i.Value == "ExternalID").ToList())
            //{
            //    values.Remove(item.Key.ToString());
            //}
            //// sum fo line 7
            //Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "ExternalID", numbers, columns, values, 7, false, numToRemuve:1);



            columns.Remove(9);
            foreach (var item in values.Where(i => i.Value == "Email").ToList())
            {
                values.Remove(item.Key.ToString());
            }
            // sum fo line  11
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "Email", numbers, columns, values, 11, false,numToRemuve:5);
        }



        public static void Webapp_Sandbox_Search_Account(RemoteWebDriver webappDriver,string nameNewList)
        {
            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            // Accounts
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


            check_Field_Button_Search(webappDriver, "105599", 10, 1);
            check_Field_Button_Search(webappDriver, "105599 98", 1, 1);


         //   check_Field_Textbox_Search(webappDriver, "EAsTERn", 12, 2);
         //   check_Field_Textbox_Search(webappDriver, "EASTERn 794 ", 1, 2);

        }



        public static void webapp_Sandbox_TSA_Smart_Search(RemoteWebDriver webappDriver,string nameNewList)
        {
            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            // Accounts
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


            Dictionary<string, string> values = new Dictionary<string, string>();
            Dictionary<int, string> columns = new Dictionary<int, string>();

            // sum fo line  16
            HashSet<int> numbers = new HashSet<int>() { 1 };
            columns.Add(10, "checkbox");
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "TSACheckbox", numbers, columns, values, 16, true, "checkbox");

            // sum fo line 6
            numbers = new HashSet<int>() { 2, 4, 6 };
            columns.Add(2, "TaxtBox");
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "TSASingleLineText", numbers, columns, values, 6, true, "TaxtBox");

            // sum fo line 17
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "TSASingleLineText", numbers, columns, values, 17, false,numToRemuve:2);



            // sum fo line 10
            numbers = new HashSet<int>() { 2, 4, 6 };
            columns.Add(4, "textarea");
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "TSAParagraphText", numbers, columns, values, 10, true, "TaxtBox");


         

            // sum fo line 17
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "TSAParagraphText", numbers, columns, values, 17, false, numToRemuve: 4);

           
            // sum fo line 10
            numbers = new HashSet<int>() { 2 };
            columns.Add(11, "select");
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "TSADropdown", numbers, columns, values, 10, true, "select");


            // sum fo line 17
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "TSADropdown", numbers, columns, values, 17, false, numToRemuve: 11);

            ///////////----------------------
            // sum fo line 6
            numbers = new HashSet<int>() { 2, 4, 6 };
            columns.Add(3, "TaxtBox");
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "TSALimitedLineText", numbers, columns, values, 6, true, "TaxtBox");


            // sum fo line 17
            Webapp_check_Field_Smart_Search_TaxtBox(webappDriver, "TSALimitedLineText", numbers, columns, values, 17, false,numToRemuve:3);



        }



        public static void Webapp_check_Field_Smart_Search_TaxtBox(RemoteWebDriver webappDriver, string field, HashSet<int> numbers, Dictionary<int, string> columns, Dictionary<string, string> values, int numRow, bool isAdd,string typeCurrently="",int numToRemuve=0)
        {
            //webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            //// Accounts
            //for (int i = 1; i < 10; i++)
            //{
            //    try
            //    {
            //        if (SafeGetValue(webappDriver, string.Format("//app-root[1]/div[1]/app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i.ToString()), "innerHTML") == "Accounts")
            //            SafeClick(webappDriver, string.Format("//app-root[1]/div[1]/app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i.ToString()));
            //        break;
            //    }
            //    catch { }
            //}

            // Select field 
            SafeClick(webappDriver, string.Format("//ul[@class='ul-smart-search']//li[@data-smartsearch='{0}']", field));

            if (isAdd)
            {

                #region switch 
                switch (typeCurrently)
                {
                    case "TaxtBox":

                        // Select last 
                        foreach (var num in numbers)
                        {

                            string value = SafeGetValue(webappDriver, string.Format("//app-advanced-search/div[@id='dvSmartSearch']//ul[@class='checkbox ng-star-inserted']/li[{0}]/label", num), "innerHTML");
                            values.Add(value, field);
                            SafeClick(webappDriver, string.Format("//app-advanced-search/div[@id='dvSmartSearch']//ul[@class='checkbox ng-star-inserted']/li[{0}]/input", num));
                        }
                        break;
                    case "Button":
                        // Select last 
                        foreach (var num in numbers)
                        {

                            string value = SafeGetValue(webappDriver, string.Format("//app-advanced-search/div[@id='dvSmartSearch']//ul[@class='checkbox ng-star-inserted']/li[{0}]/label", num), "innerHTML");
                            values.Add(value, field);
                            SafeClick(webappDriver, string.Format("//app-advanced-search/div[@id='dvSmartSearch']//ul[@class='checkbox ng-star-inserted']/li[{0}]/input", num));
                        }

                        break;

                    case "checkbox":

                        // Select last 
                        foreach (var num in numbers)
                        {
                                                                                     //app-advanced-search[1]/div[1]/div[2]/div[3]/ul[1]/li[2]/label
                            string value = SafeGetValue(webappDriver, string.Format("//app-advanced-search/div[@id='dvSmartSearch']//ul[@class='checkbox ng-star-inserted']/li[{0}]/label", num), "innerHTML");
                            values.Add(value, field);
                            SafeClick(webappDriver, string.Format("//app-advanced-search/div[@id='dvSmartSearch']//ul[@class='checkbox ng-star-inserted']/li[{0}]/input", num));
                        }

                        break;

                    case "textarea":
                        // Select last 
                        foreach (var num in numbers)
                        {

                            string value = SafeGetValue(webappDriver, string.Format("//app-advanced-search/div[@id='dvSmartSearch']//ul[@class='checkbox ng-star-inserted']/li[{0}]/label", num), "innerHTML");
                            values.Add(value, field);
                            SafeClick(webappDriver, string.Format("//app-advanced-search/div[@id='dvSmartSearch']//ul[@class='checkbox ng-star-inserted']/li[{0}]/input", num));
                        }


                        break;

                    case "date":
                      
                        break;


                    case "select":


                        // Select last 
                        foreach (var num in numbers)
                        {

                            string value = SafeGetValue(webappDriver, string.Format("//app-advanced-search[1]/div[1]/div[2]/div[3]/ul[1]/li[{0}]/label", num), "innerHTML");
                            values.Add(value, field);
                            SafeClick(webappDriver, string.Format("//app-advanced-search[1]/div[1]/div[2]/div[3]/ul[1]/li[{0}]/input", num));
                        }
                        break;

                    default:

                        break;
                }
                #endregion


               

                // Done 
                SafeClick(webappDriver, "//a[@title='Done']");
            }
            else
            {
                columns.Remove(numToRemuve);
                foreach (var item in values.Where(i => i.Value == field).ToList())
                {
                    values.Remove(item.Key.ToString());
                }

                SafeClick(webappDriver, "//div[1]/app-advanced-search[1]/div[1]/div[2]/div[1]/a[1]");
            }





            Thread.Sleep(3000);

            // Get  value
            int index;
            bool isContain = true;
            foreach (var column in columns)
            {
                #region switch 
                switch (column.Value)
                {
                    case "TaxtBox":

                        index = 1;
                        while (true)
                        {
                            try
                            {
                                string valuef = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]", index, column.Key), "innerHTML", safeWait: 100, maxRetry: 7).ToString();
                                if (!values.Keys.Contains(valuef))
                                {
                                    isContain = false;
                                    break;
                                }
                                index++;
                            }
                            catch
                            {
                                if (numRow + 1!= index)
                                {
                                    isContain = false;
                                }
                                break;
                            }
                        }

                        break;
                    case "Button":

                        index = 1;
                        while (true)
                        {
                            try
                            {
                                string valuef = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/app-custom-field-generator[1]/app-custom-button[1]/a[1]/span[1]", index, column.Key), "innerHTML", safeWait: 500, maxRetry: 3).ToString();
                                if (!values.Keys.Contains(valuef))
                                {
                                    isContain = false;
                                    break;
                                }
                                index++;
                            }
                            catch
                            {
                                if (numRow + 1 != index)
                                {
                                    isContain = false;
                                }
                                break;
                            }
                        }
                        break;

                    case "checkbox":

                        index = 1;
                        while (true)
                        {
                            try
                            {
                                string valuef = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/app-custom-field-generator[1]/app-custom-checkbox[1]/input[1]", index, column.Key), "title", safeWait: 500, maxRetry: 3).ToString();
                                if (valuef!="1")
                                {
                                    isContain = false;
                                    break;
                                }
                                index++;
                            }
                            catch
                            {
                                if (numRow + 1 != index)
                                {
                                    isContain = false;
                                }
                                break;
                            }
                        }
                        break;

                    case "textarea":

                        index = 1;
                        while (true)
                        {
                            try
                            {                                                           
                                string valuef = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/app-custom-field-generator[1]/app-custom-textarea[1]/textarea", index, column.Key), "title", safeWait: 500, maxRetry: 3).ToString();
                                if (!values.Keys.Contains(valuef))
                                {
                                    isContain = false;
                                    break;
                                }
                                index++;
                            }
                            catch
                            {
                                if (numRow +1 != index)
                                {
                                    isContain = false;
                                }
                                break;
                            }
                        }
                        break;

                    case "date":
                        index = 1;
                        while (true)
                        {
                            try
                            {
                                string valuef = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/app-custom-field-generator[1]/app-custom-date[1]/label[1]", index, column.Key), "innerHTML", safeWait: 500, maxRetry: 3).ToString();
                                if (!values.Keys.Contains(valuef))
                                {
                                    isContain = false;
                                    break;
                                }
                                index++;
                            }
                            catch
                            {
                                if (numRow + 1 != index)
                                {
                                    isContain = false;
                                }
                                break;
                            }
                        }
                        break;
                       

                    case "select":

                        index = 1;
                        while (true)
                        {
                            try
                            {
                                string valuef = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/app-custom-field-generator[1]/app-custom-select[1]/label[1]", index, column.Key), "innerHTML", safeWait: 500, maxRetry: 3).ToString();
                                if (!values.Keys.Contains(valuef))
                                {
                                    isContain = false;
                                    break;
                                }
                                index++;
                            }
                            catch
                            {
                                if (numRow + 1 != index)
                                {
                                    isContain = false;
                                }
                                break;
                            }
                        }
                        break;


                    default:

                        break;
                }
                #endregion
                if (!isContain)
                {
                    break;
                }
            }


            Assert(isContain, string.Format("smart search {0} no display", field));
        }


        public static void webapp_Sandbox_creat_Account(RemoteWebDriver webappDriver, Dictionary<string, string> Fields)
        {

            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            // Account
            for (int i = 1; i < 16; i++)
            {
                try
                {
                    if (SafeGetValue(webappDriver, string.Format("//app-root[1]/div[1]/app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i.ToString()), "innerHTML") == "Accounts")
                        SafeClick(webappDriver, string.Format("//app-root[1]/div[1]/app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i.ToString()));
                    break;
                }
                catch { }
            }

            // + button and Add
            SafeClick(webappDriver, "//div[@calss='row']//list-menu[2]//div[1]/a/span");
            SafeClick(webappDriver, "//li[@title='Add']");


            // input credentials

            string uniqName = "Automation " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            // Name  yosef ofer
            SafeSendKeys(webappDriver, "//div[1]/input[@id='Name']", uniqName);


            // Street  tora mzion
            SafeSendKeys(webappDriver, "//div[1]/input[@id='Street']", "tora mzion"+uniqName);


            // City new york
            SafeSendKeys(webappDriver, "//div[1]/input[@id='City']", "new york"+uniqName);


            // Zip Code 1111
            SafeSendKeys(webappDriver, "//div[1]/input[@id='ZipCode']", "1111"+uniqName);
            Thread.Sleep(1000);

            // Country USA
            SafeSendKeys(webappDriver, "//div[1]/input[@id='Phone']", "");
            SafeClick(webappDriver, "(.//*[normalize-space(text()) and normalize-space(.)='Country'])[1]/following::input[1]");
            Thread.Sleep(2000);
            SafeClick(webappDriver, "(.//*[normalize-space(text()) and normalize-space(.)='United States Minor Outlying Islands'])[1]/following::a[1]");



            // State  firts
            SafeSendKeys(webappDriver, "//div[1]/input[@id='Phone']", "");
            SafeClick(webappDriver, "(.//*[normalize-space(text()) and normalize-space(.)='State'])[1]/following::input[1]");
            Thread.Sleep(2000);
            SafeClick(webappDriver, "(.//*[normalize-space(text()) and normalize-space(.)='Alabama'])[1]/preceding::span[2]");


            // Phone  301-464-3080 
            SafeSendKeys(webappDriver, "//div[1]/input[@id='Phone']", "301-464-3080");


            // Email Store@qawrnty.com
            SafeSendKeys(webappDriver, "//div[1]/input[@id='Email']", "Store@qawrnty.com");


            // Price list name List firts
            SafeSendKeys(webappDriver, "//div[1]/input[@id='Phone']", "");
            SafeClick(webappDriver, "(.//*[normalize-space(text()) and normalize-space(.)='Price list name'])[1]/following::input[1]");
            Thread.Sleep(2000);
            SafeClick(webappDriver, "(.//*[normalize-space(text()) and normalize-space(.)='List 4(QQ13)'])[1]/preceding::span[2]");


            // Special price list name firts
            SafeSendKeys(webappDriver, "//div[1]/input[@id='Phone']", "");
            SafeClick(webappDriver, "(.//*[normalize-space(text()) and normalize-space(.)='Special price list name'])[1]/following::input[1]");
            Thread.Sleep(2000);
            SafeClick(webappDriver, "(.//*[normalize-space(text()) and normalize-space(.)='qweqwe 3(Sp 3)'])[1]/preceding::a[1]");


            // Save
            SafeClick(webappDriver, "//div[1]/app-bread-crumbs[1]/div[1]/div[1]/div[1]/div[2]/div[2]/button[1]");

            Thread.Sleep(4000);


          //  check_Field_Button_Search(webappDriver, uniqName, 1, 1);
        }

    }
}
