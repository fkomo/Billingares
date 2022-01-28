# move to solution root
Set-Location -Path "..\.."

try
{
	# use appsettings.Release.json
	Copy-Item Billingares.App\wwwroot\appsettings.Release.json -Destination Billingares.App\wwwroot\appsettings.json -verbose

	# copy config files to solution root (temporary)
	Copy-Item Billingares.App\Deploy\dockerfile -Destination .\dockerfile-Billingares.App -verbose
	Copy-Item Billingares.App\Deploy\nginx.conf -Destination . -verbose

	# stop&remove old docker image
	docker stop billingares.app
	docker rm billingares.app
	docker image prune -a -f

	# build new docker image
	docker build -f dockerfile-Billingares.App -t billingares.app-docker .

	# run new image on localhost:8088
	docker run -d --name billingares.app -p 8088:80 billingares.app-docker

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

	# move back app directory
	Set-Location -Path ".\Billingares.App\Deploy"
}
