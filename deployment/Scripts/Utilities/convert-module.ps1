$Origin = "c:\Projects\Sitecore Default Installs\Sitecore 9.1.1 rev. 002459\Source\deployment\Packages\Modules\Sitecore PowerShell Extensions-5.0.zip"
$Destination = "c:\Projects\Sitecore Default Installs\Sitecore 9.1.1 rev. 002459\Source\deployment\Packages\Modules\"

Import-Module "C:\Software\Azure Toolkit\Sitecore Azure Toolkit 2.0.0 rev.171010\tools\Sitecore.Cloud.Cmdlets.dll" -Verbose 
$wdpPath = ConvertTo-SCModuleWebDeployPackage -Path $Origin -Destination $Destination -Force