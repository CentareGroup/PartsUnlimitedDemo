$resourceGroupName = $OctopusParameters['Deployment.ResourceGroupName']
$resourceGroupLocation = $OctopusParameters['Deployment.ResourceGroupLocation']

# Try getting the group first
$group = Get-AzureRmResourceGroup -Name $resourceGroupName -Location $resourceGroupLocation -ErrorAction SilentlyContinue
If ($group -ne $null)
{
    Write-Host "Removing resource group '$resourceGroupName' in $resourceGroupLocation"
    $group | Remove-AzureRmResourceGroup -Confirm -Pre -Force
    Write-Host "Group removed successfully!"
}
Else
{
    Write-Host "Resource Group $resourceGroupName does not exist"
}