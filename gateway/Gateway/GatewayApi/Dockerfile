FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["GatewayApi/GatewayApi.csproj", "GatewayApi/"]
RUN dotnet restore "GatewayApi/GatewayApi.csproj"

COPY . .
WORKDIR "/src/GatewayApi"
RUN dotnet publish "GatewayApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

RUN apt-get update && apt-get install -y \
    libldap-2.4-2 \
    libsasl2-2 \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "GatewayApi.dll"]