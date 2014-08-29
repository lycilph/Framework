# Create package
$file = Get-ChildItem -Include "*.csproj" -Recurse 
.\.nuget\nuget.exe pack $file -OutputDirectory .nuget