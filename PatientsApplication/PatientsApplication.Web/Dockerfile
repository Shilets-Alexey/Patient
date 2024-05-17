#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PatientsApplication/PatientsApplication.Web/PatientsApplication.Web.csproj", "PatientsApplication/PatientsApplication.Web/"]
COPY ["PatientsApplication/PatientsApplication.BusinessLogic/PatientsApplication.BusinessLogic.csproj", "PatientsApplication/PatientsApplication.BusinessLogic/"]
COPY ["PatientsApplication/PatientsApplication.DataAccess/PatientsApplication.DataAccess.csproj", "PatientsApplication/PatientsApplication.DataAccess/"]
RUN dotnet restore "./PatientsApplication/./PatientsApplication.Web/./PatientsApplication.Web.csproj"
COPY . .
WORKDIR "/src/PatientsApplication/PatientsApplication.Web"
RUN dotnet build "./PatientsApplication.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PatientsApplication.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientsApplication.Web.dll"]