# move to solution root
Set-Location -Path ".."

try
{
	# copy config files to solution root (temporary)
	Copy-Item Billingares.App\dockerfile -Destination .\dockerfile-Billingares.App -verbose
	Copy-Item Billingares.App\nginx.conf -Destination . -verbose

	# stop&remove old docker image
	docker stop billingares.app
	docker rm billingares.app
	docker image prune -a -f

	#rem build new docker image
	docker build -f dockerfile-Billingares.App -t billingares.app-docker .

	#rem run new image on localhost:8088
	docker run -d --name billingares.app -p 8088:80 billingares.app-docker

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

# save image
docker save -o billingares.app-docker.tar billingares.app-docker

# load image
#docker load -i billingares.app-docker.tar
