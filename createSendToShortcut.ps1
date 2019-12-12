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