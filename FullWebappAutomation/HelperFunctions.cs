using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Net;
using System.Threading;
using static FullWebappAutomation.Consts;
using static FullWebappAutomation.GlobalSettings;

using System.Diagnostics;
using System.Collections.Generic;

namespace FullWebappAutomation
{
    class HelperFunctions
    {
        /// <summary>
        /// Asserts a condition and throws AssertionException if false
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void Assert(bool condition, string message)
        {
            if (condition)
                return;

            if (message != null)
            {
                throw new AssertionException(message);
            }

            else throw new AssertionException("Unknown assertion error");
        }

        /// <summary>
        /// Delegate a function into an object
        /// </summary>
        /// <param name="webappDriver">@Dan Zlotnikov</param>
        /// <param name="backofficeDriver"></param>
        public delegate void Delegator(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver);

        /// <summary>
        /// Wraps tests with a try-catch-except logic that creates logs
        /// </summary>
        /// <param name="delegatedFunction"></param>
        /// <param name="remoteWebDriver"></param> 
        public static void BasicTestWrapper(Delegator delegatedFunction, RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            string functionName = delegatedFunction.Method.Name.ToString();

            Exception error = null;
            bool testSuccess = true;

            try
            {
                webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);
                delegatedFunction(webappDriver, backofficeDriver);
            }
            catch (Exception e)
            {
                error = e;
                testSuccess = false;
            }
            finally
            {
                WriteToSuccessLog(functionName, testSuccess, error);
            }
        }

        /// <summary>
        /// Gets from home page to cart(Sales Order > Store1 > Default Catalog)
        /// </summary>
        /// <param name="webappDriver"></param>
        public static void GetToOrderCenter_SalesOrder(RemoteWebDriver webappDriver)
        {
            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            // Accounts
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div/div");

            // First account
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[1]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span");
            Thread.Sleep(5000);

            // Plus button
            SafeClick(webappDriver, "//div[@id='actionBar']/div/ul[3]/li/a/span");

            // Sales Order
            SafeClick(webappDriver, "//div[@id='actionBar']/div/ul[3]/li/ul/li/span");
            Thread.Sleep(5000);

            /*     
             *     @Dan Zlotnikov
            // Origin account
            SafeClick(webappDriver, "//body/app-root/div/app-accounts-home-page/object-chooser-modal/div/div/div/div/div/div/app-custom-list/virtual-scroll/div/div[2]/app-custom-form/fieldset/div");

            //Done
            SafeClick(driver, "//div[@id='mainCont']/app-accounts-home-page/object-chooser-modal/div/div/div/div[3]/div[2]");
            */

            // Default Catalog
            SafeClick(webappDriver, "//div[@class='scrollable-content']/div[2]");
        }

        public static void GetToOrderCenter_SalesOrder2(RemoteWebDriver webappDriver)
        {
            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            // Accounts
            SafeClick(webappDriver, "//div[@id='mainCont']/app-home-page/footer/div/div[2]/div/div");

            // First account
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div[1]/app-custom-form/fieldset/div/app-custom-field-generator/app-custom-button/a/span");


            Thread.Sleep(5000);

            // Plus button
            SafeClick(webappDriver, "//div[@id='actionBar']/div/ul[3]/li/a/span");

            // Sales Order 2
            SafeClick(webappDriver, "//div[@id='actionBar']/div/ul[3]/li/ul/li[2]/span");
            Thread.Sleep(3000);
            /*      
            // Origin account
            SafeClick(webappDriver, "//body/app-root/div/app-accounts-home-page/object-chooser-modal/div/div/div/div/div/div/app-custom-list/virtual-scroll/div/div[2]/app-custom-form/fieldset/div");

            //Done
            SafeClick(driver, "//div[@id='mainCont']/app-accounts-home-page/object-chooser-modal/div/div/div/div[3]/div[2]");
            */

            // Default Catalog
            SafeClick(webappDriver, "//div[@id='viewsContainer']/app-custom-list/virtual-scroll/div[2]/div/app-custom-form");
        }

        /// <summary>
        /// Clicks an element in the web page. Uses retry logic for a specified amount of tries. One second buffer between tries.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="elementXPath"></param>
        public static void SafeClick(RemoteWebDriver driver, string elementXPath, int safeWait = 1000, int maxRetry = 30)
        {
            IWebElement element;

            int retryCount = 1;
            while (retryCount < maxRetry)
            {
                try
                {
                    element = driver.FindElementByXPath(elementXPath);
                    Highlight(driver, element, safeWait / 2);
                    element.Click();
                    break;
                }
                catch 
                {
                    Thread.Sleep(safeWait);
                    retryCount++;
                    continue;
                }
            }

            // If succeeded, write to performance log
            if (retryCount < maxRetry)
            {
                // Get caller test function name
                StackTrace stackTrace = new StackTrace();
                string testName = stackTrace.GetFrame(1).GetMethod().Name;

                WriteToPerformanceLog(testName, "SafeClick", retryCount);
                return;
            }

            // Otherwise throw execption
            else
            {
                string errorMessage = string.Format("Click action failed for element at XPath: {0}", elementXPath);
                RetryException error = new RetryException(errorMessage);
                throw error;

            }
        }

        /// <summary>
        /// Sends keys to an element in the web page. Uses retry logic for a specified amount of tries. One second buffer between tries.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="elementXPath"></param>
        /// <param name="KeysToSend"></param>
        public static void SafeSendKeys(RemoteWebDriver driver, string elementXPath, string KeysToSend, int safeWait = 1000, int maxRetry = 30)
        {
            IWebElement element;

            int retryCount = 1;
            while (retryCount < maxRetry)
            {
                try
                {
                    element = driver.FindElementByXPath(elementXPath);
                    Highlight(driver, element, safeWait / 2);
                    element.SendKeys(KeysToSend);

                    return;
                }
                catch 
                {
                    Thread.Sleep(safeWait);
                    retryCount++;
                    continue;
                }
            }

            string errorMessage = string.Format("SendKeys action failed for element at XPath: {0}", elementXPath);
            RetryException error = new RetryException(errorMessage);
            throw error;
        }

        /// <summary>
        /// Clears an element in the web page. Uses retry logic.
        /// </summary>
        /// <param name="driver"></param>
        public static void SafeClear(RemoteWebDriver driver, string elementXPath, int safeWait = 1000, int maxRetry = 30)
        {
            IWebElement element;

            int retryCount = 1;
            while (retryCount < maxRetry)
            {
                try
                {
                    element = driver.FindElementByXPath(elementXPath);
                    Highlight(driver, element, safeWait / 2);
                    element.Clear();
                    return;
                }
                catch 
                {
                    Thread.Sleep(safeWait);
                    retryCount++;
                    continue;
                }
            }

            string errorMessage = string.Format("SendKeys action failed for element at XPath: {0}", elementXPath);
            RetryException error = new RetryException(errorMessage);
            throw error;
        }

        /// <summary>
        /// Gets value from an element in the web page. Uses retry logic for a specified amount of tries. One second buffer between tries.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="elementXPath"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static dynamic SafeGetValue(RemoteWebDriver driver, string elementXPath, string attribute, int safeWait = 1000, int maxRetry = 30)
        {
            IWebElement element;

            int retryCount = 1;
            while (retryCount < maxRetry)
            {
                try
                {
                    element = driver.FindElementByXPath(elementXPath);
                    var value = element.GetAttribute(attribute);
                    Highlight(driver, element, safeWait / 2);
                    return value;
                }
                catch 
                {
                    Thread.Sleep(safeWait);
                    retryCount++;
                    continue;
                }
            }

            string errorMessage = string.Format("GetValue action failed for element at XPath: {0}", elementXPath);
            RetryException error = new RetryException(errorMessage);
            throw error;
        }

        /// <summary>
        /// Upload File to web ERP
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pathFile"></param>
        /// <param name="elementXPath">Type of file</param>
        public static void UploadFile(RemoteWebDriver driver, string pathFile, string elementXPath, bool erp)
        {
            if (!erp)
            {
                // ERP Integration
                FullWebappAutomation.Backoffice.ERPIntegration.File_Uploads_And_Logs(driver);
            }

            // Upload Button
            SafeClick(driver, "//div/div/div[@id='loadERPFile']");


            // Choose value
            SafeClick(driver, "//div[@id='dLoadErpFilesContainer']/div/select[@id='erpFileType']");


            // API Item
            SafeClick(driver,string.Format("//div[@id='dLoadErpFilesContainer']/div/select/option[@value='{0}']", elementXPath));



            //  Browse  
            SafeClick(driver, "//div[@id='loadERPFileDialog']/div/div/a");


            // UploadFile to web
            driver.SwitchTo().ActiveElement().SendKeys(pathFile);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Thread.Sleep(10000);


            //close popue
            SafeClick(driver, "//div[@class='ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable']//a/span[@class='ui-icon ui-icon-closethick']");
            Thread.Sleep(4000);
        }
        
        /// <summary>
        /// check if stor and all line stor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="typeFile"></param>
        public static void checkFile(RemoteWebDriver driver,string typeFile)
        {
            // get  Failed Files label
            string value = SafeGetValue(driver, "//tr[@id='1']//td[@class='ll_td ll_td3']", "innerHTML");


            // file succeeded
            Assert(value == "", typeFile+" file no success upload");


            string linesInFile = SafeGetValue(driver, "//tr[@id='1']//td[@class='ll_td ll_td4']", "innerHTML");
            string SuccessLines = SafeGetValue(driver, "//tr[@id='1']//td[@class='ll_td ll_td6']", "innerHTML");



            // Line succeeded
            Assert(linesInFile == SuccessLines, string.Format("not all line of file {0} success upload",typeFile));
        }

        /// <summary>
        /// Highlights an element in the webpage
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void Highlight(IWebDriver driver, IWebElement element, int safeWait)
        {
            // Highlight element
            var jsDriver = (IJavaScriptExecutor)driver;
            string highlightJavascript = @"arguments[0].style.cssText = 'background: yellow; border-width: 2px; border-style: solid; border-color: red';";
            jsDriver.ExecuteScript(highlightJavascript, new object[] { element });

            // Restore element css to original 
            System.Threading.Thread.Sleep(safeWait);
            string originalStyleJavascript = @"arguments[0].style.cssText = '';";
            jsDriver.ExecuteScript(originalStyleJavascript, new object[] { element });
        }

        /// <summary>
        /// Creates a new log file to the grid_logs folder
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="browserName"></param>
        /// <param name="dateTime"></param>
        /// <returns> Log file path </returns>. 
        public static string CreateNewLog(string logType, string browserName, DateTime dateTime)
        {
            string strDateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff").Replace('\\', '-').Replace(' ', '_').Replace(':', '-');
            string path = string.Format("C:\\Users\\yosef.h\\Desktop\\automation_documents\\grid_logs\\CS\\{0}_{1}_{2}.json", logType, browserName, strDateTime);
            using (StreamWriter streamWriter = File.AppendText(path))
            {
                streamWriter.Write("");
            }
            return path;
        }

        /// <summary>
        /// Upload new number of login
        /// </summary>
        public static int LoadFromNewLog()
        {
            
            string json = File.ReadAllText(newCredentialsFilePath);

            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            System.IO.File.WriteAllText(newCredentialsFilePath, string.Empty);
            //string s=int.Parse(jsonObj["newLog"]);
            jsonObj["oldLog"] = jsonObj["newLog"];
            string s = jsonObj["newLog"];
            int temp=int.Parse(s);
            temp++;
            jsonObj["newLog"] = (temp).ToString();
            


            WriteToNewLog(jsonObj);

            return temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldLog"></param>
        /// <param name="newLog"></param>
        public static void WriteToNewLog(dynamic jsonObj)
        {
            using (StreamWriter streamWriter = File.AppendText(newCredentialsFilePath))
            {
                streamWriter.WriteLine(jsonObj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldLog"></param>
        /// <param name="newLog"></param>
        public static string CreateNewLogJson(string oldLog, string newLog)
        {
            dynamic newLogObject = new
            {
                oldLog,
                newLog
            };
            var json = JsonConvert.SerializeObject(newLogObject);
            return json;
        }
       
        /// <summary>
        /// Creates and returns a success json string for a test
        /// </summary>
        public static string CreateSuccessJson(string testName, bool success, Exception error)
        {
            // Create an object to dump - dynamic means it can be manipulated as I wish (like python)
            dynamic successObject = new
            {
                testName,
                success,
                error = error?.Message
            };
            var json = JsonConvert.SerializeObject(successObject);
            return json;
        }

        /// <summary>
        /// Writes a new test success string to the success log file
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        public static void WriteToSuccessLog(string testName, bool success, Exception error)
        {
            string jsonSuccessString = CreateSuccessJson(testName, success, error);
            using (StreamWriter streamWriter = File.AppendText(successLogFilePath))
            {
                streamWriter.WriteLine(jsonSuccessString);
            }
        }

        /// <summary>
        /// Creates and returns a success json string for a test
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="action"></param>
        /// <param name="tryCounter"></param>
        /// <returns></returns>
        public static string CreatePerformanceJson(string testName, string action, int tryCount)
        {
            // Create an object to dump - dynamic means it can be manipulated as I wish (like python)
            dynamic performanceObject = new
            {
                testName,
                action,
                tryCount
            };

            var json = JsonConvert.SerializeObject(performanceObject);
            return json;
        }

        /// <summary>
        /// Writes a new actions performance string to the performance log file
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="action"></param>
        /// <param name="tryCount"></param>
        public static void WriteToPerformanceLog(string testName, string action, int tryCount)
        {
            string jsonPerformanceString = CreatePerformanceJson(testName, action, tryCount);

            using (StreamWriter streamWriter = File.AppendText(performanceLogFilePath))
            {
                streamWriter.WriteLine(jsonPerformanceString);
            }
        }

        /// <summary>
        /// Creates and return a line for a bad performance action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testName"></param>
        /// <param name="actionIndexInTest"></param>
        /// <param name="tryCount"></param>
        /// <returns></returns>
        public static string CreateFinalizedPerformanceJson(string action, string testName, int actionIndexInTest, int tryCount)
        {
            // Create an object to dump - dynamic means it can be manipulated as I wish (like python)
            dynamic finalizedPerformanceObject = new
            {
                action,
                testName,
                actionIndexInTest,
                tryCount
            };

            var json = JsonConvert.SerializeObject(finalizedPerformanceObject);
            return json;
        }

        /// <summary>
        /// Analyzes the performance log and writes the laggy actions to the finalized performance log
        /// </summary>
        public static void WriteToFinalizedPerformanceLog()
        {
            string[] logLines = File.ReadAllLines(performanceLogFilePath);

            string previousTest = "";
            int actionIndexInTest = 0;

            foreach (string line in logLines)
            {
                dynamic lineObject = JsonConvert.DeserializeObject(line);
                string currentTest = lineObject.testName.ToString();

                // Count index of operation in function (set to 1 if changed function)
                actionIndexInTest = (currentTest.Equals(previousTest)) ? actionIndexInTest + 1 : 1;

                var tryCount = Int32.Parse(lineObject.tryCount.ToString());

                // Check if function exceeded try limit
                if (tryCount > actionPerformanceLimit)
                {
                    var action = lineObject.action.ToString();
                    string finalizedString = CreateFinalizedPerformanceJson(action, currentTest, actionIndexInTest, tryCount);

                    using (StreamWriter streamWriter = File.AppendText(finalizedPerformanceLogFilePath))
                    {
                        streamWriter.WriteLine(finalizedString);
                    }
                }
                previousTest = currentTest;
            }
        }

        /// <summary>
        /// Gets API data from the server. Returns an array of objects representing the query data fetched (every object is a db row).
        /// </summary>
        /// <param name="AuthorizationPassword"> API authorization </param>
        /// <param name="AuthorizationUsername"> API authorization </param>
        /// <param name="objectType"> Requested object type (transaction, user, item, etc.) </param>
        /// <param name="property"> Object attribute to be evaluated in request (id, name, email) </param>
        /// <param name="value"> Expected value of requested attribute </param>
        /// <returns></returns>
        public static dynamic GetApiData(string AuthorizationUsername, string AuthorizationPassword, string objectType, string property, string value)
        {
            var data = string.Empty;

            // API request url, with inserted data from method params
            string url = string.Format(@"https://apint.sandbox.pepperi.com/restapi/PepperiAPInt.Data.svc/V1.0/{0}?where={1}'{2}'", objectType, property, value);

            // Create the request object
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Create encoding (authorization) string
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(AuthorizationUsername + ":" + AuthorizationPassword));

            // Add authorization to request header
            request.Headers.Add("Authorization", encoded);
            

            using (var response = (HttpWebResponse)request.GetResponse())


            //Data stream from request
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }




            //Convert json API data to c# object
            dynamic DataObject = JsonConvert.DeserializeObject(data);

            return DataObject;
            
        }

        /// <summary>
        /// Gets user password from JSON file
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GetUserPassword(string username)
        {
            string[] users = File.ReadAllLines(UserCredentialsFilePath);
            foreach (string user in users)
            {
                dynamic userObject = JsonConvert.DeserializeObject(user);
                if (userObject.username == username)
                    return userObject.password;
            }
            return null;
        }

        /// <summary>
        /// Checke if any word from name is sub string in foundName 
        /// </summary>
        /// <param name="name"> </param>
        /// <param name="foundName"</param>
        /// <returns>true if found and false if not found </returns>
        public static bool serach(string name,string foundName)
        {
            //TO DO 
            string temp = null;
            int index = 0;
            do
            {
                index = name.IndexOf(" ");
                if (index == -1)
                {
                    temp = name;
                    break;
                }
                temp = name.Substring(0, index);
                if (foundName.Contains(temp))
                {
                    // Found
                    return true;
                }
                name = name.Remove(0, index + 1);
            } while (true);
            // Not found
            return false;
        }

        /// <summary>
        ///  Help Change Title Home Screen
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="newTitle"></param>
        public static void Help_Sandbox_Change_Title_Home_Screen(RemoteWebDriver backofficeDriver, string newTitle)
        {
            Backoffice.CompanyProfile.Home_Screen_Shortcut(backofficeDriver);


            // Edit Rep
            SafeClick(backofficeDriver, "//div[@id='formContTemplate']/div/div[2]/div/span[2]");


            // Edit Sales Order click
            SafeClick(backofficeDriver, "//ul[@class='clearfix ui-sortable']/li[1]/div[@class='lb-item-inner']/div[@class='lb-item-header']/span[2][@class='lb-edit editPenIcon editPenIconDisable']");


            // Input Sales Order 1
            SafeClear(backofficeDriver, "//li[1]/div[@class='lb-item-inner']/div[1]/input[1][@class='lb-title-edit']");
            SafeSendKeys(backofficeDriver, "//li[1]/div[@class='lb-item-inner']/div[1]/input[1][@class='lb-title-edit']", newTitle);


            // Save click
            SafeClick(backofficeDriver, "//div[1]/div[4]/div[@class='footer-buttons']/div[@class='save allButtons grnbtn roundCorner  fl']");
        }

        #region helper to Account
        /// <summary>
        ///  Change the log to New List Account
        /// </summary>
        /// <param name="userName"></param>
        public static void Var_Sandbox_Enable_New_List_Account(string userName)
        {

            RemoteWebDriver varDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), DesiredCapabilities.Chrome(), TimeSpan.FromSeconds(600));


            // get password
            string Password = GetUserPassword("var3@pepperi.com");


            // login to var
            Backoffice.GeneralActions.SandboxLogin(varDriver, "var3@pepperi.com", Password);


            // Search to user
            SafeSendKeys(varDriver, "//div[6]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/input[1]", userName);


            SafeClick(varDriver, "//div[6]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/a[1]");


            Thread.Sleep(3000);


            // Select 
            SafeClick(varDriver, "//div[6]/div[1]/div[1]/div[3]/div[1]/div[1]/div[1]/div[2]");


            // Edit
            SafeClick(varDriver, "//div[6]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/a[1]");


            // Enable new account list
            SafeClick(varDriver, "//input[@id='chkEnableNewAccountList']");


            // Save
            SafeClick(varDriver, "//div[@id='ctl00_MainContent_dDistributorDetailsContainer_dSave']//a[@title='Save']");

            Thread.Sleep(4000);
            varDriver.Quit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="Fields">Fields to add</param>
        /// <param name="nameList">list to add</param>
        public static void backoffice_Sandbox_Add_Available_Fields(RemoteWebDriver backofficeDriver, Dictionary<string, string> Fields,string nameList)
        {

            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);

            // Select List
            int top = 0;
            try
            {
                while (true)
                {
                    if (nameList == SafeGetValue(backofficeDriver, "//md-tab-content[@class='_md ng-scope md-no-transition md-active md-no-scroll']//div[@style='top:" + top.ToString() + "px']/div[@class='slick-cell l0 r0']", "innerHTML"))
                    {
                        SafeClick(backofficeDriver, "//div[@style='top:"+ top.ToString() + "px']/div[@class='slick-cell l3 r3']/div[@title='Edit']");
                        break;
                    }
                    top += 40;
                }
            }
            catch
            {
                Assert(false, "no exist the new list");
                return;
            }

            // Configuration
            SafeClick(backofficeDriver, "//md-pagination-wrapper/md-tab-item[2]");


            // Edit (Configuration) List View 
            SafeClick(backofficeDriver, "//div[@class='ui-widget-content slick-row even']/div[@class='slick-cell l2 r2']/div[@title='Edit']");


            // Edit Rep 
            SafeClick(backofficeDriver, "//div[@class='fl-box-title']/span[2]");

            foreach (var item in Fields)
            {
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).SendKeys(item.Key);
                SafeClick(backofficeDriver, string.Format("//li[@data-id='{0}']//div[@class='fr plusIcon plusIconDisable']", item.Value));
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).Clear();
                SafeClick(backofficeDriver, "//div[3]/div/div/span[@class='fa fa-search']");
                Thread.Sleep(1000);
            }


            // Save
            SafeClick(backofficeDriver, "//div[@class='save allButtons grnbtn roundCorner  fl']");

        }



    }
    #endregion
}

