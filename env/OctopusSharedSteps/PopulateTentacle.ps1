$baseUrl =                $OctopusParameters['Octopus.Web.BaseUrl']
$targetEnvironmentId =    $OctopusParameters['Octopus.Environment.Id']
$targetRoles =            $OctopusParameters['TargetRoles']
$authenticationAccount =  $OctopusParameters['ConnectionCredentials']
$hostnameOrIpAddress =    $OctopusParameters['MachineName']
 
$apiKey = 'API-ZERDHXHBP2HWGRZEXKWK3QGMK'

$machineName = $null
$headers = @{ 'X-Octopus-ApiKey' = $apiKey }

# Get account list to ensure correct one
$accounts = Invoke-RestMethod "${baseUrl}/api/accounts/all" -Headers $headers -Method Get
$theAccount = $accounts | ? { $_.Name -eq $authenticationAccount }

# Check to see if machine exists, if so exit
$checkName = If (![string]::IsNullOrEmpty($machineName)) { $machineName } Else { $hostnameOrIpAddress }

$currentMachines = Invoke-RestMethod "${baseUrl}/api/machines/all" -Headers $headers -Method Get
$currentMachine = $currentMachines | Where-Object { $_.Name -ieq $checkName }
If ($currentMachine -ne $null)
{
    Write-Host "Found existing machine, skipping registration."
    Exit 0
}

# Discover new machine to complete registration
$discovered = Invoke-RestMethod "${baseUrl}/api/machines/discover?host=$hostnameOrIpAddress&type=Ssh" -Headers $headers -Method Get

If (![string]::IsNullOrEmpty($machineName))
{
    $discovered.Name = $machineName
}

$discovered.Roles += $targetRoles
$discovered.EnvironmentIds += $targetEnvironmentId
$discovered.Endpoint.AccountId = $theAccount.Id

# Complete Registration of machine
$machineBody = ($discovered | ConvertTo-Json -Depth 10)
$newMachine = Invoke-RestMethod "${baseUrl}/api/machines" -Headers $headers -Method Post -Body $machineBody