using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;

namespace MyTennantDemo.CSharp.EasyRepro.Extensions.Actions.Request;
/// <summary>
///  Make use of the currently running driver to make http requests to dataverse. 
///  Assumes that the driver is already authenticated
/// </summary>
public class HttpRequest
{
    private readonly IWebDriver _driver;
    
    public HttpRequest(IWebDriver driver)
    {
        _driver = driver;
    }
    
    /// <summary>
    ///  Submit post request to Dataverse entity
    /// </summary>
    /// <param name="crmUrl"> the base environment url</param>
    /// <param name="entityName">the entity/table to update</param>
    /// <param name="filename">the name of the json file (including extension) in the src/Data folder to be submitted</param>
    public void PostData(Uri crmUrl, string entityName, string filename)
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\.."));
        
        var javascriptDirectory = projectRoot + "\\src\\Javascript\\Scripts\\";
        const string postRequestScript = "PostRequest.js";
        var script = File.ReadAllText(javascriptDirectory + postRequestScript);
        
        
        var dataDirectory = projectRoot + "\\src\\Data\\";
        var jsonData = File.ReadAllText(dataDirectory + filename);
        jsonData = jsonData.Trim('\'');
        
        // Replace placeholders with actual values
        var webApiPath = crmUrl.OriginalString + "api/data/v9.2/";
        script = script.Replace("##CRM_URL##", webApiPath );
        script = script.Replace("##ENTITY_NAME##", entityName);
        script = script.Replace("{\"##KEY\": \"VALUE##\"}", jsonData);
        
        var result = _driver.ExecuteScript(script);
        Console.WriteLine("Post request sent. Script result:" + result);
    }
}