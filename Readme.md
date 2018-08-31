# Availability Actors Model 
 
The goal of this Project is to test the Actors model approach with Akka.Net to solve the common availability problem of a reservation system. 
The general idea is to offer an endpoint which is able to answer for an availability request for a region for a dates range. 
To solve this the region actor should send the request to all its properties actor and they should say themselves if are available or not. 
 
## Prerequisites: 
 
* OS X, Windows or Linux 
* .NET Core and .NET Core SDK 
* Visual Studio Code with C# extension (or Visual Studio 2017 or newer) 
 
## Endpoints: 
 
* [GET] api/properties/list/{regionId} 
* [GET] api/properties/create/{regionId}/{propertyId} 
* [DELETE] api/properties/{regionId}/{propertyId} 
 
* [GET] api/dispo/create/{regionId}/{propertyId} 
* [DELETE] api/dispo/properties/{regionId} 
 
## Work in progress 
 
As you see only taking a look at the endpoints, the idea is still in progress. 
 
## To Do’s: 
 
* Naming clean to match the name of the repo “AvailabilityActorsModel” and not “TestDispoActors” 
* Code clean-up and refactor for the basic operations implemented. 
* Implement operations for adding and removing ‘unavailable dates’ (bookings or blocking dates) 
* Query actor to handle the operation for the search 
* Change the actors base class so change the way to code the messaging reception 
