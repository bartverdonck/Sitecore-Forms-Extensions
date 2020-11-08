# Release steps

This document describes the steps to take in order to release a new version of SFE.

## Solution
- Update AssemblyInfo.cs and set version numbers.

## Package Creation
- Open the package designer in Sitecore
- Load the Sitecore Forms Extensions.xml package definition
- Check the version number
- Generate Package
- Store package in the downloads folder

## SCWPD Creation
- Review and run the script `create-scwpd.ps1`. (Azure Toolkit needs to be installed)
- Review and run the script `create-scwpd-nodb.ps1`.

## Container Image
- Open the scwpd package, copy all content from `Content\Website` into `docker/build/module/cm/content` and `docker/build/module/cd/content`
- From the scwpd pacakge, copy `core.dacpac`, `master.dacpac` and `web.dacpac` into `docker/build/module/db`. Prefix the files with `Sitecore.`
- Run `docker-compose build module`
- Lookup image id and run `docker tag [id] bverdonck/sitecore-forms-extensions-assets:[version]`
- Login to docker `docker login --username=bverdonck` (Password in KeePass)
- Docker push `bverdonck/sitecore-forms-extensions-assets`

## Nuget
- Update `src/feature/formsextensions/code/Feature.FormsExtensions.nuspec`
- Run `nuget pack Feature.FormsExtensions.nuspec`
- Push to nuget `nuget push SitecoreFormsExtensions.Core.[version].nupkg [secret]  -Source https://api.nuget.org/v3/index.json`

## Documentation
- Update documentation in docs folder (To rund the make.bat files, sphinx must be installed `pip install -U sphinx`)
- Execute `make.bat html`