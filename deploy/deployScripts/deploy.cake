#addin nuget:?package=Cake.Curl&version=4.1.0
#addin nuget:?package=Cake.FileHelpers&version=3.2.0
#addin nuget:?package=Cake.Json&version=3.0.1
#addin nuget:?package=Newtonsoft.Json&version=11.0.2

var target = Argument("target", "Default");
var connectionString = Argument("connectionString", "");
var deployUsername = Argument("deployUsername", "");
var deployPassword = Argument("deployPassword", "");
var appName = Argument("appName" , "x");

var baseDir = MakeAbsolute(Directory("."));

Task("PrepareBackendPackage")
    .Does(()=>
    {
        var tempDir = baseDir + "/temp";
		
        Information("Unzip: backend.zip"); 
        Unzip("backend.zip", tempDir);

        Information("Updating connectionString"); 
        var settingsPath = tempDir + "/appsettings.json";
        var settingsString = FileReadText(settingsPath);
        var settings = ParseJson(settingsString);
        settings["ConnectionStrings"]["ToDoListDatabase"] = connectionString;
        settingsString = settings.ToString();

        Information("Saving new config"); 
        FileWriteText(settingsPath,settingsString);
        
        Information("Saving new config"); 
        Zip(tempDir, baseDir + "/backend_with_params.zip" );
        CleanDirectory(tempDir);
    });

Task("DeployBackend")
    .IsDependentOn("PrepareBackendPackage")
    .Does(()=>
    {
        var url = $"https://{appName}.scm.azurewebsites.net/api/zipdeploy";
        Information("appName to: " + appName ); 
        Information("url to: " +  url); 
        Information("deployPassword to: " +  deployPassword); 
        Information("deployUsername to: " +  deployUsername); 

        CurlUploadFile(
            baseDir + "/backend_with_params.zip",
            new Uri(url),
            new CurlSettings
            {
                RequestCommand = "POST",
                Username =  deployUsername, //"",
                Password = deployPassword   , //"",
                ArgumentCustomization = args => 
                {
                    return  args.Append("--fail");
                }
            }
        );
    });
Task("Default")
    .IsDependentOn("DeployBackend");

RunTarget(target);
