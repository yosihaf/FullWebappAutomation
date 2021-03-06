﻿using OpenQA.Selenium.Remote;
using System;
using static FullWebappAutomation.HelperFunctions;

namespace FullWebappAutomation
{
    public enum TransactionAccountSettingsSelection
    {
        NoAssignment,
        OrigenAndDestination,
        Destination
    }

    public enum ListViewTypeForOldList
    {
        tableView,
        cardsView,
        detailedView
    }
    class GlobalSettings
    {
        public static string successLogFilePath;
        public static string performanceLogFilePath;
        public static string finalizedPerformanceLogFilePath;
        public static string UserCredentialsFilePath;
        public static string newCredentialsFilePath;
        public static string Username;
        public static string Password;
        
        public static void Setting(RemoteWebDriver backofficeDriver,bool flag=false)
        {
            SafeClick(backofficeDriver, "//div[@id='settingCont']/div");
            if (flag)
            {
                try
                {
                    SafeClick(backofficeDriver, "//div/button/span[@class='walkme-custom-balloon-button-text']");
                }
                catch { }
            }
        }
        public static void InitLogFiles()
        {
            DateTime dateTime = DateTime.Now;

            successLogFilePath = CreateNewLog("success", "chrome", dateTime);
            performanceLogFilePath = CreateNewLog("performance", "chrome", dateTime);
            finalizedPerformanceLogFilePath = CreateNewLog("finalizedPerformance", "chrome", dateTime);
            UserCredentialsFilePath = @"C:\Users\daniel.b.PEPPERI\Desktop\automation_documents\automation_documents\automation_users\admins.json";
            newCredentialsFilePath = @"C:\Users\yosef.h\Desktop\automation_documents\automation_users\Number of New Log.json";
        }
    }
}
