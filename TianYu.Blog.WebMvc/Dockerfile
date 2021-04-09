#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TianYu.Blog.WebMvc/TianYu.Blog.WebMvc.csproj", "TianYu.Blog.WebMvc/"]
COPY ["TianYu.Blog.Infrastructure/TianYu.Blog.Infrastructure.csproj", "TianYu.Blog.Infrastructure/"]
COPY ["TianYu.Blog.Domain/TianYu.Blog.Domain.csproj", "TianYu.Blog.Domain/"]
COPY ["SqlSugar/SqlSugar.csproj", "SqlSugar/"]
COPY ["TianYu.Core.Common/TianYu.Core.Common.csproj", "TianYu.Core.Common/"]
COPY ["TianYu.Core.Log/TianYu.Core.Log.csproj", "TianYu.Core.Log/"]
COPY ["TianYu.Blog.Service/TianYu.Blog.Service.csproj", "TianYu.Blog.Service/"]
COPY ["TianYu.Core.DataBase/TianYu.Core.DataBase.csproj", "TianYu.Core.DataBase/"]
RUN dotnet restore "TianYu.Blog.WebMvc/TianYu.Blog.WebMvc.csproj"
COPY . .
WORKDIR "/src/TianYu.Blog.WebMvc"
RUN dotnet build "TianYu.Blog.WebMvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TianYu.Blog.WebMvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TianYu.Blog.WebMvc.dll"]