FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["DepartmentUserApp/DepartmentUserApp.csproj", "DepartmentUserApp/"]
COPY ["DepartmentBusinessLogic/DepartmentBusinessLogic.csproj", "DepartmentBusinessLogic/"]
COPY ["DepartmentContracts/DepartmentContracts.csproj", "DepartmentContracts/"]
COPY ["DepartmentDatabaseImplement/DepartmentDatabaseImplement.csproj", "DepartmentDatabaseImplement/"]
COPY ["DepartmentDataModels/DepartmentDataModels.csproj", "DepartmentDataModels/"]

RUN dotnet restore "DepartmentUserApp/DepartmentUserApp.csproj"

COPY . .
WORKDIR "/src/DepartmentUserApp"
RUN dotnet publish "DepartmentUserApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "DepartmentUserApp.dll"]