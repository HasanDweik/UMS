FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UMS.WebApi/UMS.WebApi/UMS.WebApi.csproj", "UMS.WebApi/"]
COPY ["UMS.Persistence/UMS.Persistence.csproj", "UMS.Persistence/"]
COPY ["UMS.Domain/UMS.Domain.csproj", "UMS.Domain/"]
COPY ["UMS.Application/UMS.Application.csproj", "UMS.Application/"]
COPY ["UMS.Infrastructure.Abstraction/UMS.Infrastructure.Abstraction.csproj", "UMS.Infrastructure.Abstraction/"]
COPY ["UMS.Infrastructure/UMS.Infrastructure.csproj", "UMS.Infrastructure/"]
RUN dotnet restore "UMS.WebApi/UMS.WebApi.csproj"
COPY . .
WORKDIR "/src/UMS.WebApi"
RUN dotnet build "UMS.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UMS.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UMS.WebApi.dll"]
