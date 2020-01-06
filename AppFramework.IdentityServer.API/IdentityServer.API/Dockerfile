FROM 172.18.228.130:8083/dotnet:aspnetcorert AS base
WORKDIR /app
EXPOSE 80

#FROM 172.18.228.130:8083/dotnet:2.1sdk AS build
#WORKDIR /src
#COPY ["AppFramework.IdentityServer.API/IdentityServer.API/IdentityServer.API.csproj", "AppFramework.IdentityServer.API/IdentityServer.API/"]
#COPY ["AppFramework.IdentityServer.API/IdentityServer.API/nuget.config", "AppFramework.IdentityServer.API/IdentityServer.API/"]

#RUN dotnet restore "AppFramework.IdentityServer.API/IdentityServer.API/IdentityServer.API.csproj"
#COPY . .
#WORKDIR "/src/AppFramework.IdentityServer.API/IdentityServer.API"
#RUN dotnet build "IdentityServer.API.csproj" -c Release -o /app

#FROM build AS publish
COPY AppFramework.IdentityServer.API/IdentityServer.API/publish/. /app
COPY . .
#RUN dotnet publish "IdentityServer.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
#COPY  /app .
ENTRYPOINT ["dotnet", "IdentityServer.API.dll"]