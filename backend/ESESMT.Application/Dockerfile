FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ESESMT.Application/ESESMT.Application.csproj", "ESESMT.Application/"]
COPY ["ESESMT.Domain/ESESMT.Domain.csproj", "ESESMT.Domain/"]
COPY ["ESESMT.Infra.Shared/ESESMT.Infra.Shared.csproj", "ESESMT.Infra.Shared/"]
COPY ["ESESMT.Service/ESESMT.Service.csproj", "ESESMT.Service/"]
COPY ["ESESMT.Infra.Data/ESESMT.Infra.Data.csproj", "ESESMT.Infra.Data/"]
COPY ["ESESMT.Infra.CrossCutting/ESESMT.Infra.CrossCutting.csproj", "ESESMT.Infra.CrossCutting/"]
RUN dotnet restore "ESESMT.Application/ESESMT.Application.csproj"
COPY . .
WORKDIR "/src/ESESMT.Application"
RUN dotnet build "ESESMT.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESESMT.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESESMT.Application.dll"]

