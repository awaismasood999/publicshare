#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CustomerProducts.API.Serilog/CustomerProducts.API.Serilog.csproj", "CustomerProducts.API.Serilog/"]
COPY ["CustomerProducts.Core/CustomerProducts.Core.csproj", "CustomerProducts.Core/"]
RUN dotnet restore "CustomerProducts.API.Serilog/CustomerProducts.API.Serilog.csproj"
COPY . .
WORKDIR "/src/CustomerProducts.API.Serilog"
RUN dotnet build "CustomerProducts.API.Serilog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerProducts.API.Serilog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerProducts.API.Serilog.dll"]