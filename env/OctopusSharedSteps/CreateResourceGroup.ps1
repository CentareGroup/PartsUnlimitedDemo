$resourceGroupName = $OctopusParameters['Deployment.ResourceGroupName']
$resourceGroupLocation = $OctopusParameters['Deployment.ResourceGroupLocation']

# Try getting the group first
$group = Get-AzureRmResourceGroup -Name $resourceGroupName -Location $resourceGroupLocation -ErrorAction SilentlyContinue
If ($group -ne $null)
{
    Write-Host "Group already exists"
}
Else
{
    Write-Host "Creating resource group '$resourceGroupName' in $resourceGroupLocation"
    New-AzureRmResourceGroup -Name $resourceGroupName -Location $resourceGroupLocation -Force
    Write-Host "Group created successfully!"
}