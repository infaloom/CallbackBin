FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["CallbackBin/CallbackBin.csproj", "CallbackBin/"]

RUN dotnet restore "CallbackBin/CallbackBin.csproj"
COPY . .
WORKDIR "/src/CallbackBin"
RUN dotnet build "CallbackBin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CallbackBin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CallbackBin.dll"]