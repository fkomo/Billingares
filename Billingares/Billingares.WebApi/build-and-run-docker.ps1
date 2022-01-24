# move to solution root
Set-Location -Path ".."

try
{
	# copy config files to solution root (temporary)
	Copy-Item Billingares.WebApi\dockerfile -Destination .\dockerfile-Billingares.WebApi -verbose

	# stop&remove old docker image
	docker stop billingares.api
	docker rm billingares.api
	docker image prune -a -f

	#rem build new docker image
	docker build -f dockerfile-Billingares.WebApi -t billingares.api-docker .

	#rem run new image on localhost:8088
	docker run -d --name billingares.api -p 8089:80 billingares.api-docker

	# remove temporary files
	Remove-Item .\dockerfile-Billingares.WebApi -verbose

	Write-Output "... Success!"
}
catch
{
	Write-Output $_
}
finally
{
	# move back app directory
	Set-Location -Path ".\Billingares.WebApi"
}

# save image
docker save -o billingares.api-docker.tar billingares.api-docker

# load image
#docker load -i billingares.api-docker.tar
