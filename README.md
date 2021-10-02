# crud-stacks
A collection of crud stacks

[![.NET](https://github.com/deanagan/crud-stacks/actions/workflows/dotnet.yml/badge.svg)](https://github.com/deanagan/crud-stacks/actions/workflows/dotnet.yml)


[![Asp.net Backend Docker](https://github.com/deanagan/crud-stacks/actions/workflows/docker-publish-aspdotnet-backend.yml/badge.svg)](https://github.com/deanagan/crud-stacks/actions/workflows/docker-publish-aspdotnet-backend.yml)


This repo is a collection of todo apps that I've written to play around with different tech stacks.

To run asp.net + react + sql server:

```yml
version: '3.7'

services:
  todo-react-frontend:
    image: ghcr.io/deanagan/crud-stacks/todo-react-frontend:latest
    ports:
      - 3001:3000
  todo-aspdotnet-backend:
    image: ghcr.io/deanagan/crud-stacks/todo-aspdotnet-backend:latest
    ports:
      - 8090:5000

  database:
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
    - 14331:1433
```
