################ Check and change these parameters before deploying ######
# The Prefix that will be used on SOLR, Website and Database instances.

$Prefix = "forms911"
$IdentityServerClientSecret = "1vdeLdlRqrfxyISl4ZjO"
$SolrPort = 8680

##########################################################################

$TemplateFolder = "$($PSScriptRoot)\..\..\Templates\IaaS\XP0" | Resolve-Path
$EnvironmentFolder = "$($PSScriptRoot)\..\..\Environments\Local" | Resolve-Path
$PackageFolder = "$($PSScriptRoot)\..\..\Packages" | Resolve-Path
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
$IdentityServerPackage = (Get-ChildItem "$PackageFolder\IaaS\XP0\Sitecore.IdentityServer * rev. * (OnPrem)_identityserver.scwdp.zip").FullName

############## Sitecore xConnect #########################################

$XConnectSiteName = "$prefix-xconnect"
$XConnectDnsName = "$XConnectSiteName.local.reference.be"
$XConnectUrl = "https://$XConnectDnsName"
$XConnectPackage = (Get-ChildItem "$PackageFolder\IaaS\XP0\Sitecore 9* rev. * (OnPrem)_xp0xconnect.scwdp.zip").FullName

############## Sitecore Website ##########################################

$SitecoreSiteName = "$prefix-sc"
$SitecoreDnsName = "$prefix.local.reference.be"
$SitecoreUrl = "https://$SitecoreDnsName"
$SitecorePackage = (Get-ChildItem "$PackageFolder\IaaS\XP0\Sitecore 9* rev. * (OnPrem)_single.scwdp.zip").FullName
$SitecoreAdminPassword = "b"

############## Install SOLR ##############################################

$SolrUrl = "https://solr:$($SolrPort)/solr"
$SolrRoot = "$($InstallFolder)Solr\solr-7.2.1"
$SolrService = "Sitecore Solr $prefix"

Push-Location $PSScriptRoot
& .\install-solr.ps1 -installFolder "$($InstallFolder)Solr" -solrPort $SolrPort -solrServiceName $SolrService -solrHost "solr" -downloadFolder $PackageFolder
Pop-Location

############## Install Sitecore XP0 ######################################

Import-Module -Name SitecoreInstallFramework -Force -RequiredVersion 2.1.0

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