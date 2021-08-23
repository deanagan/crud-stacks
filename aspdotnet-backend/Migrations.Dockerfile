# This docker file for migrations isn't used anymore but keeping it for
# reference

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["PlanningBoard.Api/PlanningBoard.Api.csproj", "./PlanningBoard.Api/"]
COPY ["Setup.sh", "./PlanningBoard.Api/"]
ENV ASPNETCORE_ENVIRONMENT Release
RUN dotnet tool install --global dotnet-ef

RUN dotnet restore "PlanningBoard.Api/PlanningBoard.Api.csproj"
COPY . .
WORKDIR "/src/PlanningBoard.Api/"

RUN /root/.dotnet/tools/dotnet-ef migrations add InitialMigrations -o ./Migrations -n PlanningBoard.Migrations


RUN chmod +x ./Setup.sh
CMD /bin/bash ./Setup.sh