FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Serenity.Calculator.Api/Serenity.Calculator.Api.csproj", "Serenity.Calculator.Api/"]
RUN dotnet restore "Serenity.Calculator.Api/Serenity.Calculator.Api.csproj"
COPY . .
WORKDIR "/src/Serenity.Calculator.Api"
RUN dotnet build "Serenity.Calculator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Serenity.Calculator.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Serenity.Calculator.Api.dll"]
