using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FullWebappAutomation.TestOnChrome;

namespace FullWebappAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DanZlotnikov : Window
    {
        public static string[] usernames; // Stores the available usernames
        public static string[] tests; // Stores the available tests

        public string chosenUsername;

        Dictionary<string, bool> testsToRun; // <testName, bool> format to store which tests were chosen to run
        Dictionary<string, bool> allTestsToRun; // <testName, bool> format to store all tests with "true"
        public static int marginTop = 15;
        public static int marginLeft = 23;

        public RadioButton[] usersRadioButtons;

        private void InitVariables()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        

            usernames = new string[] { "qatests@pepperitest.com", "daniel0@pepperitest.com" };
            tests = new string[]
            {
                "Resync",
                "Config Home Button",
                "Config App Buttons",
                "Sales Order",
                "Item Search",
                "Minimum Quantity",
                "Delete Cart Item",
                "Unit Price Discount",
                "Continue Ordering",
                "Duplicate Line Item",
                "Inventory Alert",
                "Search Activity",
                "Delete Activity",
                "Account Search Activity",
                "Account Drill Down",
                "Enter To Activity",
                "Account Activity Drilldown",
                "Breadcrumbs Navigation",
                "Duplicate Transaction",
                "Search Account",
                "Order By",
                "Image Home",
                "Creat Bayer",
                "Change title home screen",
                "Change Title All Home",
                "Menu of Home screen",
                "Online action",
                "Branding color Main",
                "Branding color Secondary",
                "Branding image logo",
                "New_Basic_List_Account_Table",
                "New_TSA_List_Account_Table"
            };

            testsToRun = new Dictionary<string, bool>();
            for (int i = 0; i < tests.Length; i++)
            {
                testsToRun[tests[i]] = false;
            }

            allTestsToRun = new Dictionary<string, bool>();
            for (int i = 0; i < tests.Length; i++)
            {
                allTestsToRun[tests[i]] = true;
            }

            usersRadioButtons = new RadioButton[usernames.Length];
        }

        private void InitRadioStackPanel()
        {
            // Create stack panel
            StackPanel usersStackPanel = new StackPanel()
            {
                Name = "UsersStackPanel"
            };

            MainGrid.Children.Add(usersStackPanel);

            // Create radiobuttons according to usernames array
            for (int i = 0; i < usernames.Length; i++)
            {
                usersRadioButtons[i] = new RadioButton
                {
                    Name = "Button" + i.ToString(),
                    Content = usernames[i],
                    GroupName = "userRadioButtons",
                    HorizontalAlignment = new HorizontalAlignment(),
                    Margin = i == 0 ? new Thickness(marginLeft, marginTop * 3, 0, 0) : new Thickness(marginLeft, marginTop, 0, 0),
                    VerticalAlignment = new VerticalAlignment()
                };
                usersRadioButtons[i].Checked += RadioButton_Checked;
            }

            foreach (RadioButton radiobutton in usersRadioButtons)
            {
                usersStackPanel.Children.Add(radiobutton);
            }
        }

        private void InitTextBlocks()
        {
            TextBlock usersTextBlock = new TextBlock
            {
                HorizontalAlignment = new HorizontalAlignment(),
                Height = 28,
                Margin = new Thickness(marginLeft, marginTop, 0, 0),
                TextWrapping = new TextWrapping(),
                VerticalAlignment = new VerticalAlignment(),
                Width = 339,
                Text = "Select test user:",
                FontWeight = FontWeights.Bold
            };

            TextBlock testsTextBlock = new TextBlock
            {
                HorizontalAlignment = new HorizontalAlignment(),
                Height = 28,
                Margin = new Thickness(marginLeft, ((marginTop * 7) + (marginTop * usersRadioButtons.Length + 1)), 0, 0),
                TextWrapping = new TextWrapping(),
                VerticalAlignment = new VerticalAlignment(),
                Width = 339,
                Text = "Select tests to run:",
                FontWeight = FontWeights.Bold
            };

            MainGrid.Children.Add(usersTextBlock);
            MainGrid.Children.Add(testsTextBlock);
        }

        private void InitCheckboxes()
        {
            CheckBox[] testsCheckboxes = new CheckBox[tests.Length];

            int checkboxMarginLeft, checkboxMarginTop;

            // Create checkboxes according to tests array
            for (int i = 0; i < tests.Length; i++)
            {
                checkboxMarginLeft = marginLeft + (marginLeft * 7) * (i / 8);
                checkboxMarginTop = (marginTop * 12) + marginTop * (i % 8);
                testsCheckboxes[i] = new CheckBox
                {
                    Content = tests[i],
                    Margin = new Thickness(checkboxMarginLeft, checkboxMarginTop, 0, 0)

                };
                testsCheckboxes[i].Checked += Checkbox_Changed;
                testsCheckboxes[i].Unchecked += Checkbox_Changed;
            }

            foreach (CheckBox checkBox in testsCheckboxes)
                MainGrid.Children.Add(checkBox);
        }

        private void InitButtons()
        {
            Button runButton = new Button
            {
                Content = "Run Selected Tests",
                Width = 156,
                Margin = new Thickness(-280, marginTop * 20, 0, 0),
                Height = 28,
            };
            runButton.Click += RunButtonClicked;

            Button runAllButton = new Button
            {
                Content = "Run All Tests",
                Width = 156,
                Margin = new Thickness(40, marginTop * 20, 0, 0),
                Height = 28,
            };
            runAllButton.Click += RunAllButtonClicked;

            Button exitButton = new Button
            {
                Content = "Exit",
                Width = 156,
                Margin = new Thickness(-120, marginTop * 24, 0, 0),
                Height = 28,
            };
            exitButton.Click += ExitButtonClicked;

            MainGrid.Children.Add(exitButton);        
            MainGrid.Children.Add(runButton);
            MainGrid.Children.Add(runAllButton);
        }
    
        private void InitGrid()
        {
            InitTextBlocks();
            InitRadioStackPanel();
            InitCheckboxes();
            InitButtons();
        }

        private void RunButtonClicked(object sender, RoutedEventArgs e)
        {
            if (chosenUsername == null)
                return;
            else RunTests(chosenUsername, testsToRun);
        }

        private void RunAllButtonClicked(object sender, RoutedEventArgs e)
        {
            RunTests(chosenUsername, allTestsToRun);
        }

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Check which radiobutton was chosen
            for (int i = 0; i < usersRadioButtons.Length; i++)
            {
                if (usersRadioButtons[i].IsChecked == true)
                    chosenUsername = usersRadioButtons[i].Content.ToString();
            }

        }

        private void Checkbox_Changed(object sender, RoutedEventArgs e)
        {
            // Get the sender of the action (Check/Uncheck)
            CheckBox obj = sender as CheckBox;
            string content = obj.Content.ToString();

            // Change testsToRun accordingly
            testsToRun[content] = (bool)obj.IsChecked;

        }

        public DanZlotnikov()
        {
            InitializeComponent();
            InitVariables();
            InitGrid();
        }
    }
}
