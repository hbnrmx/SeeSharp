### Create 'Send to > SeeSharp' shortcut

$buildPath = 'C:\SeeSharp\win10\x64'

Write-Host "================= BUILDING APP =================" -ForegroundColor Green

cd ../SeeSharp.Desktop
dotnet publish -o $buildPath -c RELEASE -f netcoreapp3.0 -r win10-x64 /p:PublishSingleFile=true /p:IncludeSymbolsInSingleFile=true
cd ../scripts

Write-Host "================== ALL DONE! ===================" -ForegroundColor Green