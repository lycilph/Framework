# Create the nupkg
$file = Get-ChildItem -Path .\.nuget\* -Include "*.nupkg"
.\.nuget\nuget.exe push $file