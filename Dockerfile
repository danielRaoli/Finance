# Usar uma imagem base com o ASP.NET Runtime do .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usar uma imagem base com o SDK do .NET 8 para construir o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o arquivo de solução e restaurar dependências
COPY ["Finance.sln", "."]
COPY ["Finance.API/Finance.API.csproj", "Finance.API/"]
RUN dotnet restore

# Copiar o restante do código e construir o projeto
COPY . .
WORKDIR "/src/Finance.API"
RUN dotnet build -c Release -o /app/build

# Publicar o aplicativo
RUN dotnet publish -c Release -o /app/publish

# Construir a imagem final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Finance.API.dll"]