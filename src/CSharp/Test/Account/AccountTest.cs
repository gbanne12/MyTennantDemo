using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTennantDemo.CSharp.EasyRepro.Extensions.Actions.Request;

namespace MyTennantDemo.CSharp.Test.Account;

[TestClass]
public class AccountTest : ModelDrivenAppTest
{

    [TestMethod]
    public void CreateAccountWithEasyRepro()
    {
        var xrmApp = StartApplication();

        const string appName = "GB App";
        xrmApp.Navigation.OpenApp(appName);

        xrmApp.Navigation.OpenSubArea("Sales", "Accounts");

        xrmApp.CommandBar.ClickCommand("New");
        
        xrmApp.CommandBar.ClickCommand("New");

        xrmApp.Entity.SetValue("name", "Garbagio time");

        xrmApp.Entity.Save();
    }
    
    
    [TestMethod]
    public void CreateAccountWithPostRequest()
    {
        StartApplication();
        var driver = Client.Browser.Driver;
        var request = new HttpRequest(driver);
        request.PostData(Config.OnlineCrmUrl,"accounts", "account.json");
    }
    
    [TestCleanup]
    public void Teardown()
    {
        if (App != null)
        {
            App.Dispose();
        }
    }
    

}