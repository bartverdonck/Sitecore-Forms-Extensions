$Origin = "c:\Projects\Sitecore-Forms-Extensions\Source\downloads\Sitecore 9.1\Sitecore Forms Extensions for Sitecore 9.1-2.2.1.zip"
$Destination = "c:\Projects\Sitecore-Forms-Extensions\Source\downloads\Sitecore 9.1\"

Import-Module "C:\Software\Azure Toolkit\Sitecore Azure Toolkit 2.0.0 rev.171010\tools\Sitecore.Cloud.Cmdlets.dll" -Verbose 
$wdpPath = ConvertTo-SCModuleWebDeployPackage -Path $Origin -Destination $Destination -Force