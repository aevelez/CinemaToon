#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1910 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1910 AS build
WORKDIR /src
COPY ["CinemaToon.Movies.API/CinemaToon.Movies.API.csproj", "CinemaToon.Movies.API/"]
COPY ["CinemaToon.Utilities.Helpers/CinemaToon.Utilities.Helpers.csproj", "CinemaToon.Utilities.Helpers/"]
COPY ["CinemaToon.Utilities.Abstractions/CinemaToon.Utilities.Abstractions.csproj", "CinemaToon.Utilities.Abstractions/"]
COPY ["CinemaToon.Entities/CinemaToon.Entities.csproj", "CinemaToon.Entities/"]
COPY ["CinemaToon.Movies.Application.Core/CinemaToon.Movies.Application.Core.csproj", "CinemaToon.Movies.Application.Core/"]
RUN dotnet restore "CinemaToon.Movies.API/CinemaToon.Movies.API.csproj"
COPY . .
WORKDIR "/src/CinemaToon.Movies.API"
RUN dotnet build "CinemaToon.Movies.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CinemaToon.Movies.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaToon.Movies.API.dll"]