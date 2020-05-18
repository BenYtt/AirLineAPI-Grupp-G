using AirLineAPI.Model;
using AutoMapper;
using AirLineAPI.Dto;


namespace AirLineAPI.Configuration
{
    public class MappedProfile
    {
        CreateMap<Passenger, PassengerDto>()
                .ReverseMap();
    }
}
