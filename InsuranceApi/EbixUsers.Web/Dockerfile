#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/EbixUsers.Web/EbixUsers.Web.csproj", "src/EbixUsers.Web/"]
COPY ["src/InsuranceApi.Infra.CrossCutting.IoC/InsuranceApi.Infra.CrossCutting.IoC.csproj", "src/InsuranceApi.Infra.CrossCutting.IoC/"]
COPY ["src/InsuranceApi.Services.Rest/InsuranceApi.Services.Rest.csproj", "src/InsuranceApi.Services.Rest/"]
COPY ["src/InsuranceApi.Domain/InsuranceApi.Domain.csproj", "src/InsuranceApi.Domain/"]
COPY ["src/InsuranceApi.Database.Dapper/InsuranceApi.Database.Dapper.csproj", "src/InsuranceApi.Database.Dapper/"]
COPY ["src/InsuranceApi.Infra.Data/InsuranceApi.Infra.Data.csproj", "src/InsuranceApi.Infra.Data/"]
COPY ["src/InsuranceApi.Application/InsuranceApi.Application.csproj", "src/InsuranceApi.Application/"]
COPY ["src/InsuranceApi.Infra.CrossCutting.Identity/InsuranceApi.Infra.CrossCutting.Identity.csproj", "src/InsuranceApi.Infra.CrossCutting.Identity/"]
RUN dotnet restore "src/EbixUsers.Web/EbixUsers.Web.csproj"
COPY . .
WORKDIR "/src/src/EbixUsers.Web"
RUN dotnet build "EbixUsers.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EbixUsers.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EbixUsers.Web.dll"]
