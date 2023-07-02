#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Create a volume for storing application database
VOLUME /app/data

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/automato.App/automato.App.csproj", "src/automato.App/"]
RUN dotnet restore "src/automato.App/automato.App.csproj"
COPY . .
WORKDIR "/src/src/automato.App"
RUN dotnet build "automato.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "automato.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "automato.App.dll"]