# AirLineAPI - Grupp G
## Tables

### Flight

| Column | Datatype | Null |
| --------- | ------------ | --------- |
| *Id*      | int  |           |
| *Manufacturer* | `string`   |      |
| *Model* | `string` |           |

### Passenger

| Column   | Datatype | Null |
| -------------- | ------------ | --------- |
| *Id*           | int  |           |
| *Name* | `string` |           |
| *IdentificationNumber* | long |           |
| *PassengerTimeTables* | `ICollection<PassengerTimeTable>` | null |

### TimeTable

| Column | Datatype | Null |
| -------------- | ------------ | --------- |
| *Id*           | int  |           |
| *Route* | `Route` |           |
| *Flight* | `Flight` |           |
| *DepartureDate* | `DateTime`  |           |
| *ArrivalDate* | `DateTime`                        |           |
| *PassengerTimeTable* | `ICollection<PassengerTimeTable>` | null |

### Destination

| Column   | Datatype | Null |
| --------------- | ------------ | --------- |
| *Id*            | int  |           |
| *City* | `string` |           |
| *Country* | `string` |           |

### Route

| Column             | Datatype      | Null |
| ------------------ | ------------- | ---- |
| *Id*               | int           |      |
| *Name*             | `string`      |      |
| *StartDestination* | `Destination` |      |
| *EndDestination*   | `Destination` |      |
| *TravelTime*       | `TimeSpan`    |      |

### User

| Column | Datatype | Null |
| ------ | -------- | ---- |
| *Id*   | int      |      |
| Name   | `string` |      |
| ApiKey | `string` |      |

## Join Tables

### PassengerTimeTable

| Column        | Datatype    | Null |
| ------------- | ----------- | ---- |
| Id            | int         |      |
| *PassengerID* | int         |      |
| *Passenger*   | `Passenger` |      |
| *TimeTableId* | int         |      |
| *TimeTable*   | `TimeTable` |      |

## Requests

### GET

### Flights

| /flights                | Get all flights                  |      |
| ----------------------- | -------------------------------- | ---- |
| /flights/<id>           | Get one flight by id             |      |
| /flights/<manufacturer> | Get all flights from manufacture |      |
| /flights/<model>        | Get all flights by model         |      |

### Passengers

| /passengers                        | Get all passengers                        |      |
| ---------------------------------- | ----------------------------------------- | ---- |
| /passengers/<id>                   | Get one passenger by id                   |      |
| /passengers/<name>                 | Get all passengers with that name         |      |
| /passengers/<IdentificationNumber> | Get one passenger by IdentificationNumber |      |
| /passengers/?includeTimeTables     | Gets all passengers and their timetables. |      |

### Destinations

| /destinations                  | Get all destinations                       |      |
| ------------------------------ | ------------------------------------------ | ---- |
| /destinations/<ID>             | Get one destination by id                  |      |
| destinations/country=<country> | Get all destinations in a specific country |      |
| destinations/city=<city>       | Get destination by name                    |      |

### Routes

| /routes                         | Get all routes                               |      |
| ------------------------------- | -------------------------------------------- | ---- |
| /routes/<id>                    | Get one route by id                          |      |
| /routes/startdestination=<city> | All routes from a specific startdesination   |      |
| /routes/enddestination=<city>   | All routes from a specific enddesination     |      |
| /routes/startcountry=<country>  | All routes, starting from a specific country |      |
| /routes/endcountry=<country>    | All routes, going to a specific country      |      |



### TimeTables

| /timetables                         | Get all timetables                                |      |
| ----------------------------------- | ------------------------------------------------- | ---- |
| /timetables/<id>                    | Get one timetable by id                           |      |
| /timetables/startdestination=<city> | All time tables from a specific start destination |      |
| /timetables/enddestination=<city>   | All time tables from a specific end destination   |      |
| /timetables/startcountry=<country>  | All time tables from a specific country           |      |
| /timetables/endcountry=<country>    | All time tables to a specific country             |      |
|                                     |                                                   |      |



### POST

|                |                  |
| -------------- | ---------------- |
| /Flights/      | Post flight      |
| /Timetables/   | Post timetable   |
| /Passengers/   | Post passenger   |
| /Destinations/ | Post destination |
| /Routes/       | Post route       |

### PUT

|                  |                 |
| ---------------- | --------------- |
| /Flights/Id      | Put flight      |
| /Timetables/Id   | Put timetable   |
| /Passengers/Id   | Put passenger   |
| /Destinations/Id | Put destination |
| /Routes/Id       | Put route       |

### DELETE

|                  |                    |
| ---------------- | ------------------ |
| /Flights/Id      | Delete flight      |
| /Timetables/Id   | Delete timetable   |
| /Passengers/Id   | Delete passenger   |
| /Destinations/Id | Delete destination |
| /Routes/Id       | Delete route       |



## Json Response

### Flights

```
https://localhost:44389/api/v1.0/flights
========================================
[
{
"id": 2,
"manufacturer": "Boeing",
"model": "T-43"
},
{
"id": 3,
"manufacturer": "Boeing",
"model": "737 MAX"
},
{
"id": 4,
"manufacturer": "Boeing",
"model": "737"
},
{
"id": 1,
"manufacturer": "Cessna",
"model": "182"
}
]

```

### Passengers

```
https://localhost:44389/api/v1.0/passengers
===========================================
[
{
"id": 1,
"name": "Greta",
"identificationNumber": 197110316689,
"passengerTimeTables": null
},
{
"id": 2,
"name": "Loe",
"identificationNumber": 196310315087,
"passengerTimeTables": null
},
{
"id": 3,
"name": "Ulla",
"identificationNumber": 199403212146,
"passengerTimeTables": null
},
{
"id": 4,
"name": "Lasse",
"identificationNumber": 198505291875,
"passengerTimeTables": null
}
]

```

### Timetables

```
https://localhost:44389/api/v1.0/timetables
[
{
"id": 1,
"route": null,
"flight": null,
"departureTime": "2021-11-09T16:00:00",
"arrivalTime": "2021-11-09T23:00:00",
"passengerTimeTables": null
},
{
"id": 2,
"route": null,
"flight": null,
"departureTime": "2021-09-01T16:00:00",
"arrivalTime": "2021-09-01T21:00:00",
"passengerTimeTables": null
},
{
"id": 3,
"route": null,
"flight": null,
"departureTime": "2021-06-20T15:15:00",
"arrivalTime": "2021-06-20T20:15:00",
"passengerTimeTables": null
},
{
"id": 4,
"route": null,
"flight": null,
"departureTime": "2021-10-02T12:00:00",
"arrivalTime": "2021-10-02T16:00:00",
"passengerTimeTables": null
}
]
```

### Destinations

```
https://localhost:44389/api/v1.0/destinations
=============================================
[
{
"id": 1,
"city": "Stockholm",
"country": "Sweden"
},
{
"id": 2,
"city": "Gothenburg",
"country": "Sweden"
},
{
"id": 3,
"city": "Oslo",
"country": "Norway"
},
{
"id": 4,
"city": "Stavanger",
"country": "Norway"
}
]

```

### Routes

```
https://localhost:44389/api/v1.0/routes
[
{
"id": 4,
"name": "STHLM-STAV",
"startDestination": {
"id": 1,
"city": "Stockholm",
"country": "Sweden"
},
"endDestination": {
"id": 4,
"city": "Stavanger",
"country": "Norway"
},
"travelTime": "07:00:00"
},
{
"id": 7,
"name": "STHLM-GBG",
"startDestination": {
"id": 1,
"city": "Stockholm",
"country": "Sweden"
},
"endDestination": {
"id": 2,
"city": "Gothenburg",
"country": "Sweden"
},
"travelTime": "04:00:00"
},
{
"id": 8,
"name": "GBG-STHLM",
"startDestination": {
"id": 2,
"city": "Gothenburg",
"country": "Sweden"
},
"endDestination": {
"id": 1,
"city": "Stockholm",
"country": "Sweden"
},
"travelTime": "04:00:00"
},
{
"id": 6,
"name": "OSLO-HEL",
"startDestination": {
"id": 3,
"city": "Oslo",
"country": "Norway"
},
"endDestination": {
"id": 5,
"city": "Helsinki",
"country": "Finland"
},
"travelTime": "05:00:00"
}
]

```







