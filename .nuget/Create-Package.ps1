# $msbuild = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe"
# & $msbuild .\DesktopOrganizer.sln /p:Configuration=Release /m

# Create package
$file = Get-Ch ildItem -Include "*.csproj" -Recurse 
.\.nuget\nug et.exe pack $file -OutputDirectory .nuget -Prop Configuration=Release