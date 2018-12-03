using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
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
    class BakeofficeTest
    {

        public static void backoffice_Sandbox_Smart_Search(RemoteWebDriver backofficeDriver, Dictionary<string, string> Fields)
        {

            //Create New List Smart_Search
            Creat_New_List_Lists_Accounts(backofficeDriver, "Smart_Search_List");

            // key=name, value=API name
            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Street", "Street");
            Fields.Add("Country", "Country");
            Fields.Add("Price List ExternalID", "PriceListExternalID");
            Fields.Add("Special price list external ID", "SpecialPriceListExternalID");
            Fields.Add("Single Line Text", "TSASingleLineText");
            Fields.Add("Limited Line Text", "TSALimitedLineText");
            Fields.Add("Paragraph Text", "TSAParagraphText");
            // TSA_Fields.Add("Date", "TSADate");
            // TSA_Fields.Add("Date + Time", "TSADateTime");
            Fields.Add("Number", "TSANumber");
            Fields.Add("Decimal Number", "TSADecimalNumber");
            Fields.Add("Currency", "TSACurrency");
            // TSA_Fields.Add("Checkbox", "TSACheckbox");
            Fields.Add("Dropdown", "TSADropdown");


            //backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Fields, "Smart_Search_List");


            // FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);


            // Edit_Rep_Permission(backofficeDriver,"Smart_Search_List");

            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);

            Sreach_Available_Fields(backofficeDriver, Fields, "Smart Search");


            Thread.Sleep(3000);
        }



        public static void backoffice_Sandbox_Search_Account(RemoteWebDriver backofficeDriver)
        {

            Creat_New_List_Lists_Accounts(backofficeDriver, "Search_List");

            // key=name, value=API name
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Street", "Street");
            Fields.Add("Price List ExternalID", "PriceListExternalID");
            Fields.Add("Special price list external ID", "SpecialPriceListExternalID");
            //Fields.Add("Price list name", "PriceLevelName");
            //Fields.Add("Country", "Country");

            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);


            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Fields, "Search_List");

            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);

            Edit_Rep_Permission(backofficeDriver, "Search_List");

            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);

            Sreach_Available_Fields(backofficeDriver, Fields, "Search");


            Thread.Sleep(3000);
        }



        public static void bakeoffice_Sandbox_Add_Basic_List(RemoteWebDriver backofficeDriver, Dictionary<string, string> Fields)
        {

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






            Creat_New_List_Lists_Accounts(backofficeDriver, "Basic_List");

            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);

            //  Add fields
            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Fields, "Basic_List");

            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);

            Edit_Rep_Permission(backofficeDriver, "Basic_List");
        }

        public static void Backoffice_Sandbox_Search_Activities(RemoteWebDriver backofficeDriver)
        {

            // Menu
            FullWebappAutomation.BackofficeNavigation.SalesActivities.Activity_Lists_New(backofficeDriver);


            // Configuration 
            Dictionary<string, string> Fields = new Dictionary<string, string>();


            Fields.Add("Order Remark", "Remark");
            Fields.Add("Account Multi Choice", "AccountTSAMultiChoice");


            FullWebappAutomation.BackofficeNavigation.SalesActivities.Activity_Lists_New(backofficeDriver);

            Sreach_Available_Fields(backofficeDriver, Fields, "Configure the fields the list may be searched by");

        }


        public static void backoffice_Sandbox_Smart_Search_(RemoteWebDriver backofficeDriver, Dictionary<string, string> TSA_Fields)
        {

            // Creat_New_List name TSA_Fields
            //  Creat_New_List(backofficeDriver, "TSA_List");



            //TSA_Fields.Add("Account ID", "ExternalID");
            //TSA_Fields.Add("Single Line Text", "TSASingleLineText");
            //TSA_Fields.Add("Limited Line Text", "TSALimitedLineText");
            //TSA_Fields.Add("Paragraph Text", "TSAParagraphText");
            ////   TSA_Fields.Add("Date", "TSADate");
            //// TSA_Fields.Add("Date + Time", "TSADateTime");
            //TSA_Fields.Add("Number", "TSANumber");
            //TSA_Fields.Add("Decimal Number", "TSADecimalNumber");
            //TSA_Fields.Add("Currency", "TSACurrency");
            ////  TSA_Fields.Add("Checkbox", "TSACheckbox");
            //TSA_Fields.Add("Dropdown", "TSADropdown");



            //  backoffice_Sandbox_Add_Available_Fields(backofficeDriver, TSA_Fields, "TSA_List");

            //    FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);

            //   Edit_Rep_Permission(backofficeDriver, "TSA_List");


            FullWebappAutomation.BackofficeNavigation.Accounts.Accounts_Lists_New(backofficeDriver);


            Sreach_Available_Fields(backofficeDriver, TSA_Fields, "Smart Search");


            Thread.Sleep(3000);
        }



        public static void bakeoffice_Sandbox_Add_views(RemoteWebDriver backofficeDriver, Dictionary<string, string> Fields)
        {
            FullWebappAutomation.BackofficeNavigation.Accounts.Views_And_Forms(backofficeDriver);

            SafeClick(backofficeDriver, "//div[@class='fl-form-box']//div[@class='fl-box-title']//span[2]");


            //delete all Layout
            for (int i = 1; ;)
            {
                try
                {
                    SafeClick(backofficeDriver, string.Format("//ul[@class='clearfix ui-sortable']//li[{0}]//div[1]//div[1]//span[4]", i), maxRetry: 2);
                    if (i == 1)
                        i++;
                }
                catch
                {
                    break;
                }

            }


            foreach (var item in Fields)
            {
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).SendKeys(item.Key);
                SafeClick(backofficeDriver, string.Format("//li[@data-id='{0}']//div[@class='fr plusIcon plusIconDisable']", item.Value));
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).Clear();
                SafeClick(backofficeDriver, "//div[@id='editFormCont']/div/div/div/span[@class='fa fa-search']");
                Thread.Sleep(1000);
            }

            // Save button
            SafeClick(backofficeDriver, "//div[@id='editFormContent']/div[5]");
            Thread.Sleep(4000);
        }


        public static void Backoffice_Sandbox_Create_Lists_Activities(RemoteWebDriver backofficeDriver, Dictionary<string, Dictionary<string, string>> allList)
        {

            #region Table_Basic_List end


            Dictionary<string, string> subDetails = new Dictionary<string, string>();
            // table value =1
            subDetails.Add("typeList", "1");
            // Date Range "Creation Date" value =5
            subDetails.Add("neme field 1", "5");
            subDetails.Add("in the last", "In the Last");
            // Input num 
            subDetails.Add("sum", "7");
            // date (Year , Months ,Weeks,Days)
            subDetails.Add("date", "Months");
            // Sort By Action Time value =1
            subDetails.Add("neme field 2", "1");
            // Filter 
            subDetails.Add("neme field 3", "5");
            // operator
            subDetails.Add("operator", "=");
            // Operator to
            subDetails.Add("Operator to", "0");


            KeyValuePair<string, Dictionary<string, string>> nameList = new KeyValuePair<string, Dictionary<string, string>>("Table_Basic_List", subDetails);


            allList.Add("Table_Basic_List", subDetails);


            // Creat_New_List name Table Basic List
            Creat_New_List_Activities(backofficeDriver, nameList);

            Dictionary<string, string> Table_Basic_Fields = new Dictionary<string, string>();
            Table_Basic_Fields.Add("Action Time", "ActionDateTime");
            Table_Basic_Fields.Add("Activity type name", "ActivityTypeID");
            Table_Basic_Fields.Add("Creation Date", "CreationDateTime");
            Table_Basic_Fields.Add("Creation ", "CreationGeoCodeLAT");
            Table_Basic_Fields.Add("Creation", "CreationGeoCodeLNG");
            Table_Basic_Fields.Add("CreatorID", "CreatorExternalID");
            Table_Basic_Fields.Add("Creator", "Creator");
            //Table_Basic_Fields.Add("Deleted", "Deleted");


            BackofficeNavigation.SalesActivities.Activity_Lists_New(backofficeDriver);
            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Table_Basic_Fields, "Table_Basic_List");
            #endregion


            #region Table_TSA_List no 

            //// Creat_New_List name Table TSA List
            //Creat_New_List_Activities(backofficeDriver, "Table_TSA_List", 1);

            #endregion


            #region Card_Basic_List end

            subDetails = new Dictionary<string, string>();

            // Card value =2
            subDetails.Add("typeList", "2");
            // Date Range "Modification Date" value = 8
            subDetails.Add("neme field 1", "8");
            subDetails.Add("in the last", "In the Last");
            // Input num 
            subDetails.Add("sum", "5");
            // date (Year , Months ,Weeks,Days)
            subDetails.Add("date", "Days");
            // Sort By Modification Date Time value =1
            subDetails.Add("neme field 2", "25");
            // Filter 
            subDetails.Add("neme field 3", "25");
            // operator
            subDetails.Add("operator", "StartWith");
            // Operator to
            subDetails.Add("Operator to", "5009");


            nameList = new KeyValuePair<string, Dictionary<string, string>>("Card_Basic_List", subDetails);


            allList.Add("Card_Basic_List", subDetails);


            // Creat_New_List name Card Basic List
            Creat_New_List_Activities(backofficeDriver, nameList);


            Dictionary<string, string> Card_Basic_Fields = new Dictionary<string, string>();


            Card_Basic_Fields.Add("External ID", "ExternalID");
            Card_Basic_Fields.Add("Modification Date", "ModificationDateTime");
            Card_Basic_Fields.Add("Planned Duration", "PlannedDuration");
            Card_Basic_Fields.Add("Planned End Time", "PlannedEndTime");
            Card_Basic_Fields.Add("Planned Start Time", "PlannedStartTime");
            Card_Basic_Fields.Add("Status", "Status");
            Card_Basic_Fields.Add("Submission ", "SubmissionGeoCodeLAT");
            Card_Basic_Fields.Add("Submission", "SubmissionGeoCodeLNG");


            FullWebappAutomation.BackofficeNavigation.SalesActivities.Activity_Lists_New(backofficeDriver);
            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Card_Basic_Fields, "Card_Basic_List");


            #endregion


            #region Card_TSA_List no


            // Creat_New_List name Table TSA List
            // Creat_New_List_Activities(backofficeDriver, nameList);

            #endregion


            #region Details_Basic_List end


            subDetails = new Dictionary<string, string>();


            // Card value =3
            subDetails.Add("typeList", "3");
            // Date Range "Modification Date" value = 8
            subDetails.Add("neme field 1", "8");
            subDetails.Add("in the last", "In the Last");
            // Input num 
            subDetails.Add("sum", "5");
            // date (Year , Months ,Weeks,Days)
            subDetails.Add("date", "Days");
            // Sort By Modification Date Time value =1
            subDetails.Add("neme field 2", "25");
            // Filter 
            subDetails.Add("neme field 3", "25");
            // operator
            subDetails.Add("operator", "StartWith");
            // Operator to
            subDetails.Add("Operator to", "5009");


            nameList = new KeyValuePair<string, Dictionary<string, string>>("Details_Basic_List", subDetails);


            allList.Add("Details_Basic_List", subDetails);


            // Creat_New_List name Details Basic List
            Creat_New_List_Activities(backofficeDriver, nameList);

            Dictionary<string, string> Details_Basic_Fields = new Dictionary<string, string>();

            Details_Basic_Fields.Add("Title", "Title");
            Details_Basic_Fields.Add("Type", "Type");
            Details_Basic_Fields.Add("email", "Agent.Email");
            Details_Basic_Fields.Add("first ", "Agent.FirstName");
            Details_Basic_Fields.Add("Remark", "Remark");
            Details_Basic_Fields.Add("Quantities", "QuantitiesTotal");
            Details_Basic_Fields.Add("Branch", "Branch");
            Details_Basic_Fields.Add("Ship To Name", "ShipToName");


            FullWebappAutomation.BackofficeNavigation.SalesActivities.Activity_Lists_New(backofficeDriver);
            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Details_Basic_Fields, "Details_Basic_List");

            #endregion


            #region Details_TSA_List no

            //// Creat_New_List name Details TSA List
            //Creat_New_List_Activities(backofficeDriver, "Details_TSA_List", 3);
            #endregion


            #region Line_Basic_List

            subDetails = new Dictionary<string, string>();

            // Details value = 4
            subDetails.Add("typeList", "4");
            // Date Range "Modification Date" value = 8
            subDetails.Add("neme field 1", "8");
            subDetails.Add("in the last", "In the Last");
            // Input num 
            subDetails.Add("sum", "5");
            // date (Year , Months ,Weeks,Days)
            subDetails.Add("date", "Days");
            // Sort By Modification Date Time value =1
            subDetails.Add("neme field 2", "25");
            // Filter 
            subDetails.Add("neme field 3", "25");
            // operator
            subDetails.Add("operator", "StartWith");
            // Operator to
            subDetails.Add("Operator to", "5009");


            nameList = new KeyValuePair<string, Dictionary<string, string>>("Line_Basic_List", subDetails);


            allList.Add("Line_Basic_List", subDetails);


            //Creat_New_List name Line Basic List
            Creat_New_List_Activities(backofficeDriver, nameList);


            Dictionary<string, string> Line_Basic_List = new Dictionary<string, string>();

            Line_Basic_List.Add("Account Address", "Account.Address");
            Line_Basic_List.Add("Account sale", "Account.Agents");
            Line_Basic_List.Add("Account Buyers", "Account.Buyers");
            Line_Basic_List.Add("Account City", "Account.City");
            Line_Basic_List.Add("Account Contact Persons", "Account.ContactPersons");
            Line_Basic_List.Add("Account Country", "Account.Country");
            Line_Basic_List.Add("Account Creation Date", "Account.CreationDate");
            Line_Basic_List.Add("Account Deleted", "Account.Deleted");


            FullWebappAutomation.BackofficeNavigation.SalesActivities.Activity_Lists_New(backofficeDriver);
            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Line_Basic_List, "Line_Basic_List");


            #endregion


            #region Line_TSA_List no

            //// Creat_New_List name Line TSA List
            //Creat_New_List_Activities(backofficeDriver, "Line_TSA_List", 4);

            #endregion


            #region map list
            //// Creat_New_List name Map Basic List
            //Creat_New_List_Activities(backofficeDriver, "Map_Basic_List", 6);



            //// Creat_New_List name Map TSA List
            //Creat_New_List_Activities(backofficeDriver, "Map_TSA_List", 6);
            #endregion


            #region add to permission all list

            BackofficeNavigation.SalesActivities.Activity_Lists_New(backofficeDriver);


            // Permission
            SafeClick(backofficeDriver, "//md-tab-item[3][@class='md-tab ng-scope ng-isolate-scope ng-binding']");


            // Edit Admin
            backoffice_Edit_Admin(backofficeDriver, "Admin", "formContTemplate2");

            // delet all
            try
            {
                while (true)
                {
                    SafeClick(backofficeDriver, string.Format("//div[1]/ul[1]/li[1]/div[1]/div[1]/span[4]"), maxRetry: 3, safeWait: 300);
                }
            }
            catch { }


            // add  all list
            try
            {
                // activity list
                SafeClick(backofficeDriver, "//div[1]/div[1]/md-tabs[1]/md-tabs-content-wrapper[1]/md-tab-content[3]/div[1]/div[1]/div[3]/div[2]/ul[1]");
                for (int i = 1; ; i++)
                {
                    SafeClick(backofficeDriver, string.Format("//div[@class='lb-bank ui-droppable']//ul//li[{0}]//div[1]//div[1]", i), maxRetry: 3, safeWait: 300);
                }
            }
            catch { }


            // Save
            SafeClick(backofficeDriver, "//div[1]/md-tabs[1]/md-tabs-content-wrapper[1]/md-tab-content[3]/div[1]/div[1]/div[4]/div[1]/div[1]");

            #endregion

        }



        public static void Bakeoffice_Sandbox_Create_Sync(RemoteWebDriver backofficeDriver)
        {
            Dictionary<string, string> subDetails = new Dictionary<string, string>();
            // table value =1
            subDetails.Add("typeList", "1");
            // Date Range "Creation Date" value =5
            subDetails.Add("neme field 1", "5");
            subDetails.Add("in the last", "In the Last");
            // Input num 
            subDetails.Add("sum", "7");
            // date (Year , Months ,Weeks,Days)
            subDetails.Add("date", "Months");
            // Sort By Action Time value =1
            subDetails.Add("neme field 2", "1");
            // Filter 
            subDetails.Add("neme field 3", "5");
            // operator
            subDetails.Add("operator", "=");
            // Operator to
            subDetails.Add("Operator to", "0");


            KeyValuePair<string, Dictionary<string, string>> nameList = new KeyValuePair<string, Dictionary<string, string>>("Sync", subDetails);

            // Creat_New_List name Sync
            Creat_New_List_Activities(backofficeDriver, nameList);
            BackofficeNavigation.CompanyProfile.App_Home_Screen(backofficeDriver);
            EditBOForm(backofficeDriver);
            //Add the list to App home page
            CustomizeToForm(backofficeDriver, new string[] { "Sync" });
        }



        public static void Creat_New_List_Activities(RemoteWebDriver backofficeDriver, KeyValuePair<string, Dictionary<string, string>> nameList)
        {
            BackofficeNavigation.SalesActivities.Activity_Lists_New(backofficeDriver);


            // + Create New List
            SafeClick(backofficeDriver, "//div[@id='btnAddNewAcc']");


            //  Input New List Fildes
            // input name
            SafeSendKeys(backofficeDriver, "//div[@class='ListName parCont clearfix']/input[@name='name']", nameList.Key);


            // input description
            SafeSendKeys(backofficeDriver, "//div[@class='ListDescription parCont clearfix']/input[@name='description']", "Automation " + nameList.Key);

            // List view type
            SafeClick(backofficeDriver, string.Format("//md-tabs-content-wrapper[1]/md-tab-content[1]//label/input[@value='{0}']", nameList.Value["typeList"]));


            // Date Range

            // neme field
            SafeClick(backofficeDriver, string.Format("//div[@class='dateRange']//a[@class='select2-choice']"));
            SafeClick(backofficeDriver, string.Format("//div[@id='select2-drop']/ul/li[{0}]/div[1]", nameList.Value["neme field 1"]));


            // in the last
            SafeClick(backofficeDriver, "//select[@class='dateOps ng-pristine ng-untouched ng-valid ng-not-empty']");
            SafeClick(backofficeDriver, string.Format("//option[@label='{0}']", nameList.Value["in the last"]));


            // sum
            SafeSendKeys(backofficeDriver, "//div[@class='ng-scope ng-isolate-scope']/div[@class='general-info ng-scope']/div[@class='dateRange']/input[1]", nameList.Value["sum"]);


            // date
            SafeClick(backofficeDriver, "(.//*[normalize-space(text()) and normalize-space(.)='External ID'])[1]/preceding::select[2]");
            SafeClick(backofficeDriver, string.Format("//option[@label='{0}']", nameList.Value["date"]));


            // Sort By
            SafeClick(backofficeDriver, "//div[@class='sort']//a[@class='select2-choice']");
            SafeClick(backofficeDriver, string.Format("//div[@id='select2-drop']/ul/li[{0}]/div[1]", nameList.Value["neme field 2"]));


            // Filter
            SafeClick(backofficeDriver, "//div[@class='filterExpressionCont']//a[@class='select2-choice']");
            SafeClick(backofficeDriver, string.Format("//div[@id='select2-drop']/ul/li[{0}]/div[1]", nameList.Value["neme field 3"]));


            // Operator
            SafeClick(backofficeDriver, string.Format("//option[@value='{0}']", nameList.Value["operator"]));


            // Operator to
            //if(nameList.Value["operator"]==)
            SafeSendKeys(backofficeDriver, "//div[@class='filterExpressionCont']//input[@class='first']", nameList.Value["Operator to"]);


            // Save
            SafeClick(backofficeDriver, "//div[@class='general-info ng-scope']/input[1]");
        }



        /// <summary>
        /// Customize the fields you're interested, select the page you want to navigate to, the form will be edit automatically according to your profile      
        /// /// TO DO ********* OPTION TO SEND PRO
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="Fields"></param>
        /// <param name="pageToNavigate"></param>
        public static void CustomizeToForm(RemoteWebDriver backofficeDriver, string[] Fields,Delegator1 pageToNavigate)
        {
            pageToNavigate(backofficeDriver:backofficeDriver);
            EditBOForm(backofficeDriver);
            foreach (var item in Fields)
            {
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).SendKeys(item);
                SafeClick(backofficeDriver, string.Format("//span[@class='fl' and contains(.,'{0}')]//following-sibling::div", item));
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).Clear();
                SafeClick(backofficeDriver, "//div[3]/div/div//span[@class='fa fa-search']");
                Thread.Sleep(1000);
            }
            SafeClick(backofficeDriver, "//div[contains(text(),'Save')]");
        }




        /// <summary>
        /// Customize the fields you're interested in to the form you're in
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="Fields"></param>
        public static void CustomizeToForm(RemoteWebDriver backofficeDriver, string[] Fields)
        {
            foreach (var item in Fields)
            {
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).SendKeys(item);
                SafeClick(backofficeDriver, string.Format("//span[@class='fl' and contains(.,'{0}')]//following-sibling::div", item));
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).Clear();
                SafeClick(backofficeDriver, "//div[3]/div/div//span[@class='fa fa-search']");
                Thread.Sleep(1000);
            }
            SafeClick(backofficeDriver, "//div[contains(text(),'Save')]");
        }


        public static void EditActivity(RemoteWebDriver backofficeDriver, string activityName)
        {
            SafeClick(backofficeDriver, String.Format("//div[text()='{0}']//following-sibling::div[2]//div[@title='Edit']", activityName));
        }



        public static void DeleteActivity(RemoteWebDriver backofficeDriver, string activityName)
        {
            SafeClick(backofficeDriver, String.Format("//div[text()='{0}']//following-sibling::div[2]//div[@title='Delete']", activityName));
        }



        /// <summary>
        /// config all option for the transaction accounts settings  
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="config"></param>
        public static void ConfigTransactionAccountSettings(RemoteWebDriver backofficeDriver, TransactionAccountSettingsSelection config)
        {
            switch (config)
            {
                case TransactionAccountSettingsSelection.NoAssignment:
                    {
                        SafeClick(backofficeDriver, "//div[@id='btnDestSetEdit']");
                        SafeClick(backofficeDriver, "//label[@for='radioNoType']");
                        SafeClick(backofficeDriver, "//div[@id='btnSaveAccTypes']");

                        break;
                    }
                case TransactionAccountSettingsSelection.Destination:
                    {
                        SafeClick(backofficeDriver, "//div[@id='btnDestSetEdit']");
                        SafeClick(backofficeDriver, "//input[@id='cb_All']");
                        SafeClick(backofficeDriver, "//li[@id='destCBList']//ul//li//label[contains(text(),'New Type')]");
                        SafeClick(backofficeDriver, "//div[@id='btnSaveAccTypes']");
                        break;
                    }
                case TransactionAccountSettingsSelection.OrigenAndDestination:
                    {
                        //destination
                        SafeClick(backofficeDriver, "//div[@id='btnDestSetEdit']");
                        SafeClick(backofficeDriver, "//input[@id='cb_All']");
                        SafeClick(backofficeDriver, "//li[@id='destCBList']//ul//li//label[contains(text(),'Customer')]");
                        SafeClick(backofficeDriver, "//div[@id='btnSaveAccTypes']");

                        //Origen
                        SafeClick(backofficeDriver, "//div[@id='btnEditCatalogsConnection2']");
                        SafeClick(backofficeDriver, "//div[@id='accTypesCBlistOrig']//label[contains(text(),'New Type')]//parent::li//input");
                        SafeClick(backofficeDriver, "//div[@id='btnSaveAccTypes']");
                        break;
                    }
            }
        }



        public static void Create_New_Transaction(RemoteWebDriver backofficeDriver, string name)
        {
            BackofficeNavigation.SalesActivities.Transaction_Types(backofficeDriver);
            SafeClick(backofficeDriver, "//div[@class='btnAddNewOrderType allButtons grnbtn roundCorner fl']");
            SafeSendKeys(backofficeDriver, "//input[@id='orderName']", name);
            SafeSendKeys(backofficeDriver, "//input[@id='orderDescription']", name);
            SafeClick(backofficeDriver, "//div[@id='icon2']");
            SafeClick(backofficeDriver, "//div[@id='btnSaveOrderType']");
            Thread.Sleep(10000);
        }


        public static void Backoffice_Transaction_Accounts_Settings(RemoteWebDriver backofficeDriver)
        {
            Create_New_Transaction(backofficeDriver, "test1");
            BackofficeNavigation.ActivityNavigation.Accounts(backofficeDriver);
            ConfigTransactionAccountSettings(backofficeDriver, TransactionAccountSettingsSelection.NoAssignment);
            Create_New_Transaction(backofficeDriver, "test2");
            BackofficeNavigation.ActivityNavigation.Accounts(backofficeDriver);
            ConfigTransactionAccountSettings(backofficeDriver, TransactionAccountSettingsSelection.Destination);
            Create_New_Transaction(backofficeDriver, "test3");
            BackofficeNavigation.ActivityNavigation.Accounts(backofficeDriver);
            ConfigTransactionAccountSettings(backofficeDriver, TransactionAccountSettingsSelection.OrigenAndDestination);
        }

    }
}
