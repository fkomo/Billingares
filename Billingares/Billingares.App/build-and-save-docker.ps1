# move to solution root
Set-Location -Path ".."

try
{
	# copy config files to solution root (temporary)
	Copy-Item Billingares.App\dockerfile -Destination .\dockerfile-Billingares.App -verbose
	Copy-Item Billingares.App\nginx.conf -Destination . -verbose

	# build new docker image
	docker build -f dockerfile-Billingares.App -t billingares.app-docker .

	# save image
	docker save -o billingares.app-docker.tar billingares.app-docker

	$timestamp = (Get-Date).ToString('yyyyMMddHHmmss')
	$deployDestination = '.\Deploy\billingares.app-docker_' + $timestamp + '.tar'

	# copy image to deploy dir
	Move-Item -Path billingares.app-docker.tar -Destination $deployDestination -verbose

	# load image
	#docker load -i billingares.app-docker.tar

	# remove temporary files
	Remove-Item .\dockerfile-Billingares.App -verbose
	Remove-Item .\nginx.conf -verbose

	Write-Output "... Success!"
}
catch
{
	Write-Output $_
}
finally
{
	# move back app directory
	Set-Location -Path ".\Billingares.App"
}