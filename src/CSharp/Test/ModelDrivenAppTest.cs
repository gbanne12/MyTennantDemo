using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using MyTennantDemo.CSharp.Configuration;

namespace MyTennantDemo.CSharp.Test;

/// <summary>
///  Tests which target the CRM model driven app should extend this class.
/// </summary>
public class ModelDrivenAppTest
{
    protected static WebClient Client { get; set; } = null!;
    protected static XrmApp App { get; set; } = null!;

    protected static readonly Config Config = ConfigBuilder.Build();


    /// <summary>
    /// Handles setup of the driver and authenticates the user through the UI.
    /// </summary>
    /// <returns>XRMApp for performing the test actions</returns>
    protected static XrmApp StartApplication()
    {
        var testSettings = new TestSettings();
        Client = new WebClient(testSettings.BrowserOptions);
        App = new XrmApp(Client);
        
        Console.WriteLine("Logging the user in to the application");
        App.OnlineLogin.Login(Config.OnlineCrmUrl, Config.OnlineUsername, Config.OnlinePassword, Config.MfaSecretKey);
        Console.WriteLine("Log-in completed successfully.");
        
        return App;
    }
}