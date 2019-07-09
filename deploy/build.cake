#addin nuget:?package=Cake.Curl
#addin nuget:?package=Cake.FileHelpers
#addin nuget:?package=Cake.Json
#addin nuget:?package=Newtonsoft.Json&version=11.0.2


//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var connectionString = Argument("connectionString");
var deployUsername = Argument("deployUsername");
var deployPassword = Argument("deployPassword");
var appName = Argument("appName");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var baseDir = Directory("..");
var backendDir = MakeAbsolute(baseDir + Directory("src/backend")).ToString();
var packageDir = MakeAbsolute(baseDir + Directory("package")).ToString();

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(packageDir);
});

Task("Compile")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var slnPath = backendDir + "/ToDoList.sln";
    DotNetCoreBuild(slnPath );
});

Task("Testing")
.IsDependentOn("Compile")
.Does(()=>{
    var unitTestPath = backendDir + "/ToDoList.Core.UnitTests/ToDoList.Core.UnitTests.csproj";
    DotNetCoreTest(unitTestPath);
});

Task("Package")
.IsDependentOn("Testing")
.Does(()=>{
    
var tempDir = MakeAbsolute(baseDir + Directory("temp/backend")).ToString();

    var webApiPath = backendDir + "/ToDoList.WebApi/ToDoList.WebApi.csproj";
    var settings = new DotNetCorePublishSettings() {
        NoBuild= true,
        OutputDirectory = tempDir,
    };

    DotNetCorePublish(webApiPath, settings);
    EnsureDirectoryExists(packageDir);
    Zip(tempDir, packageDir + "/backend.zip" );
    CleanDirectory(tempDir);
});

Task("PrepareBackendPackage")
.Does(()=>{
var deployTempDir = MakeAbsolute(baseDir + Directory("temp/depeloy/back")).ToString();
    Unzip(packageDir + "/backend.zip", deployTempDir);
    var settingsPath = deployTempDir + "/appsettings.json";
    var settingsString = FileReadText(settingsPath);
    var settings = ParseJson(settingsString);
    settings["ConnectionStrings"]["ToDoListDatabase"] = connectionString;
    settingsString = settings.ToString();
    FileWriteText(settingsPath,settingsString);
    Zip(deployTempDir, packageDir + "/backend_with_params.zip" );
    CleanDirectory(deployTempDir);
});

Task("DeployBackend")
.IsDependentOn("PrepareBackendPackage")
.Does(()=>{
    CurlUploadFile(
        packageDir + "/backend_with_params.zip",
        new Uri($"https://{appName}.scm.azurewebsites.net/api/zipdeploy"),
        new CurlSettings{
            RequestCommand = "POST",
            Username =  deployUsername, //"$da-back-program",
            Password = deployPassword, //"BLupqzPJJkxnxB1Bd9tsMFF62zruuZ7a3jHXffX1wAgQpGnowplgRHggrYXJ",
            ArgumentCustomization = args => {
             return  args.Append("--fail");
            }
                
        }
    );
});
Task("Default")
.IsDependentOn("DeployBackend");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
