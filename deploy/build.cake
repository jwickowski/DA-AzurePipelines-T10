#addin nuget:?package=Cake.Curl
#addin nuget:?package=Cake.FileHelpers
#addin nuget:?package=Cake.Json
#addin nuget:?package=Newtonsoft.Json&version=11.0.2

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var baseDir = Directory("..");
var backendDir = MakeAbsolute(baseDir + Directory("src/backend")).ToString();
var frontendDir = MakeAbsolute(baseDir + Directory("src/frontend")).ToString();
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
//.IsDependentOn("Testing")
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

Task("Deploy")
.Does(()=>{
var deployTempDir = MakeAbsolute(baseDir + Directory("temp/depeloy/back")).ToString();

    //Unzip(packageDir + "/backend.zip", deployTempDir);
    var settingsPath = deployTempDir + "/appsettings.json";
    var settingsString = FileReadText(settingsPath);
    //Information(settingsString);
var settings = ParseJson(settingsString);
settings["ConnectionStrings"]["ToDoListDatabase"] = "Server=tcp:da-sqlserver-program.database.windows.net,1433;Initial Catalog=da-database-program;Persist Security Info=False;User ID=da-sqlserver-program;Password=jkdfhafSADSAA123123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    settingsString = settings.ToString();
    FileWriteText( settingsPath,settingsString);
    Zip(deployTempDir, packageDir + "/backend_with_params.zip" );

    CleanDirectory(deployTempDir);
});

// Task("Build")
//     .IsDependentOn("Restore-NuGet-Packages")
//     .Does(() =>
// {
//     if(IsRunningOnWindows())
//     {
//       // Use MSBuild
//       MSBuild("./src/Example.sln", settings =>
//         settings.SetConfiguration(configuration));
//     }
//     else
//     {
//       // Use XBuild
//       XBuild("./src/Example.sln", settings =>
//         settings.SetConfiguration(configuration));
//     }
// });

// Task("Run-Unit-Tests")
//     .IsDependentOn("Build")
//     .Does(() =>
// {
//     NUnit3("./src/**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
//         NoResults = true
//         });
// });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
.IsDependentOn("Deploy");
    //.IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
