using Microsoft.Dynamics365.UIAutomation.Browser;
using MyTennantDemo.CSharp.Configuration;

namespace MyTennantDemo.CSharp.Test;

/// <summary>
/// Creates the browser options from the config file.
/// The BrowserOptions generated here should be passed to the EasyRepro WebClient class
/// </summary>
public class TestSettings
{
    public BrowserOptions BrowserOptions { get; }

    public TestSettings()
    {
        var config = ConfigBuilder.Build();
        if (config.BrowserType == "Chrome")
        {
            Console.WriteLine("Detected Chrome from settings file. Settings up Chrome options.");
            BrowserOptions = new ChromeSettings()
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = config.UsePrivateMode,
                UCITestMode = config.UciTestMode,
                PageLoadTimeout = TimeSpan.FromSeconds(config.PageLoadTimeoutInSeconds),
                CommandTimeout = TimeSpan.FromSeconds(config.CommandTimeoutInSeconds),
                
                UseChromeProfile = config.UseChromeProfile,
                UseChromeProfileDir = config.ChromeProfileDirectory ?? string.Empty,
            };
        }
        else
        {
            throw new NotImplementedException("Only Chrome can be used as browser type at present");
        }
        
    }

    

}