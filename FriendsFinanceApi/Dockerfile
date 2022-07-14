#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 
# AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src

COPY ["bin/Release/net6.0/publish", "/app"]

#COPY ["FriendsFinanceApi.csproj", "src/FriendsFinanceApi/"]
#RUN dotnet restore "src/FriendsFinanceApi/FriendsFinanceApi.csproj"
#COPY . ./FriendsFinanceApi
#RUN dotnet build "/src/FriendsFinanceApi/FriendsFinanceApi.csproj" -c Release -o /app/build
#
#
#FROM build AS publish
#RUN dotnet publish "FriendsFinanceApi/FriendsFinanceApi.csproj" -c Release -o /app/publish


#FROM base AS final
WORKDIR /app
#COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "/app/FriendsFinanceApi.dll"]