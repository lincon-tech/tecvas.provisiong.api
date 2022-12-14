#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8887
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Techrunch.TecVas.Provisioning.Api/Techrunch.TecVas.Provisioning.Api.csproj", "Techrunch.TecVas.Provisioning.Api/"]
COPY ["Techrunch.TecVas.Infrastructure/Techrunch.TecVas.Infrastructure.csproj", "Techrunch.TecVas.Infrastructure/"]
COPY ["Techrunch.TecVas.Entities/Techrunch.TecVas.Entities.csproj", "Techrunch.TecVas.Entities/"]
COPY ["Techrunch.TecVas.Core/Techrunch.TecVas.Core.csproj", "Techrunch.TecVas.Core/"]
COPY ["Techrunch.TecVas.Services/Techrunch.TecVas.Services.csproj", "Techrunch.TecVas.Services/"]
COPY ["Techrunch.TecVas.Data/Techrunch.TecVas.Data.csproj", "Techrunch.TecVas.Data/"]
RUN dotnet restore "Techrunch.TecVas.Provisioning.Api.csproj"
COPY . .
WORKDIR "/app/Techrunch.TecVas.Provisioning.Api"
RUN dotnet build "Techrunch.TecVas.Provisioning.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Techrunch.TecVas.Provisioning.Api.csproj" -c Release -o /app/publish

#FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Techrunch.TecVas.Provisioning.Api.dll"]

