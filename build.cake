///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var artifactsDirectory = MakeAbsolute(Directory("./artifacts"));

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
.IsDependentOn("Push-Nuget-Package");

Task("Build")
.Does(() => 
{
    foreach (var project in GetFiles("./src/**/*.csproj"))
    {
        Information(" Executando Build ");

        DotNetCoreBuild(
            project.GetDirectory().FullPath,
            new DotNetCoreBuildSettings(){
                Configuration = configuration
            }
        );
    }
});

Task("Test")
.IsDependentOn("Build")
.Does(()=> {
    Information(" Executando Test ");
    foreach (var project in GetFiles("./tests/**/*.csproj")){
        DotNetCoreTest(
            project.GetDirectory().FullPath,
            new DotNetCoreTestSettings(){
                Configuration = configuration
            }
        );
    }
});

Task("Create-Nuget-Package")
.IsDependentOn("Test")
.Does(() => {
    foreach (var project in GetFiles("./src/**/*.csproj"))
    {
        DotNetCorePack(
            project.GetDirectory().FullPath,
            new DotNetCorePackSettings(){
                Configuration = configuration,
                OutputDirectory = artifactsDirectory
            }
        );
    }
});

Task("Push-Nuget-Package")
.IsDependentOn("Create-Nuget-Package")
.Does(()=>{
    var apiKey = EnvironmentVariable("apiKey") ?? "oy2kjzju4ws76i2nzgyqwumdx4hty2wupocae3xaomezla";
    Information($"ApiKey {apiKey}");
    foreach (var package in GetFiles($"{artifactsDirectory}/*.nupkg"))
    {
        NuGetPush(package,
        new NuGetPushSettings {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = apiKey
        });
    }
});

RunTarget(target);