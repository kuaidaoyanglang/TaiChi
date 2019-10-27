# 创建构建阶段 base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# 创建构建阶段 build
# 基于microsoft/dotnet:3.0-aspnetcore-runtime构建Docker Image
FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
# 设置工作路径
WORKDIR /src

# 复制TaiChi.Core.Mvc 下的解决方案到此阶段的 TaiChi.Core.Mvc/目录  报错是因为你这个目录并没有，你可以把这个去掉或者看./DDemo是否可以
COPY ["TaiChi.Core.Mvc/TaiChi.Core.Mvc.csproj", "TaiChi.Core.Mvc/"]


RUN dotnet restore "TaiChi.Core.Mvc/TaiChi.Core.Mvc.csproj"

# 复制Dockerfile所在目录的文件到此接端的工作目录 /src ,dotnet restore 可复制后执行
COPY . .

WORKDIR "/src/TaiChi.Core.Mvc"
RUN dotnet build "TaiChi.Core.Mvc.csproj" -c Release -o /app/build

# 基于 base阶段 创建 publish 阶段，发布项目到/app
FROM build AS publish
RUN dotnet publish "TaiChi.Core.Mvc.csproj" -c Release -o /app/publish

# 基于 base阶段 创建 final 阶段
FROM base AS final
WORKDIR /app

# 从publish阶段的/app目录复制文件到当前工作目录
COPY --from=publish /app/publish .

# 入口点 执行命令
ENTRYPOINT ["dotnet", "TaiChi.Core.Mvc.dll"]