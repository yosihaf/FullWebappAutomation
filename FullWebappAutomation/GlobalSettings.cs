using System;
using static FullWebappAutomation.HelperFunctions;

namespace FullWebappAutomation
{
    class GlobalSettings
    {
        public static string successLogFilePath;
        public static string performanceLogFilePath;
        public static string finalizedPerformanceLogFilePath;
        public static string UserCredentialsFilePath;
        public static string newCredentialsFilePath;
        public static string DanUsername;
        public static string DanPassword;
        public static bool isCreat;

        public static void InitLogFiles()
        {
            DateTime dateTime = DateTime.Now;

            isCreat = false;
            successLogFilePath = CreateNewLog("success", "chrome", dateTime);
            performanceLogFilePath = CreateNewLog("performance", "chrome", dateTime);
            finalizedPerformanceLogFilePath = CreateNewLog("finalizedPerformance", "chrome", dateTime);
            UserCredentialsFilePath = @"C:\Users\yosef.h\Desktop\automation_documents\automation_users\admins.json";
            newCredentialsFilePath = @"C:\Users\yosef.h\Desktop\automation_documents\automation_users\Number of New Log.json";
        }
    }
}
