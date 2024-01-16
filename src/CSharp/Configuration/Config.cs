using System.Security;
using Microsoft.Playwright;

namespace MyTennantDemo.CSharp.Configuration;

public record Config(
    Uri OnlineCrmUrl, 
    SecureString OnlineUsername, 
    SecureString OnlinePassword, 
    SecureString MfaSecretKey,
    string BrowserType,
    bool UseChromeProfile,
    string? ChromeProfileDirectory,
    bool UsePrivateMode,
    bool UciTestMode,
    int PageLoadTimeoutInSeconds,
    int CommandTimeoutInSeconds);
    
  