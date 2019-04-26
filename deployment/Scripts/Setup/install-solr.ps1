Param(  
    $installFolder = "c:\solr",
    $solrPort = "8721",
    $solrHost = "solr",
    $solrSSL = $true,
    $solrServiceName = "solr-721",
    $downloadFolder = "$PSScriptRoot"
)

Write-Host $installFolder
Write-Host $solrPort

$solrVersion = "7.2.1"
$solrName = "solr-$solrVersion"
$solrPackage = "https://archive.apache.org/dist/lucene/solr/$solrVersion/$solrName.zip"
$solrRoot = "$installFolder\$solrName"
$solrCertificateName = $solrServiceName

$nssmFolder = "$($installFolder)\nssm"
$nssmVersion = "2.24"
$nssmPackage = "https://nssm.cc/release/nssm-$nssmVersion.zip"
$nssmRoot = "$nssmFolder\nssm-$nssmVersion"

$JavaOJDKBuildVersion = "1.8.0.201-1"
$JavaOJDKBuildZip = "java-1.8.0-openjdk-$JavaOJDKBuildVersion.b09.ojdkbuild.windows.x86_64.zip"
$JavaOJDKBuildPackage = "https://github.com/ojdkbuild/ojdkbuild/releases/download/$JavaOJDKBuildVersion/$JavaOJDKBuildZip"
$JavaOJDKBuildInstallFolder = "$env:ProgramFiles\ojdkbuild"
$JREPath = "$JavaOJDKBuildInstallFolder\java-1.8.0-openjdk-$JavaOJDKBuildVersion.b09.ojdkbuild.windows.x86_64"

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

## Verify elevated
## https://superuser.com/questions/749243/detect-if-powershell-is-running-as-administrator
$elevated = [bool](([System.Security.Principal.WindowsIdentity]::GetCurrent()).groups -match "S-1-5-32-544")
if($elevated -eq $false)
{
    throw "In order to install services, please run this script elevated."
}

function downloadAndUnzipIfRequired
{
    Param(
        [string]$toolName,
        [string]$toolFolder,
        [string]$toolZip,
        [string]$toolSourceFile,
        [string]$installRoot
    )

    if(!(Test-Path -Path $installRoot))
    {
        if(!(Test-Path -Path $toolZip))
        {
            Write-Host "Downloading $toolName..."
            <#
            Start-BitsTransfer -Source $toolSourceFile -Destination $toolZip
            # github releases has issues with BitsTransfer
            # Invoke-WebRequest is very slow in PowerShell 5.1 when showing progress bar
            #>

            $previousProgressPreference = $ProgressPreference
            $ProgressPreference = 'SilentlyContinue'
            Invoke-WebRequest -Uri $toolSourceFile -OutFile $toolZip 
            $ProgressPreference = $previousProgressPreference
        }

        if(!(Test-Path -Path $toolZip))
        {
            Write-Error "Unable to find $toolZip or download $toolSourceFile" -ErrorAction Stop
        }

        Write-Host "Extracting $toolName to $toolFolder..."
        Expand-Archive $toolZip -DestinationPath $installRoot
    }
}

if($env:JAVA_HOME -eq $null) {
    # download & extract the java archive to the right folder
    $javaZip = "$downloadFolder\$JavaOJDKBuildZip"
    downloadAndUnzipIfRequired "Java" $JavaOJDKBuildInstallFolder $javaZip $JavaOJDKBuildPackage $JavaOJDKBuildInstallFolder -ErrorAction Stop

    # Ensure Java environment variables
    Write-Host "Setting JAVA_HOME environment variable"
    [Environment]::SetEnvironmentVariable("JAVA_HOME", $JREPath, [EnvironmentVariableTarget]::Machine)
	$env:JAVA_HOME = $JREPath

    $oldPath = [Environment]::GetEnvironmentVariable("PATH", [EnvironmentVariableTarget]::Machine)
    if(!$oldPath.Contains("$JREPath\bin")) {
        $newPath = "$JREPath\bin;$oldPath"
        Write-Host "Updating PATH: $newPath"
        [Environment]::SetEnvironmentVariable("PATH", $newPath, [EnvironmentVariableTarget]::Machine)
		$env:Path = $newPath
    }

    Write-Host "Verifying install ..."
    Write-Host "java -version"
    # Redirecting Stderr to Stdout and converting to string works around NativeCommandError.
    # See https://stackoverflow.com/a/20950421 
    java.exe -version 2>&1 | %{ "$_" }
    Write-Host "javac -version"
    javac.exe -version 2>&1 | %{ "$_" }
}

# download & extract the solr archive to the right folder
$solrZip = "$downloadFolder\$solrName.zip"
downloadAndUnzipIfRequired "Solr" $solrRoot $solrZip $solrPackage $installFolder -ErrorAction Stop

# download & extract the nssm archive to the right folder
$nssmZip = "$downloadFolder\nssm-$nssmVersion.zip"
downloadAndUnzipIfRequired "NSSM" $nssmRoot $nssmZip $nssmPackage $nssmFolder -ErrorAction Stop

# if we're using HTTP
if($solrSSL -eq $false)
{
    # Update solr cfg to use right host name
    if(!(Test-Path -Path "$solrRoot\bin\solr.in.cmd.old"))
    {
        Write-Host "Rewriting solr config"

        $cfg = Get-Content "$solrRoot\bin\solr.in.cmd"
        Rename-Item "$solrRoot\bin\solr.in.cmd" "$solrRoot\bin\solr.in.cmd.old"
        $newCfg = $newCfg | % { $_ -replace "REM set SOLR_HOST=192.168.1.1", "set SOLR_HOST=$solrHost" }
        $newCfg | Set-Content "$solrRoot\bin\solr.in.cmd"
    }
}

# Ensure the solr host name is in your hosts file
if($solrHost -ne "localhost")
{
    $hostFileName = "c:\\windows\system32\drivers\etc\hosts"
    $hostFile = [System.Io.File]::ReadAllText($hostFileName)
    if(!($hostFile -like "*$solrHost*"))
    {
        Write-Host "Updating host file"
        "`r`n127.0.0.1`t$solrHost" | Add-Content $hostFileName
    }
}

# if we're using HTTPS
if($solrSSL -eq $true)
{
    # Generate SSL cert
    $existingCert = Get-ChildItem Cert:\LocalMachine\Root | where FriendlyName -eq "$solrCertificateName"
    if(!($existingCert))
    {
        Write-Host "Creating & trusting an new SSL Cert for $solrHost"

        # Generate a cert
        # https://docs.microsoft.com/en-us/powershell/module/pkiclient/new-selfsignedcertificate?view=win10-ps
        $cert = New-SelfSignedCertificate -FriendlyName "$solrCertificateName" -DnsName "$solrHost" -CertStoreLocation "cert:\LocalMachine" -NotAfter (Get-Date).AddYears(10)

        # Trust the cert
        # https://stackoverflow.com/questions/8815145/how-to-trust-a-certificate-in-windows-powershell
        $store = New-Object System.Security.Cryptography.X509Certificates.X509Store "Root","LocalMachine"
        $store.Open("ReadWrite")
        $store.Add($cert)
        $store.Close()

        # remove the untrusted copy of the cert
        $cert | Remove-Item
    }

    # export the cert to pfx using solr's default password
    if(!(Test-Path -Path "$solrRoot\server\etc\solr-ssl.keystore.pfx"))
    {
        Write-Host "Exporting cert for Solr to use"

        $cert = Get-ChildItem Cert:\LocalMachine\Root | where FriendlyName -eq "$solrName"
    
        $certStore = "$solrRoot\server\etc\solr-ssl.keystore.pfx"
        $certPwd = ConvertTo-SecureString -String "secret" -Force -AsPlainText
        $cert | Export-PfxCertificate -FilePath $certStore -Password $certpwd | Out-Null
    }

    # Update solr cfg to use keystore & right host name
    if(!(Test-Path -Path "$solrRoot\bin\solr.in.cmd.old"))
    {
        Write-Host "Rewriting solr config"

        $cfg = Get-Content "$solrRoot\bin\solr.in.cmd"
        Rename-Item "$solrRoot\bin\solr.in.cmd" "$solrRoot\bin\solr.in.cmd.old"
        $newCfg = $cfg | % { $_ -replace "REM set SOLR_SSL_KEY_STORE=etc/solr-ssl.keystore.jks", "set SOLR_SSL_KEY_STORE=etc\solr-ssl.keystore.pfx" }
        $newCfg = $newCfg | % { $_ -replace "REM set SOLR_SSL_KEY_STORE_PASSWORD=secret", "set SOLR_SSL_KEY_STORE_PASSWORD=secret" }
        $newCfg = $newCfg | % { $_ -replace "REM set SOLR_SSL_TRUST_STORE=etc/solr-ssl.keystore.jks", "set SOLR_SSL_TRUST_STORE=etc\solr-ssl.keystore.pfx" }
        $newCfg = $newCfg | % { $_ -replace "REM set SOLR_SSL_TRUST_STORE_PASSWORD=secret", "set SOLR_SSL_TRUST_STORE_PASSWORD=secret" }
        $newCfg = $newCfg | % { $_ -replace "REM set SOLR_HOST=192.168.1.1", "set SOLR_HOST=$solrHost" }
        $newCfg | Set-Content "$solrRoot\bin\solr.in.cmd"
    }
}

# install the service & runs
$svc = Get-Service "$solrServiceName" -ErrorAction SilentlyContinue
if(!($svc))
{
    Write-Host "Installing Solr service"
    &"$nssmRoot\win64\nssm.exe" install "$solrServiceName" "$solrRoot\bin\solr.cmd" "-f" "-p $solrPort"
    $svc = Get-Service "$solrServiceName" -ErrorAction SilentlyContinue
}
if($svc.Status -ne "Running")
{
    Write-Host "Starting Solr service"
    Start-Service "$solrServiceName"
}

# finally prove it's all working
$protocol = "http"
if($solrSSL -eq $true)
{
    $protocol = "https"
}

Invoke-Expression "start $($protocol)://$($solrHost):$solrPort/solr/#/"