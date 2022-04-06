FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SMSService.API/SMSService.API.csproj", "SMSService.API/"]
RUN dotnet restore "SMSService.API/SMSService.API.csproj"
COPY . .
WORKDIR "/src/SMSService.API"
RUN dotnet build "SMSService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SMSService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMSService.API.dll"]