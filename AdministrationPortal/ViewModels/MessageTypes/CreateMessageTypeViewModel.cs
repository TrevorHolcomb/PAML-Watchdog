using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.MessageTypes
{
    public class CreateMessageTypeViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CreateMessageTypeParameterTypeViewModel> Parameters { get; set; }

        public CreateMessageTypeViewModel() { }

        public CreateMessageTypeViewModel(string name, string description,
            List<CreateMessageTypeParameterTypeViewModel> parameters)
        {
            Name = name;
            Description = description;
            Parameters = parameters;
        }

    }

    public class CreateMessageTypeParameterTypeViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Enabled { get; set; }

        public CreateMessageTypeParameterTypeViewModel() { }

        public CreateMessageTypeParameterTypeViewModel(string name, string type, bool enabled)
        {
            Name = name;
            Type = type;
            Enabled = enabled;
        }

        public MessageTypeParameterType ToEntity()
        {
            return new MessageTypeParameterType
            {
                Name = this.Name,
                Type = this.Type,
            };
        }
    }
}