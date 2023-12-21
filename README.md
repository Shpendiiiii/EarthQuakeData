# EQD
## Introduction
##### This project uses the Bridge and Command pattern
-  #### The bridge pattern is used to query two APIs, and then convert the returned JSON into either YML or XML.
- #### The command pattern is used for creating commands by calling the existing methods through those commands and also helps to avoid long chains of ```if``` or ```switch``` statements


### About the APIs
I am using the USGS and SPEU public APIs to query for earthquake data.


#### USGS
The USGS is "is a science bureau within the United States Department of the Interior (https://www.doi.gov/) ." They have a public API that provides data on earthquake data.

API link: https://earthquake.usgs.gov/fdsnws/event/1/

#### SPEU

The SPEU is a website that from the EU that offers a public API as well on earthquake data.

API link: https://www.seismicportal.eu/


## Bridge Pattern

Is a versatile pattern that allows for separation of concerns, facilitates independent development among different teams to create platform or vendor agnostic products.

I used the bridge pattern so that I can add different type of methods that make calls to API endpoints on those two APIs. These methods belong to the classes that are refined abstractions.

I then used the concrete implementors to take the returned JSON and convert it into YML or XML.

The code looks clear, and enterprise worthy. The bridge pattern when used correctly facilities using other patterns with it in tandem.

I used the Command pattern.


## Command Pattern
Is a pattern that takes a request and turns it into an object. This provides a fantastic way to introduce commands to your system, because it eliminates long chains of ```if``` and ```switch```  statements. Making for clean code.

In this project, the command interface provides an execute method, whereby the classes that implement it can be created dynamically, and thus a smooth execution of methods takes place.

The available commands are stored in an enum called **CommandTypes** when the user types the command, the system looks for the type (the proper class), and instantiates that class dynamically.   The instantiated class calls the methods of the class that can make calls to the APIs. Thus the command part and api-call part of the system are separated.



#### Additional notes
I also use a utility class with static methods that are useful to the system.

The base API urls are stored in *appsettings.json*, so that if they are changed by their owners, you will only have to change them once for the system to be in sync with the API.

The docker image is there just for fun. If you want to run, I dockerized it in Linux. If you want to run the docker image when the project only had the Bridge pattern, go to the provided link for the dockerhub image.



## Prequisites to run
1. .NET 8.0 SDK
   1. Download link: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
2. If using Visual Studio, update to the latest version, 17.8

## Installation
1. git clone the repo
2. cd into EarthQuakeData/EarthQuakeData
3. ```dotnet restore```
4. ```dotnet build```
5. ```dotnet run```

### Do not run the project from an IDE directly without changing the working directory to EarthQuakeData/EarthQuakeData
### Running it from the terminal is less hassle


### If neither works, pull the docker image:
#### ```docker pull shpendiiiii/final-eqd:latest```



#### Contact me for additional questions: ```shpendaliu@pm.me```