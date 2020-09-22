#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1910 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1910 AS build
WORKDIR /src
COPY ["CinemaToonWeb.MVC/CinemaToonWeb.MVC.csproj", "CinemaToonWeb.MVC/"]
COPY ["CinemaToon.Utilities.Helpers/CinemaToon.Utilities.Helpers.csproj", "CinemaToon.Utilities.Helpers/"]
COPY ["CinemaToon.Utilities.Abstractions/CinemaToon.Utilities.Abstractions.csproj", "CinemaToon.Utilities.Abstractions/"]
COPY ["CinemaToon.Entities/CinemaToon.Entities.csproj", "CinemaToon.Entities/"]
RUN dotnet restore "CinemaToonWeb.MVC/CinemaToonWeb.MVC.csproj"
COPY . .
WORKDIR "/src/CinemaToonWeb.MVC"
RUN dotnet build "CinemaToonWeb.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CinemaToonWeb.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaToonWeb.MVC.dll"]