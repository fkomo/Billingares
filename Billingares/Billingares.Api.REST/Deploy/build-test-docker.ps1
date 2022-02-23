# move to solution root
Set-Location -Path "..\.."

try
{
	# use appsettings.Test.json
	Copy-Item Billingares.Api.REST\appsettings.Test.json -Destination Billingares.Api.REST\appsettings.json -verbose

	# copy config files to solution root (temporary)
	Copy-Item Billingares.Api.REST\Deploy\dockerfile -Destination .\dockerfile-Billingares.Api.REST -verbose

	# stop&remove old docker image
	docker stop billingares.api
	docker rm billingares.api
	docker image prune -a -f

	# build new docker image
	docker build -f dockerfile-Billingares.Api.REST -t billingares.api-docker .

	# run new image on localhost
	docker run -d --name billingares.api -p 8093:80 billingares.api-docker

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