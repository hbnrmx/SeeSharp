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

### Create 'Send to > SeeSharp' shortcut
Write-Host "==== CREATING 'Send to > SeeSharp' SHORTCUT ====" -ForegroundColor Green
$wshshell = New-Object -com wscript.shell

$shortcutPath = Join-Path -Path $wshshell.SpecialFolders.Item('sendto') -ChildPath 'SeeSharp.lnk'
if((Test-Path $shortcutPath)){
    Write-Host "    shortcut already set" -ForegroundColor Green
}
else{
    $shortcut = $wshshell.CreateShortcut($shortcutPath)
    $shortcut.TargetPath = $pagesPath
    $shortcut.Description = 'this shortcut was created by SeeSharp'
    $shortcut.Save()
    Write-Host "    shortcut created" -ForegroundColor Green
}

Write-Host "================== ALL DONE! ===================" -ForegroundColor Green