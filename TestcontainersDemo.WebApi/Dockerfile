FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["TestcontainersDemo.WebApi/TestcontainersDemo.WebApi.csproj", "TestcontainersDemo.WebApi/"]
COPY ["TestcontainersDemo.Infrastructure.Postgres/TestcontainersDemo.Infrastructure.Postgres.csproj", "TestcontainersDemo.Infrastructure.Postgres/"]
RUN dotnet restore "TestcontainersDemo.WebApi/TestcontainersDemo.WebApi.csproj"
COPY . .
WORKDIR "/src/TestcontainersDemo.WebApi"
RUN dotnet build "TestcontainersDemo.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestcontainersDemo.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestcontainersDemo.WebApi.dll"]
