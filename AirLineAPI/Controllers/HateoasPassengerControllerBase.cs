using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using AirLineAPI.HATEOAS;
using AirLineAPI.Dto;
using AirLineAPI.Model;
using AutoMapper;

namespace AirLineAPI.Controllers
{
    public class HateoasPassengerControllerBase : ControllerBase
    {
        private readonly IReadOnlyList<ActionDescriptor> _routes;

        public HateoasPassengerControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _routes = actionDescriptorCollectionProvider.ActionDescriptors.Items;
        }

        public HateoasPassengerControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IMapper mapper) : this(actionDescriptorCollectionProvider)
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
     
            return passengerDto;
        }
        internal PassengerDto HateoasSideLinks(PassengerDto passenger)
        {
            PassengerDto passengerDto = passenger;

            throw new System.NotImplementedException();

        }
    }
}