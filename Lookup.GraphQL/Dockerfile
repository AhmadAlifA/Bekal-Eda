#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lookup.GraphQL/Lookup.GraphQL.csproj", "Lookup.GraphQL/"]
COPY ["Framework.Kafka/Framework.Kafka.csproj", "Framework.Kafka/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["LookUp.Domain/LookUp.Domain.csproj", "LookUp.Domain/"]
COPY ["Framework.Auth/Framework.Auth.csproj", "Framework.Auth/"]
RUN dotnet restore "Lookup.GraphQL/Lookup.GraphQL.csproj"
COPY . .
WORKDIR "/src/Lookup.GraphQL"
RUN dotnet build "Lookup.GraphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lookup.GraphQL.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lookup.GraphQL.dll"]