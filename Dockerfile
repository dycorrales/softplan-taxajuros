FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /
COPY *.sln ./
COPY ["src/taxajuros.domain/taxajuros.domain.csproj", "taxajuros.domain/"]
COPY ["src/taxajuros.webapi/taxajuros.webapi.csproj", "taxajuros.webapi/"]
COPY . .


WORKDIR /src/taxajuros.domain
RUN dotnet restore

WORKDIR /src/taxajuros.webapi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "taxajuros.webapi.dll"]