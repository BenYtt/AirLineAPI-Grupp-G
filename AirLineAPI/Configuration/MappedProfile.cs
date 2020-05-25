using AirLineAPI.Model;
using AutoMapper;
using AirLineAPI.Dto;

namespace AirLineAPI.Configuration
{
    public class MappedProfile : Profile
    {
        public MappedProfile()
        {
            CreateMap<Passenger, PassengerDto>()
                .ReverseMap();
            CreateMap<Destination, DestinationDto>()
                .ReverseMap();
            CreateMap<Route, RouteDto>()
              .ReverseMap()
              .ForMember(e => e.EndDestination, o => o.Ignore())
              .ForMember(s => s.StartDestination, o => o.Ignore());
            CreateMap<Flight, FlightDto>()
                .ReverseMap();
            CreateMap<TimeTable, TimeTableDto>()
                .ReverseMap();
        }
    }
}