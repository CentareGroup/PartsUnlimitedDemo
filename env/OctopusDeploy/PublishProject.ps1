param ([string]$OutputDirectory,
       [string]$BuildNumber,
       [string]$Configuration)

Write-Host "Output Directory: $OutputDirectory"
Write-Host "Configuration: $Configuration"
Write-Host "Build Number: $BuildNumber"


# Download a copy of NuGet if needed
$CachedNuGetDir = Join-Path $env:APPDATA 'NuGet'
$CachedNuGetFile = Join-Path $CachedNuGetDir 'NuGet.exe'

If (!(Test-Path $CachedNuGetFile)) {

    If (!(Test-Path $CachedNuGetDir)) {
        New-Item -Path $CachedNuGetDir -ItemType Directory | Out-Null
    }

    Invoke-WebRequest 'https://www.nuget.org/nuget.exe' -OutFile $CachedNuGetFile
}

$localNuGetExe = '.nuget\nuget.exe'
If (!(Test-Path $localNuGetExe)) {
    New-Item -Path '.nuget' -ItemType Directory -Force | Out-Null
    Copy-Item -Path $CachedNuGetFile -Destination $localNuGetExe
}

# Use NuGet to pack up the website
Write-Host "Packaging up project"

$basePath = Resolve-Path -Path '.\src\PartsUnlimitedWebsite'
& $localNuGetExe pack src\PartsUnlimitedWebsite\PartsUnlimited.Web.nuspec -OutputDirectory $OutputDirectory -BasePath $basePath -Version $BuildNumber -Properties "Configuration=$Configuration" -Properties id=PartsUnlimited.Web

# Use NuGet to pull down  