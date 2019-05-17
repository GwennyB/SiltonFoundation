FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["SiltonFoundation/SiltonFoundation.csproj", "SiltonFoundation/"]
RUN dotnet restore "SiltonFoundation/SiltonFoundation.csproj"
COPY . .
WORKDIR "/src/SiltonFoundation"
RUN dotnet build "SiltonFoundation.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SiltonFoundation.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SiltonFoundation.dll"]