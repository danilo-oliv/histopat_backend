# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore histopat_back/histopat_back.csproj
RUN dotnet publish histopat_back/histopat_back.csproj -c Release -o /app

# Etapa de runtime 
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 5000
ENTRYPOINT ["dotnet", "histopat_back.dll"]
