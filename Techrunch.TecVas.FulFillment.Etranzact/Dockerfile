#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Techrunch.TecVas.FulFillment.Etranzact/Techrunch.TecVas.FulFillment.Etranzact.csproj", "Techrunch.TecVas.FulFillment.Etranzact/"]
RUN dotnet restore "Techrunch.TecVas.FulFillment.Etranzact/Techrunch.TecVas.FulFillment.Etranzact.csproj"
COPY . .
WORKDIR "/src/Techrunch.TecVas.FulFillment.Etranzact"
RUN dotnet build "Techrunch.TecVas.FulFillment.Etranzact.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Techrunch.TecVas.FulFillment.Etranzact.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Techrunch.TecVas.FulFillment.Etranzact.dll"]