$Origin = "c:\Projects\Sitecore-Forms-Extensions\Source\downloads\Sitecore 9.3.x\Sitecore Forms Extensions for Sitecore 9.3-3.0.zip"
$Destination = "c:\Projects\Sitecore-Forms-Extensions\Source\downloads\Sitecore 9.3.x\"

Import-Module "C:\Software\Azure Toolkit\Sitecore Azure Toolkit 2.0.0 rev.171010\tools\Sitecore.Cloud.Cmdlets.dll" -Verbose 
$wdpPath = ConvertTo-SCModuleWebDeployPackage -Path $Origin -Destination $Destination -Force