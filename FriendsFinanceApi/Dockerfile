#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FriendsFinanceApi/FriendsFinanceApi.csproj", "FriendsFinanceApi/"]
RUN dotnet restore "FriendsFinanceApi/FriendsFinanceApi.csproj"
COPY . .
WORKDIR "/src/FriendsFinanceApi"
RUN dotnet build "FriendsFinanceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FriendsFinanceApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FriendsFinanceApi.dll"]