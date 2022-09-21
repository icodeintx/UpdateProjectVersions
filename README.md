# Update Project Versions Numbers
Auto Update Version Numbers in Visual Studio csproj files

Having multiple solutions containing multiple projects can be cumbersome when managing version numbers.  For my project I have addopted the Ubuntu style of versioning with my own flavor that works for me.  The problem is updating the version numbers on successful build.  Some projects build NuGet pacakges and need new version numbers.  Visual Studio doesn't have an automatic version incrementor and if it did it proabably would be for some version format that didn't work for me.

Now I have this tiny project that will auto increment my versions for me when I build in Visual Studio.  The app is a console app so it can easily be utilized in scripts as well.  Plus if not using Visual Studio and don't like my version style, modify it for your own needs.

> The version style I choose is:  Year.Month.Day.Count

![image](https://user-images.githubusercontent.com/55411261/191400494-b33baf7d-b94f-45be-a0cf-7b70ad2b60fb.png)

For this to work I created an executable from this project and updated my Visual Studio project properties to run a post-build event.  The executable expects the project PathName to be passed as a parameter 

> Update this project property (build-event) Pre or Post.

![image](https://user-images.githubusercontent.com/55411261/191400632-bde708b7-0e91-4ec1-a13c-5db7d5f95ecf.png)

Paste this code (update your folder location to UpdateProjectVersions

```
call C:\src\Production\UpdateProjectVersions\UpdateProjectVersions $(ProjectDir)$(ProjectName).csproj
```

