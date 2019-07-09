
var target = Argument("target", "Default");

var baseDir = Directory("..");
var backendDir = MakeAbsolute(baseDir + Directory("src/backend")).ToString();
var packageDir = MakeAbsolute(baseDir + Directory("package")).ToString();

Task("Clean")
    .Does(() =>
    {
		Information("Cleaning directory: " + packageDir); 
        CleanDirectory(packageDir);
    });

Task("Compile")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var slnPath = backendDir + "/ToDoList.sln";
		Information("Buildig: " + slnPath); 

        DotNetCoreBuild(slnPath);
    });

Task("Testing")
    .IsDependentOn("Compile")
    .Does(()=>
    {
        var unitTestPath = backendDir + "/ToDoList.Core.UnitTests/ToDoList.Core.UnitTests.csproj";
		Information("Testing: " + unitTestPath); 

        DotNetCoreTest(unitTestPath);
    });

Task("Package")
    .IsDependentOn("Testing")
    .Does(()=>
    {
        var tempDir = MakeAbsolute(baseDir + Directory("temp/backend")).ToString();

        var webApiPath = backendDir + "/ToDoList.WebApi/ToDoList.WebApi.csproj";
        var settings = new DotNetCorePublishSettings
        {
            NoBuild = true,
            OutputDirectory = tempDir,
        };
		Information("Publish: " + webApiPath); 

        DotNetCorePublish(webApiPath, settings);
        EnsureDirectoryExists(packageDir);

		Information("Zipping: " + tempDir); 
        Zip(tempDir, packageDir + "/backend.zip" );

		Information("Cleaning: " + tempDir); 
        CleanDirectory(tempDir);
});

Task("CopyDeployScripts")
    .IsDependentOn("Package")
    .Does(()=>{

        CopyFiles("./deployScripts/*", packageDir);
		
		var packageToolsPath = packageDir + "/Tools";
		Information("Creating directory: " + packageToolsPath); 
		EnsureDirectoryExists(packageToolsPath);
		
		CopyFile("./tools/packages.config", packageToolsPath + "/packages.config");
    });


Task("Default")
    .IsDependentOn("CopyDeployScripts");

RunTarget(target);
