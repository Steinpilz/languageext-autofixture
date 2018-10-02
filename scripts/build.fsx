// include Fake lib
#r @"../packages/FAKE/tools/FakeLib.dll"
#r @"../packages/Steinpilz.DevFlow.Fake/tools/Steinpilz.DevFlow.Fake.Lib.dll"

open Fake
open Steinpilz.DevFlow.Fake 

let libParams = Lib.setup <| fun p -> 
    { p with 
        PublishProjects = !!"src/app/**/*.csproj"
        UseDotNetCliToTest = true
        UseDotNetCliToPack = true
        NuGetFeed = 
            { p.NuGetFeed with 
                ApiKey = environVarOrFail <| "NUGET_API_KEY" |> Some
            }
    }

// dotnet pack caches generated AssemblyInfo from MSBuild properties
// so it could be set old AssemblyVersion 
Target "Clean-Obj-Folders" <| fun _ ->
    !!"src/app/**/obj"
        |> FileHelper.CleanDirs
    

"Clean"
    ==> "Clean-Obj-Folders"
    ==> "Restore"
    ==> "Pack"

RunTargetOrDefault "Watch"