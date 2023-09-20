FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CashFlowApp.API/CashFlowApp.API.csproj", "CashFlowApp.API/"]
RUN dotnet restore "CashFlowApp.API/CashFlowApp.API.csproj"
COPY . .
WORKDIR "/src/CashFlowApp.API"
RUN dotnet build "CashFlowApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashFlowApp.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashFlowApp.API.dll"]
