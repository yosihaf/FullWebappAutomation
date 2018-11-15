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
        public static void Small_view(RemoteWebDriver webappDriver,string type)
        {
            // Small view
            SafeClick(webappDriver, "//div[@id='header']/div/div/ul/li/span[@class='fa fa-th-large fa-lg']");
            SafeClick(webappDriver,string.Format( "//div[@id='header']/div/div/ul/li//span[contains(text(),'{0}')]",type));
            Thread.Sleep(bufferTime);
        }

        public static void Webapp_Sandbox_New_List_Table(RemoteWebDriver webappDriver, string nameNewList, Dictionary<string, string> Fields, bool isHeader = true)
        {
            int index = 1;
            string valueH = "";
            bool isContains = true;

            Thread.Sleep(5000);


            // Accounts
            webapp_Sandbox_Home_Button(webappDriver, "Accounts");
           

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
            // Accounts
            webapp_Sandbox_Home_Button(webappDriver, "Accounts");

            // 
            select_list_general(webappDriver, nameNewList);
           


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

            // Accounts
            webapp_Sandbox_Home_Button(webappDriver, "Accounts");
           

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
            // Accounts
            webapp_Sandbox_Home_Button(webappDriver, "Accounts");
            

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

            // Accounts
            webapp_Sandbox_Home_Button(webappDriver, "Accounts");


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
            // Accounts
            webapp_Sandbox_Home_Button(webappDriver, "Accounts");
           

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



        public static void Webapp_Sandbox_Order_By(RemoteWebDriver webappDriver)
        {
            bool flag = false;

            // Accounts
            webapp_Sandbox_Home_Button(webappDriver, "Accounts");

            // Select list TSA_List
            try
            {
                SafeClick(webappDriver, string.Format("//div[@title='{0}']", "Basic_List"), safeWait: 300, maxRetry: 20);
            }
            catch
            {
                try
                {
                    if (SafeGetValue(webappDriver, "//div[@class='ellipsis']", "innerHTML") != "Basic_List")
                    {
                        SafeClick(webappDriver, "//div[@class='ellipsis']");
                        SafeClick(webappDriver, string.Format("//li[@title='{0}']", "Basic_List"));
                    }
                }
                catch
                {
                    //Assert of checking if exists list
                    Assert(false, "The list not exists");
                }
            }

            Thread.Sleep(4000);

            Dictionary<string, KeyValuePair<int, string>> Fields = new Dictionary<string, KeyValuePair<int, string>>();


            Fields.Add("Account ID", new KeyValuePair<int, string>(1, "/app-custom-form[1]/fieldset[1]//a[1]/span[1]"));
            Fields.Add("Name", new KeyValuePair<int, string>(2, "/app-custom-form[1]/fieldset[1]/div[2]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]"));
            Fields.Add("Street", new KeyValuePair<int, string>(3, "/app-custom-form[1]/fieldset[1]/div[3]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]"));
            // No work
            // Fields.Add("Country", new KeyValuePair<int, string>(4, "/app-custom-form[1]/fieldset[1]/div[4]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]"));
            Fields.Add("City", new KeyValuePair<int, string>(5, "/app-custom-form[1]/fieldset[1]/div[5]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]"));
            // No work
            // Fields.Add("State", new KeyValuePair<int, string>(6, "/app-custom-form[1]/fieldset[1]/div[6]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]"));
            Fields.Add("Phone", new KeyValuePair<int, string>(7, "/app-custom-form[1]/fieldset[1]/div[7]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]"));
            Fields.Add("Zip Code", new KeyValuePair<int, string>(8, "/app-custom-form[1]/fieldset[1]/div[8]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]"));
            Fields.Add("Email", new KeyValuePair<int, string>(9, "/app-custom-form[1]/fieldset[1]/div[9]/app-custom-field-generator[1]/app-custom-textbox[1]/label[1]"));
            // No work
            // Fields.Add("Special price list name", new KeyValuePair<int, string>(10, "//app-custom-form[1]/fieldset[1]/div[10]/app-custom-field-generator[1]/app-custom-select[1]/label[1]"));
            // No work
            // Fields.Add("Price list name", new KeyValuePair<int, string>(11, "//app-custom-form[1]/fieldset[1]/div[11]/app-custom-field-generator[1]/app-custom-select[1]/label[1]"));
            Fields.Add("Creation Date", new KeyValuePair<int, string>(12, "/app-custom-form[1]/fieldset[1]/div[12]/app-custom-field-generator[1]/app-custom-date[1]/label[1]"));


            string itemTemp = "", itemNew = "";


            // Check if oreder by Descending all field
            foreach (var Field in Fields)
            {

                // Order by Field
                SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div[1]/fieldset/div[{0}]/label", Field.Value.Key));
                SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div/fieldset/div[{0}]/div/i[@title='Descending']", Field.Value.Key));


                // Send Data from innerHTML
                string typeSendValue = "innerHTML";


                // type of order by string
                string type = "str";


                //// Send Data from title 
                //if (Field.Key == "Paragraph Text" || Field.Key == "Checkbox")
                //    typeSendValue = "title";


                //// type of order by number
                //if (Field.Key == "Currency" || Field.Key == "Number" || Field.Key == "Checkbox")
                //    type = "curr";


                if (Field.Key == "Creation Date")
                    type = "Date";


                Thread.Sleep(4000);


                // Firts accunt 
                itemTemp = SafeGetValue(webappDriver, string.Format("//div[{0}]{1}", 1, Field.Value.Value), typeSendValue);


                // Check if 6 row is order by the field 
                for (int i = 2; i <= 6; i++)
                {
                    itemNew = SafeGetValue(webappDriver, string.Format("//div[{0}]{1}", i, Field.Value.Value), typeSendValue);
                    if (type == "str")
                    {
                        if (itemTemp.CompareTo(itemNew) < 0)
                        {
                            flag = true;
                            itemTemp = Field.Key;
                            break;
                        }
                    }
                    else if (type == "curr")
                    {
                        itemTemp = itemTemp.Replace('$', ' ');
                        itemNew = itemNew.Replace('$', ' ');
                        if (float.Parse(itemTemp) < float.Parse(itemNew))
                        {
                            flag = true;
                            itemTemp = Field.Key;
                            break;
                        }
                    }
                    else if (type == "Date")
                    {
                        if (DateTime.Parse(itemTemp) < DateTime.Parse(itemNew))
                        {
                            flag = true;
                            itemTemp = Field.Key;
                            break;
                        }
                    }
                    itemTemp = itemNew;
                }
            }

        }



        public static void Webapp_Sandbox_Order_By_TSA(RemoteWebDriver webappDriver)
        {

            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);


            bool flag = false;

            webapp_Sandbox_Home_Button(webappDriver, "Accounts");


            // Select list TSA_List
            try
            {
                SafeClick(webappDriver, string.Format("//div[@title='{0}']", "TSA_List"), safeWait: 300, maxRetry: 20);
            }
            catch
            {
                try
                {
                    if (SafeGetValue(webappDriver, "//div[@class='ellipsis']", "innerHTML") != "TSA_List")
                    {
                        SafeClick(webappDriver, "//div[@class='ellipsis']");
                        SafeClick(webappDriver, string.Format("//li[@title='{0}']", "TSA_List"));
                    }
                }
                catch
                {
                    //Assert of checking if exists list
                    Assert(false, "The list not exists");
                }
            }

            Thread.Sleep(4000);

            Dictionary<string, KeyValuePair<int, string>> TSA_Fields = new Dictionary<string, KeyValuePair<int, string>>();


            TSA_Fields.Add("Account ID", new KeyValuePair<int, string>(1,"/app-custom-form[1]/fieldset[1]//a[1]/span[1]"));
            TSA_Fields.Add("Single Line Text", new KeyValuePair<int, string>(2,"/app-custom-form[1]/fieldset[1]/div[2]/label[1]"));
            TSA_Fields.Add("Limited Line Text", new KeyValuePair<int, string>(3,"/app-custom-form[1]/fieldset[1]/div[3]/label[1]"));
            TSA_Fields.Add("Paragraph Text", new KeyValuePair<int, string>(4,"/app-custom-form[1]/fieldset[1]/div[4]/label[1]"));
            TSA_Fields.Add("Date", new KeyValuePair<int, string>(5,"/app-custom-form[1]/fieldset[1]/div[5]/label[1]"));
            TSA_Fields.Add("Number", new KeyValuePair<int, string>(7,"/app-custom-form[1]/fieldset[1]/div[7]/label[1]"));
            TSA_Fields.Add("Decimal Number", new KeyValuePair<int, string>(8,"/app-custom-form[1]/fieldset[1]/div[8]/label[1]"));
            TSA_Fields.Add("Currency", new KeyValuePair<int, string>(9,"/app-custom-form[1]/fieldset[1]/div[9]/label[1]"));
            TSA_Fields.Add("Checkbox", new KeyValuePair<int, string>(10,"/app-custom-form[1]/fieldset[1]/div[10]/app-custom-checkbox[1]/input[1]"));
            TSA_Fields.Add("Dropdown", new KeyValuePair<int, string>(11,"/app-custom-form[1]/fieldset[1]/div[11]/label[1]"));
            // TSA_Fields.Add("Multi Choice", new KeyValuePair<int, string>(12,"/app-custom-form[1]/fieldset[1]/div[12]/app-custom-field-generator[1]/app-custom-select[1]/label[1]"));


            string itemTemp = "", itemNew = "";


            // Check if oreder by Descending all field
            foreach (var Field in TSA_Fields)
            {

                if(flag)
                {
                    break;
                }

                // Order by Field
                SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div[1]/fieldset/div[{0}]/label", Field.Value.Key));
                SafeClick(webappDriver, string.Format("//div[@id='viewsContainer']/app-custom-list/div/fieldset/div[{0}]/div/i[@title='Descending']", Field.Value.Key));


                // Send Data from innerHTML
                string typeSendValue = "innerHTML";


                // type of order by string
                string type = "str";


                // Send Data from title 
                if (Field.Key == "Checkbox")
                    typeSendValue = "title";


                // type of order by number
                if (Field.Key == "Currency" || Field.Key == "Number" || Field.Key == "Checkbox")
                    type = "curr";


                if (Field.Key == "Date")
                    type = "Date";


                Thread.Sleep(4000);


                // Firts accunt 
                itemTemp = SafeGetValue(webappDriver, string.Format("//div[{0}]{1}", 1, Field.Value.Value), typeSendValue);


                // Check if 6 row is order by the field 
                for (int i = 2; i <= 6; i++)
                {
                    itemNew = SafeGetValue(webappDriver, string.Format("//div[{0}]{1}", i, Field.Value.Value), typeSendValue);
                    if (type == "str")
                    {
                        if (itemTemp.CompareTo(itemNew) < 0)
                        {
                            flag = true;
                            itemTemp = Field.Key;
                            break;
                        }
                    }
                    else if (type == "curr")
                    {
                        itemTemp = itemTemp.Replace('$', ' ');
                        itemNew = itemNew.Replace('$', ' ');
                        if (float.Parse(itemTemp) < float.Parse(itemNew))
                        {
                            flag = true;
                            itemTemp = Field.Key;
                            break;
                        }
                    }
                    else if (type == "Date")
                    {
                        if (DateTime.Parse(itemTemp) < DateTime.Parse(itemNew))
                        {
                            flag = true;
                            itemTemp = Field.Key;
                            break;
                        }
                    }

                    itemTemp = itemNew;
                }
            }


            //Assert of checking 
            Assert(!flag, string.Format("The list not Descending by {0}", itemTemp));
        }



        public static void webapp_Sandbox_Home_Button(RemoteWebDriver webappDriver,string nameButton)
        {
            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            // Accounts
            for (int i = 1; i < 10; i++)
            {
                try
                {
                    if (SafeGetValue(webappDriver, string.Format("//app-root[1]/div[1]/app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i.ToString()), "innerHTML") == nameButton)
                    {
                        SafeClick(webappDriver, string.Format("//app-root[1]/div[1]/app-home-page[1]/footer[1]/div[1]/div[2]/div[{0}]/div[1]", i.ToString()));
                        break;
                    }
                   
                }
                catch { break; }
            }
        }



        public static void webapp_Sandbox_edit_Account(RemoteWebDriver webappDriver, Dictionary<string, string> fields)
        {
            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            //   var api= GetApiData(Username,Password, "accounts","(Hidden = 0)","");


            webapp_Sandbox_Home_Button(webappDriver, "Accounts");

          

            // stor data the account




        }

    }
}
