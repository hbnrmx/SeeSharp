$buildPath = 'C:\SeeSharp\win10\x64'
$pagesPath = Join-Path -Path $buildPath -ChildPath '\pages'

### build App
dotnet publish -o $buildPath -c RELEASE -f netcoreapp3.0 -r win10-x64 /p:PublishSingleFile=true /p:IncludeSymbolsInSingleFile=true

### Create 'Send to > pages' shortcut
$wshshell = New-Object -com wscript.shell

$shortcutPath = Join-Path -Path $wshshell.SpecialFolders.Item('sendto') -ChildPath 'SeeSharp.lnk'
$shortcut = $wshshell.CreateShortcut($shortcutPath)
$shortcut.TargetPath = $pagesPath
$shortcut.Description = 'this shortcut was created by SeeSharp'
$shortcut.Save()