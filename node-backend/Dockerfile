FROM node:latest

WORKDIR /usr/src/app
COPY package*.json ./

RUN npm install

# Bundling source
COPY . .

EXPOSE 8081

CMD ["npm", "run", "develop"]