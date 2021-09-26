# pull base image
FROM node

ENV REACT_APP_ENV dockerprod

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package.json ./
COPY .env.prod ./.env
RUN npm install

# add app
COPY . ./

# expose port
EXPOSE 3000

# start app
CMD ["npm", "start"]