FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ChaingeRoutePlanner/ChaingeRoutePlanner.csproj", "ChaingeRoutePlanner/"]
RUN dotnet restore "ChaingeRoutePlanner/ChaingeRoutePlanner.csproj"
COPY . .
WORKDIR "/src/ChaingeRoutePlanner"
RUN dotnet build "ChaingeRoutePlanner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChaingeRoutePlanner.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChaingeRoutePlanner.dll"]
