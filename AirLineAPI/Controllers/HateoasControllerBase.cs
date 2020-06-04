﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AirLineAPI.HATEOAS;
using AirLineAPI.Dto;
using AutoMapper;

namespace AirLineAPI.Controllers
{
    public class HateoasControllerBase : ControllerBase
    {
        private readonly IReadOnlyList<ActionDescriptor> _routes;

        public HateoasControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _routes = actionDescriptorCollectionProvider.ActionDescriptors.Items;
        }

        public HateoasControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IMapper mapper) : this(actionDescriptorCollectionProvider)
        {
        }

        internal Link UrlLink(string relation, string routeName, object values)
        {
            var route = _routes.FirstOrDefault(f => f.AttributeRouteInfo.Name == routeName);
            var method = route.ActionConstraints.OfType<HttpMethodActionConstraint>().First().HttpMethods.First();
            var url = Url.Link(routeName, values).ToLower();
            return new Link(url, relation, method);
        }

        internal PassengerDto HateoasMainLinks(PassengerDto passenger)
        {
            PassengerDto passengerDto = passenger;

            passengerDto.Links.Add(UrlLink("all", "GetAll", null));
            passengerDto.Links.Add(UrlLink("_self", "GetpassengerAsync", new { id = passengerDto.ID }));
            passengerDto.Links.Add(UrlLink("name", "GetNameAsync", new { id = passengerDto.Name }));
            passengerDto.Links.Add(UrlLink("identificationNumber", "GetIdnumberAsync", new { id = passengerDto.IdentificationNumber }));

            return passengerDto;
        }

        internal FlightDto HateoasMainLinksFlight(FlightDto flight)
        {
            var flightDto = flight;

            flightDto.Links.Add(UrlLink("all", "GetFlights", null));
            flightDto.Links.Add(UrlLink("_self", "GetFlightById", new { id = flightDto.Id }));
            flightDto.Links.Add(UrlLink("_next", "GetFlightById", new { id = flightDto.Id + 1 }));
            flightDto.Links.Add(UrlLink("_Manufacturer = boeing", "GetFlightsByManufacturer", new { Manufacturer = "boeing" }));
            flightDto.Links.Add(UrlLink("_model = 182", "GetFlightsByModel", new { Model = "182" }));

            return flightDto;
        }

        internal DestinationDto HateoasMainLinksDestinations(DestinationDto destination)
        {
            var destinationDto = destination;

            destinationDto.Links.Add(UrlLink("all", "GetDestinations", null));
            destinationDto.Links.Add(UrlLink("_self", "GetDestinationById", new { id = destinationDto.ID }));
            destinationDto.Links.Add(UrlLink("_next", "GetDestinationById", new { id = destinationDto.ID + 1 }));
            destinationDto.Links.Add(UrlLink("_cityName", "GetDestinationByCity", new { City = "stockholm"}));
            destinationDto.Links.Add(UrlLink("_counterName", "GetDestinationByCountry", new { Country = "sweden" }));

            return destinationDto;
        }

        internal RouteDto HateoasMainLinksRoute(RouteDto route)
        {
            var routeDto = route;

            routeDto.Links.Add(UrlLink("all", "GetRoutes", null));
            routeDto.Links.Add(UrlLink("_self", "GetRouteById", new { id = routeDto.Id }));
            routeDto.Links.Add(UrlLink("_self", "GetRouteById", new { id = routeDto.Id + 1 }));

            return routeDto;
        }

        internal TimeTableDto HateoasMainLinksTimeTable(TimeTableDto timeTable)
        {
            var timeTableDto = timeTable;

            timeTableDto.Links.Add(UrlLink("all", "GetTimeTables", null));

            timeTableDto.Links.Add(UrlLink("_self", "GettimetablesId", new { id = timeTableDto.ID }));

            return timeTableDto;
        }

        internal PassengerDto HateoasSideLinks(PassengerDto passenger)
        {
            PassengerDto passengerDto = passenger;

            throw new System.NotImplementedException();
        }
    }
}