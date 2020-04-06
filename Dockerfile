# RUN ALL CONTAINERS FROM ROOT (folder with .sln file):
# docker-compose build
# docker-compose up
#
# RUN JUST THIS CONTAINER FROM ROOT (folder with .sln file):
# docker build --pull -t web -f src/Web/Dockerfile .
#
# RUN COMMAND
#  docker run --name eshopweb --rm -it -p 5106:5106 web
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY *.sln .
COPY . .
WORKDIR /app
RUN dotnet restore Elsa.Guides.sln

RUN dotnet publish --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
EXPOSE 80
WORKDIR /app
COPY --from=build /app .

# Optional: Set this here if not setting it from docker-compose.yml
ENV ASPNETCORE_ENVIRONMENT Development

ENTRYPOINT ["dotnet", "Elsa.Guides.Dashboard.WebApp.dll"]
