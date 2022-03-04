FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

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
LABEL maintainer="vukasin@infaloom.com"

## using high UID to avoid possible correlation with k8s host UID
## https://kubesec.io/basics/containers-securitycontext-runasuser/
RUN adduser \
        --uid 10000 \
        --shell /bin/sh \
        --home /app \
        --no-create-home \
        --disabled-password \
        --gecos "runuser" \
        runuser

COPY --from=publish /app/publish .
USER 10000:10000

CMD ["dotnet", "CallbackBin.dll"]
