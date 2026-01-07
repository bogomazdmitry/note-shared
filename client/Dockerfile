# Build stage
FROM node:14-alpine AS build
WORKDIR /app

COPY client/package.json client/package-lock.json ./
RUN npm ci

COPY client ./
RUN npm run build -- --configuration production

# Runtime stage
FROM nginx:1.25-alpine AS runtime

# Nginx config is kept in the repo under /nginx
COPY nginx/conf.d/default.conf /etc/nginx/conf.d/default.conf

# Angular build output
COPY --from=build /app/dist/client /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
