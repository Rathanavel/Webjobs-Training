using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Starting with ClientContext, the constructor requires a URL to the 
            // server running SharePoint. 
            //ClientContext context = new ClientContext("https://vinitonline.sharepoint.com/sites/rathanavel");

            //List oList = context.Web.Lists.GetByTitle("Issues");
            //context.Load(oList);
            //context.ExecuteQuery();

            //foreach (var member in oList.GetItems(CamlQuery.CreateAllItemsQuery()))
            //{
            //    Console.WriteLine(member.Id.ToString());
            //}

            //Console.ReadLine();

            string siteURL = ConfigurationManager.AppSettings["siteURL"];
            string userName = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];

            #region ConnectTo O365  
            //Create the client context object and set the credentials  
            ClientContext clientContext = new ClientContext(siteURL);
            SecureString securePassword = new SecureString();
            foreach (char c in password.ToCharArray()) securePassword.AppendChar(c);
            clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);

            #endregion
            //Load the web  
            Web oWeb = clientContext.Web;
            clientContext.Load(oWeb.Lists);
            clientContext.ExecuteQuery();
            foreach (var member in oWeb.Lists)
            {
                Console.WriteLine(member.Title.ToString());
            }

            List oList = oWeb.Lists.GetByTitle("Issues");
            ListItemCreationInformation oItemCreatInfo = new ListItemCreationInformation();
            ListItem oItem = oList.AddItem(oItemCreatInfo);

            oItem["Title"] = DateTime.Now.ToString();
            oItem.Update();

            clientContext.Load(oList);
            clientContext.ExecuteQuery();

            Console.WriteLine("Item created ID: " + oItem.Id.ToString());
            Console.ReadLine();
        }
    }
}
