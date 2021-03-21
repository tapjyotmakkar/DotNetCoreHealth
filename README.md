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
    You are a developer for a company (ServerHosting Co) and have been tasked with developing a web-service that will provide server status data for another company (XYZ Co) to consume. You need to provide a functioning web-service to serve the data for consumption by XYZ Co. The data should include a list of servers and whether they were online or offline at a particular time (this data can be faked but should be stored & retrieved from some form of storage).

## Assumptions
* The list of servers are microservice1 and microservice2 deployed on a kubernetes cluster in Azure
* These microservices could be healthy(online) / unhealthy(offline) at any point during the day and they expose this information via /health endpoints
* There is another webservice on the same k8 cluster with a public api that clients can call which provides them server statuses for microservice1 and microservice2 at a particular time from sql server

## Solution
* have simulated healthy/unhealthy behavior in microservice1 and microservice2 by integrating inbuilt HealthChecks available on __Microsoft.Extensions.Diagnostics.HealthChecks__
* a background hosted service is alternating the health of microservice1 and microservice2 every 10 seconds
* deployed both of the above microservices to k8 cluster
* on the same k8 cluster __ServerStatusService__ is a health monitoring service exposed publically http://20.193.32.243/serverstatus that fetches the last 5 server statuses as of today for microservice1 and microservice2 from a table in the database
* __ServerStatusService__ also has a background hosted service that polls microservice1 and microservice2 on the /health endpoints and writes health data in the sql database


## Part2

    You are a developer for a company (XYZ Co) and you are writing a small web application that shows the status of the servers that host your software. You need to integrate with the Server API provided by (ServerHosting Co) to get this information. The information should be presented in an application that can be accessed on a wide variety of devices and screen sizes. Ideally this data should stay as up to date as possible.


## Solution

* have introduced the __server-status-webapp__ angular app to display the server statuses on the UI here http://20.40.125.157/, __please refresh and you would see new statuses flowing in every 10 seconds__
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
