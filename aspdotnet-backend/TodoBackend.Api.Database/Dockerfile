FROM mcr.microsoft.com/mssql/server:2019-latest

ARG PROJECT_DIR=/tmp/devdatabase
RUN mkdir -p $PROJECT_DIR
WORKDIR $PROJECT_DIR
COPY TodoBackend.Api.Database/SqlScripts/* ./
COPY TodoBackend.Api.Database/wait-for-it.sh ./
COPY TodoBackend.Api.Database/entrypoint.sh ./
COPY TodoBackend.Api.Database/setup.sh ./

CMD ["/bin/bash", "entrypoint.sh"]