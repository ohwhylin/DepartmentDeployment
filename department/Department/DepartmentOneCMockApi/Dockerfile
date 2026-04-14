FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["DepartmentOneCMockApi/DepartmentOneCMockApi.csproj", "DepartmentOneCMockApi/"]
COPY ["DepartmentDataModels/DepartmentDataModels.csproj", "DepartmentDataModels/"]

RUN dotnet restore "DepartmentOneCMockApi/DepartmentOneCMockApi.csproj"

COPY . .

WORKDIR "/src/DepartmentOneCMockApi"
RUN dotnet publish "DepartmentOneCMockApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DepartmentOneCMockApi.dll"]