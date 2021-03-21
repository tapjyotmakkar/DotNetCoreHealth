# Tools and technology used
    .net core
    .net core EF
    .net hosted background services
    angular
    bootstrap
    Docker
    Azure Container Registry
    Azure Kubernetes Service
    Azure SQL Databases
    

# Problem statement

## Part1

    Webservice that provides server status for a list of servers at a particular time from storage

## Solution

* have worked under the assumption that the list of servers could be __microservices1__ and __microservice2__ on a k8 cluster that could be healthy / unhealthy at any point during the day and they expose this information on their /health endpoints
* have simulated healthy/unhealthy behavior in these microservice by overriding inbuilt HealthChecks available on __Microsoft.Extensions.Diagnostics.HealthChecks__
* using a background hosted service every 10 seconds I swap them between being healthy/unhealthy
* on the same k8 cluster ServerStatusService is a health monitoring service exposed publically http://20.193.32.243/serverstatus that fetches the last 5 server statuses as of today for microservice1 and microservice2 from a table in the database
* __ServerStatusService__ also has background hosted service that polls microservice1 and microservice2 on the /health endpoints and writes health data in the sql database


## Part2

    Integrate with server status API and show status data on a wide variety of screen sizes and devices

## Solution

* have introduced the __server-status-webapp__ angular app to display the server statuses on the UI here http://20.40.125.157/
* leveraged __bootstrap__ to make the UI responsive
* used inbuilt angular htppclient to call the publically exposed /serverstatus endpoint by __ServerStatusService__ as CORS is enabled on the service
                                                                                                                                                                                
# Running locally
 backend services can be brought up in docker containers locally from the root of the porject by executing

        docker-compose -f .\docker-compose.yml --build

frontend angular app can be brought up by navigating to __/server-status-webapp__ executing

        docker build -t server-status-webapp .
        docker run --name server-status-webapp -p 8080:80 server-status-webapp
      
# Deploying to kubernetes
* logged into portal.azure.com
* created a aks_acs_resource resource group on the basic subscription
* created a container registry to upload docker containers to
* created a sql server database

    <img src="https://github.com/tapjyotmakkar/DotNetCoreHealth/blob/master/azure_resources.JPG">

* create a kubernetes service to host all the artifacts

    <img src="https://github.com/tapjyotmakkar/DotNetCoreHealth/blob/master/k8_resources.JPG">

* k8 deployment scripts

        deploy-microservice1-aks.yml
        deploy-microservice2-aks.yml
        deploy-serverstatusservice-aks.yml
        deploy-serverstatusweb-aks.yml
