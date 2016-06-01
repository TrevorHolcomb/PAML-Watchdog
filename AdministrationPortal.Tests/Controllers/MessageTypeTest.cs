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
                var controller = kernel.Get<MessageTypesController>();

                CreateMessageTypeViewModel viewModel = new CreateMessageTypeViewModel(
                        "RabbitMQ Queue Size Update",
                        "A message from rabbitmq detailing how many elements are currently enqueued in it."
                        );

                viewModel.ParametersEnabled[0] = true;
                viewModel.ParameterNames[0].Value = "paramName";
                var supportedParameterType = viewModel.SupportedParameterTypes.ElementAtOrDefault(0).Text;
                viewModel.ParameterTypes.Add(supportedParameterType);

                // Act
                var result = controller.Create(viewModel) as ActionResult;

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
                    Description = "A message from rabbitmq detailing how many elements are currently enqueued in it."
                });
                messageTypeRepository.Save();

                // Arrange
                var controller = kernel.Get<MessageTypesController>();

                // Act
                var result = controller.Edit("RabbitMQ Queue Size Update") as ViewResult;
                
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
                });
                messageTypeRepository.Save();

                // Arrange
                var controller = new MessageTypesController();
                kernel.Inject(controller);

                // Act
                var messageType = messageTypeRepository.GetByName("RabbitMQ Queue Size Update");
                var result = controller.Edit(messageType.Name) as ViewResult;

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
                });
                messageTypeRepository.Save();

                // Arrange
                var controller = new MessageTypesController();
                controller.MessageTypeRepository = messageTypeRepository;

                // Act
                var result = controller.Details("RabbitMQ Queue Size Update") as ViewResult;

                // Assert
                Assert.NotNull(result);
            }
        }
    }
}
