$Origin = "C:\Projects\Sitecore-Forms-Extensions\downloads\Sitecore 10.1\Sitecore Forms Extensions for SC10.1-4.0.zip"
$Destination = "C:\Projects\Sitecore-Forms-Extensions\downloads\Sitecore 10.1\"

Import-Module "c:\Applications\Sitecore Azure Toolkit 2.5.0\tools\Sitecore.Cloud.Cmdlets.dll" -Verbose 
$wdpPath = ConvertTo-SCModuleWebDeployPackage -Path $Origin -Destination $Destination -Force