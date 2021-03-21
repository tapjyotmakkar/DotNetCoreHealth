FROM node:12.7-alpine As build

WORKDIR /usr/src/app

COPY package.json ./
RUN npm cache clean --force
RUN npm i
COPY . .
RUN npm run build

FROM nginx:1.17.1-alpine
COPY --from=build /usr/src/app/dist/server-status-webapp /usr/share/nginx/html