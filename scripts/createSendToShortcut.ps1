### Create 'Send to > SeeSharp' shortcut

$documentsPath = [Environment]::GetFolderPath("MyDocuments")
$pagesPath = Join-Path $documentsPath -ChildPath SeeSharp | Join-Path -ChildPath pages

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
    Write-Host "    shortcut created at $pagesPath" -ForegroundColor Green
}
Write-Host "================== ALL DONE! ===================" -ForegroundColor Green