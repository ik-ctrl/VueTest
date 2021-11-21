FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SimpleBackend.WebApi/SimpleBackend.WebApi.csproj", "SimpleBackend.WebApi/"]
RUN dotnet restore "SimpleBackend.WebApi/SimpleBackend.WebApi.csproj"
COPY . .
WORKDIR "/src/SimpleBackend.WebApi"
RUN dotnet build "SimpleBackend.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleBackend.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleBackend.WebApi.dll"]
