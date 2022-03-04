# move to solution root
Set-Location -Path "..\.."

# path to referenced libraries/projects
$ujeby = '..\..\Ujeby\Deploy\'
$ujebyBlazorBase = $ujeby + 'Ujeby.Blazor.Base.dll'
$ujebyApiClientBase = $ujeby + 'Ujeby.Api.Client.Base.dll'

try
{
	# gather referenced libraries
	New-Item -Force -ItemType directory -Path .\Deploy\3rd
	Copy-Item $ujebyBlazorBase -Destination .\Deploy\3rd\Ujeby.Blazor.Base.dll -verbose -force
	Copy-Item $ujebyApiClientBase -Destination .\Deploy\3rd\Ujeby.Api.Client.Base.dll -verbose -force

	# use appsettings.Test.json
	Copy-Item Billingares.App\wwwroot\appsettings.Test.json -Destination Billingares.App\wwwroot\appsettings.json -verbose

	# copy config files to solution root (temporary)
	Copy-Item Billingares.App\Deploy\dockerfile -Destination .\dockerfile-Billingares.App -verbose
	Copy-Item Billingares.App\Deploy\nginx.conf -Destination . -verbose

	# stop&remove old docker image
	docker stop billingares.app
	docker rm billingares.app
	docker image prune -a -f

	# build new docker image
	docker build -f dockerfile-Billingares.App -t billingares.app-docker .

	# run new image on localhost
	docker run -d --name billingares.app -p 8091:80 billingares.app-docker

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
