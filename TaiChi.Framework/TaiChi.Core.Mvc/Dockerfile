FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["TaiChi.Core.Mvc/TaiChi.Core.Mvc.csproj", "TaiChi.Core.Mvc/"]
RUN dotnet restore "TaiChi.Core.Mvc/TaiChi.Core.Mvc.csproj"
COPY . .
WORKDIR "/src/TaiChi.Core.Mvc"
RUN dotnet build "TaiChi.Core.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaiChi.Core.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaiChi.Core.Mvc.dll"]