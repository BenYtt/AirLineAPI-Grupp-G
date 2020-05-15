using AirLineAPI.Db_Context;
using AirLineAPI.Model;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using Xunit;
using Castle.Core.Logging;
using AirLineAPI.Services;
using Microsoft.Extensions.Logging;

namespace AirlineAPI.Test
{
    public class UnitTest1
    {
        [Fact]
        public async void GetPassengersFailToGetIfNoPassengerExist()
        {
            //arrange
            IList<Passenger> passengers = GeneratePassengers();
            var airlineContextMock = new Mock<AirLineContext>();
            airlineContextMock.Setup(p => p.Passengers).ReturnsDbSet(passengers);

            var logger = Mock.Of<ILogger<PassengerRepo>>();
            var passengerRepo = new PassengerRepo(airlineContextMock.Object, logger);

            //act
            var thePassengers = await passengerRepo.GetPassengers();

            //assert
            Assert.Equal(1, (int)thePassengers.Length);

        }
        public static IList<Passenger> GeneratePassengers()
        {
            return new List<Passenger>
            {
                new Passenger{ ID=1, Name="Greta", IdentificationNumber=197110316689, PassengerTimeTables= null }
            };
            
        }
    }
}
