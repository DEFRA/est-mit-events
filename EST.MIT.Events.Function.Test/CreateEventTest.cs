﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MIT.Events.Function;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EST.MIT.Events.Function.Test
{

    public class CreateEventTest
    {
        [Fact]
        public void CreateEvent_CreatesNewEventEntityWithCorrectProperties()
        {
            var loggerMock = new Mock<ILogger>();
            var tableEntityMock = new Mock<MitEvent>();
            var queueItem = "{\"PartitionKey\":\"testPartitionKey\",\"RowKey\":\"testRowKey\",\"Data\":\"Hello\",\"EventType\":\"Todolo\"}"; 
            MitEvent? eventEntity = null;
          
            CreateEntity.AddQueueItem(queueItem, out eventEntity, loggerMock.Object);

            Assert.NotNull(eventEntity);
            Assert.Equal("testPartitionKey", eventEntity.PartitionKey);
            Assert.Equal("testRowKey", eventEntity.RowKey);
            Assert.Equal("Hello", eventEntity.Data);
            Assert.Equal("Todolo", eventEntity.EventType);
        }

        [Fact]
        public void CreateEvent_CreatesNewEventEntityWithOutCorrectPropertiesAndFails()
        {
            var loggerMock = new Mock<ILogger>();
            var httpRequestMock = new Mock<HttpRequest>();
            var queueItem = "{}";
            MitEvent? eventEntity = null;

            CreateEntity.AddQueueItem(queueItem, out eventEntity, loggerMock.Object);

            Assert.NotNull(eventEntity);
            Assert.NotEqual("testPartitionKey", eventEntity.PartitionKey);
            Assert.NotEqual("testRowKey", eventEntity.RowKey);
            Assert.NotEqual("Hello", eventEntity.Data);
            Assert.NotEqual("Todolo", eventEntity.EventType);



            //var loggerMock = new Mock<ILogger>();
            //var httpRequestMock = new Mock<HttpRequest>();
            //var tableEntityMock = default(MockEventTableEntity);
            //var partitionKey = "testPartitionKey";
            //var rowKey = "testRowKey";
            //var result = Function.QueryEventWithPartitionandRowKey(httpRequestMock.Object, tableEntityMock, loggerMock.Object, partitionKey, rowKey);

            //Assert.NotNull(result);
            //Assert.IsType<NotFoundResult>(result);
        }
    }
}