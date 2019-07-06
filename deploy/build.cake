//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var backendDir = MakeAbsolute(Directory("../src/backend")).ToString();
var frontendDir =MakeAbsolute(Directory("../src/frontend")).ToString();

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
    DotNetCoreBuild(slnPath);
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
.IsDependentOn("Compile");
    //.IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
