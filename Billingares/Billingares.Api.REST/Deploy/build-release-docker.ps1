# move to solution root
Set-Location -Path "..\.."

try
{
	# use appsettings.Release.json
	Copy-Item Billingares.Api.REST\appsettings.Release.json -Destination Billingares.Api.REST\appsettings.json -verbose

	# copy config files to solution root (temporary)
	Copy-Item Billingares.Api.REST\Deploy\dockerfile -Destination .\dockerfile-Billingares.Api.REST -verbose

	# build new docker image
	docker build -f dockerfile-Billingares.Api.REST -t billingares.api-docker .
	
	# save & copy image to deploy dir
	$timestamp = (Get-Date).ToString('yyyyMMddHHmmss')
	$deployDestination = '.\Deploy\billingares.api-docker_' + $timestamp + '.tar'
	docker save -o $deployDestination billingares.api-docker

	# load image
	#docker load -i billingares.api-docker.tar

	Write-Output "... Success!"
}
catch
{
	Write-Output $_
}
finally
{
	# remove temporary files
	Remove-Item .\dockerfile-Billingares.Api.REST -verbose

	# restore appsettings.Debug.json
	Copy-Item Billingares.Api.REST\appsettings.Debug.json -Destination Billingares.Api.REST\appsettings.json -verbose

	# move back to app directory
	Set-Location -Path ".\Billingares.Api.REST\Deploy"
}