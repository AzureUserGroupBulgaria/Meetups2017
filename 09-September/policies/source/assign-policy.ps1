$definition = New-AzureRmPolicyDefinition `
    -Name "Allowed VM SKUs" `
    -DisplayName "Restrict VM SKUs that can be used." `
    -Policy '.\policy\vm-skus.rules.json'

$rg = Get-AzureRmResourceGroup -Name "policy"

New-AzureRMPolicyAssignment `
    -Name "Allowed VM SKUs" `
    -Scope $rg.ResourceId `
    -PolicyDefinition $definition