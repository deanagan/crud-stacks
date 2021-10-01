FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 5000
# EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5000
# ENV ASPNETCORE_URLS=https://+:5001
ENV ASPNETCORE_ENVIRONMENT DockerDebug

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["TodoBackend.Api/TodoBackend.Api.csproj", "TodoBackend.Api/"]
RUN dotnet restore "TodoBackend.Api/TodoBackend.Api.csproj"
COPY . .
WORKDIR "/src/TodoBackend.Api"
RUN dotnet build "TodoBackend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoBackend.Api.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoBackend.Api.dll"]
