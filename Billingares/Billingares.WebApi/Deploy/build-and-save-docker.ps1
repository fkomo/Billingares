# move to solution root
Set-Location -Path "..\.."

try
{
	# use appsettings.Release.json
	Copy-Item Billingares.WebApi\appsettings.Release.json -Destination Billingares.WebApi\appsettings.json -verbose

	# copy config files to solution root (temporary)
	Copy-Item Billingares.WebApi\Deploy\dockerfile -Destination .\dockerfile-Billingares.WebApi -verbose

	# build new docker image
	docker build -f dockerfile-Billingares.WebApi -t billingares.api-docker .
	
	# save image
	docker save -o billingares.api-docker.tar billingares.api-docker

	$timestamp = (Get-Date).ToString('yyyyMMddHHmmss')
	$deployDestination = '.\Deploy\billingares.api-docker_' + $timestamp + '.tar'

	# copy image to deploy dir
	Move-Item -Path billingares.api-docker.tar -Destination $deployDestination -verbose

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
	Remove-Item .\dockerfile-Billingares.WebApi -verbose

	# restore appsettings.Debug.json
	Copy-Item Billingares.WebApi\appsettings.Debug.json -Destination Billingares.WebApi\appsettings.json -verbose

	# move back to app directory
	Set-Location -Path ".\Billingares.WebApi\Deploy"
}