# pull base image
FROM node

ENV REACT_APP_ENV dockerprod

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package.json ./
COPY package-lock.json ./
COPY .env.prod /app/.env

RUN npm install

# add app
COPY . ./

RUN rm .env.prod

# expose port
EXPOSE 3001

# start app
CMD ["npm", "start"]