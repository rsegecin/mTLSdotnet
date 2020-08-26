FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Client/*.csproj ./Client/
COPY ClientService/*.csproj ./ClientService/
RUN dotnet restore Client

# Copy everything else and build
COPY Client/ ./Client/
COPY ClientService/ ./ClientService/
RUN dotnet publish Client -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY Certificates/CA/ca.crt /usr/local/share/ca-certificates/ca_mtls_demo.crt

RUN adduser \
  --disabled-password \
  --home /app \
  --gecos '' -u 1001 appuser \
  && chown -R appuser /app \
  && chmod 644 /usr/local/share/ca-certificates/ca_mtls_demo.crt && update-ca-certificates

USER appuser

WORKDIR /app

COPY --from=build-env /app/out .

HEALTHCHECK --interval=5s --timeout=3s CMD curl -X GET "http://localhost:5140/api/Client" || exit 1

ENTRYPOINT ["dotnet", "Client.dll"]