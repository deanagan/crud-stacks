[![.NET](https://github.com/deanagan/planning-board-backend/actions/workflows/dotnet.yml/badge.svg)](https://github.com/deanagan/planning-board-backend/actions/workflows/dotnet.yml)
[![Docker](https://github.com/deanagan/planning-board-backend/actions/workflows/docker-publish.yml/badge.svg)](https://github.com/deanagan/planning-board-backend/actions/workflows/docker-publish.yml)
# Docker image created with:
docker build --pull -t todo .

# Docker run
docker run -d -p 8090:80 --name todo todo

# Docker stop
docker stop todo

# Docker check container in use
docker ps -a

# Docker remove container
docker rm [CONTAINER ID]

# Docker commit
docker commit -m "Add comment here" -a "[full name]" todo [docker hub username]/todo:latest

# Docker login and push committed image
docker login
docker push [docker hub username]/todo:latest

# To run docker compose
`docker-compose up --build`
or
`docker-compose build` followed by `docker-compose up`

# To run sql server as a container (note password may vary)
`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=1Secure*Password1" -p 14331:1433 -d mcr.microsoft.com/mssql/server:2019-latest`

# Testing Notes
To run tests with verbosity enabled:
`dotnet test -l:"console;verbosity=detailed"`

# To run using base image:

```yml
version: '3.4'

services:
  todo:
    image: docker.pkg.github.com/deanagan/planning-board-backend/todo:latest
    ports:
      - "8090:80"
    depends_on:
      - db
  db:
    image: mcr.microsoft.com/mssql/server:2019-latest
    user: root
    environment:
      SA_PASSWORD: "1Secure*Password1"
      ACCEPT_EULA: "Y"
      MSSQL_PID: "Express"
    volumes:
      - ./sqlvolume/data:/var/opt/mssql/data
      - ./sqlvolume/log:/var/opt/mssql/log
      - ./sqlvolume/secrets:/var/opt/mssql/secrets
    ports:
    - "14331:1433"

```
