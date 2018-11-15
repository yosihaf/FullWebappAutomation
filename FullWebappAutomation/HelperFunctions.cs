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
using System.Linq;

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
            WebappTest.webapp_Sandbox_Home_Button(webappDriver, "Accounts");


            // First account
            first_account(webappDriver,1);

            

            // ... button
            SafeClick(webappDriver, "//div[@id='topBarContainer']//list-menu/div/a/span");

            // Sales Order
            SafeClick(webappDriver, "//div/div/top-bar/div/div/list-menu/div/ul//li[@title='Sales Order']");
            Thread.Sleep(5000);

            /*     
             *     @Dan Zlotnikov
            // Origin account
            SafeClick(webappDriver, "//body/app-root/div/app-accounts-home-page/object-chooser-modal/div/div/div/div/div/div/app-custom-list/virtual-scroll/div/div[2]/app-custom-form/fieldset/div");

            //Done
            SafeClick(driver, "//div[@id='mainCont']/app-accounts-home-page/object-chooser-modal/div/div/div/div[3]/div[2]");
            */
            try
            {
                // Default Catalog
                SafeClick(webappDriver, "//div[@class='scrollable-content']/div[2]",safeWait:300,maxRetry:4);
            }
            catch{ }


        }

        public static void first_account(RemoteWebDriver webappDriver, int index)
        {
            // First account
            SafeClick(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]//a[1]/span[1]", index));
            Thread.Sleep(5000);
        }


        public static void GetToOrderCenter_SalesOrder2(RemoteWebDriver webappDriver)
        {
            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            webappDriver.Navigate().GoToUrl(webappSandboxHomePageUrl);

            // Accounts
            WebappTest.webapp_Sandbox_Home_Button(webappDriver, "Accounts");

            // First account
            first_account(webappDriver, 1);

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
            List<string> nameList = name.Split(' ').ToList();
            foreach (var item in nameList)
            {
                if (!foundName.ToLower().Contains(item.ToLower()))
                {
                    return false;
                }
            }

            return true;
        }

        public static void backoffice_Edit_Admin(RemoteWebDriver backofficeDriver, string type,string id= "formContTemplate")
        {

            
            int k = 2;
            while (true)
            {
                try
                {
                    if (SafeGetValue(backofficeDriver, string.Format("//div[@id='{1}']/div/div[{0}]/div/span[1]", k, id), "innerHTML", maxRetry: 3, safeWait: 300) == type)
                    {
                        SafeClick(backofficeDriver, string.Format("//div[@id='{1}']/div/div[{0}]/div/span[2]", k, id));
                        break;
                    }
                }
                catch
                {
                    k = 1;
                    type = "Rep";

                }
                k++;
            }
        }


        /// <summary>
        ///  Help Change Title Home Screen
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="newTitle"></param>
        public static void Help_Sandbox_Change_Title_Home_Screen(RemoteWebDriver backofficeDriver, string newTitle)
        {
            Backoffice.CompanyProfile.Home_Screen_Shortcut(backofficeDriver);


            // Edit Admin
            backoffice_Edit_Admin(backofficeDriver, "Admin");



            // Edit Sales Order click
            SafeClick(backofficeDriver, "//ul[@class='clearfix ui-sortable']/li[1]/div[@class='lb-item-inner']/div[@class='lb-item-header']/span[2][@class='lb-edit editPenIcon editPenIconDisable']");


            // Input Sales Order 1
            SafeClear(backofficeDriver, "//li[1]/div[@class='lb-item-inner']/div[1]/input[1][@class='lb-title-edit']");
            SafeSendKeys(backofficeDriver, "//li[1]/div[@class='lb-item-inner']/div[1]/input[1][@class='lb-title-edit']", newTitle);


            // Save click
            SafeClick(backofficeDriver, "//div[1]/div[4]/div[@class='footer-buttons']/div[@class='save allButtons grnbtn roundCorner  fl']");
        }

        #region helper to Account

        public static void check_Field_Textbox_Search(RemoteWebDriver webappDriver, string Street, int count, int column)
        {
            // Click search button
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/a/span");

            // Input name
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/input");
            SafeSendKeys(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/input", Street);

            // Click search button
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/a/span");
            Thread.Sleep(bufferTime);



            string foundName = "";
            int index = 2;
            bool isFound = true;
            while (true)
            {
                try
                {                                                          //div[4]/app-custom-form[1]/fieldset[1]/div[2]/app-custom-field-generator/app-custom-textbox/label
                    foundName = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset/div[{1}]/app-custom-field-generator/app-custom-textbox/label", index, column), "innerHTML", safeWait: 100, maxRetry: 7).ToString();
                    index++;
                    if (!serach(Street, foundName))
                    {
                        isFound = false;
                        break;
                    }
                }
                catch
                {
                    break;
                }
            }

            Assert(isFound && index == count + 2, "Account search by column " + column + " failed (found name doesn't match expected)");


            SafeClick(webappDriver, "//div[@id='topBarContainer']/div/search/div/a[2]/span");
            Thread.Sleep(4000);
        }

        /// <summary>
        /// check column type Button
        /// </summary>
        /// <param name="webappDriver"></param>
        /// <param name="AccountId"></param>
        /// <param name="count"></param>
        public static void check_Field_Button_Search(RemoteWebDriver webappDriver, string AccountId, int count, int column)
        {

            // Click search button
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/a/span");



            // Input name
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/input");
            SafeSendKeys(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/input", AccountId);

            // Click search button
            SafeClick(webappDriver, "//app-generic-list/div/div/div/top-bar/div/div/search/div/a/span");
            Thread.Sleep(bufferTime);



            string foundName = "";
            int index = 1;
            bool isFound = true;

            // Check All rows is contain
            while (true)
            {
                try
                {
                    //div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/app-custom-field-generator[1]/app-custom-button[1]/a[1]/span
                    foundName = SafeGetValue(webappDriver, string.Format("//div[{0}]/app-custom-form[1]/fieldset[1]/div[{1}]/app-custom-field-generator[1]/app-custom-button[1]/a[1]/span", index,column), "innerHTML", safeWait: 100, maxRetry: 7).ToString();
                    index++;
                    if (!serach(AccountId, foundName))
                    {
                        isFound = false;
                        break;
                    }
                }
                catch
                {
                    break;
                }
            }

            Assert(isFound && index == count + 1, "Account search failed (found name doesn't match expected)");

          

            SafeClick(webappDriver, "//div[@id='topBarContainer']/div/search/div/a[2]/span");
            Thread.Sleep(4000);
        }

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


            // Enable new Account list
            SafeClick(varDriver, "//input[@id='chkEnableNewAccountList']");


            // Enable new Activities list
            SafeClick(varDriver, "//input[@id='chkEnableNewActivityList']");
            SafeClick(varDriver, "//div[@id='msgModalRightBtn']");


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


            // Select List
            int top = 0;
            try
            {
                while (true)
                {
                    if (nameList == SafeGetValue(backofficeDriver, "//md-tab-content[@class='_md ng-scope md-no-transition md-active md-no-scroll']//div[@style='top:" + top.ToString() + "px']/div[@class='slick-cell l0 r0']", "innerHTML"))
                    {
                        // Edit List
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


            // Edit Admin 
            backoffice_Edit_Admin(backofficeDriver, "Admin");


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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backofficeDriver"></param>
        public static void Edit_Rep_Permission(RemoteWebDriver backofficeDriver,string List,bool isAll=false)
        {
            // Permission
            SafeClick(backofficeDriver, "//md-tab-item[3][@class='md-tab ng-scope ng-isolate-scope ng-binding']");


            // Edit Admin
            backoffice_Edit_Admin(backofficeDriver, "Admin", "formContTemplate2");


            try
            {
                // Account Lists
                SafeClick(backofficeDriver, "//div[1]/div[1]/md-tabs[1]/md-tabs-content-wrapper[1]/md-tab-content[3]/div[1]/div[1]/div[3]/div[2]/ul[1]");
                SafeClick(backofficeDriver, "//div[1]/md-tabs[1]/md-tabs-content-wrapper[1]/md-tab-content[3]/div[1]/div[1]/div[3]/div[2]/ul[1]/div[1]/ul[1]/li[1]/div[1]/div[1]");
                if(isAll)
                SafeClick(backofficeDriver, "//div[1]/md-tabs[1]/md-tabs-content-wrapper[1]/md-tab-content[3]/div[1]/div[1]/div[3]/div[2]/ul[1]/div[1]/ul[1]/li[2]/div[1]/div[1]");
            }
            catch { }
            

            // Save
            SafeClick(backofficeDriver, "//div[1]/md-tabs[1]/md-tabs-content-wrapper[1]/md-tab-content[3]/div[1]/div[1]/div[4]/div[1]/div[1]");
        }


        /// <summary>
        /// Creat_New_List
        /// </summary>
        /// <param name="backofficeDriver"></param>
        /// <param name="nameNewList">new the new list</param>
        public static void Creat_New_List_Lists_Accounts(RemoteWebDriver backofficeDriver, string nameNewList)
        {
            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);

            // + Create New List
            SafeClick(backofficeDriver, "//div[@id='btnAddNewAcc']");


            //  Input New List Fildes
            // input name
            SafeSendKeys(backofficeDriver, "//div[@class='ListName parCont clearfix']/input[@name='name']", nameNewList);


            // input description
            SafeSendKeys(backofficeDriver, "//div[@class='ListDescription parCont clearfix']/input[@name='description']", "Automation " + nameNewList);


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


        public static void backoffice_Custom_Fields_Acccounts(RemoteWebDriver webappDriver, RemoteWebDriver backofficeDriver)
        {
            FullWebappAutomation.Backoffice.Accounts.Fields(backofficeDriver);

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
            TSA_Fields.Add("Attachment", "TSAAttachment");
            TSA_Fields.Add("Signature Drawing", "TSASignatureDrawing");
            TSA_Fields.Add("Phone number", "TSAPhonenumber");
            TSA_Fields.Add("Link", "TSALink");
            //TSA_Fields.Add("Email", "TSAEmail");




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

        }


        public static void Sreach_Available_Fields(RemoteWebDriver backofficeDriver, Dictionary<string, string> Fields,string nameSearch)
        {

            FullWebappAutomation.Backoffice.Accounts.Accounts_Lists_New(backofficeDriver);


            // Configuration
            SafeClick(backofficeDriver, "//md-pagination-wrapper/md-tab-item[2]");


            // Select List
            int top = 0;
            try
            {
                while (true)
                {
                    if (nameSearch == SafeGetValue(backofficeDriver, "//md-tab-content[@class='_md ng-scope md-no-scroll md-active']//div[@style='top:" + top.ToString() + "px']//div[@class='slick-cell l0 r0']", "innerHTML"))
                    {
                        SafeClick(backofficeDriver, "//md-tab-content[@class='_md ng-scope md-no-scroll md-active']//div[@style='top:" + top.ToString() + "px']//div[@title='Edit']");
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

            // Edit Admin 
            backoffice_Edit_Admin(backofficeDriver, "Admin", "formContTemplate1");


            foreach (var item in Fields)
            {
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).SendKeys(item.Key);
                SafeClick(backofficeDriver, string.Format("//li[@data-id='{0}']//div[@class='fr plusIcon plusIconDisable']", item.Value));
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).Clear();
                SafeClick(backofficeDriver, "//div[3]/div/div//span[@class='fa fa-search']");
                Thread.Sleep(1000);
            }


            // Save
            SafeClick(backofficeDriver, "//div[@class='footer-buttons']/div[@class='save allButtons grnbtn roundCorner  fl']");

            Thread.Sleep(2500);
        }


        public static void Sreach_Available_Fields_Per_List(RemoteWebDriver backofficeDriver, Dictionary<string, string> Fields, string nameSearch,string nameList)
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
                        SafeClick(backofficeDriver, "//div[@style='top:" + top.ToString() + "px']/div[@class='slick-cell l3 r3']/div[@title='Edit']");
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


            // Select List
            top = 0;
            try
            {
                while (true)
                {
                    if (nameSearch == SafeGetValue(backofficeDriver, "//md-tab-content[@class='_md ng-scope md-no-scroll md-no-transition-remove md-no-transition-remove-active md-active']//div[@style='top:" + top.ToString() + "px']//div[@class='slick-cell l0 r0']", "innerHTML"))
                    {
                        SafeClick(backofficeDriver, "//md-tab-content[@class='_md ng-scope md-no-scroll md-no-transition-remove md-no-transition-remove-active md-active']//div[@style='top:" + top.ToString() + "px']//div[@title='Edit']");
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

            // Edit Admin 
            backoffice_Edit_Admin(backofficeDriver, "Admin", "formContTemplate");


            foreach (var item in Fields)
            {
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).SendKeys(item.Key);
                SafeClick(backofficeDriver, string.Format("//li[@data-id='{0}']//div[@class='fr plusIcon plusIconDisable']", item.Value));
                backofficeDriver.FindElement(By.Id("txtSearchBankFields")).Clear();
                SafeClick(backofficeDriver, "//div[3]/div/div//span[@class='fa fa-search']");
                Thread.Sleep(1000);
            }


            // Save
            SafeClick(backofficeDriver, "//div[@class='footer-buttons']/div[@class='save allButtons grnbtn roundCorner  fl']");

            Thread.Sleep(2500);
        }


        public static void select_list_general(RemoteWebDriver webappDriver, string nameNewList)
        {
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
        }

    }
    #endregion
}

