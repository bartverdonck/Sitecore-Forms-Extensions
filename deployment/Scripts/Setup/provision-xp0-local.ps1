################ Check and change these parameters before deploying ######
# The Prefix that will be used on SOLR, Website and Database instances.

$Prefix = "forms920"
$IdentityServerClientSecret = "dzQ2B2jg42P76h1wmdtY"
$SolrPort = 8500

##########################################################################

$TemplateFolder = "$($PSScriptRoot)\..\..\Templates\sitecore-920\XP0-OnPremise" | Resolve-Path
$EnvironmentFolder = "$($PSScriptRoot)\..\..\Environments\Local" | Resolve-Path
$PackageFolder = "$($PSScriptRoot)\..\..\Packages" | Resolve-Path
$SitecorePackageFolder = "$($PackageFolder)\sitecore-920\XP0-OnPremise"
$ProjectRootFolder = "$($PSScriptRoot)\..\..\..\..\" | Resolve-Path

##########################################################################

$json = Get-Content ("$($EnvironmentFolder)\environmentsettings.json") | Out-String | ConvertFrom-Json
$SqlServer = $json.SqlServer
$SqlAdminUser = $json.SqlAdminUser
$SqlAdminPassword = $json.SqlAdminPassword

##########################################################################

$ReferenceCertificateName = "*.local.reference.be"
$InstallFolder = "$($ProjectRootFolder)$($Prefix)\"
$LicenseFile = "$EnvironmentFolder/license.xml"

############## Sitecore Identity #########################################

$IdentityServerSiteName = "$prefix-identityserver"
$IdentityServerDnsName = "$IdentityServerSiteName.local.reference.be"
$IdentityServerUrl = "https://$IdentityServerDnsName"
$IdentityServerPackageUrl = "https://nalostoragedevops.blob.core.windows.net/packages/sitecore-911/XP0-OnPremise/Sitecore.IdentityServer%202.0.1%20rev.%2000166%20(OnPrem)_identityserver.scwdp.zip?sv=2018-03-28&si=deployscripts&sr=b&sig=wGE2wLRVwSMAi9j20pffMxGHf5c%2FTtlKqdgFWreIyLM%3D"
$IdentityServerPackageName = "Sitecore.IdentityServer 3.0.0 rev. 00211 (OnPrem)_identityserver.scwdp.zip"
$IdentityServerPackage = "$SitecorePackageFolder\$IdentityServerPackageName"

############## Sitecore xConnect #########################################

$XConnectSiteName = "$prefix-xconnect"
$XConnectDnsName = "$XConnectSiteName.local.reference.be"
$XConnectUrl = "https://$XConnectDnsName"
$XConnectPackageUrl = "https://nalostoragedevops.blob.core.windows.net/packages/sitecore-911/XP0-OnPremise/Sitecore%209.1.1%20rev.%20002459%20(OnPrem)_xp0xconnect.scwdp.zip?sv=2018-03-28&si=deployscripts&sr=b&sig=d2eSmCU5JOR7wT5nec%2BWKqK3kKFUhIGVE7SuL1aWXxg%3D"
$XConnectPackageName = "Sitecore 9.2.0 rev. 002893 (OnPrem)_xp0xconnect.scwdp.zip"
$XConnectPackage = "$SitecorePackageFolder\$XConnectPackageName"

############## Sitecore Website ##########################################

$SitecoreSiteName = "$prefix-sc"
$SitecoreDnsName = "$prefix.local.reference.be"
$SitecoreUrl = "https://$SitecoreDnsName"
$SitecorePackageUrl = "https://nalostoragedevops.blob.core.windows.net/packages/sitecore-911/XP0-OnPremise/Sitecore%209.1.1%20rev.%20002459%20(OnPrem)_single.scwdp.zip?sv=2018-03-28&si=deployscripts&sr=b&sig=mBdlvViJnxvRpxoAJPabTVfBMN3jz0g3cVE6mZWyLaw%3D"
$SitecorePackageName = "Sitecore 9.2.0 rev. 002893 (OnPrem)_single.scwdp.zip"
$SitecorePackage = "$SitecorePackageFolder\$SitecorePackageName"
$SitecoreAdminPassword = "b"

################ Download Required Packages ##############################

function downloadIfRequired
{
    Param(
        [string]$url,
        [string]$targetFolder,
        [string]$targetFile
    )
    If(!(test-path $targetFolder))
    {
            New-Item -ItemType Directory -Force -Path $targetFolder
    }

    if(!(Test-Path -Path "$targetFolder/$targetFile"))
    {
        Write-Host "Downloading $url..."
        Invoke-WebRequest -Uri $url -OutFile "$targetFolder/$targetFile"
    }
}

#downloadIfRequired -url $IdentityServerPackageUrl -targetFolder $SitecorePackageFolder -targetFile $IdentityServerPackageName
#downloadIfRequired -url $XConnectPackageUrl -targetFolder $SitecorePackageFolder -targetFile $XConnectPackageName
#downloadIfRequired -url $SitecorePackageUrl -targetFolder $SitecorePackageFolder -targetFile $SitecorePackageName

############## Install Prerequisites #####################################
Install-Module -Name SitecoreInstallFramework -RequiredVersion 2.1.0 -Confirm
Import-Module -Name SitecoreInstallFramework -Force -RequiredVersion 2.1.0

Push-Location $TemplateFolder
#Install-SitecoreConfiguration -Path .\Prerequisites.json
Pop-Location

############## Install SOLR ##############################################

$SolrUrl = "https://solr:$($SolrPort)/solr"
$SolrRoot = "$($InstallFolder)Solr\solr-7.2.1"
$SolrService = "Sitecore Solr $prefix"

Push-Location $PSScriptRoot
& .\install-solr.ps1 -installFolder "$($InstallFolder)Solr" -solrPort $SolrPort -solrServiceName $SolrService -solrHost "solr" -downloadFolder $PackageFolder
Pop-Location

############## Install SOLR Custom Indexes ###############################
Push-Location $PSScriptRoot
& .\install-custom-solr-cores.ps1 -solrServiceName $SolrService -solrRootFolder $SolrRoot -solrCoresRoot "$($PackageFolder)/solr-cores"
Pop-Location

############## Install Sitecore XP0 ######################################

# Install XP0 via combined partials file.
$singleDeveloperParams = @{
    Path = "$TemplateFolder\XP0-SingleDeveloper.json"
    SqlServer = $SqlServer
    SqlAdminUser = $SqlAdminUser
    SqlAdminPassword = $SqlAdminPassword
    SitecoreAdminPassword = $SitecoreAdminPassword
    SolrUrl = $SolrUrl
    SolrRoot = $SolrRoot
    SolrService = $SolrService
    Prefix = $Prefix
    
    LicenseFile = $LicenseFile
    InstallFolder = $InstallFolder

    IdentityServerCertificateName = $ReferenceCertificateName
    IdentityServerSiteName = $IdentityServerSiteName
    SitecoreIdentityAuthority = $IdentityServerUrl
    IdentityServerPackage = $IdentityServerPackage
    IdentityServerDnsName = $IdentityServerDnsName
    ClientSecret = $IdentityServerClientSecret

    XConnectCertificateName = $ReferenceCertificateName
    XConnectSiteName = $XConnectSiteName
    XConnectCollectionService = $XConnectUrl
    XConnectPackage = $XConnectPackage
    XConnectXP0DnsName = $XConnectDnsName

    SitecoreXP0SitecoreCert = $ReferenceCertificateName
    SitecoreSitename = $SitecoreSiteName
    SitecorePackage = $SitecorePackage
    SitecoreXP0DnsName = $SitecoreDnsName                
    
    PasswordRecoveryUrl = $SitecoreUrl    
    AllowedCorsOrigins = $SitecoreUrl
}

Push-Location $TemplateFolder
Install-SitecoreConfiguration @singleDeveloperParams
Pop-Location
