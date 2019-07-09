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
    //CleanDirectory(buildDir);
});

Task("Compile")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var slnPath = backendDir + "/ToDoList.sln";
    DotNetCoreBuild(slnPath );
});

Task("Testing")
.Does(()=>{
    var unitTestPath = backendDir + "/ToDoList.Core.UnitTests/ToDoList.Core.UnitTests.csproj";
    DotNetCoreTest(unitTestPath);
});

Task("Publish")
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
.IsDependentOn("Publish");
    //.IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
