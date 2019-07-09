#addin nuget:?package=Cake.Curl&version=4.1.0
#addin nuget:?package=Cake.FileHelpers&version=3.2.0
#addin nuget:?package=Cake.Json&version=3.0.1
#addin nuget:?package=Newtonsoft.Json&version=11.0.2

var target = Argument("target", "Default");
var connectionString = Argument("connectionString");
var deployUsername = Argument("deployUsername");
var deployPassword = Argument("deployPassword");
var appName = Argument("appName");

var baseDir = Directory(".");

Task("PrepareBackendPackage")
    .Does(()=>
    {
        var tempDir = MakeAbsolute(baseDir + Directory("temp")).ToString();
		
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
        Zip(tempDir, packageDir + "/backend_with_params.zip" );
        CleanDirectory(tempDir);
    });

Task("DeployBackend")
    .IsDependentOn("PrepareBackendPackage")
    .Does(()=>
    {
        var url = $"https://{appName}.scm.azurewebsites.net/api/zipdeploy";
        Information("Deploy to: " +  url); 

        CurlUploadFile(
            packageDir + "/backend_with_params.zip",
            new Uri(url),
            new CurlSettings
            {
                RequestCommand = "POST",
                Username =  deployUsername, //"$da-back-program",
                Password = deployPassword, //"BLupqzPJJkxnxB1Bd9tsMFF62zruuZ7a3jHXffX1wAgQpGnowplgRHggrYXJ",
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
