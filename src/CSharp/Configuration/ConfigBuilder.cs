using System.Text.Json;
using Microsoft.Dynamics365.UIAutomation.Browser;

namespace MyTennantDemo.CSharp.Configuration;

public static class ConfigBuilder
{
    public static Config Build()
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\.."));
        var pathToFile = Path.Combine(projectRoot, "testsettings.json");

        var jsonContent = File.ReadAllText(pathToFile);
        var jsonDocument = JsonDocument.Parse(jsonContent);
        var root = jsonDocument.RootElement;

        // AppSettings
        var appSettings = root.GetProperty("AppSettings");
        var onlineCrmUrl = TryGetStringProperty(appSettings, "OnlineCrmUrl");
        var onlineUsername = TryGetStringProperty(appSettings, "OnlineUsername");
        var onlinePassword = TryGetStringProperty(appSettings, "OnlinePassword");
        var mfaSecretKey = TryGetStringProperty(appSettings, "MfaSecretKey");

        if (onlineCrmUrl == null)
        {
            throw new ArgumentNullException(nameof(onlineCrmUrl), "CRM URL required in appsettings.json");
        }

        if (onlineUsername == null)
        {
            throw new ArgumentNullException(nameof(onlineUsername), "CRM Username required in appsettings.json");
        }

        if (onlinePassword == null)
        {
            throw new ArgumentNullException(nameof(onlinePassword), "CRM password required in appsettings.json");
        }


        // BrowserSettings
        var browserSettings = root.GetProperty("BrowserSettings");
        var browserType = TryGetStringProperty(browserSettings, "BrowserType");
        var useChromeProfile = TryGetBooleanProperty(browserSettings, "UseChromeProfile");
        var chromeProfileDirectory = TryGetStringProperty(browserSettings, "ChromeProfileDirectory");
        var usePrivateMode = TryGetBooleanProperty(browserSettings, "UsePrivateMode");
        var uciTestMode = TryGetBooleanProperty(browserSettings, "UCITestMode");
        var pageLoadTimeoutInSeconds = TryGetIntProperty(browserSettings, "PageLoadTimeoutInSeconds");
        var commandTimeoutInSeconds = TryGetIntProperty(browserSettings, "CommandTimeoutInSeconds");

        if (browserType == null)
        {
            throw new ArgumentNullException(nameof(browserType), "Browser type required in appsettings.json");
        }

        return new Config(
            new Uri(onlineCrmUrl),
            onlineUsername.ToSecureString(),
            onlinePassword.ToSecureString(),
            mfaSecretKey.ToSecureString(),
            browserType,
            useChromeProfile,
            chromeProfileDirectory,
            usePrivateMode,
            uciTestMode,
            pageLoadTimeoutInSeconds,
            commandTimeoutInSeconds);
    }

    private static bool TryGetBooleanProperty(JsonElement parentElement, string propertyName)
    {
        if (parentElement.TryGetProperty(propertyName, out JsonElement propertyElement))
        {
            return propertyElement.ValueKind == JsonValueKind.True;
        }

        return false; // Default value if the property is missing
    }

    private static string? TryGetStringProperty(JsonElement parentElement, string propertyName)
    {
        if (parentElement.TryGetProperty(propertyName, out JsonElement propertyElement) &&
            propertyElement.ValueKind == JsonValueKind.String)
        {
            return propertyElement.GetString();
        }

        return null; // Default value if the property is missing or not a string
    }

    private static int TryGetIntProperty(JsonElement parentElement, string propertyName)
    {
        if (parentElement.TryGetProperty(propertyName, out JsonElement propertyElement) &&
            propertyElement.ValueKind == JsonValueKind.Number)
        {
            return propertyElement.GetInt32();
        }

        return 0; // Default value if the property is missing
    }
}