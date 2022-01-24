FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Billingares.App/Billingares.App.csproj", "Billingares.App/"]
COPY ["Billingares.Base/Billingares.Base.csproj", "Billingares.Base/"]
COPY ["Ujeby.Blazor.Base/Ujeby.Blazor.Base.csproj", "Ujeby.Blazor.Base/"]
RUN dotnet restore "Billingares.App/Billingares.App.csproj"
COPY . .
WORKDIR "/src/Billingares.App"
RUN dotnet build "Billingares.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Billingares.App.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf