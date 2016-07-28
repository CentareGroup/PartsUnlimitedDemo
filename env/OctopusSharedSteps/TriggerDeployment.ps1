$baseUrl = $OctopusParameters['Octopus.Web.BaseUrl']
$apiKey =  'API-ZERDHXHBP2HWGRZEXKWK3QGMK'

$targetEnvironmentId = $OctopusParameters['Octopus.Environment.Id']
$releaseNumber =       $OctopusParameters['Octopus.Release.Number']
$targetProject =       $OctopusParameters['TargetProject.Name']
$waitForDeploy =       [System.Convert]::ToBoolean($OctopusParameters['TargetProject.WaitForDeploy'])
$releaseOption =       $OctopusParameters['TargetProject.ReleaseOption']

$headers = @{ 'X-Octopus-ApiKey' = $apiKey }

# Get project list to ensure correct one
$projects = Invoke-RestMethod "${baseUrl}/api/projects/all" -Headers $headers -Method Get
$theProject = $projects | ? { $_.Name -ieq $targetProject }

If ($targetProject -eq $null)
{
    Write-Error "Cannot locate project $targetProject" -ErrorAction Continue
    Exit -1
}

# Get the target release
$projectId = $theProject.Id

If ($releaseOption -ieq 'sameVersion')
{
    
    Write-Verbose "Locating release $releaseNumber for project $targetProject - $projectId"
    $release = Invoke-RestMethod "${baseUrl}/api/projects/${projectId}/releases/${releaseNumber}" -Headers $headers -Method Get -ErrorAction ContinueSilently
    
    If ($release -eq $null)
    {
        Write-Error "Cannot locate release $releaseNumber for project $targetProject" -ErrorAction Continue
        Exit -2
    }
}
Else
{
    Write-Verbose "Locating latest release for project $targetProject - $projectId"
    $progression = Invoke-RestMethod "${baseUrl}/api/progression/${projectId}" -Headers $headers -Method Get
    $release = $progression.Releases[0].Release;
    Write-Verbose "Using Release $($release.Version)"
}

# Fire off deployment
$body = @{ 
    EnvironmentId = $targetEnvironmentId
    ForcePackageDownload = $false
    ForcePackageRedeployment = $false
    ReleaseId = $release.Id
    UseGuidedFailure = $false
} | ConvertTo-Json -Depth 10

$deployment = Invoke-RestMethod "${baseUrl}/api/deployments" -Method Post -Body $body -Headers $headers

$deploymentName = $deployment.Name
$webUrl = $baseUrl + $deployment.Links.Web
Write-Host "Triggered deployment $deploymentName, $webUrl"

If ($waitForDeploy -eq $false) {
    Exit 0
}

$taskUrl = $baseUrl + $deployment.Links.Task
While ($true) {
    Write-Verbose "Waiting for deployment to complete..."
    $taskResult = Invoke-RestMethod $taskUrl -Method Get -Headers $headers
    If ($taskResult.IsCompleted) {
        If (!$taskResult.FinishedSuccessfully) {
            Write-Error "Deployment Failed: $($taskResult.ErrorMessage)"
            Exit -3
        }
        
        Write-Verbose "Deployment completed without errors"
        Break;
    }
    sleep 30
}