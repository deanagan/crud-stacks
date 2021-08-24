# This docker file for migrations isn't used anymore but keeping it for
# reference

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["TodoBackend.Api/TodoBackend.Api.csproj", "./TodoBackend.Api/"]
COPY ["Setup.sh", "./TodoBackend.Api/"]
ENV ASPNETCORE_ENVIRONMENT Release
RUN dotnet tool install --global dotnet-ef

RUN dotnet restore "TodoBackend.Api/TodoBackend.Api.csproj"
COPY . .
WORKDIR "/src/TodoBackend.Api/"

RUN /root/.dotnet/tools/dotnet-ef migrations add InitialMigrations -o ./Migrations -n TodoBackend.Migrations


RUN chmod +x ./Setup.sh
CMD /bin/bash ./Setup.sh