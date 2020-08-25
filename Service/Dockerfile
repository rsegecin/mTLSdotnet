FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

RUN adduser \
  --disabled-password \
  --home /app \
  --gecos '' -u 1001 appuser \
  && chown -R appuser /app
USER appuser

WORKDIR /app

COPY bin/publish/ .

HEALTHCHECK --interval=5s --timeout=3s CMD curl -X GET "http://localhost:5020/api/Service" || exit 1

ENTRYPOINT ["dotnet", "Service.dll"]