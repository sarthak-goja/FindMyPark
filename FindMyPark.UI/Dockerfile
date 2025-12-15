# Build Stage
FROM node:20-alpine AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build -- --configuration=production

# Run Stage
FROM nginx:alpine
COPY --from=build /app/dist/find-my-park.ui/browser /usr/share/nginx/html
# Copy custom nginx config if needed, otherwise default is used
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
