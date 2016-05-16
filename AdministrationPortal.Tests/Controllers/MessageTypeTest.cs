using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdministrationPortal.Controllers;
using AdministrationPortal.ViewModels.MessageTypes;
using Ninject;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using Xunit;

namespace AdministrationPortal.Tests.Controllers
{
    public class MessageTypeTest
    {
        [Fact]
        public void Index()
        {
            using (var kernel = new StandardKernel(new TestingModule()))
            {
                // Arrange
                var controller = new MessageTypesController();
                kernel.Inject(controller);

                // Act
                var result = controller.Index() as ViewResult;

                // Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void Create()
        {
            using (var kernel = new StandardKernel(new TestingModule()))
            {
                // Arrange
                var controller = new MessageTypesController();
                kernel.Inject(controller);

                // Act
                var result = controller.Create() as ViewResult;
                
                // Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void CreatePost()
        {
            using (var kernel = new StandardKernel(new TestingModule()))
            {
                // Arrange
                var controller = new MessageTypesController();
                kernel.Inject(controller);

                // Act
                var parameters = new List<CreateMessageTypeParameterTypeViewModel>()
                {
                    new CreateMessageTypeParameterTypeViewModel("QueueSize", "integer", true),
                    new CreateMessageTypeParameterTypeViewModel(null, null, false)
                };

                var result = controller.Create(new CreateMessageTypeViewModel(
                        "RabbitMQ Queue Size Update", 
                        "A message from rabbitmq detailing how many elements are currently enqueued in it.", 
                        parameters)) as ViewResult;

                // Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void Edit()
        {
            using (var kernel = new StandardKernel(new TestingModule()))
            {
                var messageTypeRepository = kernel.Get<Repository<MessageType>>();
                messageTypeRepository.Insert(new MessageType
                {
                    Name = "RabbitMQ Queue Size Update",
                    Description = "A message from rabbitmq detailing how many elements are currently enqueued in it.",
                    Id = 0
                });
                messageTypeRepository.Save();

                // Arrange
                var controller = new MessageTypesController();
                kernel.Inject(controller);

                // Act
                var result = controller.Edit(0) as ViewResult;
                
                // Assert
                Assert.NotNull(result);
            }
        }


        [Fact]
        public void EditPost()
        {
            using (var kernel = new StandardKernel(new TestingModule()))
            {
                var messageTypeRepository = kernel.Get<Repository<MessageType>>();
                messageTypeRepository.Insert(new MessageType
                {
                    Name = "RabbitMQ Queue Size Update",
                    Description = "A message from rabbitmq detailing how many elements are currently enqueued in it.",
                    Id = 0
                });
                messageTypeRepository.Save();

                // Arrange
                var controller = new MessageTypesController();
                kernel.Inject(controller);

                // Act
                var messageType = messageTypeRepository.GetById(0);
                var result = controller.Edit(new CreateMessageTypeViewModel
                (
                    messageType.Name,
                    messageType.Description,
                    messageType.MessageTypeParameterTypes.Select(parameter => new CreateMessageTypeParameterTypeViewModel(parameter.Name, parameter.Type, true)).ToList()
                )) as ViewResult;

                // Assert
                Assert.NotNull(result);
            }
        }


        [Fact]
        public void Details()
        {
            using (var kernel = new StandardKernel(new TestingModule()))
            {
                var messageTypeRepository = kernel.Get<Repository<MessageType>>();
                messageTypeRepository.Insert(new MessageType
                {
                    Name = "RabbitMQ Queue Size Update",
                    Description = "A message from rabbitmq detailing how many elements are currently enqueued in it.",
                    Id = 0
                });
                messageTypeRepository.Save();

                // Arrange
                var controller = new MessageTypesController();
                kernel.Inject(controller);

                // Act
                var result = controller.Details(0) as ViewResult;

                // Assert
                Assert.NotNull(result);
            }
        }
    }
}
