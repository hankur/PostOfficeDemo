# PostOffice Demo

This backend + frontend demo application was created to demonstrate my skills in .NET 5 and Aurelia.  

The main goal was to build a strong foundation for any similar client-server setup:

- domain objects with annotations for auto-generating database structure
- data access layer with repositories (also provider and factory exmaples)
- business logic layer with services (again with provider and factory)
- web API layer with input models (and validation), mappers and controllers
- client application, which uses the API and also has domain objects, services, and controller-view setup

## Overview

This demo is a management system for post offices to help register parcels, bags with either parcels or letters, and shipments. Shipment is a collection of bags that are put on the same plane. A bag of letters, which are not separate entities in the system, are counted, weighed and priced as a bag. A bag of parcels is a collection of individual parcels, which have a recipient and destination, also weight and price.

The client application provides a simple overview of shipments, bags and parcels. The user can also insert shipments, bags into shipments and parcels into bags.

## Setup

### Prerequisites

1. Download and install .NET 5 (https://dotnet.microsoft.com/download) and Node.js (https://nodejs.org/en/download/)
2. Create an MS SQL database and remember the access details to configure the backend later

### Run locally

1. Modify the database connection string in `WebApp/appsettings.json` to connect to the created database
2. If you wish, you can configure the backend launch settings in `WebApp/Properties/launchSettings.json`
3. Navigate to the WebApp folder and run [`dotnet run`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run)
4. If you wish, you can configure the frontend launch settings in `Client/aurelia_project/aurelia.json`. You can also configure the backend URL in `Client/config/environment.json`
4. Navigate to the Client folder and run [`npm start`](https://docs.npmjs.com/cli/v7/commands/npm-start). You can learn more by reading the [Client/README.md](Client/README.md)
5. Both backend and frontend websites should be open in the browser now. If not, navigate (by default) to https://localhost:5001/ for backend API docs and https://localhost:8080/ for frontend client application

### Build and deploy

#### Build
1. Navigate to the WebApp folder and run `dotnet publish -c Release -r linux-x64 --self-contained true` to build the backend app for Linux systems and build the required dependencies into the application itself
2. Navigate to the Client folder and run `npm run build` to build the frontend app. You can learn more by reading the [Client/README.md](Client/README.md)

#### Deploy

1. Modify the database connection string in `WebApp/bin/Release/net5.0/linux-x64/publish/WebApp/appsettings.json` to connect to the created database. You can also modify the port backend will listen to
2. Execute `WebApp/bin/Release/net5.0/linux-x64/publish/WebApp` to start the backend application
3. Configure the backend URL in `Client/config/environment.json`
4. Open `Client/dist/index.html` in the browser

## TODO

- Unit tests
- Add missing CRUD operations (currently, only Create and Read are supported)
- Better structure for bags (currently they share the same object, but this isn't really ideal)