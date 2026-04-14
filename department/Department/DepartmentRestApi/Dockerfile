FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["DepartmentRestApi/DepartmentRestApi.csproj", "DepartmentRestApi/"]
COPY ["DepartmentBusinessLogic/DepartmentBusinessLogic.csproj", "DepartmentBusinessLogic/"]
COPY ["DepartmentContracts/DepartmentContracts.csproj", "DepartmentContracts/"]
COPY ["DepartmentDatabaseImplement/DepartmentDatabaseImplement.csproj", "DepartmentDatabaseImplement/"]
COPY ["DepartmentDataModels/DepartmentDataModels.csproj", "DepartmentDataModels/"]

RUN dotnet restore "DepartmentRestApi/DepartmentRestApi.csproj"

COPY . .
WORKDIR "/src/DepartmentRestApi"
RUN dotnet publish "DepartmentRestApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "DepartmentRestApi.dll"]