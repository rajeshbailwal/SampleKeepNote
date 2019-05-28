# base image
FROM node:12.2.0

# install chrome for protractor tests
RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add -
RUN sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
RUN apt-get update && apt-get install -yq google-chrome-stable

# set working directory
RUN mkdir /usr/src/keepnoteapp
WORKDIR /usr/src/keepnoteapp

# add `/usr/src/keepnoteapp/node_modules/.bin` to $PATH
ENV PATH /usr/src/keepnoteapp/node_modules/.bin:$PATH

# install and cache app dependencies
COPY package.json /usr/src/keepnoteapp/package.json
COPY package-lock.json /usr/src/keepnoteapp/package-lock.json
RUN npm install
RUN npm install -g @angular/cli@1.7.1

# add app
COPY . /usr/src/keepnoteapp

# start app
#CMD npm start
CMD ng serve --host 0.0.0.0


# FROM node:8.11.2-alpine as node

# WORKDIR /usr/src/app

# COPY package*.json ./

# RUN npm install

# COPY . .

# RUN npm run build
