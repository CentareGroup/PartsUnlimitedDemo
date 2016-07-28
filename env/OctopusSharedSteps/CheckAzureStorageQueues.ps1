$StorageAccountName = $OctopusParameters["StorageAccountName"]
$ResourceGroupName = $OctopusParameters["ResourceGroupName"]

Function Create-StorageQueue ([string]$QueueName) {
    Write-Verbose -Verbose -Message "Checking Storage Queue: $QueueName"
    $existingQueue = Get-AzureStorageQueue -Name $QueueName -ErrorAction SilentlyContinue
    If (!$existingQueue) {
        Write-Verbose -Verbose -Message "Creating Storage Queue: $QueueName"
        $null = New-AzureStorageQueue –Name $QueueName
    }
    Else {
         Write-Verbose -Verbose -Message "Storage Queue Already Exists: $QueueName"
    }
}

# Set the current storage account
Write-Verbose "Setting Storage Account to $StorageAccountName from $ResourceGroupName" -Verbose
Set-AzureRmCurrentStorageAccount -ResourceGroupName $ResourceGroupName -StorageAccountName $StorageAccountName
Write-Verbose "Set Storage Account to $StorageAccountName from $ResourceGroupName!" -Verbose

Create-StorageQueue "orders"
Create-StorageQueue "product"

Write-Verbose "Getting Storage Key" -Verbose
$key = (Get-AzureRmStorageAccountKey -ResourceGroupName $ResourceGroupName -StorageAccountName $StorageAccountName).Key1
$storageAccountConnString = "DefaultEndpointsProtocol=https;AccountName=${StorageAccountName};AccountKey=$key"

Write-Host "Storage Connection String: $storageAccountConnString"
Set-OctopusVariable -Name "StorageConnectionString" -Value $storageAccountConnString