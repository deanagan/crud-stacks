# Docker Notes


## Docker build
docker build --pull -t dreamcharlie .

## Docker run
docker run -p 8081:8081 -d --name dreamcharlie dreamcharlie


## Using postman, we can access the API at 8081

# Docker stop
docker stop dreamcharlie

# Docker check container in use
docker ps -a

# Docker remove container
docker rm [CONTAINER ID]

# Docker commit
docker commit -m "Add comment here" -a "[full name]" dreamcharlie [docker hub username]/dreamcharlie:latest

# Docker login and push committed image
docker login
docker push [docker hub username]/dreamcharlie:latest
