Remove-Item ..\QuickLook.Plugin.PagViewer.qlplugin -ErrorAction SilentlyContinue

$files = Get-ChildItem -Path ..\bin\Release\ -Exclude *.pdb,*.xml
Compress-Archive $files ..\QuickLook.Plugin.PagViewer.zip
Move-Item ..\QuickLook.Plugin.PagViewer.zip ..\QuickLook.Plugin.PagViewer.qlplugin

Write-Host "Created QuickLook.Plugin.PagViewer.qlplugin"
Get-Item ..\QuickLook.Plugin.PagViewer.qlplugin | Select-Object Name, Length
