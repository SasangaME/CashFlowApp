FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CashFlowApp.Db/CashFlowApp.Db.csproj", "CashFlowApp.Db/"]
RUN dotnet restore "CashFlowApp.Db/CashFlowApp.Db.csproj"
COPY . .
WORKDIR "/src/CashFlowApp.Db"
RUN dotnet build "CashFlowApp.Db.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashFlowApp.Db.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashFlowApp.Db.dll"]
