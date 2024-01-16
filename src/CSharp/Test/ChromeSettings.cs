using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium.Chrome;

namespace MyTennantDemo.CSharp.Test;

/// <summary>
///  Add additional arguments to the chrome driver that are not available via the EasyRepro BrowserOptions class.
/// </summary>
public class ChromeSettings : BrowserOptions
{
    public bool UseChromeProfile { get; set; } = true;
    public string UseChromeProfileDir { get; set; } = Path.Combine(Directory.GetCurrentDirectory() + "\\profileDir\\");

    public override ChromeOptions ToChrome()
    {
        var chromeOptions = base.ToChrome();

        if (UseChromeProfile)
        {
            chromeOptions.AddArguments("--user-data-dir=" + UseChromeProfileDir);
        }

        chromeOptions.AddUserProfilePreference("profile.cookie_controls_mode", this.CookieСontrolsMode);
        return chromeOptions;
    }
}