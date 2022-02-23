# move to solution root
Set-Location -Path "..\.."

try
{
	# use appsettings.Release.json
	Copy-Item Billingares.App\wwwroot\appsettings.Release.json -Destination Billingares.App\wwwroot\appsettings.json -verbose

	# copy config files to solution root (temporary)
	Copy-Item Billingares.App\Deploy\dockerfile -Destination .\dockerfile-Billingares.App -verbose
	Copy-Item Billingares.App\Deploy\nginx.conf -Destination . -verbose

	# build new docker image
	docker build -f dockerfile-Billingares.App -t billingares.app-docker .

	# save & copy image to deploy dir
	$timestamp = (Get-Date).ToString('yyyyMMddHHmmss')
	$deployDestination = '.\Deploy\billingares.app-docker_' + $timestamp + '.tar'
	docker save -o $deployDestination billingares.app-docker

	# load image
	#docker load -i billingares.app-docker.tar

	Write-Output "... Success!"
}
catch
{
	Write-Output $_
}
finally
{
	# remove temporary files
	Remove-Item .\dockerfile-Billingares.App -verbose
	Remove-Item .\nginx.conf -verbose

	# restore appsettings.Debug.json
	Copy-Item Billingares.App\wwwroot\appsettings.Debug.json -Destination Billingares.App\wwwroot\appsettings.json -verbose

	# move back to app directory
	Set-Location -Path ".\Billingares.App\Deploy"
}