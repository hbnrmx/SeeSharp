$buildPath = 'C:\SeeSharp\win10\x64'
$pagesPath = Join-Path -Path $buildPath -ChildPath '\pages'

### build App
Write-Host "BUILDING APP" -ForegroundColor Green
cd SeeSharp.Desktop
dotnet publish -o $buildPath -c RELEASE -f netcoreapp3.0 -r win10-x64 /p:PublishSingleFile=true /p:IncludeSymbolsInSingleFile=true
cd ..

### Create 'Send to > SeeSharp' shortcut
Write-Host "CREATING 'Send to > SeeSharp' SHORTCUT" -ForegroundColor Green
$wshshell = New-Object -com wscript.shell

$shortcutPath = Join-Path -Path $wshshell.SpecialFolders.Item('sendto') -ChildPath 'SeeSharp.lnk'
$shortcut = $wshshell.CreateShortcut($shortcutPath)
$shortcut.TargetPath = $pagesPath
$shortcut.Description = 'this shortcut was created by SeeSharp'
$shortcut.Save()

Write-Host "ALL DONE!" -ForegroundColor Green