#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Payment/Payment.GarphQL/Payment.GarphQL.csproj", "Payment/Payment.GarphQL/"]
COPY ["Framework.Kafka/Framework.Kafka.csproj", "Framework.Kafka/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["Payment/Payment.Domain/Payment.Domain.csproj", "Payment/Payment.Domain/"]
COPY ["Framework.Auth/Framework.Auth.csproj", "Framework.Auth/"]
RUN dotnet restore "Payment/Payment.GarphQL/Payment.GarphQL.csproj"
COPY . .
WORKDIR "/src/Payment/Payment.GarphQL"
RUN dotnet build "Payment.GarphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.GarphQL.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.GarphQL.dll"]