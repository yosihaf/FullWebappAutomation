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

            Creat_New_List(backofficeDriver, "Smart_Search_List");

            // key=name, value=API name
            
            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Street", "Street");
            Fields.Add("Country", "Country");
            Fields.Add("Price List ExternalID", "PriceListExternalID");
            Fields.Add("Special price list external ID", "SpecialPriceListExternalID");



            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Fields, "Smart_Search_List");

            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);

            Edit_Rep_Permission(backofficeDriver);


            Sreach_Available_Fields(backofficeDriver, Fields, "Smart Search");


            Thread.Sleep(3000);
        }



        public static void backoffice_Sandbox_Search_Account(RemoteWebDriver backofficeDriver)
        {
            Creat_New_List(backofficeDriver, "Search");

            // key=name, value=API name
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Street", "Street");
            Fields.Add("Price List ExternalID", "PriceListExternalID");
            Fields.Add("Special price list external ID", "SpecialPriceListExternalID");
            //Fields.Add("Price list name", "PriceLevelName");
            //Fields.Add("Country", "Country");


            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Fields, "Search_List");

            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);

            Edit_Rep_Permission(backofficeDriver);


            Sreach_Available_Fields(backofficeDriver, Fields, "Search");


            Thread.Sleep(3000);
        }



        public static void bakeoffice_Sandbox_Add_Basic_List(RemoteWebDriver backofficeDriver, Dictionary<string, string> Fields)
        {
            Fields.Add("Name", "Name");
            Fields.Add("City", "City");
            Fields.Add("Street", "Street");
            Fields.Add("Sales Reps", "Agents");
            Fields.Add("Price List ExternalID", "PriceListExternalID");
            Fields.Add("Special price list external ID", "SpecialPriceListExternalID");
            Fields.Add("Account ID", "ExternalID");
            Fields.Add("Email", "Email");
            Fields.Add("Country", "Country");



            Creat_New_List(backofficeDriver, "Basic_List");

            //  Add fields
            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, Fields, "Basic_List");

            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);

            Edit_Rep_Permission(backofficeDriver);
        }



        public static void backoffice_Sandbox_Creat_TSA_Fields_And_Added(RemoteWebDriver backofficeDriver, Dictionary<string, string> TSA_Fields)
        {

            // Creat_New_List name TSA_Fields
            Creat_New_List(backofficeDriver, "TSA_List");



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
           
            

            backoffice_Sandbox_Add_Available_Fields(backofficeDriver, TSA_Fields, "TSA_List");

            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);

            Edit_Rep_Permission(backofficeDriver);


        }

    }
}
