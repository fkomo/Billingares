# move to solution root
Set-Location -Path "..\.."

try
{
	# use appsettings.Release.json
	Copy-Item Billingares.WebApi\appsettings.Release.json -Destination Billingares.WebApi\appsettings.json -verbose

	# copy config files to solution root (temporary)
	Copy-Item Billingares.WebApi\Deploy\dockerfile -Destination .\dockerfile-Billingares.WebApi -verbose

	# stop&remove old docker image
	docker stop billingares.api
	docker rm billingares.api
	docker image prune -a -f

	# build new docker image
	docker build -f dockerfile-Billingares.WebApi -t billingares.api-docker .

	# run new image on localhost:8088
	docker run -d --name billingares.api -p 8089:80 billingares.api-docker

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

	# move back app directory
	Set-Location -Path ".\Billingares.WebApi\Deploy"
}