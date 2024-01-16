# Overview
Example for using the [features/dotnet/core branch](https://github.com/microsoft/EasyRepro/tree/features/dotnet/core) of EasyRepro to create tests for Microsoft Dynamics/Power Apps. 

# Steps
1. Checkout the features/dotnet/core branch of easy repro
2. Checkout this project to be part of the same solution
3. Add the configuration details for the application in testsettings.json of this project root
4. Create a new test by extending ModelDrivenAppTest
5. Call ModelDrivenAppTest.StartApplication() to launch the browser and sign in to the application
6. Use the EasyRepro classes to perform any actions you then need.


# testSettings.json
- "OnlineUsername":  The username to use for signing in to the model driven app
- "OnlinePassword": The matching password for the above username
-    "OnlineCrmUrl": The URL for the model driven app with i.e. "https://org9e533c5d.crm4.dynamics.com/",
-    "MfaSecretKey": The client secret for the above username multi factor authentication.    
-    "BrowserType": The browser driver to use.  Only Chrome is demonstrated here.
-    "UseChromeProfile": Whether to use a chrome profile when launching the browser. This will avoid need to repeatedly authenticate.
-    "ChromeProfileDirectory": The path to the chrome profile directorty
-    "UsePrivateMode": Whether to use incognito/private mode.  If you want to force re-authentication
-    "UCITestMode": Whether to enable EasyRepro's UCI test mode flag
-    "PageLoadTimeoutInSeconds": The time in seconds Easy repro will wait for on page load before throwing error.  
-    "CommandTimeoutInSeconds": The time in seconds Easy repro will wait for when attempting commands before throwing error. 

Note: Easy Repro also has a default timeout value of 30s that is used in various places of the code base that is not available to easily override. 
