# Release steps

This document describes the steps to take in order to release a new version of SFE.

## Solution
- Update AssemblyInfo.cs and set version numbers.
- 

## Package Creation

## Container Image

## Nuget
- Update `src/feature/formsextensions/code/Feature.FormsExtensions.nuspec`
- Run `nuget pack Feature.FormsExtensions.nuspec`
- Push to nuget `nuget push SitecoreFormsExtensions.Core.[version].nupkg [secret]  -Source https://api.nuget.org/v3/index.json`