FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Service/*.csproj ./Service/
RUN dotnet restore Service

# Copy everything else and build
COPY Service/ ./Service/
RUN dotnet publish Service -c Release -o out

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

HEALTHCHECK --interval=5s --timeout=3s CMD curl -X GET "http://localhost:5150/api/Service" || exit 1

ENTRYPOINT ["dotnet", "Service.dll"]