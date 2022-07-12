#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/automato/automato.csproj", "src/automato/"]
COPY ["src/automato.Infrastructure.LiteDB/automato.Infrastructure.LiteDB.csproj", "src/automato.Infrastructure.LiteDB/"]
COPY ["src/automato.Domain.Services/automato.Domain.Services.csproj", "src/automato.Domain.Services/"]
COPY ["src/automato.Domain/automato.Domain.csproj", "src/automato.Domain/"]
COPY ["src/automato.Framework/automato.Framework.csproj", "src/automato.Framework/"]
RUN dotnet restore "src/automato/automato.csproj"
COPY . .
WORKDIR "/src/src/automato"
RUN dotnet build "automato.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "automato.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "automato.dll"]