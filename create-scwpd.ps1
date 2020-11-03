$Origin = "c:\Projects\Sitecore-Forms-Extensions\downloads\Sitecore 9.3.x\Sitecore Forms Extensions for SC9.3 and SC10-3.1.zip"
$Destination = "c:\Projects\Sitecore-Forms-Extensions\downloads\Sitecore 9.3.x\"

Import-Module "c:\Applications\Sitecore Azure Toolkit 2.5.0\tools\Sitecore.Cloud.Cmdlets.dll" -Verbose 
$wdpPath = ConvertTo-SCModuleWebDeployPackage -Path $Origin -Destination $Destination -Force