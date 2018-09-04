using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullWebappAutomation
{
    class Consts
    {
        // Webapp url data:
        public const string webappSandboxLoginPageUrl = "https://app.sandbox.pepperi.com/#/";
        public const string webappSandboxHomePageUrl = "https://app.sandbox.pepperi.com/#/HomePage";

        // Backoffice url data:
        public const string backofficeSandboxLoginPageUrl = "https://sandbox.pepperi.com/Login/WrntyLogin.aspx";
        public const string backofficeSandboxHomePageUrl = "https://sandbox.pepperi.com/Views/Dashboards/OrdersDashboard.aspx?cls=1&lang=en";
        public const string backofficeSandboxHomePageUrlFreeTrial = "https://sandbox.pepperi.com/views/payments/Registration.aspx?culture=en-us&country=il&test=1&Attr=&new_searchword=null&new_searchresulttype=null&buy=false&came=3&pillarId";

        public const int actionPerformanceLimit = 3;
        public const int bufferTime = 3000;

        public enum ObjectType
        {
            transactions,
            items,
            transaction_lines,
            account_sellout,
            users,
            contacts,
            accounts,
            item_dimensions1,
            price_lists,
            item_dimensions2,
            item_prices,
            catalogs,
            account_catalogs,
            NULL,
            types,
            activities,
            type_safe_attribute,
            roles,
            account_users,
            images,
            attachments,
            profiles,
            inventory,
            account_inventory,
            user_defined_tables,
            special_price_lists
        }
    }
}
