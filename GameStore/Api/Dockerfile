FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY . .
RUN dotnet publish GameStore.Api.csproj -o /published /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine
WORKDIR /app
COPY --from=build /published .
ENTRYPOINT [ "dotnet", "GameStore.dll" ]