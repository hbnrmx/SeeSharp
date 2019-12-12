$buildPath = 'C:\SeeSharp\win10\x64'
$documentsPath = [Environment]::GetFolderPath("MyDocuments")
$pagesPath = Join-Path $documentsPath -ChildPath SeeSharp | Join-Path -ChildPath pages
Write-Host $pagesPath -ForegroundColor Green


#exit
### build App
Write-Host "================= BUILDING APP =================" -ForegroundColor Green

cd SeeSharp.Desktop
dotnet publish -o $buildPath -c RELEASE -f netcoreapp3.0 -r win10-x64 /p:PublishSingleFile=true /p:IncludeSymbolsInSingleFile=true
cd ..

Write-Host "================== ALL DONE! ===================" -ForegroundColor Green