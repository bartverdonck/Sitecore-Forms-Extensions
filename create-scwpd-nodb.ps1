
##########################################################################
#  
#  Usage: manualy extract the parameters.xml file from the original .scwdp.zip file and reference in this file.
#  Fill in the packagepath and paramfile path.
#
##########################################################################

$PackagePath = "C:\Projects\Sitecore-Forms-Extensions\downloads\Sitecore 10.1\Sitecore Forms Extensions for SC10.1-4.0.2.scwdp.zip"
$ParamFile = "C:\Temp\parameters.xml"

##########################################################################

$PackageDestinationPath = $($PackagePath).Replace(".scwdp.zip", "-nodb.scwdp.zip")

$msdeploy = "C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe"
$verb = "-verb:sync"
$source = "-source:package=`"$PackagePath`""
$destination = "-dest:package=`"$($PackageDestinationPath)`""
$declareParamFile = "-declareparamfile=`"$($ParamFile)`""
$skipDbFullSQL = "-skip:objectName=dbFullSql"
$skipDbDacFx = "-skip:objectName=dbDacFx"

# read parameter file
[xml]$paramfile_content = Get-Content -Path $ParamFile
$paramfile_paramnames = $paramfile_content.parameters.parameter.name
$params = ""
foreach($paramname in $paramfile_paramnames){
   $tmpvalue = "tmpvalue"
   if($paramname -eq "License Xml"){ $tmpvalue = "LicenseContent"}
   if($paramname -eq "IP Security Client IP"){ $tmpvalue = "0.0.0.0"}
   if($paramname -eq "IP Security Client IP Mask"){ $tmpvalue = "0.0.0.0"}
   $params = "$params -setParam:`"$paramname`"=`"$tmpvalue`""
}

# create new package
Invoke-Expression "& '$msdeploy' --% $verb $source $destination $declareParamFile $skipDbFullSQL $skipDbDacFx $params"