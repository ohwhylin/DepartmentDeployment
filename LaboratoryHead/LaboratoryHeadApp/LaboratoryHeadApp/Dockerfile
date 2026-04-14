FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . .

WORKDIR /src/LaboratoryHead/LaboratoryHeadApp/LaboratoryHeadApp
RUN dotnet restore "LaboratoryHeadApp.csproj"
RUN dotnet publish "LaboratoryHeadApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "LaboratoryHeadApp.dll"]