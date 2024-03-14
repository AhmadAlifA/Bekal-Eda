#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Order/Order.GarphQL/Order.GarphQL.csproj", "Order/Order.GarphQL/"]
COPY ["Framework.Kafka/Framework.Kafka.csproj", "Framework.Kafka/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["Order/Order.Domain/Order.Domain.csproj", "Order/Order.Domain/"]
COPY ["Framework.Auth/Framework.Auth.csproj", "Framework.Auth/"]
RUN dotnet restore "Order/Order.GarphQL/Order.GarphQL.csproj"
COPY . .
WORKDIR "/src/Order/Order.GarphQL"
RUN dotnet build "Order.GarphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.GarphQL.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.GarphQL.dll"]