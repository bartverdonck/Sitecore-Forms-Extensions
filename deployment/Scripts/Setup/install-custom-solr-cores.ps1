Param(  
    $solrServiceName,
    $solrCoresRoot,
    $solrRootFolder
)
$DestinationFolder = "$($solrRootFolder)/server/solr" | Resolve-Path

Get-Service "$solrServiceName" | Stop-Service
Copy-Item -Path "$($solrCoresRoot)/*" -Destination $DestinationFolder -Recurse -Force
Start-Service "$solrServiceName" | Stop-Service