FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app

COPY *.sln .
COPY PlanningBoard.Api/*.csproj ./PlanningBoard.Api/
COPY PlanningBoard.Api.UnitTests/*.csproj ./PlanningBoard.Api.UnitTests/
COPY PlanningBoard.Api.IntegrationTests/*.csproj ./PlanningBoard.Api.IntegrationTests/

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o ./out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0

ENV ASPNETCORE_ENVIRONMENT Release
WORKDIR /app
COPY --from=build-env app/out ./
#RUN dotnet test -l:"console;verbosity=detailed"

ENTRYPOINT ["dotnet", "PlanningBoard.Api.dll"]
