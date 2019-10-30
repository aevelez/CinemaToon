#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1903 AS build
WORKDIR /src
COPY ["../CinemaToon.Theaters.API/CinemaToon.Theaters.API.csproj", "../CinemaToon.Theaters.API/"]
COPY ["../CinemaToon.Theaters.Data.Context/CinemaToon.Theaters.Data.Context.csproj", "../CinemaToon.Theaters.Data.Context/"]
COPY ["CinemaToon.Entities/CinemaToon.Entities.csproj", "CinemaToon.Entities/"]
COPY ["CinemaToon.Utilities.Abstractions/CinemaToon.Utilities.Abstractions.csproj", "CinemaToon.Utilities.Abstractions/"]
RUN dotnet restore "../CinemaToon.Theaters.API/CinemaToon.Theaters.API.csproj"
COPY . .
WORKDIR "/src/../CinemaToon.Theaters.API"
RUN dotnet build "CinemaToon.Theaters.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CinemaToon.Theaters.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaToon.Theaters.API.dll"]