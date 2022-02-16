# move to solution root
Set-Location -Path ".."

try
{
	Set-Location -Path "Billingares.App\Deploy"
	powershell .\build-and-save-docker.ps1
	Set-Location -Path "..\.."

	Set-Location -Path "Billingares.Api.REST\Deploy"
	powershell .\build-and-save-docker.ps1
	Set-Location -Path "..\.."
}
catch
{
	Write-Output $_
}
finally
{
	# move back to app directory
	Set-Location -Path ".\Deploy"
}